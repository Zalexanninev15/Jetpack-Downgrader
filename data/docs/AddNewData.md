# Add new data

*Adding localizations and modifications*

## Add new localization(s)

1. Download the [latest available version](https://github.com/Zalexanninev15/Jetpack-Downgrader/releases/latest)
2. Unzip the archive and go to the **files** folder, then to the **languages** folder
3. As a source for translation, take the official and always timely updated English localization from **EN.zcf** open this file in text editor (as [Notepad++](https://notepad-plus-plus.org/))
4. Copy the file to any location and translate only the values of variables. *The value of the **Language** variable must be written in English!* If some things are unclear, that is, the source code, do not hesitate to peek into it ;)
5. Save your translation under the name of the language code, but without the dash ([list of all codes](https://htmlpreview.github.io/?https://github.com/Zalexanninev15/Jetpack-Downgrader/blob/unstable/data/docs/Language%20Code%20Table.html))
6. Copy the file to the **languages** folder and open the GUI. Next, select the language change icon (next to the logo) and select your language, which is indicated under the value of the **Language variable**. Or simply select a language from the language change window that appears (only the first launch)
7. **Check your translation very carefully!**
8. After successful tests, send a request (in issue) with file of localization for adding the localization to the repository. Your localization will be added to the application itself immediately in the next update!

## Add new modification(s)

1. Create a ZIP archive with the modification files. It is important to note that the contents of the archive will be simply unpacked to the game folder in the future. Therefore, do not create folders with modification files, because the folder will be unpacked, and not the modification files themselves! There is no need to add ASI Loader to the archive, it is installed automatically!
2. Download the official plugin as a blank (for example: [GInput](https://raw.githubusercontent.com/Zalexanninev15/Jetpack-Downgrader/unstable/data/mods/info/txts/GInput.txt), only "Save as txt"!)
3. Rename the file name to the name of the new modification
4. Fill in the information of modification in text editor (as [Notepad++](https://notepad-plus-plus.org/)) by lines and save file

**Lines:**
* 1 - name
* 2 - version
* 3 - author
* 4 - description (the maximum size can be about 174 characters)
* 5 - link to the modification website
* 6 - direct link to screenshot 1
* 7 - direct link to screenshot 2
* 8 - direct link to screenshot 3
* 9 - direct link or link to MEGA to the ZIP archive

5. Send a request (in issue) for adding the modification to the repository (and list of modifications) with the file with a information about the modification (steps 2-4) 
