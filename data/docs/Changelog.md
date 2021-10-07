# Changelog of Jetpack Downgrader

## [Release] Jetpack Downgrader Version 2.3.0.1

**This patch fixes the loading of the list of modifications. The error during loading is associated with an increase in the amount of modification information. In the patch, the number of possible modifications has been increased from 10 to 30. Appearance has also been improved, in some moments, and other minor edits have been made**

## [Release] Jetpack Downgrader Version 2.3

### In the next stable version, the downgrader will be moved to the GUI, and localization support for downgrader strings will also be added!

### 👉 GUI Version 1.3.1

- Most of the functions that I often use in my projects have moved to my [VitNX](https://github.com/Zalexanninev15/VitNX) library. This change made it possible to remove unnecessary code and optimize the existing one. This library also replaced the DarkUI library, because it has the necessary controls for WinForms
- Added Screenshot Viewer (more convenient) for modifications
- Added checking for updates (when launching the application)
- Added a new type of dialog with a question icon for questions
- Added a check for Windows < 8.1, now the checkbox with DirectPlay will not be displayed for Windows below this one. Now this possibility is available without downgrade (optional)
- Added a [full list of versions changes](https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/Changelog.md) (in repository)
- Localizations files have been seriously improved
- For versions != 1.0 the "Forced" checkbox is no longer displayed. If the version == 1.0, but does not have the checkbox set, then the downgrade will not be performed (the button will disappear)
- Localizations improvements
- Extra code was removed
- Improved display of the language selection panel
- Format of file for storing localizations has been changed from ZCF to XML
- Minor edits

### 👉 Downgrader Version 1.16-1

- Most of the functions that I often use in my projects have moved to my [VitNX](https://github.com/Zalexanninev15/VitNX) library. This change made it possible to remove unnecessary code and optimize the existing one
- Changed the icon so that it is not confused with the icon for the GUI
- Minor edits

## [Release] Jetpack Downgrader Version 2.2

### Meet version 2.2! [A lot of changes have been made](https://github.com/Zalexanninev15/Jetpack-Downgrader/commit/5a54deb5efc9bd33889acb01e9246f5838c907be), so the update is necessary for everyone! I recommend a clean unpacking, without updating the previous version

### 👉 GUI Version 1.2.3.3

- Added the About window
- The system of settings and localizations has been rewritten. Files and code have become much easier and more convenient to edit and read
- A huge number of edits have been made
- The GitHub icon has been replaced with a help icon (analogous to pressing the **F1** key)
- Improved optimization for x64
- Updated localizations and:  
  ~ Added more translatable strings  
  ~ Added a translation display from the selected localization (before its application) for the localization selection window when the application is first launched  
  ~ Removed outdated dependencies
- Fixed the problem with false antivirus results (possibly temporarily)
- Fixed a bug when rolling back the game version 1.0, if the backup file is equal in hash to the used file version 1.0
- Fixed a bug displaying the window size with the choice of language when the application is first launched
- Fixed a bug with the language selection panel when downloading files or after the Downgrader is finished
- Removed unnecessary and outdated code

### 👉 Downgrader Version 1.15.0.2

- The system of settings has been rewritten. Files and code have become much easier and more convenient to edit and read
- Improved user interaction
- Improved startup optimization
- Minor improvements
- Fixed the problem with false antivirus results
- Removed unnecessary and outdated code + removed outdated settings (if you are going to use the Downgrader separately from the GUI, the [documentation for deleted functions](https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/DowngraderSettingsOld.md) is left unchanged)

## [Release] Jetpack Downgrader Version 2.1

- Added a button to close the application after the end of the downgrade
- Algorithm for storing settings has been changed. Now the folder is not clogged **C:\Users%username%\AppData\Local**. Check the path **C:\Users%username%\AppData\Local\Zalexanninev15** and delete this folder
- Functionality of showing progress on the taskbar in Windows has been completely rewritten. Now additional libraries are not required (which has slightly reduced the size of the overall project release), the native WinAPI is used instead
- Updated localizations + fixed a bug with the downgrade start button in the Russian localization
- Improving interaction with GUI
- General improvements and edits

## [Release] Jetpack Downgrader Version 2.0

### As promised, here is a new version, this time with a graphical interface and a lot of new features. It took a really long time (2.5 months), but now I'm ready to present you a new version! 🎉 🥂 🥳 | [Official post in VK (RU)](https://vk.com/wall-119683810_15357)

- Added a full-featured graphical interface-launcher with a huge number of settings for the downgrader and the game
- The size of the patches has decreased for the second time! From 884 MB to 517 MB. For the first time, something like this happened in [version 1.10](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/tag/1.10-PublicBeta), in which the patch size changed from 1.75 GB to 884 MB
- The overall process of downgrading the game version has been significantly improved, everything has become faster and more stable. Many actions have been converted to simple ones or removed altogether
- Changed the format for backups of original files (**bak** to **jpb**)
- Added coloring of all downrader messages in different colors to make it easier to highlight the actions performed/performed, as well as errors and just text
- Added the output of a new path to the game after the end of the work
- If file with settings (jpd.ini) is not found, it will be created automatically with the default values
- Renamed 1 setting. Value of 1 setting was changed, which allowed us to fix various kinds of bugs
- Work has been done to improve the code and optimize it for 64-bit systems
- The version calculation system has been changed, which now does not rely on the version of the downgrader. Now each component (GUI and Downgrader) has its own version, and the general version of the components assembly is recorded in the release based on the list of changes
- Folder with DirectX is not deleted, which allows you not to download the installer again when you re-use the downgrader
- Significantly improved the registration of the game in the system
- A huge number of changes and improvements for both users and code and performance
- Fixed all bugs and errors

## [Unstable Release] Jetpack Downgrader Version 2.0-rc2

### Improved version of [Release Candidate 1](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/tag/2.0-rc). Fixed a huge number of errors, bugs and made general improvements to the interface and code. I also added support for modifications in the form of EXE installers. The changes affect many aspects, so I recommend using the new version from scratch. Patches must be downloaded in the GUI itself, there are no separate archives with patches!

## [Unstable Release] Jetpack Downgrader Version 2.0-rc

### As promised, here is a new version, this time with a graphical interface. It took a really long time (2 months), but now I'm ready to present you a new version! 🎉🥂🥳

### [Guide](https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/README.md) for the Release Candidate version

### 👉 Main changes

- Added a full-featured graphical interface-launcher with a huge number of settings for the downgrader and the game
- The size of the patches has decreased for the second time! From 884 MB to 517 MB. For the first time, something like this happened in [version 1.10](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/tag/1.10-PublicBeta), in which the patch size changed from 1.75 GB to 884 MB
- The overall process of downgrading the game version has been significantly improved, everything has become faster and more stable. Many actions have been converted to simple ones or removed altogether
- Changed the format for backups of original files (**bak** to **jpb**)
- Added coloring of all downrader messages in different colors to make it easier to highlight the actions performed/performed, as well as errors and just text
- If file with settings (jpd.ini) is not found, it will be created automatically with the default values
- Renamed 1 setting. Value of 1 setting was changed, which allowed us to fix various kinds of bugs
- Work has been done to improve the code and optimize it for 64-bit systems
- The version calculation system has been changed, which now does not rely on the version of the downgrader. Now each component (GUI and Downgrader) has its own version, and the general version of the components assembly is recorded in the release based on the list of changes

### 👉 Other changes

- Added the output of a new path to the game after the end of the work
- Folder with DirectX is not deleted, which allows you not to download the installer again when you re-use the downgrader
- Significantly improved the registration of the game in the system
- A huge number of changes and improvements for both users and code and performance
- Fixed bugs
- Changed the project name in code

### Do not try to install SA-MP from the section with modifications, now we are working on a fix!

## [Unstable Release] Jetpack Downgrader Version 1.12.4

- Added GUI (Final Alpha version)
- 1 change in the patch file
- Changed the download link to the DirectX installer
- Changed the project name in code

## [Unstable Release] Jetpack Downgrader Version 1.12.2 [DEV 3.2]

- Added text color coloring in the app console depending on the current action
- Added outputs of the path to the game folder
- If file with settings (**jpd.ini**) is not found, it will be created automatically with the default values
- Folder with DirectX is not deleted, which allows you not to download the installer again when you re-use the downgrader
- Code improvements
- Fixed text color detection when launching the app (especially noticeable on a light background)
- Fixed some other bugs

## [Release] Jetpack Downgrader Version 1.11.6

- Downgrader has become more intelligent, now the application does not do what is required of it
- Added 2 new settings, 1 setting is renamed
- Added a reference to the license and link to the repository
- Updated the game folder selection dialog
- Improved removal of game settings
- Optimization and performance improvements
- Minor improvements for other developers (if **Component=true** and **SelectFolder=false**)
- Other improvements

## Jetpack Downgrader Version 1.11.6 [DEV 2 & 2.1]

- Added text color coloring in the app console depending on the current action [BETA]
- Added outputs of the path to the game folder
- If file with settings (jpd.ini) is not found, it will be created automatically with the default values
- Folder with DirectX is not deleted, which allows you not to download the installer again when you re-use the downgrader
- Code improvements
- Fixed text color detection when launching the app (especially noticeable on a light background)
- Fixed some other bugs

## Jetpack Downgrader Version 1.11.6 [DEV 1]

- If file with settings (jpd.ini) is not found, it will be created automatically with the default values
- Folder with DirectX is not deleted, which allows you not to download the installer again when you re-use the downgrader
- Code improvements made

## [Unstable Release] Jetpack Downgrader Version 1.11-Dev 1.5

- Added a reference to the license
- Updated the game folder selection dialog (tested on Windows 10)
- Improved removal of game settings
- Improved code optimization and overall performance
- Minor improvements for other developers (if **Component=true** and **SelectFolder=false**)
- Other improvements

## Jetpack Downgrader Version 1.11-Dev 1.4

- Added 1 new setting (for Windows 10)
- Added a reference to the license
- Added link to the repository
- Updated the game folder selection dialog (tested on Windows 10)
- Improved removal of game settings
- Improved code optimization and overall performance
- Minor improvements for other developers (if Component=true and SelectFolder=false)
- Other improvements

## [Unstable Release] Jetpack Downgrader Version 1.11-Dev 1.3

- Added a reference to the license
- Updated the game folder selection dialog (tested on Windows 10)
- Improved removal of game settings
- Improved code optimization and overall performance
- Minor improvements for other developers (if **Component=true** and **SelectFolder=false**)
- Other improvements

## Jetpack Downgrader Version 1.11-Dev 1.2

- Added a reference to the license
- Improved removal of game settings
- Minor improvements for other developers (if Component=true and SelectFolder=false)
- Other improvements

## Jetpack Downgrader Version 1.11-Dev 1.1

- Added a reference to the license
- Minor improvements for other developers (if Component=true and SelectFolder=false)
- Other improvements

## Jetpack Downgrader Version 1.11-Dev 1.0

- Minor improvements for other developers (if Component=true and SelectFolder=false)

## [Release] Jetpack Downgrader Version 1.10.1-Beta

- Now you don't need to download archive with patches, you can only download the updated application and the main files
- Improved the definition of game version 1.0
- Minor edits
- Removed unnecessary dependencies
- Removed unused code

## [Release] Jetpack Downgrader Version 1.10-Beta

**This version is based on [this](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/tag/1.10-PublicBeta) version, but does not have a Russian-language text. Some edits were also made.**

## [Release] Jetpack Downgrader Version 1.10-PublicBeta

What changed after the first public beta https://vk.com/wall-119683810_14645 ([from 31.01.2021](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/tag/1.0-PublicBeta)):

- The project changed its name from **SADRW2** (**SA Downgrader RW2**) to **Jetpack Downgrader**
- Added patch support, which reduced the size of additional files from 1.75 GB to 884 MB
- Settings:
  a) Added 3 new ones
  b) Removed 1 old one
  c) Slightly changed the functions that are performed by some settings
