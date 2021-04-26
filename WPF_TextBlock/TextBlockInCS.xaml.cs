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

namespace WPF_TextBlock
{
    /// <summary>
    /// TextBlockInCS.xaml 的交互逻辑
    /// </summary>
    public partial class TextBlockInCS : Window
    {
        public TextBlockInCS()
        {
            InitializeComponent();

            TextBlock tb = new TextBlock();
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Margin = new Thickness(10); //注意这里new什么对象
            tb.Inlines.Add("An example on ");
            tb.Inlines.Add(new Run("the TextBlock control") { FontWeight = FontWeights.Bold });
            tb.Inlines.Add("using");
            tb.Inlines.Add(new Run("inline ") { FontStyle = FontStyles.Italic });
            tb.Inlines.Add(new Run("text formatting ") { Foreground = Brushes.Blue });
            tb.Inlines.Add("from ");
            tb.Inlines.Add(new Run("Code-Behind") { TextDecorations = TextDecorations.Underline });
            tb.Inlines.Add(".");
            this.Content = tb;
        }
    }
}
