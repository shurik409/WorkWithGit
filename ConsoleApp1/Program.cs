using System;
using Octokit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var product = new ProductHeaderValue("MyAmazingApps");
            var github = new GitHubClient(product);
            var connection = new Connection(product);
            
            Task task = new Task(() => ListUserRepos(github, connection));
            task.Start();
            Console.ReadLine();
        }

        private static async void ListUserRepos(GitHubClient client, Connection connect)
        {
            User githubUser = await client.User.Get("shurik409");
            
            IReadOnlyList<Repository> githubUserRepos = await client.Repository.GetAllForUser(githubUser.Login);

            foreach(Repository r in githubUserRepos)
            {
                Console.WriteLine(r.Id + r.Name + r.Size );
                Task task = new Task(() => testGitApi(r.Id, connect));
                task.Start();
                
            }

            
            
        }

        private static async void testGitApi(long id, Connection connect)
        {
            var rep = new Repository(id);
            var apiConnect = new ApiConnection(connect);
            RepositoryCommitsClient clients = new RepositoryCommitsClient(apiConnect);
            //Console.WriteLine("\n" + rep.Name + "\n");
            try
            {
                IReadOnlyList<Octokit.GitHubCommit> repCommit = await clients.GetAll(id);
                foreach (GitHubCommit c in repCommit)
                {
                    Console.WriteLine(c.Commit.Message);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("NULL");
            }

            
        }
    }
}
