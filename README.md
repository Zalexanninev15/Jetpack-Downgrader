# Jetpack Downgrader | [Latest release](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/latest) | [Topic](https://gtaforums.com/topic/969056-jetpack-downgrader)

![alt](https://github.com/Zalexanninev15/Jetpack-Downgrader/raw/unstable/data/docs/logo.png)

[![](https://img.shields.io/badge/OS-Windows-informational?logo=windows)](https://github.com/Zalexanninev15/Jetpack-Downgrader)
[![](https://img.shields.io/github/release/Zalexanninev15/Jetpack-Downgrader)](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/latest)
[![](https://img.shields.io/github/release-pre/Zalexanninev15/Jetpack-Downgrader)](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases)
[![](https://img.shields.io/github/downloads/Zalexanninev15/Jetpack-Downgrader/total.svg)](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases)
[![](https://img.shields.io/github/last-commit/Zalexanninev15/Jetpack-Downgrader/unstable.svg)](https://github.com/Zalexanninev15/Jetpack-Downgrader/commits/unstable)
[![](https://img.shields.io/github/stars/Zalexanninev15/Jetpack-Downgrader.svg)](https://github.com/Zalexanninev15/Jetpack-Downgrader/stargazers)
[![](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## Screenshot

![alt](https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/gui.png?raw=true)

## Description

App for downgrading the game Grand Theft Auto: San Andreas to version 1.0

## System requirements

* **OS:** Windows 7/8/8.1/10
* **Additional:** .NET Framework 4.8

## Authors

* Zalexanninev15 - programmer and creator [![](https://img.shields.io/badge/donate-QIWI-FF8C00.svg)](https://qiwi.com/n/ZALEXANNINEV15) [![](https://img.shields.io/badge/donate-YooMoney-8B3FFD.svg)](https://yoomoney.ru/to/410015106319420)
* Vadim M. - consultant [![](https://img.shields.io/badge/donate-Patreon-FF424D.svg)](https://www.patreon.com/NationalPepper)

## Features

* Easy to use and lots of additional downgrade settings
* The ability to fine-tune the downgrading process, the *Jetpack Downgrader* itself, and some other aspects
* Using patches that weigh significantly less than the files from the game version 1.0
* Support for installing modifications after downgrading the game version
* Smart checking for different versions and other aspects that will help you perform downgrades, in many cases, successfully
* Display process of downgrade and maximum speed of operations
* Support for localizations to different languages (GUI only)
* Support for hotkeys (**Ctrl + O**, **Ctrl + Y**, **Ctrl + Z**, **Ctrl + S**, **F12**, **F4**, **F1**)

## Supported versions

* 1.0 [exe only] (if forced)
* 1.01 [exe only] 
* 2.0 [fully supported]
* Steam [fully supported]
* Rockstar Games Launcher [fully supported]

**Only licensed versions of the game are supported, work on pirated builds is not guaranteed and will never be added!!!**

## Usage

*You can also open this document by pressing **F1***

1. Launch **app.exe**
2. Select language using corresponding icon (to left of the project logo). *At the moment, list of languages is very limited. You can create a localization in your language and request it to be added to the repository, after which it will be added in the next update*
3. Select the first stage and select the path to game folder (or press **Ctrl + O**) and necessary settings (the recommended ones are already checked)
4. If necessary, select the second stage. Select the desired modification (correction and improvement of the game) using the drop-down list (the list below is a list of all available modifications from the repository, for more convenient display). After selecting a modification, select the checkbox and wait for downloading the cache with mod's files
5. Select stage 3 (or press **Ctrl + S**). The console will appear and downgrade process will start... If we can't find the patch files, you'll need to download them first (that's why step 3 is called "Download patches"), only then the button with the title "Downgrade" will appear
6. After the end, make sure that there are no errors in the console, this downgrade of the game is completed
7. If you have selected the modifications, you still need to wait until they are fully unpacked
8. After all operations are completed, a will be displayed a success message (although it may be displayed even if an error occurred somewhere, so start the game and make sure that everything was successful)
9. If there are problems, press the **F12** key and start the downgrade process again

## Links

* If you want to use more powerful settings for the game's downgrade/downgrader setup, then you need to read the [help about CUI settings](https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/CUI.md) about it
* [Add new data (localization(s) or modification(s))](https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/AddNewData.md)

## Used libraries

* [DarkUI](https://github.com/Zalexanninev15/DarkUI) ([MIT License](https://github.com/RobinPerris/DarkUI/blob/master/LICENSE))
* [MegaApiClient](https://github.com/gpailler/MegaApiClient) ([MIT License](https://github.com/gpailler/MegaApiClient/blob/master/LICENSE))
* [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json) ([MIT License](https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md))
* [Microsoft.WindowsAPICodePack.Shell](https://www.nuget.org/packages/Microsoft-WindowsAPICodePack-Shell) ([MICROSOFT SOFTWARE LICENSE TERMS](https://github.com/contre/Windows-API-Code-Pack-1.1/blob/master/LICENSE))
* [Microsoft-WindowsAPICodePack-Core](https://github.com/contre/Windows-API-Code-Pack-1.1) ([MICROSOFT SOFTWARE LICENSE TERMS](https://github.com/contre/Windows-API-Code-Pack-1.1/blob/master/LICENSE))

## Build

Open solution *Jetpack Downgrader* and compile all projects using [Visual Studio 2019](https://visualstudio.microsoft.com/vs). You also need to manually or from NuGet restore/install [*MegaApiClient*](https://www.nuget.org/packages/MegaApiClient) (with [*Newtonsoft.Json*](https://www.nuget.org/packages/Newtonsoft.Json)) and [*Microsoft.WindowsAPICodePack.Shell*](https://www.nuget.org/packages/Microsoft-WindowsAPICodePack-Shell) (with [*Microsoft-WindowsAPICodePack-Core*](https://www.nuget.org/packages/Microsoft-WindowsAPICodePack-Core)) package and manually compile my [fork](https://github.com/Zalexanninev15/DarkUI) of the [DarkUI](https://github.com/RobinPerris/DarkUI) library and attach to the GUI project.