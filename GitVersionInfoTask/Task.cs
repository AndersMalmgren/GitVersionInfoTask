using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Build.Framework;

namespace GitVersionInfo
{
    public abstract class Task : ITask
    {
        public string RepoPath { get; set; }
        public string Path { get; set; }
        public IBuildEngine BuildEngine { get; set; }
        public ITaskHost HostObject { get; set; }

        public abstract bool Execute();
    }
}
