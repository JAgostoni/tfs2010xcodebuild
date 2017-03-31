# Xcode Build Template Arguments
This document describes the arguments availble for this build template.

* Mac Build Machine
	* Clean Up - Removes all files which were copied to the Mac build machine after the process completes
	* Mac Build Folder - The absolute path where to store the Xcode project to build
	* Mac Hostname - The hostname or IP address of the Mac build machine
	* Mac Username - The username to use during the SCP/SSH remote login
	* Mac Password - The password to use during the SCP/SSH remote login
* Xcode Build (see the man page of xcodebuild for details)
	* Architecture - Value for the -architecture parameter of xcodebuild
	* Build actions - Space delimited list of build actions
	* Build All Targets - Set to True to build all targets in the Xcode project (ignores the target parameter)
	* Configuration - Value for the -config parameter of xcodebuild
	* Project Path - Optional path to the .xcodeproj package if it is not in the remote build folder
	* Scheme - Value for the -scheme parameter of xcodebuild
	* SDK - Value for the -sdk parameter of xcodebuild
	* Target - Value for the -target parameter of xcodebuild
	* Workspace - Value for the -workspace parameter of xcodebuild