using Newtonsoft.Json;
using RepositorioGitHub.Dominio;
using RepositorioGitHub.Infra.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioGitHub.Infra.Repositorio
{
    public class ContextRepository : IContextRepository
    {
        // path to txt TesteW12Github(2)\RepositorioGitHub.Infra\Repositorio\Repository.txt
        string path = @"C:\Users\prbecker\Desktop\TesteW12Github(2)\RepositorioGitHub.Infra\Repositorio\Repository.txt";
        public bool ExistsByCheckAlready(Favorite favorite)
        {
            // declaration
            string jsonString = JsonConvert.SerializeObject(favorite);
            string[] lines = File.ReadAllLines(path);

            // loop for favorite match
            foreach(string line in lines)
            {
                if(string.Compare(jsonString, line) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Favorite> GetAll()
        {
            // declaration
            List<Favorite> favoriteList = new List<Favorite>();
            string[] lines = File.ReadAllLines(path);

            // loop to get all favorites
            foreach (string line in lines)
            {
                var jsonObject = JsonConvert.DeserializeObject<Favorite>(line);

                favoriteList.Add(jsonObject);
            }
            
            return favoriteList;
        }

        public bool Insert(Favorite favorite)
        {
            string jsonString = JsonConvert.SerializeObject(favorite);
            if(!ExistsByCheckAlready(favorite))
            {
                StreamWriter sr = new StreamWriter(path, true);
                sr.WriteLine(jsonString);
                sr.Close();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
