using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookService.Book
{
    //导出类
    [Export("BookService", typeof(IBookService))] //指定调用 public ExportAttribute(string contractName, Type contractType);
    //协定名称为 BookService ，可以自定义起名，可以重复，但需要管理好否则会造成混乱
    //导出类型为 IBookService ，那么导入时必须也是 IBookService
    public class HistoryBook : IBookService
    {
        public string BookName { get; set; }

        public string GetBookName()
        {
            return "HistoryBook";
        }
    }
}
