using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_DependencyProperty.AttachedDependencyPropertyExample
{
    /// <summary>
    /// 附加的依赖项属性也是依赖项属性，因为他可以被用于任何DependencyObject对象，所以附加的依赖项属性不需要有属性封装器，进而附加的依赖项属性所属对象类型不必派生自DependencyObject或其子类
    /// 
    /// 与依赖项属性不同的是，附加的依赖项属性被应用到的类并非定义附加属性的那个类
    /// </summary>
    public class MyClass   //如果成员都是静态的，那么也可以是静态类
    {
        #region MyAttachedDependencyPropertyProperty附加的依赖项属性
        public static readonly DependencyProperty MyAttachedDependencyPropertyProperty =
            DependencyProperty.RegisterAttached(     //使用RegisterAttached定义附加的依赖项属性
                "MyAttachedDependencyProperty"
                , typeof(string)                    
                , typeof(MyClass));      

        //对于附加的依赖项属性，不必定义属性封装器，反而需要调用两个静态方法来设置和获取属性值
        public static string GetMyAttachedDependencyProperty(DependencyObject dependencyObject)
        {
            if(dependencyObject == null)
            {
                throw new ArgumentNullException();
            }
            return (string)dependencyObject.GetValue(MyAttachedDependencyPropertyProperty);
        }
        public static void SetMyAttachedDependencyProperty(DependencyObject dependencyObject, string value)
        {
            if(dependencyObject == null)
            {
                throw new ArgumentNullException();
            }
            dependencyObject.SetValue(MyAttachedDependencyPropertyProperty, value);
        }
        #endregion

        #region MyAttachedDependencyPropertyWithTypeMetadata附加的依赖项属性，提供PropertyMetadata
        public static readonly DependencyProperty MyAttachedDependencyPropertyWithTypeMetadataProperty =
            DependencyProperty.RegisterAttached(
                "MyAttachedDependencyPropertyWithTypeMetadata"
                , typeof(int)
                , typeof(MyClass)
                , new PropertyMetadata(
                    0                                                           
                    , (dependencyObject, dependencyPropertyChangedEventArgs) => {  
                        //...
                    }
                    , (dependencyObject, baseValue) => {                           
                        //...
                        return baseValue;
                    }));

        public static int GetMyAttachedDependencyPropertyWithTypeMetadata(DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException();
            }
            return (int)dependencyObject.GetValue(MyAttachedDependencyPropertyWithTypeMetadataProperty);
        }
        public static void SetMyAttachedDependencyPropertyWithTypeMetadata(DependencyObject dependencyObject, int value)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException();
            }
            dependencyObject.SetValue(MyAttachedDependencyPropertyWithTypeMetadataProperty, value);
        }
        #endregion

        #region MyAttachedDependencyPropertyWithTypeMetadataAndValidateValueCallbackProperty附加的依赖项属性，提供PropertyMetadata和ValidateValueCallback
        public static readonly DependencyProperty MyAttachedDependencyPropertyWithTypeMetadataAndValidateValueCallbackProperty =
            DependencyProperty.Register(
                "MyAttachedDependencyPropertyWithTypeMetadataAndValidateValueCallback"
                , typeof(int)
                , typeof(MyClass)
                , new PropertyMetadata(
                    0
                    , (dependencyObject, dependencyPropertyChangedEventArgs) => {
                        //...
                    }
                    , (dependencyObject, baseValue) => {
                        //...
                        return baseValue;
                    })
                , value => {                                  
                    //...
                    return true;
                });
        public static int GetMyAttachedDependencyPropertyWithTypeMetadataAndValidateValueCallback(DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException();
            }
            return (int)dependencyObject.GetValue(MyAttachedDependencyPropertyWithTypeMetadataAndValidateValueCallbackProperty);
        }
        public static void SetMyAttachedDependencyPropertyWithTypeMetadataAndValidateValueCallback(DependencyObject dependencyObject, int value)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException();
            }
            dependencyObject.SetValue(MyAttachedDependencyPropertyWithTypeMetadataAndValidateValueCallbackProperty, value);
        }
        #endregion
    }
}
