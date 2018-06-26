using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace AndroidReleaseCandidateGenerator
{
    public class StringsProvider
    {
        public string routeToLocalRepositoryFolder; 
        public string mobileFileLocation; 
        public string tabletFileLocation;
        public string androidToolsPath;

        public string alignCommand;
        public string signCommand;               
        public string verifySignatureCommand;
        public string sha512Command;

        public string keyStoreFileMobile;
        public string keyStoreFileTablet;

        public string fullMobileOutputPathLocation;
        public string fullTabletOutputPathLocation;

        public string pocRootFolderPath;
        public string mPocRootFolderPath;

        public StringsProvider()
        {
            ResourceManager rm = new ResourceManager("AndroidReleaseCandidateGenerator.Properties.Resources", Assembly.GetExecutingAssembly());
            this.routeToLocalRepositoryFolder = rm.GetString("routeToLocalRepositoryFolder", CultureInfo.CurrentCulture);
            this.mobileFileLocation = rm.GetString("mobileFileLocation", CultureInfo.CurrentCulture);
            this.tabletFileLocation = rm.GetString("tabletFileLocation", CultureInfo.CurrentCulture);
            this.androidToolsPath = rm.GetString("androidToolsPath", CultureInfo.CurrentCulture);

            this.alignCommand = rm.GetString("alignCommand", CultureInfo.CurrentCulture);
            this.signCommand = rm.GetString("signCommand", CultureInfo.CurrentCulture);
            this.verifySignatureCommand = rm.GetString("verifySignatureCommand", CultureInfo.CurrentCulture);
            this.sha512Command = rm.GetString("sha512Command", CultureInfo.CurrentCulture);

            this.keyStoreFileMobile = rm.GetString("keyStoreFileMobile", CultureInfo.CurrentCulture);
            this.keyStoreFileTablet = rm.GetString("keyStoreFileTablet", CultureInfo.CurrentCulture);

            this.pocRootFolderPath = rm.GetString("pocRootFolderPath", CultureInfo.CurrentCulture);
            this.mPocRootFolderPath = rm.GetString("mPocRootFolderPath", CultureInfo.CurrentCulture);

            this.fullMobileOutputPathLocation = this.routeToLocalRepositoryFolder + this.mobileFileLocation;
            this.fullTabletOutputPathLocation = this.routeToLocalRepositoryFolder + this.tabletFileLocation;
        }

        public AndroidOutputVersioning ReleaseCandidateNameData(bool isTablet)
        {
            AndroidOutputVersioning fileInfo = new AndroidOutputVersioning();

            DirectoryInfo directory;
            if (isTablet)
            {
                directory = new DirectoryInfo(fullTabletOutputPathLocation);

            }
            else
            {
                directory = new DirectoryInfo(fullMobileOutputPathLocation);
            }

            FileInfo[] Files = directory.GetFiles("*.apk");
            if (Files.Length == 1)
            {
                string file = Files[0].Name;
                string[] splited = file.Split('_');
                if (splited.Length == 2)
                {
                    fileInfo.name = splited[0].Split('-')[0] + "-" + splited[0].Split('-')[1];
                    fileInfo.version = splited[0].Split('-')[2];
                    fileInfo.identifier = splited[1].Split('.')[0];
                }
            }
            else
            {
                Console.WriteLine("Too many files in outputfolder please just place the release candidate one.");
            }

            return fileInfo;
        }

        public void CopyToClipBoard(string text)
        {
            Thread thread = new Thread(() => Clipboard.SetText(text));
            thread.SetApartmentState(ApartmentState.STA); 
            thread.Start();
            thread.Join(); 
        }
    }
}
