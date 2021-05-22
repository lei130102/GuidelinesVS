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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Animation
{
    public class RandomJitterEase : EasingFunctionBase
    {
        // Store a random number generator.
        private Random rand = new Random();

        /// <summary>
        /// This required override simply provides a live instance of your easing function.
        /// </summary>
        /// <returns></returns>
        protected override Freezable CreateInstanceCore()
        {
            return new RandomJitterEase();
        }

        //需要编写的几乎所有逻辑都在EaseInCore()方法中运行。该方法接受一个规范化的时间值——本质上，是表示动画进度的从0到1之间的值。
        //当动画开始时，规范化的时间值是0.他从该点开始增加，直到在动画结束点达到1

        //在动画运行期间，每次更新动画的值时WPF都会调用EaseInCore()方法。确切的调用频率取决于动画的帧频，但可以预期每秒调用EaseInCore()方法
        //的次数接近60

        //为执行缓动，EaseInCore()方法采用规范化的时间值，并以某种方式对其进行调整。EaseInCore()方法返回的调整后的值，随后被用于调整动画的进度。
        //例如
        //如果EaseInCore()方法返回0，动画被返回到其开始点。
        //如果EaseInCore()方法返回1，动画跳到其结束点
        //然而，EaseInCore()方法的返回值并不局限于这一范围——例如，可返回1.5以使动画过渡运行自身50%
        
        //如果仅仅 return normalizedTime;那么没有缓动

        //CubicEase的等价效果是
        //return Math.Pow(normalizedTime, 3);
        //因为规范化的时间值是小数，其立方值是更小的小数；所以该方法的效果是最初减慢动画，并当规范化的时间值(及其立方值)接近于1时导致动画加速

        //注意，在EaseInCore()方法中执行的缓动是当使用EaseIn缓动模式时得到的缓动。有趣的是，这就是所需的全部工作，因为WPF足够智能，他会自动为
        //EaseOut和EaseInOut设置计算互补的行为

        //如果希望查看当动画运行时计算出的缓动值，可在EaseInCore()方法中使用System.Diagnostics.Debug类的WriteLine()方法
        //当在Visual Studio中调试应用程序时，该方法会将您提供的值写入到Output窗口中
        protected override double EaseInCore(double normalizedTime)
        {
            // To see the values add code like this:
            // System.Diagnostics.Debug.WriteLine(...);

            // Make sure there's no jitter in the final value.
            if(normalizedTime == 1)
            {
                return 1;
            }

            // Offset the value by a random amount.
            return Math.Abs(normalizedTime - (double)rand.Next(0, 10) / (2010 - Jitter));
        }

        public int Jitter
        {
            get
            {
                return (int)GetValue(JitterProperty);
            }
            set
            {
                SetValue(JitterProperty, value);
            }
        }

        public static readonly DependencyProperty JitterProperty =
            DependencyProperty.Register("Jitter", typeof(int), typeof(RandomJitterEase), new UIPropertyMetadata(1000), new ValidateValueCallback(ValidateJitter));

        private static bool ValidateJitter(object value)
        {
            int jitterValue = (int)value;
            return ((jitterValue <= 2000) && (jitterValue >= 0));
        }
    }

    /// <summary>
    /// CustomEasingFunction.xaml 的交互逻辑
    /// </summary>
    public partial class CustomEasingFunction : Window
    {
        public CustomEasingFunction()
        {
            InitializeComponent();
        }
    }
}
