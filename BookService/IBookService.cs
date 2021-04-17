using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//MEF ，全称 Managed Extensibility Framework (托管可扩展框架)。
//MEF 是专门致力于解决扩展性问题的框架，MSDN中对MEF有这样一段说明：

//Managed Extensibility Framework 或 MEF 是一个用于创建可扩展的轻型应用程序的库。
//应用程序开发人员可利用该库发现并使用扩展，而无需进行配置。扩展开发人员还可以利用该
//库轻松地封装代码，避免生成脆弱的硬依赖项。通过 MEF ，不仅可以在应用程序内重用扩展，
//还可以在应用程序之间重用扩展。



//添加对 System.ComponentModel.Composition 命名空间的引用

namespace BookService
{
    public interface IBookService
    {
        string BookName { get; set; }
        string GetBookName();
    }
}
