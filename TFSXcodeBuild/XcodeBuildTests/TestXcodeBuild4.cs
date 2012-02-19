using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XcodeBuild;

namespace XcodeBuildTests
{
    [TestClass]
    public class TestXcodeBuild4
    {
        [TestMethod]
        public void NoArgsTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            Assert.AreEqual("/usr/bin/xcodebuild", xb.ToString());
        }

        [TestMethod]
        public void ProjectArgTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.ProjectPath = "~/Documents/Project/Test.xcodeproj";
            Assert.AreEqual("/usr/bin/xcodebuild -project ~/Documents/Project/Test.xcodeproj", xb.ToString());
        }

        [TestMethod]
        public void SchemeArgTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.Scheme = "MyApp";
            Assert.AreEqual("/usr/bin/xcodebuild -scheme MyApp", xb.ToString());
        }

        [TestMethod]
        public void TargetArgTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.Target = "MyTarget";
            Assert.AreEqual("/usr/bin/xcodebuild -target MyTarget", xb.ToString());

            xb.AllTargets = true;
            Assert.AreEqual("/usr/bin/xcodebuild -alltargets", xb.ToString());

        }

        [TestMethod]
        public void WorkspaceArgTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.Workspace = "MyWorkspace";
            Assert.AreEqual("/usr/bin/xcodebuild -workspace MyWorkspace", xb.ToString());
        }

        [TestMethod]
        public void ConfigArgTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.Configuration = "MyConfig";
            Assert.AreEqual("/usr/bin/xcodebuild -configuration MyConfig", xb.ToString());
        }

        [TestMethod]
        public void ArchArgTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.Architecture = "MyArch";
            Assert.AreEqual("/usr/bin/xcodebuild -arch MyArch", xb.ToString());
        }

        [TestMethod]
        public void SDKArgTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.SDK = "MySdk";
            Assert.AreEqual("/usr/bin/xcodebuild -sdk MySdk", xb.ToString());
        }

        [TestMethod]
        public void ActionArgTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.BuildActions = "build archive";
            Assert.AreEqual("/usr/bin/xcodebuild build archive", xb.ToString());
        }

        [TestMethod]
        public void MultipleArgsTest()
        {
            XcodeBuild4 xb = new XcodeBuild4();
            xb.BinPath = "/usr/bin/xcodebuild";
            xb.SDK = "MySdk";
            xb.AllTargets = true;
            xb.Architecture = "MyArch";

            Assert.AreEqual("/usr/bin/xcodebuild -alltargets -arch MyArch -sdk MySdk", xb.ToString());
        }

        [TestMethod]
        public void TestBuildFolder()
        {
            XcodeBuild4 xb = new XcodeBuild4();

            Assert.AreEqual("build", xb.BuildFolder);
            xb.ProjectPath = "/folder/my.xcodeproj";
            Assert.AreEqual("/folder/build", xb.BuildFolder);
        }

    }
}
