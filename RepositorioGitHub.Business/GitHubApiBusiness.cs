using RepositorioGitHub.Business.Contract;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Dominio.Interfaces;
using RepositorioGitHub.Infra.Contract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using RepositorioGitHub.Infra.Repositorio;

namespace RepositorioGitHub.Business
{
    public class GitHubApiBusiness: IGitHubApiBusiness
    {
        private readonly IContextRepository _context;
        private readonly IGitHubApi _gitHubApi;

        public GitHubApiBusiness(IContextRepository context, IGitHubApi gitHubApi)
        {
            _context = context;
            _gitHubApi = gitHubApi;
        }

        public ActionResult<GitHubRepositoryViewModel> Get()
        {
            // declaration
            ActionResult<GitHubRepositoryViewModel> result = new ActionResult<GitHubRepositoryViewModel>();
            result.Results = new List<GitHubRepositoryViewModel>();
            var repositories = _gitHubApi.GetRepository("prbecker");

            // instantiation
            result.IsValid = repositories.IsValid;
            result.Message = repositories.Message;
            foreach (var repository in repositories.Results)
            {
                // temp var to use on the loop
                GitHubRepositoryViewModel resulttemp = new GitHubRepositoryViewModel();
                resulttemp.Id = repository.Id;
                resulttemp.Name = repository.Name;
                resulttemp.FullName = repository.FullName;
                resulttemp.Owner = repository.Owner;
                resulttemp.Description = repository.Description;
                resulttemp.Url = repository.Url;
                resulttemp.Homepage = repository.Homepage;
                resulttemp.Language = repository.Language;
                resulttemp.UpdatedAt = repository.UpdatedAt;
                // add to actionresults.results
                result.Results.Add(resulttemp);
            }

            return result;
        }

        public ActionResult<GitHubRepositoryViewModel> GetById(long id)
        {
            // declaration
            ActionResult<GitHubRepositoryViewModel> result = new ActionResult<GitHubRepositoryViewModel>();
            result.Results = new List<GitHubRepositoryViewModel>();
            result.Result = new GitHubRepositoryViewModel();
            var repositories = _gitHubApi.GetRepositoryById("prbecker", id);

            // instantiation
            result.Result.Id = repositories.Results[0].Id;
            result.Result.Name = repositories.Results[0].Name;
            result.Result.FullName = repositories.Results[0].FullName;
            result.Result.Owner = repositories.Results[0].Owner;
            result.Result.Description = repositories.Results[0].Description;
            result.Result.Url = repositories.Results[0].Url;
            result.Result.Homepage = repositories.Results[0].Homepage;
            result.Result.Language = repositories.Results[0].Language;
            result.Result.UpdatedAt = repositories.Results[0].UpdatedAt;
            //add to actionresults.results
            result.Results.Add(result.Result);

            return result;
        }

        public ActionResult<RepositoryViewModel> GetByName(string name)
        {
            // declaration
            ActionResult<RepositoryViewModel> result = new ActionResult<RepositoryViewModel>();
            result.Result = new RepositoryViewModel();
            var repositories = _gitHubApi.GetRepositoryByName(name);

            // instantiation
            result.Result.TotalCount = repositories.Results[0].TotalCount;
            result.Result.Repositories = repositories.Results[0].Repositories;

            return result;
        }

        public ActionResult<FavoriteViewModel> GetFavoriteRepository()
        {
            ActionResult<FavoriteViewModel> result = new ActionResult<FavoriteViewModel>();
            result.Results = new List<FavoriteViewModel>();
            List<Favorite> favoriteList = _context.GetAll();

            foreach(Favorite favorite in favoriteList)
            {
                FavoriteViewModel tempFavorite = new FavoriteViewModel();

                tempFavorite.Id = favorite.Id;
                tempFavorite.Description = favorite.Description;
                tempFavorite.Language = favorite.Language;
                tempFavorite.UpdateLast = favorite.UpdateLast;
                tempFavorite.Owner = favorite.Owner;
                tempFavorite.Name = favorite.Name;

                result.Results.Add(tempFavorite);
            }

            return result;
        }

        public ActionResult<GitHubRepositoryViewModel> GetRepository(string owner, long id)
        {
            // declaration
            ActionResult<GitHubRepositoryViewModel> result = new ActionResult<GitHubRepositoryViewModel>();
            result.Results = new List<GitHubRepositoryViewModel>();
            result.Result = new GitHubRepositoryViewModel();
            var repositories = _gitHubApi.GetRepositoryById(owner, id);

            // instantiation
            result.IsValid = true;
            result.Result.Id = repositories.Results[0].Id;
            result.Result.Name = repositories.Results[0].Name;
            result.Result.FullName = repositories.Results[0].FullName;
            result.Result.Owner = repositories.Results[0].Owner;
            result.Result.Description = repositories.Results[0].Description;
            result.Result.Url = repositories.Results[0].Url;
            result.Result.Homepage = repositories.Results[0].Homepage;
            result.Result.Language = repositories.Results[0].Language;
            result.Result.UpdatedAt = repositories.Results[0].UpdatedAt;
            //add to actionresults.results
            result.Results.Add(result.Result);

            return result;
        }

        public ActionResult<FavoriteViewModel> SaveFavoriteRepository(FavoriteViewModel view)
        {
            // declaration
            ActionResult<FavoriteViewModel> result = new ActionResult<FavoriteViewModel>();
            Favorite favorite = new Favorite();
            // instantiation
            favorite.Id = view.Id;
            favorite.Description = view.Description;
            favorite.Language = view.Language;
            favorite.UpdateLast = view.UpdateLast;
            favorite.Owner = view.Owner;
            favorite.Name = view.Name;

            result.IsValid = _context.Insert(favorite);

            return result;
        }

    }
}
