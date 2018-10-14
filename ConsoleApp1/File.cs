using System;
using System.Collections.Generic;
using System.Text;
using LibGit2Sharp;

namespace ConsoleApp1
{
    class File
    {
        protected string file_path;
        protected int commit_count;

        public File(string path)
        {
            file_path = path;
            commit_count = 0;
        }

        public void increase()
        {
            commit_count++;
        }

        public int count
        {
            get{
                return commit_count;
            }
        }

        public string path
        {
            get{
                return file_path;
            }
            set
            {
                file_path = value;
            }
        }
    }
}
