using System;
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
            listView1.Columns.Add("User Name", 100);
            listView1.Columns.Add("Nick Name", 100);
            listView1.Columns.Add("Email", 100);
            listView1.Columns.Add("Could eamail login?", 100);
            listView1.Columns.Add("Password", 100);
            listView1.Columns.Add("Website", 100);
            listView1.Columns.Add("Notes", 100);
            listView1.Columns.Add("Created Date", 100);
            listView1.Columns.Add("Last Modified Date", 100);

            listView1.MultiSelect = false;
            listView1.FullRowSelect = true;

            // search condition
            cBoxSearchType.Items.Add("Any");
            cBoxSearchType.Items.Add("Username");
            cBoxSearchType.Items.Add("Email");


            if (viewModel.LoadDataFromDB())
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
                        item.Password,
                        item.Website,
                        item.Notes,
                        item.CreatedDate.ToShortDateString(),
                        item.LastModifiedDate.ToShortDateString()
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
            newFrm.AfterFormClosed += () =>
            {
                btnReloadAll_Click(null, null);
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
                frm.AfterFormClosed += () =>
                {
                    btnReloadAll_Click(null, null);
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
                    btnReloadAll_Click(null, null);
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
            if (viewModel.LoadDataFromDB())
            {
                InitListViewData(viewModel.PassItemList);
            }
            else
                DialogHelper.ShowMessage("Failed to load data from database.");

            tabPage1.Focus();
        }

        private void btnGoSearch_Click(object sender, EventArgs e)
        {
            string strFilter = " where ";
            switch (cBoxSearchType.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    strFilter += "username =" + txtKeywords.Text.Trim();
                    break;
                case 2:
                    strFilter += "email =" + txtKeywords.Text.Trim();
                    break;
            }

            if (viewModel.LoadDataFromDB(strFilter))
            {
                InitListViewData(viewModel.PassItemList);
            }
            else
                DialogHelper.ShowMessage("Failed to load data from database.");

            tabPage1.Focus();
        }

        private void FrmList_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
