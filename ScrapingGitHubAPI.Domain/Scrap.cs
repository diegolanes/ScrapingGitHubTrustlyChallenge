using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using HtmlAgilityPack;

namespace ScrapingGitHubAPI.Domain
{
    public class Scrap
    {
        public static void getUrlContent(string baseUrl, string path ,ConcurrentBag<ItemDescription> itemsDescription)
        {
            var webClient = new WebClient();
            var pageContent = webClient.DownloadString(HttpUtility.UrlDecode(baseUrl + path));
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();

            htmlDocument.LoadHtml(pageContent);
            var childNodes = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'js-navigation-item')]");

            Parallel.ForEach(childNodes, node =>
            {
                if (node.InnerText.Replace("\n", "").Trim() != ". .")
                {
                    if (node.SelectSingleNode("div[1]/svg").Attributes["aria-label"].Value == "Directory")
                    {
                        getUrlContent(baseUrl, node.SelectSingleNode("div[2]/span/a").Attributes["href"].Value,itemsDescription);
                    }
                    else
                    {
                        itemsDescription.Add(getFileData(baseUrl, node));
                    }
                }
            });
        }
        public static string getExtension(string url)
        {
            string textAfterLastSlash = url.Substring(url.LastIndexOf("/") + 1, url.Length - url.LastIndexOf("/") - 1);
            if (textAfterLastSlash.Contains("."))
            {
                return textAfterLastSlash.Split('.')[textAfterLastSlash.Split('.').Count() - 1];
            }
            else
            {
                return "";
            }
        }
        public static List<string> getFileListInformation(string url)
        {
            var webClient = new WebClient();
            var pageContent = webClient.DownloadString(HttpUtility.UrlDecode(url));
            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(pageContent);

            var fileSizeAndLineNumbersSpan = htmlDocument.DocumentNode.SelectNodes("//span[(@class='file-info-divider')]/parent::*");

            if (fileSizeAndLineNumbersSpan == null)
            {
                fileSizeAndLineNumbersSpan = htmlDocument.DocumentNode.SelectNodes("//div[contains(@class,'text-mono')]");
            }

            var fileInfoArray = fileSizeAndLineNumbersSpan[0].InnerText.Trim().Split('\n');

            List<string> fileInfoListFiltered = new List<string>();
            foreach (var item in fileInfoArray)
            {
                fileInfoListFiltered.Add(item.Trim());
            }
            return fileInfoListFiltered;
        }
        public static int getNumberOfLines(List<string> fileInfoList)
        {
            var numberOfLines = 0;
            if (fileInfoList.Count > 1)
                numberOfLines = Convert.ToInt32(fileInfoList.FirstOrDefault(f => f.Contains("line")).Split(' ')[0]);

            return numberOfLines;
        }
        public static double getFileSize(List<string> fileInfoList)
        {
            var getOnlySizeRgxExpression = new Regex(@"[^\d.]");
            var sizeValue = getOnlySizeRgxExpression.Replace(fileInfoList[fileInfoList.Count - 1], "");
            var sizeUnit = fileInfoList[fileInfoList.Count - 1].Replace(sizeValue, "").Trim().ToUpper();
            sizeValue = sizeValue.Replace(".", ",");

            switch (sizeUnit)
            {
                case "B":
                    return Convert.ToDouble(sizeValue);
                case "KB":
                    return Convert.ToDouble(sizeValue) * ScrapUtils.kbMultiplier;
                case "MB":
                    return Convert.ToDouble(sizeValue) * ScrapUtils.mbMultiplier;
                case "GB":
                    return Convert.ToDouble(sizeValue) * ScrapUtils.gbMultiplier;
                default:
                    return Convert.ToDouble(sizeValue);
            }
        }
        public static ItemDescription getFileData(string baseUrl, HtmlNode node)
        {
            var name = node.SelectSingleNode("div[2]/span/a").InnerText;
            var type = node.SelectSingleNode("div[1]/svg").Attributes["aria-label"].Value;
            var url = baseUrl + node.SelectSingleNode("div[2]/span/a").Attributes["href"].Value;
            var extension = getExtension(url);
            var fileListInformation = getFileListInformation(url);
            var size = getFileSize(fileListInformation);
            var numberOfLines = getNumberOfLines(fileListInformation);

            return new ItemDescription()
            {
                Name = name,
                Type = type,
                Url = url,
                Extension = extension,
                Size = size,
                NumberOfLines = numberOfLines
            };
        }
        public static List<ItemDescriptionResult> getResultList(List<ItemDescription> itemsDescription)
        {
            return itemsDescription
                .GroupBy(i => i.Extension)
                .Select(i => new ItemDescriptionResult()
            {
                Extension = i.Key,
                Count = i.Where(f => f.Extension == i.Key).Count(),
                Lines = Convert.ToInt32(i.Sum(f => f.NumberOfLines)),
                Bytes = Convert.ToInt32(i.Sum(f => f.Size))
            }).OrderBy(i => i.Extension).ToList();
        }
    }
    public class ItemDescription
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string Extension { get; set; }
        public double Size { get; set; }
        public int NumberOfLines { get; set; }
    }
    public class ItemDescriptionResult
    {
        public string Extension { get; set; }
        public int Bytes { get; set; }
        public int Count { get; set; }
        public int Lines { get; set; }
    }
}
