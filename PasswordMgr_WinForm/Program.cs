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
            frm.AfterFormClosed += () =>
            {
                // This window is only one running in the main thread, so never close it when AfterFormClosed.
                frm.Hide();
                return false;
            };
            //frm.ShowDialog();

            Application.Run(frm);
        }
    }
}
