# TurtleHub
[TortoiseGit](https://tortoisegit.org/) issue tracker plugin for projects hosted on GitHub. Release versions can be 
downloaded on the [Release](../../releases) page.

![TH-issue-overview](/docs/img/TH-issue-overview.png  "Example of TurtleHub's issue picker.")

**Note:** This project is not affiliated with or endorsed by GitHub, Inc.

## Setup
1. Obviously, [TortoiseGit](https://tortoisegit.org/) (and therefore [Git](https://git-scm.com)) must be installed. 
1. Install TurtleHub with the [setup file suitable for your system](../../releases) at the predetermined default 
location.
1. Integrate TurtleHub with your project.  
There are two ways: *Local Integration* and *BugTraq Integration*.  
**For information about which method to choose and detailed information about the integration methods and features, see 
the [Setup.md](/docs/Setup.md).**  
Following below is a Quick Start, though:  
    ### Quick Start - Integration
    1. Open the context menu and go to  
    `TortoiseGit > Settings > Issue Tracker Integration`.
    1. `Add...`: TurtleHub should show up under the `Providers` dropdown menu.
    1. `Working Tree Path`: set to the directory of a local git repository.
    1. `Options`: set TurtleHub's **Parameters**:
        1. Add `owner` and `repository name` located on GitHub. 
        1. `keyword`: indicates the prefix TurtleHub uses when you choose and insert issues into the log message. 
        It is completely customizable and ommittable.  
            ![TH-options](/docs/img/TH-options.png "Options Dialog")
        1. `Reference Full Repository Name`: This creates issue references in the style of `owner/repository#1234` 
        instead of `#1234`.  
            ![TH-options-external-repo](/docs/img/TH-options-external-repo.png "Referencing whole Repository")
        1. `Show Pull Requests by Default`: On *disabled*, TurtleHub will by default only show issues from the 
        tracker without any pull requests.  
        On *enabled*, TurtleHub will **also** show pull requests alongside issues.

    **Advanced integration instructions with detailed information about the features can be found at the 
    [Setup.md](/docs/Setup.md).**

## Usage
When committing, the issue chooser should appear at the top right of the dialog. Open it and let TurtleHub fetch the 
issues. If you want to see pull requests, too, enable the checkbox at the bottom.
Select your desired issues. These will be inserted into your log message.  

To also enable the creation of clickable URL-links on issue numbers in the log, you need to setup some 
[Further BugTraq configuration](/docs/Setup.md#further-bugtraq-configuration).

## Development
The code is developed using Visual Studio 2015. In order to develop TurtleHub, first use one of the installers and 
keep the default installation directory. When Visual Studio builds the new DLL it will copy over the installed files.

Running the `build.cmd` batch file will build the installers and place them in the `bin` directory.

## License
This code is released under the [GNU General Public License version 2](http://www.gnu.org/licenses/gpl-2.0.txt).
