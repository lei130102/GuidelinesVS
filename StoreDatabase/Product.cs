using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//构建数据对象

//数据对象是准备在用户界面中显示的信息包。只要由公有属性组成(不支持字段和私有属性)，任何类都可供使用。此外，如果希望使用这个对象进行修改
//(通过双向绑定)，那么属性不能是只读的











//更改通知

//(假设下面的实现目前是：
//public class Product
//{
//  private string modelNumber;
//  public string ModelNumber
//  {
//      get
//      {
//          return modelNumber;
//      }
//      set
//      {
//          modelNumber = value;
//      }
//  }
//}
//)

//Product绑定示例工作得很好，因为每个Product对象在本质上是固定不变的(用户在链接的文本框中编辑文本的情形除外)

//对于简单的情况，主要关注显示内容，并让用户编辑他们，这种行为是完全可以接受的。然而，很容易想到其他不同的情况，比如可能在代码中的其他
//地方对绑定的Product对象进行修改。
//
//例如，设想有一个Increase Price按钮，执行下面一行代码：
//product.UnitCost *= 1.1M;

//(注意：尽管可在数据上下文中检索Product对象，但该例假定您还将Product对象作为窗口类的成员变量进行存储，这样可简化代码并且需要的
//类型转换更少)

//当运行上面的代码时，将发现尽管Product对象已经发生了变化，但文本框中仍保留原来的数值。这是因为文本框无法了解到值已经变了
//可使用如下三种方法解决这个问题：
//1.将Product类中的每个属性都改为依赖项属性(对于这种情况，Product类必须继承自DependencyObject类)。尽管这种方法可使WPF自动执行
//相应的工作(这很好)，但最合理的做法是将其用于元素——在窗口中具有可视化外观的类。对于像Product这样的数据类，这并非最自然的方法
//2.可为每个属性引发事件。对于这种情况，事件必须以propertyNameChanged的形式进行命名(如UnitCostChanged)。当属性变化时，由您负责引发事件
//3.可实现System.ComponentModel.INotifyPropertyChanged接口，该接口需要名为PropertyChanged的事件。无论何时属性发生变化，都必须引发
//PropertyChanged事件，并且通过将属性名称作为字符串提供来指示哪个属性发生了变化。当属性发生变化时，仍由您负责引发事件，但不必为
//每个属性定义单独的事件

//第一种方法依赖于WPF的依赖项属性基础架构，而第二种和第三种方法依赖于事件。通常，当创建数据对象时，会使用第三种方法。对于非元素类而言，这是
//最简单的选择
//(注意，实际上，还可以使用另一种方法。如果怀疑绑定对象已经发生变化，并且绑定对象不支持任何恰当方式的更改通知，这时可检索BindingExpression对象
//使用FrameworkElement.GetBindingExpression()方法)，并调用BindingExpression.UpdateTarget()方法来触发更新。显然，这是最笨拙的解决方案

//下面重新规划Product类的定义，Product类现在使用INotifyPropertyChanged接口，并添加实现了PropertyChanged事件的代码：

namespace StoreDatabase
{
    public class Product : INotifyPropertyChanged
    {
        private string modelNumber;
        public string ModelNumber
        {
            get
            {
                return modelNumber;
            }
            set
            {
                modelNumber = value;

                //现在只需要在所有属性设置器中引发PropertyChanged事件即可
                OnPropertyChanged(new PropertyChangedEventArgs("ModelNumber"));

                //提示：
                //如果几个数值都发生了变化，可调用OnPropertyChanged()方法并传递空字符串，这会告诉WPF重新评估绑定到类的
                //任何属性的绑定表达式
            }
        }

        private string modelName;
        public string ModelName
        {
            get
            {
                return modelName;
            }
            set
            {
                modelName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("ModelName"));
            }
        }

        private decimal unitCost;
        public decimal UnitCost
        {
            get
            {
                return unitCost;
            }
            set
            {
                unitCost = value;
                OnPropertyChanged(new PropertyChangedEventArgs("UnitCost"));
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Description"));
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
            }
        }

        private int categoryID;
        public int CategoryID
        {
            get
            {
                return categoryID;
            }
            set
            {
                categoryID = value;
            }
        }

        private string productImagePath;
        public string ProductImagePath
        {
            get
            {
                return productImagePath;
            }
            set
            {
                productImagePath = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        public Product(string modelNumber, string modelName, decimal unitCost, string description)
        {
            ModelNumber = modelNumber;
            ModelName = modelName;
            UnitCost = unitCost;
            Description = description;
        }

        public Product(string modelNumber, string modelName, decimal unitCost, string description,
            string productImagePath)
            : this(modelNumber, modelName, unitCost, description)
        {
            ProductImagePath = productImagePath;
        }

        public Product(string modelNumber, string modelName, decimal unitCost, string description,
            int categoryID, string categoryName, string productImagePath)
            : this(modelNumber, modelName, unitCost, description)
        {
            CategoryName = categoryName;
            ProductImagePath = productImagePath;
            CategoryID = categoryID;
        }

        public override string ToString()
        {
            return ModelName + " (" + ModelNumber + ")";
        }

        // This for testing date editing. The value isn't actually stored in the database
        private DateTime dateAdded = DateTime.Today;
        public DateTime DateAdded
        {
            get
            {
                return dateAdded;
            }
            set
            {
                dateAdded = value;
            }
        }
    }
}
