using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordMgr_WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FrmMainEntry frm = new FrmMainEntry();
            frm.ShowDialog();
            if (frm.gFormToShow == DialogResultInfo.FrmList)
            {
                FrmList frmList = new FrmList();
                frmList.ShowDialog();
            }

            //Application.Run(new FrmMainEntry());
        }
    }
}
