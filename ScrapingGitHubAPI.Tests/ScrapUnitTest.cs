using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScrapingGitHubAPI.Domain;
using System.Collections.Generic;

namespace ScrapingGitHubAPI.Tests
{
    [TestClass]
    public class ScrapUnitTest
    {
        [TestMethod]
        public void TestExistingExtensionFile()
        {
            var extension = Scrap.getExtension("https://github.com/amfe/slider-js/blob/master/samples/T1BZP9FO8XXXb1upjX.jpg_q50.jpg");
            Assert.AreEqual(extension, "jpg");
        }
        [TestMethod]
        public void TestFileSizeWhenHasLines()
        {                        
            Assert.AreEqual(Scrap.getFileSize(getfileInfoWhenHasLines()), 5610);
        }
        [TestMethod]
        public void TestFileSizeWhenHasNoLines()
        {
            Assert.AreEqual(Scrap.getFileSize(getfileInfoWhenHasNoLines()), 61);
        }
        [TestMethod]
        public void TestNumberOfLinesWhenHasLines()
        {
            Assert.AreEqual(Scrap.getNumberOfLines(getfileInfoWhenHasLines()), 3);
        }
        [TestMethod]
        public void TestNumberOfLinesWhenHasNoLines()
        {
            Assert.AreEqual(Scrap.getNumberOfLines(getfileInfoWhenHasNoLines()), 0);
        }

        public static List<string> getfileInfoWhenHasLines()
        {
            var fileInfoList = new List<string>();
            fileInfoList.Add("3 lines (2 sloc)");
            fileInfoList.Add("");
            fileInfoList.Add("5.61 KB");
            return fileInfoList;
        }
        public static List<string> getfileInfoWhenHasNoLines()
        {
            var fileInfoList = new List<string>();
            fileInfoList.Add("61 Bytes");
            return fileInfoList;
        }

    }
}
