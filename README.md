# TurtleHub
[TortoiseGit](https://tortoisegit.org/) issue tracker plugin for projects hosted on GitHub. Release versions can be downloaded on the [Release](https://github.com/dail8859/TurtleHub/releases) page.

![Screen Shot](/img/screenshot.png)

**Note:** This project is not affiliated with or endorsed by GitHub, Inc.

## Setup
Obviously, [TortoiseGit](https://tortoisegit.org/) must be installed. There are two ways to integrate TurtleHub:

### a) Local Integration
This is a simple setup for a local repository. Use this if you don't plan to change the file path to the desired local repository.

* Once TurtleHub is installed, open the context menu and go to `TortoiseGit > Settings > Issue Tracker Integration`.
* Click `Add...`. TurtleHub should show up under the `Providers` dropdown menu.
* The `Working Tree Path` should be set to the directory of a local git repository.
* Click `Options`
  * Add the owner and repository name located on GitHub. You can get these for example from the URL of the repository: e.g. for `https://github.com/dail8859/TurtleHub` the owner is `dail8859` and the repository name is `TurtleHub`.
  * The keyword indicates the prefix TurtleHub uses when you choose and insert issues into the log message. By default it is `Closes` and will format to something like `Closes #1234`. With this Github can automatically close an issue. However, the keyword is completely customizable. Either set it to one of the predefined keywords or edit the Parameters field directly after closing the Options dialog.

### b) BugTraq Integration
BugTraq is a TortoiseGit independent git configuration and uses the git configuration hierarchy. Use this if you want to set up a tracker integration independently from your local repository path (e.g. on portable mediums) or if you want to setup a shared configuration for your project or system.

* Optionally: If you do this the first time, you probably want to setup the *Local Integration* from above first. This is *only* needed to retrieve the setup parameters for this method. Afterwards you can remove the *Local Integration* again.
* On your local repository open the context menu and go to `TortoiseGit > Settings > Issue Tracker Config` 
* If you set up the *Local Integration* in the first step: In the `Effective` scope, check the `IBugTraqProvider` frame near the bottom. You should see your current values for the TurtleHubs UUID and parameters. Copy them.
* Change the scope to your desired scope, e.g. `Project`. In the `IBugTraqProvider` frame near the bottom insert the UUID (i.e. GUID) and parameters for TurtleHub (e.g. from the previous step).
* Now you can delete your *Local Integration* entry from `Issue Tracker Integration` again, if you did so.
* You can share/commit your BugTraq git config (e.g. `.tgitconfig` file on `Project` scope).

## Usage
When committing, the issue chooser should appear at the top right of the dialog. Open it, let it fetch the issues, and select your desired issues. These will be inserted into your log message.

## Development
The code is developed using Visual Studio 2015. In order to develop TurtleHub, first use one of the installers and keep the default installation directory. When Visual Studio builds the new DLL it will copy over the installed files.

Running the `build.cmd` batch file will build the installers and place them in the `bin` directory.

## License
This code is released under the [GNU General Public License version 2](http://www.gnu.org/licenses/gpl-2.0.txt).
