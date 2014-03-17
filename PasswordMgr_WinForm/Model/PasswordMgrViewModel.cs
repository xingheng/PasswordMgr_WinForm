using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Data;
using SQLite_CSharp;

namespace PasswordMgr_WinForm
{
    class PasswordMgrViewModel
    {
        public string DataFileLocation;
        private const string gTableName = "passwordtb";

        private ObservableCollection<PasswordItem> gList;

        public event EventHandler PasswordSaved;

        public PasswordMgrViewModel()
        { 
            gList = new ObservableCollection<PasswordItem>();
        }

        public bool LoadDBFile(string databaseURL)
        {
            if (File.Exists(databaseURL))
            {
                DBOperation.connectionString = "Data Source=" + databaseURL;
                DataTable dt = null;
                dt = (DataTable)DBOperation.SQLiteRequest_Read("SELECT * FROM " + gTableName);
                foreach (DataRow row in dt.Rows)
                {
                    PasswordItem newPass = null;
                    int count = row.ItemArray.Length;

                    switch (count)
                    {
                        case 0:
                            newPass = new PasswordItem(row.ItemArray[count].ToString());
                            break;
                        case 1:
                            newPass.Systemname = row.ItemArray[count].ToString();
                            break;
                        case 2:
                            newPass.Username = row.ItemArray[count].ToString();
                            break;
                        case 3:
                            newPass.Nickname = row.ItemArray[count].ToString();
                            break;
                        case 4:
                            newPass.Email = row.ItemArray[count].ToString();
                            break;
                        case 5:
                            newPass.FEmailIsLogin = bool.Parse(row.ItemArray[count].ToString());
                            break;
                        case 6:
                            newPass.Password = row.ItemArray[count].ToString();
                            break;
                        case 7:
                            newPass.Website = row.ItemArray[count].ToString();
                            break;
                        case 8:
                            newPass.Notes = row.ItemArray[count].ToString();
                            break;
                        case 9:
                            newPass.CreatedDate = BaseClassHelper.DateTimeFromString(row.ItemArray[count].ToString());
                            break;
                        case 10:
                            newPass.LastModifiedDate = BaseClassHelper.DateTimeFromString(row.ItemArray[count].ToString());
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(false, "Additional Column?");
                            break;
                    }

                    gList.Add(newPass);
                }

                return true;
            }
            else
                return false;
        }

        public bool InsertNewItem(PasswordItem passItem)
        {
            bool status = false;

            string cmdText = "INSERT INTO " + gTableName +
                " VALUES(@id,@systemname,@username,@nickname,@email,@fEmailIsLogin,@password,@website,@notes,@createdDate,@lastModifiedDate)";
            Exception ret = DBOperation.SQLiteRequest_Write(cmdText,
                "@id", passItem.ID,
                "@systemname", passItem.Systemname,
                "@username", passItem.Username,
                "@nickname", passItem.Nickname,
                "@email", passItem.Email,
                "@fEmailIsLogin", passItem.FEmailIsLogin,
                "@password", passItem.Password,
                "@website", passItem.Website,
                "@notes", passItem.Notes,
                "@createdDate", passItem.CreatedDate,
                "@lastModifiedDate", passItem.LastModifiedDate);
            if (ret != null)
            {
                DialogHelper.ShowExcetion(ret, "cmdText: " + cmdText);
                status = false;
            }
            else
                status = true;

            if (PasswordSaved != null)
                PasswordSaved(null, null);
            return status;
        }

    }
}
