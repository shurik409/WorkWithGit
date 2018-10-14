using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibGit2Sharp;

namespace ConsoleApp1
{
    class Class1
    {
        public void TestLibGit()
        {
            var temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            string path = Repository.Clone("https://github.com/shurik409/practice-front", temp, new CloneOptions { OnTransferProgress = Class1.TransferProgress});

            var repo = new Repository(path);
            var commits = repo.Commits.Reverse<Commit>();
            var database = repo.ObjectDatabase;
            var diff = repo.Diff;
            var first = commits.First<Commit>();
            Commit previous = first;
            Console.WriteLine(first.Message + " First");
            foreach (Commit c in commits)
            {
                Console.WriteLine(c.Message + " " + c.Id);
                var comId = c.Id;
                var cTree = c.Tree;
                TreeChanges change = diff.Compare<TreeChanges>(previous.Tree, cTree);
                Console.WriteLine(change.Count<TreeEntryChanges>() + " Test");
                previous = c;
            }    

           /* foreach (Branch b in repo.Branches.Where(b => !b.IsRemote))
            {
                Console.WriteLine(string.Format("{0}{1}", b.IsCurrentRepositoryHead ? "*" : " ", b.FriendlyName));
                Console.WriteLine(b.Commits);
            }*/
            
        }
        public static bool TransferProgress(TransferProgress progress)
        {
           // Console.WriteLine($"Objects: {progress.ReceivedObjects} of {progress.TotalObjects}, Bytes: {progress.ReceivedBytes}");
            return true;
        }
        
    }
}
