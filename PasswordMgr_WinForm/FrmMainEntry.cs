using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordMgr_WinForm
{
    public enum DialogResultInfo
    {
        None, FrmMainEntry, FrmList
    }

    public partial class FrmMainEntry : Form
    {
        PasswordMgrViewModel viewModel;

        public DialogResultInfo gFormToShow = DialogResultInfo.None;

        public FrmMainEntry()
        {
            InitializeComponent();
            viewModel = new PasswordMgrViewModel();
        }

        private void FrmMainEntry_Load(object sender, EventArgs e)
        {
            this.Name = GlobalConfig.AppName;

            txtDBPath.Text = GlobalConfig.LoadConfig();

            openFileDialog1.Filter = "Database file|*.db|All files|*.*";
            openFileDialog1.Multiselect = false;

            cBoxEmail.Checked = true;   // Usually the email address could be the login name for websites.
            this.txtUsername.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSystemname.Text.Trim()) &&
                String.IsNullOrEmpty(txtUsername.Text.Trim()) &&
                String.IsNullOrEmpty(txtEmail.Text.Trim()) &&
                String.IsNullOrEmpty(txtPassword.Text.Trim()) &&
                String.IsNullOrEmpty(txtWebsite.Text.Trim()))
            {
                MessageBox.Show("Lack of important value!");
                return;
            }

            PasswordItem newItem = new PasswordItem();
            newItem.Systemname = txtSystemname.Text.Trim();
            newItem.Username = txtUsername.Text.Trim();
            newItem.Nickname = txtNickname.Text.Trim();
            newItem.Email = txtEmail.Text.Trim();
            newItem.FEmailIsLogin = cBoxEmail.Checked;
            newItem.Password = txtPassword.Text.Trim();
            newItem.Website = txtWebsite.Text.Trim();
            newItem.Notes = txtNotes.Text.Trim();

            viewModel.InitDatabase(txtDBPath.Text.Trim());
            if (viewModel.InsertNewItem(newItem))
            {
                MessageBox.Show("Insert a new item successfully!");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtSystemname.Text = txtUsername.Text = txtNickname.Text = txtEmail.Text
                = txtPassword.Text = txtWebsite.Text = txtNotes.Text = "";
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            gFormToShow = DialogResultInfo.FrmList;
            this.Close();
        }

        private void btnChangeDB_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDBPath.Text = openFileDialog1.FileName;
            }
        }
    }
}
