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

        public static readonly string DefaultDBPath = ".\\Data\\pdb.db";

        public static string LoadConfig()
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
#if DEBUG
                else
                    DialogHelper.ShowMessage(string.Format("Didn't found the DBPath element in file '{0}'.", configFile.FullName), "LoadConfig");
#endif
            }
            else
            {
                FileInfo dbInfo = new FileInfo(DefaultDBPath);
                if (dbInfo.Exists)
                {
                    string fullpath = dbInfo.FullName;
                    SaveConfig(fullpath);
                    result = fullpath;
                }
            }

            return result;
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
