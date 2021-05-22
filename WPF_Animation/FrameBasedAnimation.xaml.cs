using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Animation
{
    /// <summary>
    /// FrameBasedAnimation.xaml 的交互逻辑
    /// </summary>

    public partial class FrameBasedAnimation : System.Windows.Window
    {

        public FrameBasedAnimation()
        {
            InitializeComponent();
        }

        //当单击其中某个按钮时，清空集合，并将事件处理程序关联到CompositionTarget.Rendering事件
        private bool rendering = false;
        private void cmdStart_Clicked(object sender, RoutedEventArgs e)
        {
            if (!rendering)
            {
                ellipses.Clear();
                canvas.Children.Clear();

                CompositionTarget.Rendering += RenderFrame;
                rendering = true;
            }
        }
        private void cmdStop_Clicked(object sender, RoutedEventArgs e)
        {
            StopRendering();
        }

        private void StopRendering()
        {
            CompositionTarget.Rendering -= RenderFrame;
            rendering = false;
        }

        //应用程序使用集合跟踪每个椭圆的EllipseInfo对象，还有几个窗口级别的字段，他们记录计算椭圆下落时使用的各种细节
        private List<EllipseInfo> ellipses = new List<EllipseInfo>();

        private double accelerationY = 0.1;
        private int minStartingSpeed = 1;
        private int maxStartingSpeed = 50;
        private double speedRatio = 0.1;
        private int minEllipses = 20;
        private int maxEllipses = 100;
        private int ellipseRadius = 10;

        private void RenderFrame(object sender, EventArgs e)
        {
        //如果椭圆不存在，渲染代码会自动创建他们。渲染代码创建随机数量的椭圆(当前为20到100个)，并使他们具有相同的尺寸和颜色。
        //椭圆被放在Canvas面板的顶部，但他们沿着X轴随机移动
            if (ellipses.Count == 0)
            {
                // Animation just started. Create the ellipses.
                int halfCanvasWidth = (int)canvas.ActualWidth / 2;

                Random rand = new Random();
                int ellipseCount = rand.Next(minEllipses, maxEllipses + 1);
                for (int i = 0; i < ellipseCount; i++)
                {
                    Ellipse ellipse = new Ellipse();
                    ellipse.Fill = Brushes.LimeGreen;
                    ellipse.Width = ellipseRadius;
                    ellipse.Height = ellipseRadius;
                    Canvas.SetLeft(ellipse, halfCanvasWidth + rand.Next(-halfCanvasWidth, halfCanvasWidth));
                    Canvas.SetTop(ellipse, 0);
                    canvas.Children.Add(ellipse);

                    EllipseInfo info = new EllipseInfo(ellipse, speedRatio * rand.Next(minStartingSpeed, maxStartingSpeed));
                    ellipses.Add(info);
                }
            }
            else
            {
                //如果椭圆已经存在，代码处理更有趣的工作，以便进行动态显示。使用Canvas.SetTop()方法缓慢移动每个椭圆。移动距离取决于指定的速度
                for (int i = ellipses.Count - 1; i >= 0; i--)
                {
                    //为提高性能，一旦椭圆到达Canvas面板的底部，就从跟踪集合中删除椭圆。这样，就不需要再处理他们。当遍历集合时，为了能够工作而
                    //不会导致丢失位置，需要向后迭代，从集合的末尾向起始位置迭代
                    //如果椭圆尚未达到Canvas面板的底部，代码会提高速度(此外，为获得磁铁吸引效果，还可以根据椭圆与Canvas面板底部的距离来设置速度)

                    EllipseInfo info = ellipses[i];
                    double top = Canvas.GetTop(info.Ellipse);
                    Canvas.SetTop(info.Ellipse, top + 1 * info.VelocityY);

                    if (top >= (canvas.ActualHeight - ellipseRadius * 2 - 10))
                    {
                        // This circle has reached the bottom.
                        // Stop animating it.
                        ellipses.Remove(info);
                    }
                    else
                    {
                        // Increase the velocity.
                        info.VelocityY += accelerationY;
                    }

                    //最后，如果所有椭圆都已从集合中删除，就移除事件处理程序，然后结束动画：
                    if (ellipses.Count == 0)
                    {
                        // End the animation.
                        // There's no reason to keep calling this method
                        // if it has no work to do.
                        StopRendering();
                    }
                }
            }
        }
    }

    public class EllipseInfo
    {
        public Ellipse Ellipse
        {
            get; set;
        }

        public double VelocityY
        {
            get; set;
        }

        public EllipseInfo(Ellipse ellipse, double velocityY)
        {
            VelocityY = velocityY;
            Ellipse = ellipse;
        }
    }
}

//显然，可扩展这个动画以使圆跳跃和分散等。使用的技术是相同的——只是需要使用更复杂的公式计算速度

//当构建基于帧的动画时需要注意如下问题：他们不依赖于时间。换句话说，动画可能在性能好的计算机上运动得更快，因为帧率会增加，会更频繁地调用
//CompositionTarget.Rendering事件。为补偿这种效果，需要编写考虑当前时间的代码

//开始学习基于帧的动画的最好方式是查看WPF SDK提供的每一帧动画都非常详细的示例(本章的示例代码也提供了该例)。该例演示了几种粒子系统效果，并且
//使用自定义的TimeTracker类实现了依赖于时间的基于帧的动画
