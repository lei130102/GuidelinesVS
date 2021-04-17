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
    public class MusicBook : IBookService
    {
        public string BookName { get; set; }

        /// <summary>
        /// 导出私有字段/属性
        /// </summary>
        [Export(typeof(string))]
        private string _privateBookName = "Private Music BookName";

        /// <summary>
        /// 导出公有字段/属性
        /// </summary>
        [Export(typeof(string))]
        public string _publicBookName = "Public Music BookName";

        /// <summary>
        /// 导出公有方法
        /// </summary>
        /// <returns></returns>
        [Export(typeof(Func<string>))]
        public string GetBookName()
        {
            return "MathBook";
        }

        /// <summary>
        /// 导出私有方法
        /// </summary>
        /// <param name="price"></param>
        /// <returns></returns>
        [Export(typeof(Func<int, string>))]
        private string GetBookPrice(int price)
        {
            return "$" + price;
        }
    }
}
