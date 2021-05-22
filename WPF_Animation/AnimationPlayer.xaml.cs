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
    /// <summary>
    /// AnimationPlayer.xaml 的交互逻辑
    /// </summary>
    public partial class AnimationPlayer : Window
    {
        public AnimationPlayer()
        {
            InitializeComponent();
        }



        //监视动画进度
        //为使这个动画播放器更加精致，可添加一些文本来显示时间的流逝，并添加进度条来指示动画执行的进度
        //添加这些细节相当简单。首先需要使用TextBlock元素显示时间，而后需要使用ProgressBar控件显示图形进度条。您可能认为，可使用数据绑定表达式设置
        //TextBlock值和ProgressBar内容，但这是行不通的。因为从故事板中检索当前动画时钟相关信息的唯一方法是使用方法，如GetCurrentTime()和GetCurrentProgress()。
        //无法从属性中获取相同的信息

        //最简单的解决方法是响应下面列出的某个故事板事件
        //Completed                                    动画已经到达终点
        //CurrentGlobalSpeedInvalidated                速度发生了变化，或者动画被暂停、重新开始、停止或移到某个新的位置。当动画时钟反转时(在可反转动画的终点)
        //                                             以及当动画加速和减速时，也会引发该事件
        //CurrentStateInvalidated                      动画已经开始或结束
        //CurrentTimeInvalidated                       动画时钟已经向前移动了一个步长，正在更改动画。当动画开始、停止或结束时也会引发该事件
        //RemoveRequested                              动画正在被移除。使用动画的属性随后会返回为原来的值

        //本实例需要使用CurrentTimeInvalidated事件，每次向前移动动画时钟时都会引发该事件(通常，每秒60次，但如果执行的代码需要更长时间，可能会丢失时钟刻度)

        //当引发CurrentTimeInvalidated事件时，发送者是Clock对象(Clock类位于System.Windows.Media.Animation命名空间)。可以通过Clock对象检索当前时间，当前时间使用
        //TimeSpan对象表示；并且可检索当前进度，当前进度使用0~1之间的数值表示

        private void fadeStoryboard_CurrentTimeInvalidated(object sender, EventArgs e)
        {
            // Sender is the clock that was created for this storyboard.
            Clock storyboardClock = (Clock)sender;

            if(storyboardClock.CurrentProgress == null)
            {
                lblTime.Text = "[[ stopped ]]";
                progressBar.Value = 0;
            }
            else
            {
                lblTime.Text = storyboardClock.CurrentTime.ToString();
                progressBar.Value = (double)storyboardClock.CurrentProgress;
            }

            //如果使用Clock.CurrentProgress属性，就不必为确定进度条的属性值而执行任何计算。相反，可简单地使用最小值0和最大值1配置进度条。这样，就可以很容易地
            //使用Clock.CurrentProgress属性值来设置ProgressBar.Value
        }

        //当拖动Slider控件上的滑块时，下面的事件处理程序会进行响应。该事件处理程序获取滑动条的值(范围是0~3)，并使用该数值应用新的速率
        private void sldSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //SetSpeedRatio()
            //方法需要两个参数
            //第一个参数是顶级动画容器(在本例中是当前窗口)，所有故事板方法都需要这个引用
            //第二个参数是新的速率
            fadeStoryboard.SetSpeedRatio(this, sldSpeed.Value);
        }
    }
}
