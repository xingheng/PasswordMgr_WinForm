using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordMgr_WinForm
{
    public class BaseClassHelper
    {
        #region DateTime Helper
        public static long TimeSpanFromDateTime(DateTime dt)
        {
            if (dt.CompareTo(DateTime.MinValue) > 0 && dt.CompareTo(DateTime.MaxValue) < 0)
            {
                return dt.Ticks;
            }
            return 0;
        }

        public static DateTime DateTimeFromString(string strDT)
        {
            DateTime result = DateTime.MinValue;
            if (DateTime.TryParse(strDT, out result))
                return result;
            return result;
        }

        #endregion
    }
}
