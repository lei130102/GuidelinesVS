using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace CustomBehaviorsLibrary
{
    //创建行为

    //提供使用鼠标在Canvas面板上拖动元素的功能(将该功能转为可重用的行为，可为Canvas面板上的所有元素提供拖动支持)
    //原理：代码监听鼠标事件并修改设置相应Canvas坐标的附加属性

    //创建行为步骤:(注意，在理想情况下，不必自己创建行为，而是使用其他人已经创建好的行为)
    //1.创建一个WPF类库程序集(CustomBehaviorsLibrary)，添加对System.Windows.Interactivity.dll程序集的引用
    //2.然后创建一个继承自Behavior<T>基类的类DragInCanvasBehavior。并将行为限制到特定的元素(T)(或使用UIElement或者FrameworkElement将他们都包含进来)
    //3.覆盖OnAttached()和OnDetaching()方法。当调用OnAttached()方法时，可通过AssociatedObject属性访问放置行为的元素，并可关联事件处理程序。当调用OnDetaching()方法时，移除事件处理程序

    public class DragInCanvasBehavior : Behavior<UIElement>
    {
        private Canvas canvas;

        protected override void OnAttached()
        {
            base.OnAttached();

            // Hook up event handlers.
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp += AssociatedObject_MouseLeftButtonUp;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            // Detach event handlers.
            this.AssociatedObject.MouseLeftButtonDown -= AssociatedObject_MouseLeftButtonDown;
            this.AssociatedObject.MouseMove -= AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeftButtonUp -= AssociatedObject_MouseLeftButtonUp;
        }

        /// <summary>
        /// Keep track of when the element is being dragged.
        /// </summary>
        private bool isDragging = false;

        /// <summary>
        /// When the element is clicked, record the exact position where the click is made.
        /// </summary>
        private Point mouseOffset;

        private void AssociatedObject_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Find the canvas.
            if(canvas == null)
            {
                canvas = VisualTreeHelper.GetParent(this.AssociatedObject) as Canvas;
            }

            //Dragging mode begins.
            isDragging = true;

            // Get the position of the click relative to the element
            // (so the top-left corner of the element is (0,0))
            mouseOffset = e.GetPosition(AssociatedObject);

            // Capture the mouse. This way you'll keep receiveing the MouseMove event even if the user jerks the mouse
            // off the element.
            AssociatedObject.CaptureMouse();
        }

        private void AssociatedObject_MouseMove(object sender, MouseEventArgs e)
        {
            if(isDragging)
            {
                // Get the position of the element relative to the Canvas.
                Point point = e.GetPosition(canvas);

                //Move the element.
                AssociatedObject.SetValue(Canvas.TopProperty, point.Y - mouseOffset.Y);
                AssociatedObject.SetValue(Canvas.LeftProperty, point.X - mouseOffset.X);
            }
        }

        private void AssociatedObject_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(isDragging)
            {
                AssociatedObject.ReleaseMouseCapture();
                isDragging = false;
            }
        }
    }
}
