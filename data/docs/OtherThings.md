# Other things

*Actions are performed in the **files** folder*

## Downgrader

### Usage

```shell
jpd "E:\Games\Grand Theft Auto San Andreas"
```

### Features

* As simple as possible interaction (default). Specify the folder and answer the questions - you can easily get version 1.0 with the necessary settings
* The ability to fine-tune the downgrading process, the *Jetpack Downgrader* itself, and some other aspects
* Using delta patches that weigh significantly less than the files from the game version 1.0
* Smart checking for different versions and other aspects that will help you perform downgrades, in many cases, successfully
* Easy integration of the application as a separate component in your project
* Display process of downgrade and maximum speed of operations

### Settings in downgrader.xml

In **downgrader.xml**, you can specify the settings for the Downgrader. You can also open the settings by pressing **F4** in the GUI. If you don't see this file, it means that the current settings of the Downgrader are set by default (try to change something in the GUI and you will see the file)

#### All settings

* **CreateBackups** - creating backups (**[full file name] + .bak**) of original files before downgrading them. Game can be returned and restored from the settings of the downgrader in GUI, it can also be activated by a keyboard shortcut **Ctrl + Z**
* **CreateShortcut** - creating a shortcut to a file **gta_sa.exe** on the desktop (only after a successful downgrade)
* **ResetGame** - remove the game configuration file **gta_sa.set** for prevents crash
* **RGL_GarbageCleaning** - remove unneeded files (**index.bin** and **MTLX.dll**) from the Rockstar Games Launcher version of the game (only after a successful downgrade). These files are not used in the game version 1.0
* **RegisterGamePath** - add entries to the registry so that the game is better recognized by mods and programs (e.g.: launchers, SAMP and other projects) (only after a successful downgrade). This function is also available in the GUI ("Downgrader settings") separately from the downgrader itself, it can also be called using a keyboard shortcut **Ctrl + Y**
* **CopyGameToNewPath** - make a copy of the game folder (**[folder name] + _Downgraded**) to prevent accidental updates to the game (after MD5-scan). It is highly recommended to enable it for the Steam/Rockstar Games Launcher versions of the game!!!
* **EnableDirectPlay** - enable the Windows DirectPlay component to run the game on Windows 10
* **InstallDirectXComponents** - install additional DirectX files to run the game
* **Forced** - force the app to continue running even if EXE file are already use from the game version 1.0 (MD5). Downgrader will only work with the EXE file, the rest of the game files will just be checked, so they must be original. For non-1.0 versions, only the EXE file will be patched
* **UserMode** - using the progress bar to display the progress of operations, without a full log of all actions

### Old versions of Downgrader

Information about the old versions (1.14 and below) of the Downgrader is [here](https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/DowngraderSettingsOld.md)

## GUI

### Settings in gui.xml

If you don't see this file, it means that the current settings of the Downgrader are set by default (try to change language and you will see the file)

#### All settings

* **LanguageCode** - code of the currently used language (code is taken from the name of the file in the **languages** folder - **[language code].xml**)
* **FirstLaunch** - is this the first GUI launch