- Added more information about the project
- Added many new checks, which will definitely increase the stability of the application
- Greatly improved the user experience of the description in the application log
- Improved the algorithm for determining the game version
- Significantly improved description of settings
- Improved optimization
- Improved support for sandboxes and virtual machines
- Other changes and improvements
- Fixed a huge number of bugs and bugs, but they may still be present

## [Unstable Release] Jetpack Downgrader Version 1.9-Beta

- Added 1 new setting
- Added a check for the presence of **patcher.exe**
- Significantly improved description of settings
- Significantly improved user experience
- Slightly improved optimization
- Fixed a lot of bugs

## Jetpack Downgrader Version 1.9 [DEV 1]

- Added 1 new setting, removed (hidden) 1 old setting
- Added new descriptions for different features
- Improve communication
- Improved support for version 1.0 (from version 1.8.1 [DEV)
- Fixed bugs (from version 1.8.1 [DEV)

## Jetpack Downgrader Version 1.8.1 [DEV]

- Improved support for version 1.0
- Fixed bugs

## [Unstable Release] Jetpack Downgrader Version 1.8-Beta

- Added 2 new settings, returned 1 old setting (was removed in version 1.5-Beta)
- Added support for Windows styles
- Significant improvements to the downgrader, which should now do its job much faster
- Significantly improved user experience
- Improved description of some things
- Improved optimization
- Removed a lot of unnecessary code
- Removed the mention to the "orange subject"

## [Unstable Release] Jetpack Downgrader Version 1.5-Beta

- The project's name changed to **Jetpack Downgrader**
- Added full support for delta patches to apply them to different versions of the game
- Code optimizations
- Minor improvements to the console interface
- End of file support **path.txt**
- Changed the names of some settings, removed 1 setting
- Fixed bugs
- Other changes and improvements

## SADRW2 Version 1.5-Dev-2

- Significant improvements to the downgrader, the method of operation is fully functional, but further integration of the method into the downgrader is still on pause
- Full renaming of the downgrader
- End of file support path.txt
- Minor edits

## SADRW2 Version 1.5-Dev 1

- Downgrader [CF] updated to version 2.0-Alpha (no patches, but work is underway)
- Minor edits

## SADRW2 Version 1.4-Dev

- The project's name changed to Jetpack Downgrader
- Work on the delta patches continues, I added the SHA1 hash for all files and rebuilt the cache (not available, until the Beta version is released)
- Other changes

## SADRW2 1.3.0.1-Dev

- Downgrade updated to version 1.3.0.1-Dev
- Work on the graphical interface has begun!

## [Release] SADRW2 Version 1.0-PublicBeta

### This version is based on version 1.3-Beta, but has several changes:

- Changed the way to submit information about the app
- Fixed code interaction issues

### Post: [Больше месяца назад я написал пост о том, что.. | Vadim M [Официальный паблик] | ВКонтакте](https://vk.com/wall-119683810_14645)

**P.S. All information in the archive is prepared for Russian-speaking users [[Russian FAQ](https://telegra.ph/SA-Downgrader-RW2-01-31)]. I attached a cache to the release, just in case**

## [Unstable Release] SADRW2 Version 1.3-Beta

- Added 2 new settings
- Improved performance and optimization
- Reduced the number of files in the cache
- RGL → Rockstar Games Launcher
- Minor improvements

## [Unstable Release] SADRW2 Version 1.2-Beta

- Added new setting
- Changed the name of the application's EXE file
- Significantly improved and fixed the work with the Steam version of the game
- Improved cache handling, there have been some changes
- Improved messages
- Removed unnecessary message
- I continue to work with xdelta patches

## SADRW2 Version 1.2-Dev 2

- Added a check to avoid adding unnecessary branches to the Windows registry
- Improved messages
- Fixed working with the Steam version of the game
- Removed unnecessary message

## SADRW2 Version 1.2-Dev 1

- Added new setting
- Minor edits to messages
- I continue to work with xdelta patches

## SADRW2 Version 1.2-Dev 0.2

- Changed messages when creating backups of files and EXE files
- Improved cache handling, now using 1 less file
- Standard files have been added to the repository: config.in and path.txt
- Removed 2 files from the repository

## SADRW2 Version 1.2-Dev 0.1

- Minor edits to error messages
- Improved documentation (for version 1.1-Beta)

## [Unstable Release] SADRW2 Version 1.1-Beta

- Added 3 new settings
- Added support for the game version 1.01 (so for yourself, but maybe someone will need it)
- Added additional file copying to ensure compatibility with Steam and launch using the "old things"
- Added a lot more information about different things
- Changed the logic for working with the Steam version of the game
- Changed the logic of some components
- Now the app always has its own version
- Cache updated
- Improved the work of creating backups
- Improved backup creation for EXE files
- Improved code performance
- Improved work with UAC
- Improved error messages
- Fixed possible errors

## [Unstable Release] SADRW2 Version 1.0-Beta

- Added downgrader (completely redesigned concept, temporarily)
- Added MD5 scanning for downgraded files
- Added 3 more settings for special enthusiasts
- Added a description of the app settings
- Rewritten MD5 file scanning
- Improved display of errors
- Fixed errors descriptions
- Fixed caches
- Other changes

## SADRW2 Version 0.2 [DEV_Update_1]

- I started developing a downgrader, the basis
- Improved the backup function

## SADRW2 Version 0.2 [DEV]

- Added basic backup features
- Added new messages
- Added the first sketches for the patch file (so far, only SDK files)

## SADRW2 Version 0.1.4.2

- Improved the operation of the application as a component, wrote a new instruction for getting the path to the game files
- Minor changes

## SADRW2 Version 0.1.4.1

- Added an icon for the app
- Changed the title text of the console

## SADRW2 Version 0.1.4

- Added checks for game files from version 1.0
- Added information about the project
- Added a new parameter "Install Crack"
- General code optimization was performed
- Removed code that will no longer be used, although it was planned otherwise
- Visual Studio is now used for development, but the files remain compatible with SharpDevelop

## Preparing to SADRW2 Version 0.1.4

- Added the most basic checks (MD5)
- Removed MD5 for crack version 2.0

## SADRW2 Version 0.1.3.6

- Added a co-author
- Improved support for game versions

## SADRW2 Version 0.1.3.5

- Added a check for the game version 1.0

## SADRW2 Version 0.1.3.4

- Added a check for the presence of the game directory
- Improved the display of information in the log

## SADRW2 Version 0.1.3.3

- Added support for other versions of the game version 2.0

## SADRW2 Version 0.1.3.2

- Fixed working with game version 2.0

## SADRW2 Version 0.1.3.1

- Changed the way the log is displayed
- Removed unnecessary settings

## SADRW2 Version 0.1.3

- Added checker for game files [BETA]

## SADRW2 Version 0.1 Dev-2

- Fixed version detection errors
- Added an example of interaction with the utility
- Compiled version added

## SADRW2 Version 0.1 Dev-1

- The first version in the repository. In theory, it can only determine the version of the game.
