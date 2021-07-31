using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.Test_SaveFileDialog
{
    public static class Test
    {
        public static void Normal()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel file(*,xls)|*.xls";
            sfd.FileName = "报表";
            sfd.RestoreDirectory = true;
            if(sfd.ShowDialog() == DialogResult.OK)
            {

            }

        }

        public static void run()
        {

        }
    }
}
