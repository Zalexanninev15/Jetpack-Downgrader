using System;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;

namespace JetpackGUI
{
    public class LanguagesStringReader
    {
        public String Language = "null";
        public String FirstTitle = "null";
        public String SelectLang = "null";
        public String ApplyAndLaunch = "null";
        public String Languages = "null";
        public String Stage = "null";
        public String OtherActions = "null";
        public String Tab1 = "null";
        public String PathLabel = "null";
        public String CBG1 = "null";
        public String CBG2 = "null";
        public String Tab2 = "null";
        public String List = "null";
        public String ASILoader = "null";
        public String AboutMod = "null";
        public String ModName = "null";
        public String ModVersion = "null";
        public String ModAuthor = "null";
        public String DescriptionMod = "null";
        public String TopicOfMod = "null";
        public String DownloadingModCache = "null";
        public String Downgrade = "null";
        public String DownloadPatches = "null";
        public String DownloadingPatches = "List";
        public String DownloadingDirectXFiles = "null";
        public String ModWord = "null";
        public String Mbyte = "null";
        public String WishPlay = "null";
        public String Play = "null";
        public String CloseApp = "null";
        public String CreateBackups = "null";
        public String CreateShortcut = "null";
        public String ResetGame = "null";
        public String RGL_GarbageCleaning = "null";
        public String RegisterGamePath = "null";
        public String CopyGameToNewPath = "null";
        public String Forced = "null";
        public String EnableDirectPlay = "null";
        public String InstallDirectXComponents = "null";
        public String InstallMod = "null";
        public String Request = "null";
        public String Error = "null";
        public String Warning = "null";
        public String FolderSelectDialog = "null";
        public String WishDownloadPatches = "null";
        public String WishDownloadDirectXFiles = "null";
        public String InstallModQuestion = "null";
        public String WishDowngrader = "null";
        public String ModSucces = "null";
        public String Succes = "null";
        public String BindingOK = "null";
        public String ReturnUsingBackups = "null";
        public String AboutModDamaged = "null";
        public String ModFailure = "null";
        public String WishReturnUsingBackups = "null";
        public String WishRegGame = "null";
        public String PathNotFound = "null";
        public String BrowserNotFound = "null";
        public String NetworkNotFound = "null";
        public String OfflineMode = "null";
        public String NewPath = "null";
        public String YouCanDelete = "null";
        public String Activation = "null";
        public String Deactivation = "null";
        public String AboutTitle = "null";
        public String Version = "null";
        public String Authors = "null";
        public String Zalexanninev15 = "null";
        public String VadimM = "null";
        public String License = "null";
        public String Localization = "null";
        public String LocalizationBy = "null";
        public String AboutDonate = "null";
        public String AboutIssues = "null";
        public String AboutSite = "null";
        public String AboutTopic = "null";
    }
    
    public class Localization
    {
        public LanguagesStringReader Fieldss;
        public Localization() { Fieldss = new LanguagesStringReader(); }

        public void ReadXml()
        {
            if (File.Exists(Application.StartupPath + @"\files\languages\" + TempValues.SelectedLanguage))
            {
                XmlSerializer ser = new XmlSerializer(typeof(LanguagesStringReader));
                using (TextReader reader = new StreamReader(Application.StartupPath + @"\files\languages\" + TempValues.SelectedLanguage))  { Fieldss = ser.Deserialize(reader) as LanguagesStringReader; } 
            }
        }
    }
}