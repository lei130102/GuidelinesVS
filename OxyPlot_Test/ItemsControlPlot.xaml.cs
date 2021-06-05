using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public class PlotModelInfo
    {
        public string Title { get; set; }
        public PlotModel Model { get; set; }
    }

    public class ItemsControlPlotViewModel
    {
        public ItemsControlPlotViewModel()
        {
            PlotModelList = new ObservableCollection<PlotModelInfo>();
        }

        public ObservableCollection<PlotModelInfo> PlotModelList { get; private set; }

        public void execute()
        {
            for(int i = 0; i < 100; ++i)
            {
                {
                    PlotModelInfo modelInfo = new PlotModelInfo();
                    modelInfo.Title = "Example " + i;
                    modelInfo.Model = new PlotModel { Title = "Example " + i };
                    modelInfo.Model.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
                    PlotModelList.Add(modelInfo);
                }
            }
        }
    }

    /// <summary>
    /// ItemsControlPlot.xaml 的交互逻辑
    /// </summary>
    public partial class ItemsControlPlot : Window
    {
        public ItemsControlPlot()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.execute();
        }
    }
}
