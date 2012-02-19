using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XcodeBuildSSHClient;
using XcodeBuild;

namespace XcodeBuildSSHClientTests
{
    [TestClass]
    public class TestSSHClient
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestCopy()
        {
            Client xclient = new Client();
            xclient.Hostname = "";
            xclient.Username = "";
            xclient.Password = "";
            XcodeBuild4 buildCommand = new XcodeBuild4();


            buildCommand.ProjectPath = @"/Users/jagostoni/Documents/build/build/CRN.xcodeproj";
            buildCommand.SDK = "iphonesimulator5.0";
            xclient.ExecuteCommand("mkdir -p /Users/jagostoni/Documents/build");
            xclient.CopyDirectoryToServerUsingScp(@"C:\projects\UBM\trunk\iPad\CRN", "/Users/jagostoni/Documents/build");
            TestContext.WriteLine(xclient.ExecuteRemoteBuildCommand(buildCommand));

            xclient.CopyDirectoryFromServerUsingScp(buildCommand.BuildFolder, @"c:/projects/output");
            
            xclient.ExecuteCommand("rm -rf /Users/jagostoni/Documents/build");

        }
    }
}
