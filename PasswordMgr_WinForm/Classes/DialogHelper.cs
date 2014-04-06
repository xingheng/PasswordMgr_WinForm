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
        public static void ShowMessage(string content, string title = "")
        {
            MessageBox.Show(content, title);
        }

        public static void ShowExcetion(Exception ex, string title = "")
        {
            MessageBox.Show(ex.ToString(), title);
        }

        public static void ShowErrorMessage(string text, string title = "")
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowWarningMessage(string text, string title = "")
        {
            MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
