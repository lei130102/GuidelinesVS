using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Test_DateTime
{
    public static class Test
    {
        public static void run()
        {
            DateTime Ddate = DateTime.ParseExact("20071101 000000", "yyyyMMdd HHmmss", CultureInfo.CurrentCulture);
            string dateTime = Ddate.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
