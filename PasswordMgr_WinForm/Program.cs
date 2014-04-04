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
            frm.AfterFormClosed += (sender, e) =>
            {
                if (sender != null && sender.GetType() == typeof(DialogResultInfo))
                {
                    switch ((DialogResultInfo)sender)
                    {
                        case DialogResultInfo.FrmList:
                            FrmList frmList = new FrmList();
                            frmList.ShowDialog();
                            break;
                        default:
                            break;
                    }
                }
            };
            frm.ShowDialog();

            //Application.Run(new FrmMainEntry());
        }
    }
}
