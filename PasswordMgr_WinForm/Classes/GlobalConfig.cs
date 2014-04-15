using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace PasswordMgr_WinForm
{
    class GlobalConfig
    {
        public const string AppName = "Password Manager for Windows (Will Han)";


        const string gConfigFileLocation = "PasswordMgr.ini";

        private static readonly string DefaultDBPath = ".\\Data\\pdb.db";

        private static string _DatabaseFileURL; // The database that to be connected.
        public static string DatabaseFileURL
        {
            get { return _DatabaseFileURL; }
        }

        public static void LoadConfig()
        {
            try
            {
                string result = "";

                // When user run this app at the first time, the config file .ini doesn't exist.
                FileInfo configFile = new FileInfo(gConfigFileLocation);
                if (configFile.Exists)
                {
                    XDocument xdoc = XDocument.Load(gConfigFileLocation);
                    XElement dbPathElement = xdoc.Root.Element("DBPath");
                    if (dbPathElement != null)
                        result = dbPathElement.Value;
                    else
                        DialogHelper.ShowMessage(string.Format("Didn't find the DBPath element in file '{0}'.", configFile.FullName), "LoadConfig");
                }
                else
                {
                    FileInfo dbInfo = new FileInfo(DefaultDBPath);
                    if (dbInfo.Exists)
                    {
                        // Copy the db file to ~/Documents/PasswordMgr
                        string userDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        DirectoryInfo dirInfo = new DirectoryInfo(userDocuments);
                        if (dirInfo.Exists)
                        {
                            var subDirInfo = dirInfo.CreateSubdirectory("PasswordMgr");

                            string destDBFile = subDirInfo.FullName + "\\" + dbInfo.Name;
                            File.Copy(dbInfo.FullName, destDBFile, true);

                            SaveConfig(destDBFile);
                            result = destDBFile;
                        }
                    }
                    else
                        DialogHelper.ShowMessage("Didn't find the original database file at '" + dbInfo.FullName + "'", "LoadConfig");
                }

                _DatabaseFileURL = result;
            }
            catch (Exception ex)
            {
                DialogHelper.ShowExcetion(ex, "LoadConfig");
            }
        }

        public static bool SaveConfig(string dbfileurl)
        {
            XDocument xdoc = new XDocument(
                new XElement("PasswordMgrConfig",
                    new XElement("DBPath", dbfileurl)
                    )
                );

            xdoc.Save(gConfigFileLocation);

            return true;
        }
    }
}
