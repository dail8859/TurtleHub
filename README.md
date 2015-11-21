# TurtleHub
[TortoiseGit](https://tortoisegit.org/) issue tracker plugin for projects hosted on GitHub.

*This is still in the early development stages, but should still be useful.*

Release versions can be downloaded on the [Release](https://github.com/dail8859/TurtleHub/releases) page.

**Note:** This project is not affiliated with or endorsed by Github, Inc.

## Usage
Obviously, [TortoiseGit](https://tortoisegit.org/) must be installed. Once TurtleHub is installed, right-click and go to `TortoiseGit > Settings > Issue Tracker Integration` and click `Add...`. TurtleHub should show up under the `Providers` dropdown menu. The `Working Tree Path` should be set to the directory of a local git repository. Click `Options` and add the username/repository of the repository located on Github.

## Development
The code has been developed using Visual Studio 2013. Running the `build.cmd` batch file will build the code and also create the installers in the `bin` directory.

## License
This code is released under the [GNU General Public License version 2](http://www.gnu.org/licenses/gpl-2.0.txt).