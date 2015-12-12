# TurtleHub
[TortoiseGit](https://tortoisegit.org/) issue tracker plugin for projects hosted on GitHub. Release versions can be downloaded on the [Release](https://github.com/dail8859/TurtleHub/releases) page.

![screenshot](https://dl.dropboxusercontent.com/u/13788271/TurtleHub/screenshot2.png)

**Note:** This project is not affiliated with or endorsed by GitHub, Inc.

## Usage
Obviously, [TortoiseGit](https://tortoisegit.org/) must be installed. Once TurtleHub is installed, right-click and go to `TortoiseGit > Settings > Issue Tracker Integration` and click `Add...`. TurtleHub should show up under the `Providers` dropdown menu. The `Working Tree Path` should be set to the directory of a local git repository. Click `Options` and add the owner and repository name located on GitHub.

## Development
The code is developed using Visual Studio 2013. In order to develop TurtleHub, first use one of the installers and keep the default installation directory. When Visual Studio builds the new it will copy over the installed files.

Running the `build.cmd` batch file will build the installers and place them in the `bin` directory.

## License
This code is released under the [GNU General Public License version 2](http://www.gnu.org/licenses/gpl-2.0.txt).
