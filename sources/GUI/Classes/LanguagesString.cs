using System.Xml.Serialization;

namespace JetpackGUI
    {
        [XmlRoot(ElementName = "LanguagesString")]
        public class LanguagesString
        {
            [XmlElement(ElementName = "Language")]
            public string Language { get; set; }

            [XmlElement(ElementName = "FirstTitle")]
            public string FirstTitle { get; set; }

            [XmlElement(ElementName = "SelectLang")]
            public string SelectLang { get; set; }

            [XmlElement(ElementName = "ApplyAndLaunch")]
            public string ApplyAndLaunch { get; set; }

            [XmlElement(ElementName = "Languages")]
            public string Languages { get; set; }

            [XmlElement(ElementName = "Stage")]
            public string Stage { get; set; }

            [XmlElement(ElementName = "OtherActions")]
            public string OtherActions { get; set; }

            [XmlElement(ElementName = "Tab1")]
            public string Tab1 { get; set; }

            [XmlElement(ElementName = "PathLabel")]
            public string PathLabel { get; set; }

            [XmlElement(ElementName = "CBG1")]
            public string CBG1 { get; set; }

            [XmlElement(ElementName = "CBG2")]
            public string CBG2 { get; set; }

            [XmlElement(ElementName = "Tab2")]
            public string Tab2 { get; set; }
        
            [XmlElement(ElementName = "List")]
            public string List { get; set; }
        
            [XmlElement(ElementName = "ASILoader")]
            public string ASILoader { get; set; }
        
            [XmlElement(ElementName = "AboutMod")]
            public string AboutMod { get; set; }
        
            [XmlElement(ElementName = "ModName")]
            public string ModName { get; set; }
        
            [XmlElement(ElementName = "ModVersion")]
            public string ModVersion { get; set; }
        
            [XmlElement(ElementName = "ModAuthor")]
            public string ModAuthor { get; set; }
        
            [XmlElement(ElementName = "DescriptionMod")]
            public string DescriptionMod { get; set; }
        
            [XmlElement(ElementName = "TopicOfMod")]
            public string TopicOfMod { get; set; }
        
            [XmlElement(ElementName = "DownloadingModCache")]
            public string DownloadingModCache { get; set; }
        
            [XmlElement(ElementName = "Downgrade")]
            public string Downgrade { get; set; }
        
            [XmlElement(ElementName = "DownloadPatches")]
            public string DownloadPatches { get; set; }
        
            [XmlElement(ElementName = "DownloadingPatches")]
            public string DownloadingPatches { get; set; }
        
            [XmlElement(ElementName = "DownloadingDirectXFiles")]
            public string DownloadingDirectXFiles { get; set; }
        
            [XmlElement(ElementName = "ModWord")]
            public string ModWord { get; set; }
        
            [XmlElement(ElementName = "Mbyte")]
            public string Mbyte { get; set; }
        
            [XmlElement(ElementName = "WishPlay")]
            public string WishPlay { get; set; }
        
            [XmlElement(ElementName = "Play")]
            public string Play { get; set; }
        
            [XmlElement(ElementName = "CloseApp")]
            public string CloseApp { get; set; }
        
            [XmlElement(ElementName = "CreateBackups")]
            public string CreateBackups { get; set; }
        
            [XmlElement(ElementName = "CreateShortcut")]
            public string CreateShortcut { get; set; }
        
            [XmlElement(ElementName = "ResetGame")]
            public string ResetGame { get; set; }
        
            [XmlElement(ElementName = "RGL_GarbageCleaning")]
            public string RGL_GarbageCleaning { get; set; } 
        
            [XmlElement(ElementName = "RegisterGamePath")]
            public string RegisterGamePath { get; set; } 
        
            [XmlElement(ElementName = "CopyGameToNewPath")]
            public string CopyGameToNewPath { get; set; } 
        
            [XmlElement(ElementName = "Forced")]
            public string Forced { get; set; }
        
            [XmlElement(ElementName = "EnableDirectPlay")]
            public string EnableDirectPlay { get; set; }
        
            [XmlElement(ElementName = "InstallDirectXComponents")]
            public string InstallDirectXComponents { get; set; }
        
            [XmlElement(ElementName = "InstallMod")]
            public string InstallMod { get; set; } 
        
            [XmlElement(ElementName = "Request")]
            public string Request { get; set; }
        
            [XmlElement(ElementName = "Information")]
            public string Information { get; set; }

            [XmlElement(ElementName = "Warning")]
            public string Warning { get; set; }

            [XmlElement(ElementName = "Error")]
            public string Error { get; set; }

            [XmlElement(ElementName = "Question")]
            public string Question { get; set; }

            [XmlElement(ElementName = "FolderSelectDialog")]
            public string FolderSelectDialog { get; set; } 
        
            [XmlElement(ElementName = "WishDownloadPatches")]
            public string WishDownloadPatches { get; set; }
        
            [XmlElement(ElementName = "WishDownloadDirectXFiles")]
            public string WishDownloadDirectXFiles { get; set; } 
        
            [XmlElement(ElementName = "InstallModQuestion")]
            public string InstallModQuestion { get; set; }
        
            [XmlElement(ElementName = "WishDowngrader")]
            public string WishDowngrader { get; set; }
        
            [XmlElement(ElementName = "ModSucces")]
            public string ModSucces { get; set; } 
        
            [XmlElement(ElementName = "Succes")]
            public string Succes { get; set; } 
        
            [XmlElement(ElementName = "BindingOK")]
            public string BindingOK { get; set; }
        
            [XmlElement(ElementName = "ReturnUsingBackups")]
            public string ReturnUsingBackups { get; set; }
        
            [XmlElement(ElementName = "AboutModDamaged")]
            public string AboutModDamaged { get; set; }
        
            [XmlElement(ElementName = "ModFailure")]
            public string ModFailure { get; set; }
        
            [XmlElement(ElementName = "WishReturnUsingBackups")]
            public string WishReturnUsingBackups { get; set; }
        
            [XmlElement(ElementName = "WishRegGame")]
            public string WishRegGame { get; set; } 
        
            [XmlElement(ElementName = "PathNotFound")]
            public string PathNotFound { get; set; }
        
            [XmlElement(ElementName = "BrowserNotFound")]
            public string BrowserNotFound { get; set; }
        
            [XmlElement(ElementName = "NetworkNotFound")]
            public string NetworkNotFound { get; set; } 
        
            [XmlElement(ElementName = "OfflineMode")]
            public string OfflineMode { get; set; }
        
            [XmlElement(ElementName = "NewPath")]
            public string NewPath { get; set; }
        
            [XmlElement(ElementName = "YouCanDelete")]
            public string YouCanDelete { get; set; }
        
            [XmlElement(ElementName = "Activation")]
            public string Activation { get; set; } 
        
            [XmlElement(ElementName = "Deactivation")]
            public string Deactivation { get; set; }
        
            [XmlElement(ElementName = "AboutTitle")]
            public string AboutTitle { get; set; } 
        
            [XmlElement(ElementName = "Version")]
            public string Version { get; set; }
        
            [XmlElement(ElementName = "Authors")]
            public string Authors { get; set; }
        
            [XmlElement(ElementName = "Zalexanninev15")]
            public string Zalexanninev15 { get; set; }
        
            [XmlElement(ElementName = "VadimM")]
            public string VadimM { get; set; }
        
            [XmlElement(ElementName = "License")]
            public string License { get; set; }
        
            [XmlElement(ElementName = "Localization")]
            public string Localization { get; set; }
        
            [XmlElement(ElementName = "LocalizationBy")]
            public string LocalizationBy { get; set; } 
        
            [XmlElement(ElementName = "AboutDonate")]
            public string AboutDonate { get; set; }
        
            [XmlElement(ElementName = "AboutIssues")]
            public string AboutIssues { get; set; }
        
            [XmlElement(ElementName = "AboutSite")]
            public string AboutSite { get; set; } 
        
            [XmlElement(ElementName = "AboutTopic")]
            public string AboutTopic { get; set; } 

            [XmlAttribute(AttributeName = "xsd")]
            public string Xsd { get; set; }

            [XmlAttribute(AttributeName = "xsi")]
            public string Xsi { get; set; }

            [XmlText]
            public string Text { get; set; }
        }
    }