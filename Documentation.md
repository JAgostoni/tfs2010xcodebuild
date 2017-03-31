# Quick Start
This document provides  quick start guide to get Xcode builds running via TFS 2010 Team Build

## Requirements
The following are required to automated Xcode builds with TFS:
* A Mac running Xcode 4
* TFS 2010
* TFS 2010 Team Build Server
* Xcode source code under TFS version control (I use svnbridge.codeplex.com)

## Prepare the Mac build computer
# Create a build user account
## System Preferences->Users & Groups->Add a new user
# Enable SSH
## System Preferences->Sharing
## Enable Remote Login
# Ensure the latest Xcode 4 app is installed and functional

## Prepare the Team Build Server
# Download the source and compile or download the binaries for TFS Xcode Build
# Add the assemblies to TFS Source Control
## This should be a central location where all custom build assemblies would be kept
# Add the XAML Build Templates (XcodeBuildTemplate.xaml) to TFS version control
## This can be kept anywhere, but someplace central is best
# Check in the custom assemblies and build template
# Update the build controller to reference this custom assembiles location
## Open Team Explorer
## Expan a Team Project, right click on Builds and select Manage Build Controllers
## Select the appropriate build controller and click Properties
## Select the above path in version control to locate the custom build assemblies
# Save and close all dialogs

## Create a new Build Definition
# Open a Team Project in Team Explorer
# Right-click on the Builds node, select New Build Definition
# Enter in the desired details in the General and Trigger tabs
# In the Workspace tab, ENSURE that the only mapping which exists points to the directory containing the .xcodeproj package/folder
# In the process tab, Expand the template section
# Click the New button (first build definition for this Team Project only)
# Browse to the XcodeBuildTemplate.xaml checked in earlier
## Your choice whether or not to copy it into the current Team Project or use the central copy
# Enter in the desired build properties (see [Build Template Arguments](Build-Template-Arguments)  for a description of the parameters)
# Save the build definition


