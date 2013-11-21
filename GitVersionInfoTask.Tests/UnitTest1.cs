using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GitVersionInfo.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var task = new VersionInfoTask();
            task.RepoPath = @"";
            task.Path = "";
            task.Execute();

        }
    }
}
