﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordMgr_WinForm
{
    public partial class FrmList : Form
    {
        PasswordMgrViewModel viewModel;

        public FrmList()
        {
            InitializeComponent();
            viewModel = new PasswordMgrViewModel();
        }

        private void FrmList_Load(object sender, EventArgs e)
        {
            this.Text = GlobalConfig.AppName + " - List";

            // init list view.
            listView1.View = View.Details;
            listView1.Columns.Add("System Name", 100);
            listView1.Columns.Add("User Name", 80);
            listView1.Columns.Add("Nick Name", 80);
            listView1.Columns.Add("Email", 100);
            listView1.Columns.Add("Could eamail login?", 130);
            listView1.Columns.Add("Password", 80);
            listView1.Columns.Add("Website", 130);
            listView1.Columns.Add("Notes", 100);
            listView1.Columns.Add("Created Date", 100);
            listView1.Columns.Add("Last Modified Date", 100);

            listView1.MultiSelect = false;
            listView1.FullRowSelect = true;

            // search condition
            cBoxSearchType.Items.Add("Any");
            cBoxSearchType.Items.Add("Username");
            cBoxSearchType.Items.Add("Email");


            ReloadData();
        }

        private void ReloadData(string filter = "")
        {
            if (viewModel.LoadDataFromDB(filter))
            {
                InitListViewData(viewModel.PassItemList);
            }
            else
                DialogHelper.ShowMessage("Failed to load data from database.");
        }

        private void InitListViewData(ObservableCollection<PasswordItem> dataList)
        {
            listView1.Items.Clear();

            foreach (var item in dataList)
            {
                ListViewItem viewItem = new ListViewItem(
                    new string[] { 
                        item.Systemname,
                        item.Username,
                        item.Nickname,
                        item.Email,
                        item.FEmailIsLogin.ToString(),
                        "******", //item.Password,
                        item.Website,
                        item.Notes,
                        item.CreatedDate.ToString("MM/dd/yyyy hh:mm"),
                        item.LastModifiedDate.ToString("MM/dd/yyyy hh:mm")
                    }
                );
                viewItem.Tag = item;    // Save the data source in Tag for getting it easily later.
                listView1.Items.Add(viewItem);
            }
            int count = listView1.Items.Count;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            FrmMainEntry newFrm = new FrmMainEntry();
            newFrm.OnFormEntityClosing += () =>
            {
                ReloadData();
                return true;
            };
            newFrm.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            PasswordItem selectedItem;
            if (GetSeletedPassItem(out selectedItem) && selectedItem != null)
            {
                FrmMainEntry frm = new FrmMainEntry(selectedItem);
                frm.OnFormEntityClosing += () =>
                {
                    ReloadData();
                    return true;
                };
                frm.ShowDialog();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            PasswordItem selectedItem;
            if (GetSeletedPassItem(out selectedItem) && selectedItem != null)
            {
                if (DialogResult.OK == MessageBox.Show(selectedItem.ToString(), "Delete?"))
                {
                    if (!viewModel.DeletePassItem(selectedItem))
                    {
                        DialogHelper.ShowErrorMessage("Delete failed!\r\n\r\n" + selectedItem.ToString(), "Delete?");
                    }
                    ReloadData();
                }
            }
        }

        private bool GetSeletedPassItem(out PasswordItem item)
        {
            bool result = false;

            var list = listView1.SelectedItems;
            if (list.Count > 0)
            {
                ListViewItem viewItem = list[0];
                item = viewItem.Tag as PasswordItem;
                if (item != null)
                    result = true;
            }
            else
                item = null;

            return result;
        }

        private void btnReloadAll_Click(object sender, EventArgs e)
        {
            ReloadData();
            tabControl1.SelectedIndex = 0;
        }

        private void btnGoSearch_Click(object sender, EventArgs e)
        {
            string strColumn = "";
            switch (cBoxSearchType.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    strColumn = "username";
                    break;
                case 2:
                    strColumn = "email";
                    break;
            }

            string strFilter = viewModel.GenerateFilterByColumn(txtKeywords.Text.Trim(), strColumn);
            ReloadData(strFilter);
            tabControl1.SelectedIndex = 0;
        }

        private void FrmList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            btnUpdate_Click(sender, e);
        }
    }
}
