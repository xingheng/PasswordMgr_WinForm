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

            listView1.MultiSelect = true;
            listView1.FullRowSelect = true;


            if (viewModel.LoadDataFromDB())
            {
                InitListViewData(viewModel.PassItemList);
            }
            else
                DialogHelper.ShowMessage("Failed to load data from database.");
        }

        private void InitListViewData(ObservableCollection<PasswordItem> dataList)
        {
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
                listView1.Items.Add(viewItem);
            }
        }
    }
}
