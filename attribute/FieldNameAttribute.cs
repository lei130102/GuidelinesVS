using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace attribute
{
    //定义一个以Attribute结尾、派生自Attribute的类，从而定义 自定义特性
    [AttributeUsage(
        AttributeTargets.Property    //必需 用于标识自定义特性可以应用到哪些类型的程序元素上，这里是指应用到属性上，可以用|分隔多个，但有两个值不对应于任何元素要素：Assembly和Module，他们可以放在源代码的任何地方，但需要用关键字Assembly或者Module作为前缀：[assembly:SomeAssemblyAttribute(Parameters)] [module:SomeAssemblyAttribute(Parameters)]
        , AllowMultiple = false      //可选 是否可以多次应用到同一项上
        , Inherited = false          //可选 是否要应用到类或者接口上的特性也可以自动应用到所有派生的类或接口上，要应用到方法或者属性上也可以自动应用到该方法或者属性等的重写版本上
        )]
    public class FieldNameAttribute : Attribute
    {
        private string _name;

        public FieldNameAttribute(string name)
        {
            _name = name;
        }

        public string Comment { get; set; }
    }


    //使用之前定义的 FieldNameAttribute
    public class Test
    {
        //查找名为 FieldNameAttribute 的类
        //SocialSecurityNumber 是特性参数，必需，必须有对应的构造函数
        [FieldName("SocialSecurityNumber")]   //这里一般写 FieldName ，不写为 FieldNameAttribute
        public string SocialSecurityNumber { get; set; }

        //Comment = "This is the primary key field" 是特性可选参数，可选，不必有对应的构造函数，但要有对应的public属性或字段
        [FieldName("SocialSecurityName", Comment = "This is the primary key field")]
        public string SocialSecurityName { get; set; }
    }
}
