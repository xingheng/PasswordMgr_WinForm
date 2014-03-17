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

        public static readonly string DefaultDBPath = ".\\Data\\pdb";

        public static string LoadConfig()
        {
            string result = "";

            FileInfo configFile = new FileInfo(gConfigFileLocation);
            if (configFile.Exists)
            {
                XDocument xdoc = XDocument.Load(gConfigFileLocation);
                XElement dbPathElement = xdoc.Element("DBPath");
                if (dbPathElement != null)
                    result = dbPathElement.Value;
            }
            else
            {
                FileInfo dbInfo = new FileInfo(DefaultDBPath);
                if (dbInfo.Exists)
                    result = dbInfo.FullName;
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
