# SA Downgrader RW2| [Download](https://github.com/Zalexanninev15/SADRW2/releases/latest)
![alt](https://image.jimcdn.com/app/cms/image/transf/dimension=117x10000:format=png/path/s876f79fd6a5f4193/image/i1971da86cd486af0/version/1610909548/image.png)
## Description
Application for downgrading the version of the game Grand Theft Auto: San Andreas from Rockstar Games to version 1.0

## Versions of GTA SA

* 1.01 [exe only]
* 2.0 [supported]
* Steam [fully supported]
* RGL [fully supported]

## Configuration

1. To a file **path.txt** you must specify the path to the game. For example: D:\TEST\STEAM_SA
2. Download [cache.exe](https://drive.google.com/file/d/11zLQ_HKTXjQzsiJUN7yecGJGXymhgIy0/view) and copy this file to SADRW2 folder. Run this file and wait...

In **config.ini**, you can specify some parameters for the app, it is not recommended to change something if you do not know what each variable is responsible for and what are the values

### Settings in config.ini

#### [Downgrader]

* **CreateBackups** - creating backups (full file name + .bak) of files before downgrading them (recommended)
* **SetReadOnly** - set the "Read-only" attribute for each file after downgrade (recommended for Steam and RGL versions)
* **CreateShortcut** - creating a shortcut to a file **gta_sa.exe** on the desktop (only after successfully downgrading the game version)
* **ResetGame** - deleting the game configuration file - **gta_sa.set** (after getting the game version)

#### [SADRW2]

* **Component** - this setting is **only** necessary if you decide to use SADRW2 in your projects (the setting will help you skip the "user-interface" and remove the waiting for input after the end of the application). This setting disables access to the file **path.txt** and adds the ability to specify, as a parameter for running the EXE application, the path to the folder with the game. For example: start sadrw2.exe D:\TEST\STEAM_SA
* **SelectFolderUI** - using GUI for select the folder with the game (path using **path.txt** or **Copmonent=true** are overwritten to the new)

#### [Only]

* **GameVersion** - get only the game version (can be used with the setting **ResetGame**)
* **NextCheckFiles** - get only the game version and check files (only those that are used for downgrading)
* **NextCheckFilesAndCheckMD5** - the same as **NextCheckFiles**, but it also scans files by MD5 and reveals which files are copied from the game version 1.0

## Build
Compile using [SharpDevelop](https://sourceforge.net/projects/sharpdevelop/)