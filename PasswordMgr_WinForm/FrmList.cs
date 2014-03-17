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
    public partial class FrmList : Form
    {
        public FrmList()
        {
            InitializeComponent();
        }

        private void FrmList_Load(object sender, EventArgs e)
        {
            this.Name = GlobalConfig.AppName + " - List";
        }
    }
}
