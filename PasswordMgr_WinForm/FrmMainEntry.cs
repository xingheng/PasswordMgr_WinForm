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
    public partial class FrmMainEntry : Form
    {
        PasswordMgrViewModel viewModel;

        public delegate bool/* Should we close the window? */ RouteEventHandler();
        public event RouteEventHandler OnFormEntityClosing;

        bool IsInsertMode;  // Is this instance for insertion or update?
        string gUpdatedPassItemID;  // To save the PassItem's ID to be updated, null when it's insertion mode.

        public FrmMainEntry()
        {
            InitializeComponent();
            viewModel = new PasswordMgrViewModel();
            IsInsertMode = true;
        }

        public FrmMainEntry(PasswordItem passItem)
            : this()
        {
            if (passItem != null)
            {
                IsInsertMode = false;

                gUpdatedPassItemID = passItem.ID;
                txtSystemname.Text = passItem.Systemname;
                txtUsername.Text = passItem.Username;
                txtNickname.Text = passItem.Nickname;
                txtEmail.Text = passItem.Email;
                cBoxEmail.Checked = passItem.FEmailIsLogin;
                txtPassword.Text = passItem.Password;
                txtWebsite.Text = passItem.Website;
                txtNotes.Text = passItem.Notes;
            }
            else
                DialogHelper.ShowMessage("FrmMainEntry: Wrong Parameters");
        }

        private void FrmMainEntry_Load(object sender, EventArgs e)
        {
            this.Text = GlobalConfig.AppName;

            toolTip1.InitialDelay = 10;
            toolTip1.SetToolTip(txtPassword, txtPassword.Text);
            txtPassword.TextChanged += (s, eArgs) =>
            { toolTip1.SetToolTip(txtPassword, txtPassword.Text); };

            if (IsInsertMode)
            {
                txtDBPath.Text = GlobalConfig.LoadConfig();
                viewModel.InitDatabase(txtDBPath.Text.Trim());

                openFileDialog1.Filter = "Database file|*.db|All files|*.*";
                openFileDialog1.Multiselect = false;

                cBoxEmail.Checked = true;   // Usually the email address could be the login name for websites.
                this.txtUsername.Focus();
            }
            else
            {
                btnSave.Text = "Update";
                btnNew.Enabled = false;
                btnShowList.Enabled = false;
                btnChangeDB.Enabled = false;
            }
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

            PasswordItem newItem = null;
            if (IsInsertMode)
            {
                newItem = new PasswordItem();
            }
            else
            {
                newItem = new PasswordItem(gUpdatedPassItemID);
                newItem.LastModifiedDate = DateTime.Now;
            }

            newItem.Systemname = txtSystemname.Text.Trim();
            newItem.Username = txtUsername.Text.Trim();
            newItem.Nickname = txtNickname.Text.Trim();
            newItem.Email = txtEmail.Text.Trim();
            newItem.FEmailIsLogin = cBoxEmail.Checked;
            newItem.Password = txtPassword.Text.Trim();
            newItem.Website = txtWebsite.Text.Trim();
            newItem.Notes = txtNotes.Text.Trim();

            if (IsInsertMode)
            {
                if (viewModel.InsertNewItem(newItem))
                    MessageBox.Show("Insert a new item successfully!");
            }
            else
            {
                if (viewModel.UpdatePassItem(newItem))
                    MessageBox.Show("Update a new item successfully!");
            }

            if (OnFormEntityClosing != null && OnFormEntityClosing())
                this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            txtSystemname.Text = txtUsername.Text = txtNickname.Text = txtEmail.Text
                = txtPassword.Text = txtWebsite.Text = txtNotes.Text = "";
        }

        private void btnShowList_Click(object sender, EventArgs e)
        {
            FrmList listWindow = new FrmList();
            listWindow.Show();

            if (OnFormEntityClosing != null && OnFormEntityClosing())
                this.Close();
            else
                this.Hide();
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
