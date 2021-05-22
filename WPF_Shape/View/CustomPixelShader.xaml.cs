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
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_Shape.View
{
    public class GrayscaleEffect : ShaderEffect
    {
        public GrayscaleEffect()
        {
            Uri pixelShaderUri = new Uri("pack://application:,,,/View/Grayscale_Compiled.ps", UriKind.Absolute);

            // Load the information from the .ps file.
            PixelShader = new PixelShader();
            PixelShader.UriSource = pixelShaderUri;

            UpdateShaderValue(InputProperty);
        }

        //如果使用采用特定输入参数的像素着色器，需要做的工作比上面的示例要更复杂一点。对于这种情况，需要通过调用RegisterPixelShaderSamplerProperty()
        //静态方法创建相应的依赖项属性
        public static readonly DependencyProperty InputProperty =
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(GrayscaleEffect), 0 /* assigned to sampler register S0 */);

        public Brush Input
        {
            get
            {
                return (Brush)GetValue(InputProperty);
            }
            set
            {
                SetValue(InputProperty, value);
            }
        }
    }
    /// <summary>
    /// CustomPixelShader.xaml 的交互逻辑
    /// </summary>
    public partial class CustomPixelShader : Window
    {
        public CustomPixelShader()
        {
            InitializeComponent();
        }

        private void chkEffect_Click(object sender, RoutedEventArgs e)
        {
            if(chkEffect.IsChecked != true)
            {
                img.Effect = null;
            }
            else
            {
                img.Effect = new GrayscaleEffect();
            }
        }
    }
}
