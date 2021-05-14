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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_CompositionTargetRenderingAnimations.SimpleExample
{
    /// <summary>
    /// SimpleExample.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleExample : Page
    {
        Random rand = new Random();

        public SimpleExample()
            : base()
        {
            InitializeComponent();

            CompositionTarget.Rendering += UpdateColor;
        }

        protected void UpdateColor(object sender, EventArgs e)
        {
            // Set a random color
            animatedBrush.Color = Color.FromRgb((byte)rand.Next(255), (byte)rand.Next(255), (byte)rand.Next(255));
        }
    }
}
