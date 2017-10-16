# Table of Contents:                        
* [Which method should I choose?](#which-method-should-i-choose)
* [a) Local Integration](#a-local-integration)
* [b) BugTraq Integration](#b-bugtraq-integration)
  * [Further BugTraq configuration](#further-bugtraq-configuration)

# Which method should I choose?
#### Local Integration
This is a simple setup for a local repository.    

Use this if you...
* want to use TurtleHub only locally without sharing the setup.
* **and** do not plan to change the file path to the desired local repository.

**On how to setup Local Integration see [a) Local Integration](#a-local-integration) below.**

#### BugTraq Integration
*BugTraq* is a configuration effort to integrate issue trackers into version control systems. Therefore it is 
independent from TurtleHub. It is mainly known from SVN but found implementation into Git as well. It uses the Git 
configuration hierarchy and thus can be local, shared, global or system wide. 

While the general BugTraq configuration is TortoiseGit independent, the plugin-specific setup of TurtleHub 
listed below should be able to work specifically for the whole *Tortoise-* family. Though, it was not tested for 
TortoiseSVN or other implementations, yet. Feel free to test it and give us feedback.

Use this if you...
* want to setup a shared configuration for your project, team or system.
* **or** want to set up a portable tracker integration. i.e. independent from your local repository path like on 
portable mediums.

Because of the benefits, we recommend this option over the *Local Integration*, even if you want to use it only 
locally.

**On how to setup BugTraq Integration see 
[b) BugTraq Integration](#b-bugtraq-integration) below.**

**For further BugTraq configurations about issue linking and a fallback for users without TurtleHub, see 
[Further BugTraq configuration](#further-bugtraq-configuration) below.**




# a) Local Integration
## Steps:
1. Once TurtleHub is installed, open the context menu and go to  
`TortoiseGit > Settings > Issue Tracker Integration`.
1. Click `Add...`. TurtleHub should show up under the `Providers` dropdown menu.
1. The `Working Tree Path` should be set to the directory of a local git repository.
1. Click `Options` to set TurtleHub's **Parameters**:
    1. Add the **owner** and **repository name** located on GitHub. You can get these for example from the URL of the 
    repository:  
    e.g. for `https://github.com/dail8859/TurtleHub` the owner is `dail8859` and the repository name is 
    `TurtleHub`.
    1. The **keyword** indicates the prefix TurtleHub uses when you choose and insert issues into the log message. This 
    might be `Closes`, for example, and will format to something like `Closes #1234`. With any of the predefined 
    keywords in place, Github can close a mentioned issue automatically 
    ([see Github keywords documentation](https://help.github.com/articles/closing-issues-using-keywords/)). However, 
    the keyword is completely customizable. If you don't want to use Github's auto-close feature every time, simply 
    choose `<None>` or - after closing the Options dialog - edit the `Parameters` field `keyword=` manually to whatever 
    you like.  
        ![TH-options](/docs/img/TH-options.png "Options Dialog")
        ![TH-config-keyword](/docs/img/TH-config-keyword.png "Keyword parameter")
    1. **Reference Full Repository Name** enables the extended notation when inserting the issue number into the log 
    message. This is needed if the repository you are pushing to is not on the same repository as the issues or pull 
    requests you are referencing.  
    This means: Github parses the short notation `#1234` assuming the current repository. 
    This is suitable for your own repositories. However, for forks you may want to use the issues or pull requests of 
    the original/upstream repository instead. For this to work, Github needs the full reference in the 
    form `another-owner/external-repository#1234` 
    ([see Github auto link documentation](
    https://help.github.com/articles/autolinked-references-and-urls/#issues-and-pull-requests)
    ). This extended notation works always, while the simple notation should only be used if you are sure you are in 
    the same repository as the issues or pull requests you are referencing.  
    Please note, as you usually only have read rights on external repositories, you might want to remove 
    or replace the `Closes` (or equivalent) keyword. (see previous screenshot).  
        ![TH-options-external-repo](/docs/img/TH-options-external-repo.png "Referencing whole Repository")
    1. If **Show Pull Requests by Default** is *disabled*, TurtleHub will by default only show issues from the 
    tracker without any pull requests. However, in the issue picker you can still let them show up manually when 
    needed.  
    If this flag is *enabled*, TurtleHub will **also** show pull requests alongside issues. You still can hide them 
    manually in the picker, though.

# b) BugTraq Integration
## Steps: 
1. **Optionally:** If you do this the first time, you probably want to setup the *Local Integration* from above first. 
This is **only** needed to retrieve the setup parameters for this method. Afterwards you can remove the *Local 
Integration* again.
1. On your local repository that you want to setup, open the context menu and go to  
`TortoiseGit > Settings > Issue Tracker Config` 
1. **If you had set up the *Local Integration* in the first step:**  
In the `Effective` scope, check the `IBugTraqProvider` 
frame near the bottom. You should see your current values for the TurtleHub's UUID and parameters. Copy the parameters.
1. Change the scope to your desired scope, e.g. `Project`. In the `IBugTraqProvider` frame near the bottom, remove the 
`inherit` checkboxes and insert the 
UUIDs (a.k.a. GUIDs) and parameters for TurtleHub (e.g. from the previous step).
    1. The 32-bit UUID is: `{B2C6EC0F-8742-4792-9FDC-10635D2C118C}`
    1. The 64-bit UUID is: `{B2C6EC0F-8742-4792-9FDC-10635D2C118B}`
    1. Feel free to add both UUIDs if you want to share your config.  
    ![TH-BugTraq-Plugin](/docs/img/TH-BugTraq-Plugin.png "TurtleHub in BugTraq")
1. Now you can delete your *Local Integration* entry from `Issue Tracker Integration` if you had created one.
1. You can share/commit your BugTraq config. On `Project` scope this is in the `.tgitconfig` of the repository root. 
`Local` scope saves to `.git/config`.
For an example, see [our own BugTraq config](../.tgitconfig): 
  ```
  [bugtraq]
    ### TurtleHub specific config
    provideruuid = {B2C6EC0F-8742-4792-9FDC-10635D2C118C}
    provideruuid64 = {B2C6EC0F-8742-4792-9FDC-10635D2C118B}
    providerparams = "owner=dail8859;repository=TurtleHub;keyword=<None>;reffullrepo=True;showprsbydefault=True"
  ```


## Further BugTraq configuration
If you look into the example file above or at the screenshots, you can see further BugTraq configurations. 
These are independent from TurtleHub. For more information about what they are and how to use them, use the `Help` 
button at the bottom:  
![TH-BugTraq-general](/docs/img/TH-BugTraq-general.png "General BugTraq config")  
Their main purpose is to provide links on issue numbers when browsing log messages in TortoiseGit. They'll link to the 
issue's URL on the tracker:  
![TH-BugTraq-link](/docs/img/TH-BugTraq-link.png "Example of BugTraq issue linking")  
They are also responsible to provide a simple issue input field for a setup without TurtleHub:  
![TH-no-TH-fallback](/docs/img/TH-no-TH-fallback.png "BugTraq issue insertion without TurtleHub")

### Enable Issue Linking
* Make sure `bugtraq.url` refers to the tracker URL you want to refer to and add `%BUGID` in place of where the 
issue number goes. e.g.
`https://github.com/dail8859/TurtleHub/issues/%BUGID%`
* Feel free to use this RegEx for `bugtraq.logregex`:  
    ```regex
    (?:\S+\/\S+)?#(\d+)
    ```
    It will link against the short and extended Github issue notations.
  ```
  [bugtraq]
    ### general BugTraq config ###

    # Link issue numbers that are found in log browser messages to this URL:
    url = https://github.com/dail8859/TurtleHub/issues/%BUGID%
    
    # Find issue numbers in log browser messages according to this regex:
    logregex = "(?:\\S+\\/\\S+)?#(\\d+)"
  ```
### Fallback for users without TurtleHub
* Further properties are ignored if TurtleHub is set up and running. However, **if the IBugTraqProvider properties are 
not set (or commented out)**, an 
edit box appears at the top right hand side instead. There, issue numbers can be inserted manually. Check TortoiseGit's 
*Help* documentation for further information.  
  ```
  [bugtraq]
    ### Fallback! For users without TurtleHub: 
    # Either comment out the 'provider*' properties above!
    # Or overwrite on local scope with empty fields.
    # An input box will appear instead. It's content will be inserted with this syntax:
    message = "dail8859/TurtleHub#%BUGID%"
    append = true
    
    # Label shown next to the input field if no provider plugin is present:
    label = "Issue/Pull Request #"
    
    # The typed issue / pull request IDs must be numbers:
    number = true
  ```
* To easily let users use this fallback locally without the need to change a `Project` scope config, see the next 
section.

### General BugTraq config
* If `bugtraq.warnifnoissue` is enabled, it checks your current commit for issue numbers and warns you if you don't 
provide one when committing. It checks messages according to the RegEx of the `bugtraq.logregex` property. If not 
present it checks against `bugtraq.message`.
  ```
  [bugtraq]
    ### general BugTraq config ###

    # Enable or disable warning, if no issue number is provided upon commit:
    warnifnoissue = false
  ```
* All properties can be overwritten on a lower scope, without changing the original higher-order config file. For 
example: If the `Project` scope's `.tgitconfig` file declares the following properties:  
    ![TH-inherit-project](/docs/img/TH-inherit-project.png "BugTraq properties on project scope")      
    They can be easily overwritten locally:      
    ![TH-inherit-local-overwrite](/docs/img/TH-inherit-local-overwrite.png 
    "Overwrite BugTraq properties on local scope")  
    This way users without TurtleHub can disable the IBugTraqProvider properties to enable the edit field fallback. 
    Or someone might also change the Keyword or the warning or other properties. This way a personal config can be 
    achieved without the need to change the `Project` scope config.
