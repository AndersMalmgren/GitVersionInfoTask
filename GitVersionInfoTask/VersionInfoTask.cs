using System;
using System.IO;
using System.Linq;
using GitVersionInfo.Common;
using Microsoft.Build.Framework;
using NGit.Api;

namespace GitVersionInfo
{
    public class VersionInfoTask : Task
    {
        public override bool Execute()
        {
            Version = new VersionInfo(RepoPath, Path).GetVersion();
            return true;
        }

        [Output]
        public string Version { get; set; }
    }
}
