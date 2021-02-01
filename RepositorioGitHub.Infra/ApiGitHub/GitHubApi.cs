using Newtonsoft.Json;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Dominio.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RepositorioGitHub.Infra.ApiGitHub
{
    public class GitHubApi : IGitHubApi
    {
        public ActionResult<GitHubRepository> GetRepository(string owner)
        {
            // declaration
            ActionResult<GitHubRepository> repository = new ActionResult<GitHubRepository>();
            repository.Results = new List<GitHubRepository>();

            // get json from url
            string url = "https://api.github.com/users/" + owner + "/repos";
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("user-agent", "Only a test!");
            string jsonString = webClient.DownloadString(url);

            // create a jsonObject list
            var jsonObjects = JsonConvert.DeserializeObject<List<GitHubRepository>>(jsonString);
            
            // loop for ActionResult.results
            foreach (var jsonObject in jsonObjects)
            {
                repository.Result = jsonObject;
                repository.Results.Add(repository.Result);
            }

            return repository;
        }

        public ActionResult<RepositoryModel> GetRepositoryByName(string name)
        {
            // declaration
            ActionResult<RepositoryModel> search = new ActionResult<RepositoryModel>();
            search.Results = new List<RepositoryModel>();

            // get json from url
            string url = "https://api.github.com/search/repositories?q=" + name;
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("user-agent", "Only a test!");
            string jsonString = webClient.DownloadString(url);

            var jsonObject = JsonConvert.DeserializeObject<RepositoryModel>(jsonString);

            search.Results.Add(jsonObject);

            return search;
        }

        public ActionResult<GitHubRepository> GetRepositoryById(string owner, long id)
        {
            // declaration
            ActionResult<GitHubRepository> repository = new ActionResult<GitHubRepository>();
            repository.Results = new List<GitHubRepository>();

            // get json from url
            string url = "https://api.github.com/users/" + owner + "/repos";
            WebClient webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            webClient.Headers.Add("user-agent", "Only a test!");
            string jsonString = webClient.DownloadString(url);

            // create a jsonObject list
            var jsonObjects = JsonConvert.DeserializeObject<List<GitHubRepository>>(jsonString);

            // loop for ActionResult.results
            foreach (var jsonObject in jsonObjects)
            {
                repository.Result = jsonObject;
                if(repository.Result.Id == id)
                {
                    repository.Results.Add(repository.Result);
                }
            }

            return repository;
        }
    }
}
