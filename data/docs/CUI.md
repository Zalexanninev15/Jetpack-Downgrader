# Downgrader CUI [jpd.exe]

*Actions are performed in the **files** folder*

## CUI features

* As simple as possible interaction (default). Specify the folder and answer the questions - you can easily get version 1.0 with the necessary settings
* The ability to fine-tune the downgrading process, the *Jetpack Downgrader* itself, and some other aspects
* Using delta patches that weigh significantly less than the files from the game version 1.0
* Smart checking for different versions and other aspects that will help you perform downgrades, in many cases, successfully
* Easy integration of the application as a separate component in your project
* Display process of downgrade and maximum speed of operations

## Usage and configuration

1. Run **jpd.exe** and select the folder with the game (if **SelectFolder=true**)
2. Answer the questions (if **UseMsg=true**) and wait end of work...

## Settings in jpd.ini

In **jpd.ini**, you can specify the settings for this application, it is not recommended to change anything if you don't know exactly what each setting is responsible for and what its values and results are!!! It is important to remember that settings are always more important than the answer you have chosen (if **UseMsg=true**). You can also open the settings by pressing **F4** in the GUI

### Downgrader

* **CreateBackups** - creating backups (**[full file name] + .bak**) of original files before downgrading them. Game can be returned and restored from the settings of the downgrader in GUI, it can also be activated by a keyboard shortcut **Ctrl + Z**
* **CreateShortcut** - creating a shortcut to a file **gta_sa.exe** on the desktop (only after a successful downgrade)
* **ResetGame** - remove the game configuration file **gta_sa.set** for prevents crash (after getting game version)
* **GarbageCleaning** - remove unneeded files (**index.bin** and **MTLX.dll**) from the Rockstar Games Launcher version of the game (only after a successful downgrade). These files are not used in the game version 1.0
* **RegisterGamePath** - add entries to the registry so that the game is better recognized by mods and programs (e.g.: launchers, SAMP and other projects) (only after a successful downgrade). This function is also available in GUI (downgrader settings) separately from the downgrader itself, it can also be called using a keyboard shortcut **Ctrl + Y**
* **CreateNewGamePath** - make a copy of the game folder (**[folder name] + _Downgraded**) to prevent accidental updates to the game (after MD5-scan). It is highly recommended to enable it for the Steam/Rockstar Games Launcher versions of the game!!!
* **Forced** - force the app to continue running even if EXE file are already use from the game version 1.0 (MD5). Downgrader will only work with the EXE file, the rest of the game files will just be checked, so they must be original. For non-1.0 versions, only the EXE file will be patched
* **EnableDirectPlay** - enable the Windows DirectPlay component to run the game on Windows 10 (after getting game version)
* **InstallDirectXComponents** - install additional DirectX files to run the game (after getting game version)

### JPD

* **SelectFolder** - using UI for select the folder with the game (path using **Copmonent=true**, overwritten to a new one)
* **ConsoleTransparency** - use transparency for console
* **UseMsg** - use Windows message boxes to notify you when you select an action, as well as to show a positive result of the application's performance. If the selection dialog appears, the selection will be placed second to the settings: **CreateBackups**, **CreateShortcut**, **ResetGame**, **GarbageCleaning**, **RegisterGamePath**, **CreateNewGamePath**, **Forced**, **EnableDirectPlay**, **InstallDirectX** 
* **UseProgressBar** - use the progress bar to indicate the progress of each of the downgrader stages. **During operation, all logs of the current stage of work are ignored!!!**
* **Component** - this setting is **only** necessary if you decide to use *Jetpack Downgrader* in your projects (this setting will help you skip the UI (in console) and removes the waiting (if in the processes will not be found **app.exe**) for input after the processes in the app are completed). This setting adds the ability to specify the path to the game folder as a parameter for launching the application

**Example for CMD**

  ```shell
jpd "E:\Games\Grand Theft Auto San Andreas"
  ```

### Only

* **GameVersion** - get only the game version (can be used with the settings **ResetGame**, **EnableDirectPlay**, **InstallDirectX**)
* **NextCheckFiles** - get only the game version and check files (only those that are used for downgrading)
* **NextCheckFilesAndCheckMD5** - the same as **NextCheckFiles**, but it also scan files by MD5 and reveals which files are used from the game version 1.0 (information will only be visible if **UseProgressBar=false**)