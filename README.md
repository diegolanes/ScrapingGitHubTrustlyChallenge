# Scraping GitHub Trustly Challenge

## 💻 About the project

This project is a web api that performs a scraping in a github repository that returns as a response a json with the total number of lines,
the number of files with the same extension, the number of bytes of these files, with everything grouped by extension, in the following pattern:

[ {
    "extension": "babelrc",
    "bytes": 29,
    "count": 1,
    "lines": 3
  },
  {
    "extension": "gif",
    "bytes": 43,
    "count": 1,
    "lines": 0
  }]
  
  ### To hit the endpoint, use the url:
  #### http://scraper-git.herokuapp.com/api/scrap/
  ##### * should be http://...
  <br>
  #### to start the scraping, append the user and repo at the end of the url:

 #### http://scraper-git.herokuapp.com/api/scrap/{user}/{repo}

  ### If you're running locally:
  #### https://localhost:44339/api/scrap/amfe/slider-js
  
## 🛠️ Desafio

Requirements: 
    • Your API must be written using Java 8 or newer, ECMAscript 2015 or newer, or C# 8.0; 
    • Data must be retrieved from Github website by using web scraping techniques. Do not use Github’s API or download the source code as a ZIP file; 
    • Do not use web scraping libraries. We would like to know your ideas on how it can be done; 
    • Your API must support thousands of concurrent requests; 
    • We think it’s ok if the first request to a particular repository takes some time to respond (since you depend on Github website response times), but we don’t expect the subsequent requests to be long; 
    • We don’t expect to get timeout errors; 
    • We must understand your code and use your API without having to ask you any questions. Our primary language is English so please use it on comments and documentation; 
    • We’d like to see SOLID principles in your solution; 
    • You are free to choose your API contracts (parameters and response formats) but we’d like to be able to integrate it with any other existing solutions; 
    • You don’t need to persist any data (but feel free to do it if you want); 
    • We’d like to see at least one automated test; 
    • You must deploy your solution to a cloud provider like Amazon AWS or Heroku and send us the link to access it. It’s a plus if you publish a Docker image with your application (including its dependencies) in a registry like Docker Hub and let us know how to get it. 


## 🚀 Challenge

This project was developed as a technical challenge from Trustly for a full stack developer position.
