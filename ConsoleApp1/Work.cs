using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LibGit2Sharp;

namespace ConsoleApp1
{
    class Work
    {
        public void WorkWithRepo()
        {
            var temp = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            string path = Repository.Clone("https://github.com/shurik409/petya", temp, new CloneOptions { OnTransferProgress = Work.TransferProgress});

            var repo = new Repository(path);

            WorkWithCommit(repo);
            
            
        }

        public static bool TransferProgress(TransferProgress progress)
        {
           // Console.WriteLine($"Objects: {progress.ReceivedObjects} of {progress.TotalObjects}, Bytes: {progress.ReceivedBytes}");
            return true;
        }

        
        public void WorkWithCommit(Repository repo)
        {
            var commits = repo.Commits.Reverse<Commit>();
            var diff = repo.Diff;
            Tree previous = null;
            List<File> files = new List<File> { } ;
            foreach (Commit c in commits)
            {
                //Console.WriteLine(c.Message);
                foreach (TreeEntryChanges change in diff.Compare<TreeChanges>(previous, c.Tree))
                {
                    //Console.WriteLine("{0}:{1}", change.Status, change.Path);
                    if (findFile(change.Path, files) != null)
                    {
                        findFile(change.Path, files).increase();
                    }
                    else {
                        files.Add(new File(change.Path));
                        if (change.Status != ChangeKind.Added)
                            findFile(change.Path, files).increase();
                    }
                }
                previous = c.Tree;
            }
            Console.WriteLine();
            foreach (File file in files)
            {
                Console.WriteLine("{0} : {1}", file.path, file.count);
            }
            Console.WriteLine();
            Console.WriteLine(files.Count);
        }
        
        /*public bool fileExists(string path, List<File> files)
        {
            foreach(File file in files)
            {
                if(file.path == path)
                {
                    return true;
                }
            }
            return false;
        }*/

        public File findFile(string path, List<File> files)
        {
                foreach (File file in files)
                {
                    if (file.path == path)
                    {
                        return file;
                    }
                }
            return null;
        }
    }
}
