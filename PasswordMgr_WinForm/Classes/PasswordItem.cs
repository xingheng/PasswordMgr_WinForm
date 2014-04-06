using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordMgr_WinForm
{
    public class PasswordItem
    {
        #region Members to be displayed
        private string systemname;
        public string Systemname
        {
            get { return systemname; }
            set { systemname = value; }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string nickname;
        public string Nickname 
        {
            get { return nickname; }
            set { nickname = value; }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private bool fEmailIsLogin;
        public bool FEmailIsLogin
        {
            get { return fEmailIsLogin; }
            set { fEmailIsLogin = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string website;
        public string Website
        {
            get { return website; }
            set { website = value; }
        }

        private string notes;
        public string Notes
        {
            get { return notes; }
            set { notes = value; }
        }
        #endregion

        #region Members not to be displayed
        private string id;
        public string ID
        {
            get { return id; }
            private set { id = value; }
        }

        private DateTime createdDate;
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        private DateTime lastModifiedDate;
        public DateTime LastModifiedDate
        {
            get { return lastModifiedDate; }
            set { lastModifiedDate = value; }
        }
        #endregion

        #region Construtors
        public PasswordItem()
        {
            this.ID = Guid.NewGuid().ToString();
        }

        public PasswordItem(string aId)
        {
            this.ID = aId;
        }

        public PasswordItem(string asystem, string auser, string anick, string aemail, bool fEmail, string apassword, string awebsite, string anotes)
        {
            this.ID = Guid.NewGuid().ToString();
            this.createdDate = this.lastModifiedDate = DateTime.Now;

            this.Systemname = asystem;
            this.Username = auser;
            this.Nickname = anick;
            this.Email = aemail;
            this.FEmailIsLogin = fEmail;
            this.Password = apassword;
            this.Website = awebsite;
            this.Notes = anotes;
        }
        #endregion

        public override string ToString()
        {
            return string.Format("ID:{0}, System name: {1}, User name:{2}, Nick name: {3}, Email:{4}, FEmailIsLogin:{5}, Password:{6}, Website:{7}, Notes:{8}, CreatedDate:{9}, LastModifiedDate:{10}",
                this.ID, this.Systemname, this.Username, this.Nickname, this.Email, this.FEmailIsLogin, this.Password, this.Website, this.Notes, this.CreatedDate, this.LastModifiedDate);
        }
    }
}
