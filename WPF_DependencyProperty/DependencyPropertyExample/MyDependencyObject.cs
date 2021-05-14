using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

/*
 
DispatcherObject
      |
DependencyObject


 */

namespace WPF_DependencyProperty.DependencyPropertyExample
{
    /// <summary>
    /// 依赖项属性需要有.NET属性封装器，而.NET属性封装器需要调用DependencyObject.GetValue和DependencyObject.SetValue非静态成员函数，所以依赖项属性所属对象类型必须派生自DependencyObject或其子类
    ///
    /// 附加的依赖项属性也是依赖项属性，因为他可以被用于任何DependencyObject对象，所以附加的依赖项属性不需要有属性封装器，进而附加的依赖项属性所属对象类型不必派生自DependencyObject或其子类
    /// </summary>
    public class MyDependencyObject : DependencyObject
    {
        #region MyDependencyProperty依赖项属性
        /// <summary>
        /// 1.定义依赖项属性MyDependencyPropertyProperty
        /// 要求：
        /// public + static + readonly + DependencyProperty类型字段 + 字段名以Property结尾(这是约定)
        ///
        /// 2.注册依赖项属性
        /// 定义处注册或者在静态构造函数中注册
        /// </summary>
        public static readonly DependencyProperty MyDependencyPropertyProperty = 
            DependencyProperty.Register(
                "MyDependencyProperty"              //属性名
                , typeof(string)                    //属性使用的数据类型
                , typeof(MyDependencyObject));      //拥有该属性的类型

        /// <summary>
        /// 3.添加属性包装器
        /// 注意，不要在这里添加其他额外代码，比如验证数据或者引发事件，因为WPF中的其他功能可能会忽略属性封装器
        /// </summary>
        public string MyDependencyProperty
        {
            get
            {
                return (string)GetValue(MyDependencyPropertyProperty); //GetValue函数是DependencyObject类的成员函数，可见只有派生自DependencyObject类的对象才能有依赖项属性
            }
            set
            {
                SetValue(MyDependencyPropertyProperty, value);         //SetValue函数是DependencyObject类的成员函数，可见只有派生自DependencyObject类的对象才能有依赖项属性
            }
        }
        #endregion

        #region MyDependencyPropertyWithTypeMetadata依赖项属性，提供PropertyMetadata
        public static readonly DependencyProperty MyDependencyPropertyWithTypeMetadataProperty =
            DependencyProperty.Register(
                "MyDependencyPropertyWithTypeMetadata"
                , typeof(int)
                , typeof(MyDependencyObject)
                , new PropertyMetadata(
                    0                                                                //DefaultValue：为依赖项属性设置默认值
                    , (dependencyObject, dependencyPropertyChangedEventArgs) => {    //PropertyChangedCallback：设置回调函数，当依赖项属性的值发生变化时调用该回调函数
                        //...
                    }
                    , (dependencyObject, baseValue) => {                             //CoerceValueCallback：设置回调函数，用于在验证依赖项属性之前尝试“纠正”属性值
                        //...
                        return baseValue;
                    }));

        public int MyDependencyPropertyWithTypeMetadata
        {
            get
            {
                return (int)GetValue(MyDependencyPropertyWithTypeMetadataProperty);
            }
            set
            {
                SetValue(MyDependencyPropertyWithTypeMetadataProperty, value);
            }
        }
        #endregion

