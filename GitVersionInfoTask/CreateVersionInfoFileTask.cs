using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GitVersionInfo.Common;
using Microsoft.Build.Framework;
using Sharpen;

namespace GitVersionInfo
{
    public class CreateVersionInfoFileTask : Task
    {
        public override bool Execute()
        {
            if (string.IsNullOrEmpty(Version))
            {
                VersionInfoFilePath = string.IsNullOrEmpty(VersionInfoFilePath)
                               ? System.IO.Path.Combine(RepoPath, Path ?? string.Empty, "VersionInfo.cs")
                               : VersionInfoFilePath;

                Version = new VersionInfo(RepoPath, Path).GetVersion();
            }

            if (!File.Exists(VersionInfoFilePath) || !File.ReadAllText(VersionInfoFilePath).Contains(Version))
            {
                File.WriteAllText(VersionInfoFilePath, string.Format(@"using System.Reflection;  
[assembly: AssemblyVersion(""{0}"")]
[assembly: AssemblyFileVersion(""{0}"")]", Version));
            }

            return true;
        }

        public string Version { get; set; }
        public string VersionInfoFilePath { get; set; }
    }
}
