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
            Work work = new Work();
            work.WorkWithRepo();
            Console.ReadLine();

            /* Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            var product = new ProductHeaderValue("MyAmazingApps");
            var github = new GitHubClient(product);
            var connection = new Connection(product);

           Console.Write("Nickname:");

            string nickname = Console.ReadLine();

            Task task = new Task(() => ListUserRepos(github, connection, nickname));
            task.Start();*/
        }

        /*private static async void ListUserRepos(GitHubClient client, Connection connect, string nickname)
        {
            User githubUser = await client.User.Get(nickname);
            
            IReadOnlyList<Repository> githubUserRepos = await client.Repository.GetAllForUser(githubUser.Login);

            
            var apiConnect = new ApiConnection(connect);
            RepositoryCommitsClient clients = new RepositoryCommitsClient(apiConnect);
            
            foreach (Repository r in githubUserRepos)
            {
                Console.WriteLine(r.Name + "/n");


                

/*
                try
                {
                    IReadOnlyList<Octokit.GitHubCommit> repCommit = await clients.GetAll(r.Id);
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

            
            
        }*/
        
    }
}
