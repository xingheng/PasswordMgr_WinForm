using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasswordMgr_WinForm
{
    public class DialogHelper
    {
        public static void ShowExcetion(Exception ex, string title = "")
        {
            MessageBox.Show(ex.ToString(), title);
        }
    }
}
