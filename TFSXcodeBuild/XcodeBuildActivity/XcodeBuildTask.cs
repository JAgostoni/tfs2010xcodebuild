using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Activities.Tracking;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Build.Workflow.Activities;
using XcodeBuildSSHClient;
using XcodeBuild;

using System.Diagnostics;

namespace XcodeBuildActivity
{
    [BuildActivity(HostEnvironmentOption.All)]
    public sealed class XcodeBuildTask : CodeActivity
    {
        [RequiredArgument]
        public InArgument<IBuildDetail> BuildDetail { get; set; }

        [RequiredArgument]
        public InArgument<string> MacHostname { get; set; }

        [RequiredArgument]
        public InArgument<string> MacUsername { get; set; }
 
        [RequiredArgument]
        public InArgument<string> MacPassword { get; set; }

        [RequiredArgument]
        public InArgument<string> MacBuildFolder { get; set; }

        public InArgument<string> SourceCodeFolder { get; set; }

        public InArgument<string> XcodeBuildBinPath { get; set; }
        public InArgument<string> XcodeProjectPath { get; set; }
        public InArgument<string> XcodeScheme { get; set; }
        public InArgument<string> XcodeTarget { get; set; }
        public InArgument<bool> XcodeAllTargets { get; set; }
        public InArgument<string> XcodeWorkspace { get; set; }
        public InArgument<string> XcodeConfiguration { get; set; }
        public InArgument<string> XcodeArchitecture { get; set; }
        public InArgument<string> XcodeSDK { get; set; }
        public InArgument<string> XcodeBuildActions { get; set; }
        public InArgument<bool> CleanUp { get; set; }


        // If your activity returns a value, derive from CodeActivity<TResult>
        // and return the value from the Execute method.
        protected override void Execute(CodeActivityContext context)
        {
            IBuildDetail bd = context.GetValue(this.BuildDetail);

            // Setup the SSH client details
            Client buildClient = new Client();
            buildClient.Hostname = context.GetValue(this.MacHostname);
            buildClient.Username = context.GetValue(this.MacUsername);
            buildClient.Password = context.GetValue(this.MacPassword);
            string remoteDirectory = context.GetValue(this.MacBuildFolder);
            string sourceDirectory = context.GetValue(this.SourceCodeFolder);
            string buildOutputDirectory = bd.DropLocation;
            bool cleanUp = context.GetValue(this.CleanUp);

            if (!remoteDirectory.EndsWith(@"/")) remoteDirectory = remoteDirectory + "/";

            // Setup the command object
            XcodeBuild4 buildCommand = new XcodeBuild4();
            buildCommand.AllTargets = context.GetValue(this.XcodeAllTargets);
            buildCommand.Architecture = context.GetValue(this.XcodeArchitecture);
            buildCommand.BinPath = context.GetValue(this.XcodeBuildBinPath);
            buildCommand.BuildActions = context.GetValue(this.XcodeBuildActions);
            buildCommand.Configuration = context.GetValue(this.XcodeConfiguration);
            buildCommand.ProjectPath = context.GetValue(this.XcodeProjectPath);
            buildCommand.Scheme = context.GetValue(this.XcodeScheme);
            buildCommand.SDK = context.GetValue(this.XcodeSDK);
            buildCommand.Target = context.GetValue(this.XcodeTarget);
            buildCommand.Workspace = context.GetValue(this.XcodeWorkspace);

            context.TrackBuildMessage("Creating build directory on remote Mac build agent ...", BuildMessageImportance.Normal);
            buildClient.ExecuteCommand("mkdir -p " + EscapeString(remoteDirectory)); ;

            context.TrackBuildMessage("Copying Xcode source to remote Mac build agent...", BuildMessageImportance.Normal);
            buildClient.CopyDirectoryToServerUsingScp(sourceDirectory, remoteDirectory);

            context.TrackBuildMessage("Running xcodebuild on remote Mac build agent...", BuildMessageImportance.Normal);

            string output = buildClient.ExecuteCommand("cd " + EscapeString(remoteDirectory) + "; " + buildCommand.ToString());

            
            bool success = output.Contains("BUILD SUCCEEDED");
            if (!success)
            {
                context.TrackBuildError("Xcode Build Failed");

            }
            else
            {
                context.TrackBuildMessage("Copying build output to build server...", BuildMessageImportance.Normal);
                buildClient.CopyDirectoryFromServerUsingScp(remoteDirectory + @"/build", buildOutputDirectory);

            }
            context.TrackBuildMessage("Wrtiting compile output to build server...", BuildMessageImportance.Normal);
            System.IO.File.WriteAllText(buildOutputDirectory + @"\xcodebuildoutput.txt", output);

            if (cleanUp) buildClient.ExecuteCommand("rm -rf  " + EscapeString(remoteDirectory));
        }



        private string EscapeString(string cleanString)
        {
            return cleanString.Replace(" ", @"\ ");
        }

    }
}
