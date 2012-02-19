using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XcodeBuild;
using Tamir.SharpSsh;

namespace XcodeBuildSSHClient
{
    public class Client
    {
        public string Hostname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
 

        public void CopyDirectoryToServerUsingScp(string sourceDirectory, string remoteDirectory)
        {
            Scp transfer = new Scp(this.Hostname, this.Username);
            transfer.Password = this.Password;
            transfer.Recursive = true;
            transfer.Connect();
            transfer.Put(sourceDirectory, remoteDirectory);
            transfer.Close();
        }

        public void CopyDirectoryFromServerUsingScp(string remoteDirectory, string targetDirectory)
        {
            Scp transfer = new Scp(this.Hostname, this.Username);
            transfer.Password = this.Password;
            transfer.Recursive = true;
            transfer.Connect();
            transfer.Get(remoteDirectory, targetDirectory);
            transfer.Close();
        }

        public string ExecuteRemoteBuildCommand(XcodeBuild.XcodeBuild buildCommand) {
            return ExecuteCommand(buildCommand.ToString());
        }

        public string ExecuteCommand(string command)
        {
            SshExec exec = new SshExec(this.Hostname, this.Username);
            exec.Password = this.Password;
            exec.Connect();
            string output = exec.RunCommand(command);

            exec.Close();

            return output;

        }
    }
}
