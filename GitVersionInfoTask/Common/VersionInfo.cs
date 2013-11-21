using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GitVersionInfo.Model;
using NGit.Api;

namespace GitVersionInfo.Common
{
    internal class VersionInfo
    {
        private readonly string repoPath;
        private readonly string localPath;

        public VersionInfo(string repoPath, string localPath)
        {
            this.repoPath = repoPath;
            this.localPath = localPath;
        }
        
        public string GetVersion()
        {
            var count = GetCommitCount();

            var serializer = new DataContractSerializer(typeof (List<Model.Version>));

            using (var stream = File.OpenRead("versions.xml"))
            {
                var versions = (serializer.ReadObject(stream) as List<Model.Version>)
                    .OrderBy(v => v.GitRevCount).ToList();

                for (var index = 0; index < versions.Count; index++)
                {
                    var version = versions[index];

                    if(count >= version.GitRevCount && (index == versions.Count - 1 || versions[index+1].GitRevCount > count))
                    {
                        return string.Format("{0}.{1}.{2}.0", version.Major, version.Minor, count);
                    }
                    
                }

                throw new Exception("No version in version.xml matches git revision");
            }
        }

        private int GetCommitCount()
        {
            var git = Git.Open(repoPath);
            var allCommits = git.Log().Call().OrderBy(c => c.CommitTime);
            if (string.IsNullOrEmpty(localPath))
                return allCommits.Count() + 1;

            var commits = git
                .Log()
                .AddPath(localPath)
                .Call()
                .ToList();

            if (!commits.Any()) return 0;

            var commit = commits
                .OrderByDescending(c => c.CommitTime)
                .First();

            return allCommits
                  .Select((c, i) => new { Commit = c, Index = i })
                  .First(c => c.Commit.Id.Name == commit.Id.Name)
                  .Index + 1;
        }
    }
}
