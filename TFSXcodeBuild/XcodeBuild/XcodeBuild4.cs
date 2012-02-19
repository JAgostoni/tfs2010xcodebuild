using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XcodeBuild
{
    public class XcodeBuild4 : XcodeBuild
    {
        public string BinPath { get; set; }         // Path to xcodebuild (default /usr/bin/xcodebuild)
        public string ProjectPath { get; set; }     // Optional.  Path to .xcodeproj folder/package
        public string Scheme { get; set; }          // Optional. Build scheme to use.
        public string Target { get; set; }          // Optional. Target to build
        public bool AllTargets { get; set; }        // Optional. Build all targets.  Ignores Target.
        public string Workspace { get; set; }       // Optional.  Workspace to build
        public string Configuration { get; set; }   // Optional.  Configuration to build (e.g. Debug or Release)
        public string Architecture { get; set; }    // Optional.  Architecture to build
        public string SDK { get; set; }             // Optional.  SDK to build
        public string BuildActions { get; set; }    // Optional.  List of build actions separated by spaces (e.g. build archive clean)

        public string BuildFolder 
        { 
            get 
            {
                if (String.IsNullOrEmpty(this.ProjectPath))
                {
                    return "build";
                }
                else
                {
                    return this.ProjectPath.Substring(0, this.ProjectPath.LastIndexOf("/")) + "/build";
                }

            } 
        }

        public XcodeBuild4()
        {
            this.BinPath = "/usr/bin/xcodebuild";
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.BinPath);

            // -project
            if (!String.IsNullOrEmpty(this.ProjectPath)) { sb.Append(" -project "); sb.Append(EscapeString(this.ProjectPath)); }

            // -target / -alltargets
            if (this.AllTargets)
            {
                sb.Append(" -alltargets");
            }
            else if (!String.IsNullOrEmpty(this.Target))
            {
                sb.Append(" -target ");
                sb.Append(EscapeString(this.Target));
            }

            // -scheme
            if (!String.IsNullOrEmpty(this.Scheme)) { sb.Append(" -scheme "); sb.Append(EscapeString(this.Scheme)); }

            // -workspace
            if (!String.IsNullOrEmpty(this.Workspace)) { sb.Append(" -workspace "); sb.Append(EscapeString(this.Workspace)); }

            // -config
            if (!String.IsNullOrEmpty(this.Configuration)) { sb.Append(" -configuration "); sb.Append(EscapeString(this.Configuration)); }

            // -arch
            if (!String.IsNullOrEmpty(this.Architecture)) { sb.Append(" -arch "); sb.Append(EscapeString(this.Architecture)); }

            // -sdk
            if (!String.IsNullOrEmpty(this.SDK)) { sb.Append(" -sdk "); sb.Append(EscapeString(this.SDK)); }

            // Build Actions
            if (!String.IsNullOrEmpty(this.BuildActions)) { sb.Append(" "); sb.Append(this.BuildActions); }

            return sb.ToString();
        }

        private string EscapeString(string cleanString)
        {
            return cleanString.Replace(" ", @"\ ");
        }
    }
}
