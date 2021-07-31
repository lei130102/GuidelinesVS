using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommonLib.Test_FolderBrowserDialog
{
    //打开文件夹对话框

    public static class Test
    {
        public static void run()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "选择所有文件存放目录";
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                string sPath = fbd.SelectedPath;
                MessageBox.Show(sPath);
            }
        }
    }

    //Description

    //此屬性可以定義在對話框的樹型視圖上顯示的文本

    //RootFolder

    //定義用戶從什麼文件夾開始瀏覽，此屬性可以設置Environment.SpecialFolder枚舉中的一個值。表示啟始目錄。

    //ShowNewFolderButton

    //是否可以創建新文件夾，默认为true（显示按钮）。
}
