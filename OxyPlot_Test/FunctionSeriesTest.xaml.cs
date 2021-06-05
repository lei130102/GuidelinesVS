using OxyPlot;
using OxyPlot.Series;
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

namespace OxyPlot_Test
{
    public class FunctionSeriesTestViewModel
    {
        public FunctionSeriesTestViewModel()
        {
            this.MyPlotModel = new PlotModel { Title = "Example 1" };
            this.MyPlotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }

        public PlotModel MyPlotModel { get; private set; }
    }

    /// <summary>
    /// FunctionSeriesTest.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionSeriesTest : Window
    {
        public FunctionSeriesTest()
        {
            InitializeComponent();
        }
    }
}