        #region MyDependencyPropertyWithTypeMetadataAndValidateValueCallbackProperty依赖项属性，提供PropertyMetadata和ValidateValueCallback
        public static readonly DependencyProperty MyDependencyPropertyWithTypeMetadataAndValidateValueCallbackProperty =
            DependencyProperty.Register(
                "MyDependencyPropertyWithTypeMetadataAndValidateValueCallback"
                , typeof(int)
                , typeof(MyDependencyObject)
                , new PropertyMetadata(
                    0
                    , (dependencyObject, dependencyPropertyChangedEventArgs) => {
                        //...
                    }
                    , (dependencyObject, baseValue) => {
                        //...
                        return baseValue;
                    })
                , value => {                                     //ValidateValueCallback：设置回调函数，验证依赖属性的值是否有效
                    //...
                    return true;
                });
        public int MyDependencyPropertyWithTypeMetadataAndValidateValueCallback
        {
            get
            {
                return (int)GetValue(MyDependencyPropertyWithTypeMetadataAndValidateValueCallbackProperty);
            }
            set
            {
                SetValue(MyDependencyPropertyWithTypeMetadataAndValidateValueCallbackProperty, value);
            }
        }
        #endregion

        //依赖项属性的两个特性：

        ////1.更改通知
        //当依赖性属性发生变化时：
        //a.通过数据绑定传递信息
        //b.通过触发器传递信息
        //c.调用PropertyMetadata中的PropertyChangedCallback回调函数(如果已经定义了该函数)
        //所以当依赖项属性变化时，如果希望进行响应，那么：
        //a.使用依赖项属性创建绑定
        //b.编写能够自动改变其他属性或开始动画的触发器
        //c.提供PropertyChangedCallback回调函数

        ////2.动态值识别(依赖项属性因该行为而得名——本质上，依赖项属性依赖于多个属性提供者，每个提供者都有各自的优先级。当从属性检索值时，WPF属性系统会通过一系列步骤获取最终值)
        //WPF决定依赖项属性值的过程：
        //a.确定基本值
        //  a1.默认值(由PropertyMetadata提供)
        //  a2.继承而来的值
        //  a3.来自主题样式的值
        //  a4.来自项目样式的值
        //  a5.本地值(使用代码或者XAML直接为对象设置的值)
        //b.如果属性是使用表达式设置的，就对表达式进行求值。当前，WPF支持两类表达式：数据绑定和资源
        //c.如果属性是动画的目标，就应用动画
        //d.运行CoerceValueCallback回调函数来修正属性值

        //WPF提供了两种方法来阻止非法值：
        //(不能通过属性封装器的set来阻止，因为WPF属性系统可能不会调用属性封装器的set而直接调用SetValue方法直接设置)
        //1.ValidateValueCallback：该回调函数可接受或拒绝新值。通常，该回调函数用于捕获违反属性约束的明显错误
        //2.CoerceValueCallback：该回调函数可将新值修改为更能被接受的值。该回调函数通常用于处理为相同对象设置的依赖项属性值相互冲突的问题
        //作用过程：
        //a.首先，CoerceValueCallback方法有机会修改提供的值(通常，使提供的值和其他属性相容)，或者返回DependencyProperty.UnsetValue，这会完全拒绝修改
        //b.接下来激活ValidateValueCallback方法。该方法返回true以接受一个值作为合法值，或者返回false拒绝值(与CoerceValueCallback方法不同，ValidateValueCallback方法不能访问设置属性的实际对象，这意味着不能检查其他属性值)
        //c.最后，如果前两个阶段都获得成功，就会触发PropertyChangedCallback方法。此时，如果希望为其他类提供通知，可以引发更改事件
    }

    /// <summary>
    /// 在MySharedDependencyObject类，共享MyDependencyObject定义的依赖项属性MyDependencyPropertyProperty
    /// 
    /// (如果MySharedDependencyObject派生自MyDependencyObject，那么直接重用MyDependencyProperty即可)
    /// </summary>
    public class MySharedDependencyObject : DependencyObject
    {
        public static readonly DependencyProperty MyDependencyPropertyProperty =
            MyDependencyObject.MyDependencyPropertyProperty.AddOwner(typeof(MySharedDependencyObject)); //AddOwner还有几个重载函数，可以在共享的基础上再添加新的PropertyMetadata等

        public string MyDependencyProperty
        {
            get
            {
                return (string)GetValue(MyDependencyPropertyProperty); 
            }
            set
            {
                SetValue(MyDependencyPropertyProperty, value);         
            }
        }
    }
}
