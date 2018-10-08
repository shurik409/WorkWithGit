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

            Console.Write("Nickname:");

            string nickname = Console.ReadLine();

            Task task = new Task(() => ListUserRepos(github, connection, nickname));
            task.Start();
            Console.ReadLine();
        }

        private static async void ListUserRepos(GitHubClient client, Connection connect, string nickname)
        {
            User githubUser = await client.User.Get(nickname);
            
            IReadOnlyList<Repository> githubUserRepos = await client.Repository.GetAllForUser(githubUser.Login);

            foreach(Repository r in githubUserRepos)
            {
                Console.WriteLine(r.Name);
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
