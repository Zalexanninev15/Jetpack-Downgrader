# SA Downgrader RW2 | [Download](https://github.com/Zalexanninev15/SADRW2/releases/tag/1.0-PublicBeta)
![alt](https://image.jimcdn.com/app/cms/image/transf/dimension=117x10000:format=png/path/s876f79fd6a5f4193/image/i1971da86cd486af0/version/1610909548/image.png)
## Description
App for downgrading the version of the game Grand Theft Auto: San Andreas from Rockstar Games to version 1.0

## Versions of GTA SA

* 1.01 [exe only]
* 2.0 [fully supported]
* Steam [fully supported]
* Rockstar Games Launcher [fully supported]

## Usage and configuration

1. Download [cache.exe](https://drive.google.com/file/d/1HSojn8KnocnrdmO7Ius8OIDieA3SZlft/view) and copy this file to SADRW2 folder. Run this file and wait...
2. Run **sadrw2.exe** and select the folder with the game (if **SelectFolderUI=true**)
3. Wait for the end of the work...

### Settings in config.ini

##### !!!THE SETTINGS CAN BE CHANGED BEFORE LAUNCHING THE APP!!!

In **config.ini**, you can specify the settings for this application, it is not recommended to change anything if you don't know exactly what each setting is responsible for and what its values and results are


#### [Downgrader]

* **CreateBackups** - creating backups (full file name + .bak) of files before downgrading them (recommended)
* **SetReadOnly** - set the "Read-only" attribute for each file after downgrade
* **CreateShortcut** - creating a shortcut to a file **gta_sa.exe** on the desktop (only after a successful downgrade)
* **ResetGame** - deleting the game configuration file - **gta_sa.set** (after getting the game version)
* **GamePath** - add entries to the registry so that the game is better recognized by mods and programs
* **CopyGame** - make a copy of the game folder to prevent accidental updates to the game (after MD5-scan)
* **Forced** - force the app to continue running even if some non-EXE files are already use from the game version 1.0 (MD5)

#### [SADRW2]

* **Component** - this setting is **only** necessary if you decide to use SA Downgrader RW2 in your projects (this setting will help you skip the UI and removes the waiting for input after the processes in the app are completed). This setting disables access to the file **path.txt** and adds the ability to specify, as a parameter for running the EXE application, the path to the folder with the game. 

 Example for cmd: 

  ```shell
  start sadrw2 "E:\Games\Grand Theft Auto San Andreas"
  ```

* **SelectFolderUI** - using UI for select the folder with the game (path using **path.txt** or **Copmonent=true**, overwritten to a new one)

#### [Only]

* **GameVersion** - get only the game version (can be used with the setting **ResetGame**)
* **NextCheckFiles** - get only the game version and check files (only those that are used for downgrading)
* **NextCheckFilesAndCheckMD5** - the same as **NextCheckFiles**, but it also scan files by MD5 and reveals which files are used from the game version 1.0

### path.txt

##### !!!THIS FILE CAN BE CHANGED BEFORE LAUNCHING THE APP!!!

To a file **path.txt** you must specify the path to the game. For example: D:\TEST\STEAM_SA