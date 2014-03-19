﻿using System;
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
        private const string gTableName = "passwordtb";

        private ObservableCollection<PasswordItem> passItemList;
        public ObservableCollection<PasswordItem> PassItemList
        {
            get
            {
                return passItemList;
            }
        }

        public event EventHandler PasswordSaved;

        public PasswordMgrViewModel()
        { 
            passItemList = new ObservableCollection<PasswordItem>();
        }

        public void InitDatabase(string databaseURL)
        {
            if (File.Exists(databaseURL))
            {
                DBOperation.connectionString = "Data Source=" + databaseURL;
            }
            else
                DialogHelper.ShowExcetion(new Exception("Database file doesn't exist"), "InitDatabase");
        }

        public bool LoadDataFromDB()
        {
            DataTable dt = null;
            dt = (DataTable)DBOperation.SQLiteRequest_Read("SELECT * FROM " + gTableName);
            foreach (DataRow row in dt.Rows)
            {
                PasswordItem newPass = null;
                int count = row.ItemArray.Length;

                for (int index = 0; index < count; index++)
                {
                    switch (index)
                    {
                        case 0:
                            newPass = new PasswordItem(row.ItemArray[index].ToString());
                            break;
                        case 1:
                            newPass.Systemname = row.ItemArray[index].ToString();
                            break;
                        case 2:
                            newPass.Username = row.ItemArray[index].ToString();
                            break;
                        case 3:
                            newPass.Nickname = row.ItemArray[index].ToString();
                            break;
                        case 4:
                            newPass.Email = row.ItemArray[index].ToString();
                            break;
                        case 5:
                            newPass.FEmailIsLogin = row.ItemArray[index].ToString() == "1" ? true : false;
                            break;
                        case 6:
                            newPass.Password = row.ItemArray[index].ToString();
                            break;
                        case 7:
                            newPass.Website = row.ItemArray[index].ToString();
                            break;
                        case 8:
                            newPass.Notes = row.ItemArray[index].ToString();
                            break;
                        case 9:
                            newPass.CreatedDate = BaseClassHelper.DateTimeFromString(row.ItemArray[index].ToString());
                            break;
                        case 10:
                            newPass.LastModifiedDate = BaseClassHelper.DateTimeFromString(row.ItemArray[index].ToString());
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(false, "Additional Column?");
                            break;
                    }
                }

                passItemList.Add(newPass);
            }

            return true;
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
