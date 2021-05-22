using Newtonsoft.Json.Linq;
using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public delegate bool TSDPointClusterTestEvaluationIndexCalculation([In][MarshalAs(UnmanagedType.LPStr)] string inputJson, [In][Out][MarshalAs(UnmanagedType.LPStr)] StringBuilder outputJson, [In][Out][MarshalAs(UnmanagedType.SysUInt)] ref UIntPtr outputJsonLength);

    struct StandardData
    {
        public double Salinity;
        public double Watertemp;
    }

    struct TestData
    {
        public double Salinity;
        public double Watertemp;
        public double Depth;
    }

    class TestDataConvexHull
    {
        public double Depth { get; set; }
        public List<int> Indices { get; set; }
        public List<double> MinEllipse { get; set; }

        public TestDataConvexHull()
        {
            Indices = new List<int>();
            MinEllipse = new List<double>();
        }
    }

    /// <summary>
    /// ScatterDiagram.xaml 的交互逻辑
    /// </summary>
    public partial class ScatterDiagram : Window
    {
        public ScatterDiagram()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<StandardData> standardData;
            TestData testData;

            //准备数据
            PrepareData(out standardData, out testData);

            double minimumX = Double.MaxValue;
            double maximumX = Double.MinValue;
            double minimumY = Double.MaxValue;
            double maximumY = Double.MinValue;
            for(int i = 0; i < standardData.Count; ++i)
            {
                if(standardData[i].Salinity < minimumX)
                {
                    minimumX = standardData[i].Salinity;
                }

                if(standardData[i].Salinity > maximumX)
                {
                    maximumX = standardData[i].Salinity;
                }

                //

                if(standardData[i].Watertemp < minimumY)
                {
                    minimumY = standardData[i].Watertemp;
                }

                if(standardData[i].Watertemp > maximumY)
                {
                    maximumY = standardData[i].Watertemp;
                }
            }

            if(testData.Salinity < minimumX)
            {
                minimumX = testData.Salinity;
            }
            if (testData.Salinity > maximumX)
            {
                maximumX = testData.Salinity;
            }

            //

            if (testData.Watertemp < minimumY)
            {
                minimumY = testData.Watertemp;
            }

            if (testData.Watertemp > maximumY)
            {
                maximumY = testData.Watertemp;
            }

            //plotView.Model = new OxyPlot.PlotModel { Title = "T-S-D 点聚图", IsLegendVisible = true, LegendBackground = OxyColor.FromRgb(255, 255, 255), Background = OxyColor.FromRgb(232, 233, 255) };
            plotView.Model = new OxyPlot.PlotModel { Title = "T-S-D 点聚图", IsLegendVisible = true };
            plotView.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Bottom, Title = "盐度", Minimum = minimumX - 10, Maximum = maximumX + 10 });
            plotView.Model.Axes.Add(new LinearAxis() { Position = AxisPosition.Left, Title = "温度", Minimum = minimumY - 10, Maximum = maximumY + 10 });

            plotView.Model.Series.Clear();

            //标准数据
            {
                ScatterSeries ss = new ScatterSeries() { Title = "标准数据", MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0, 0, 255) };
                for(int i = 0; i < standardData.Count; ++i)
                {
                    ss.Points.Add(new ScatterPoint(standardData[i].Salinity, standardData[i].Watertemp));
                }
                plotView.Model.Series.Add(ss);
            }

            //测试数据
            {
                ScatterSeries ss = new ScatterSeries() { Title = "被检数据", MarkerType = MarkerType.Diamond, MarkerFill = OxyColor.FromRgb(255, 0, 0) };
                ScatterPoint pt = new ScatterPoint(testData.Salinity, testData.Watertemp);
                pt.Tag = "深度：" + testData.ToString();
                ss.Points.Add(pt);

                plotView.Model.Series.Add(ss);
            }

            double n;
            List<int> standardDataConvexHullIndices;
            List<TestDataConvexHull> testDataConvexHullIndices;
            calculationConvexHull(standardData, testData, out n, out standardDataConvexHullIndices, out testDataConvexHullIndices);

            double Xc;
            double Yc;
            double dipAngle;
            double a2;
            double b2;
            calculationEllipse(
                testDataConvexHullIndices[0].MinEllipse[0]
                , testDataConvexHullIndices[0].MinEllipse[1]
                , testDataConvexHullIndices[0].MinEllipse[2]
                , testDataConvexHullIndices[0].MinEllipse[3]
                , testDataConvexHullIndices[0].MinEllipse[4]
                , testDataConvexHullIndices[0].MinEllipse[5]
                , out Xc
                , out Yc
                , out dipAngle
                , out a2
                , out b2
                );

            PolylineAnnotation polylineAnnotation = new PolylineAnnotation();
            for(int i = 0; i < standardDataConvexHullIndices.Count; ++i)
            {
                polylineAnnotation.Points.Add(new DataPoint(
                    standardData[standardDataConvexHullIndices[i]].Salinity
                    ,
                    standardData[standardDataConvexHullIndices[i]].Watertemp
                    ));
            }
            //为了闭合
            polylineAnnotation.Points.Add(new DataPoint(
                standardData[standardDataConvexHullIndices[0]].Salinity
                ,
                standardData[standardDataConvexHullIndices[0]].Watertemp
                ));

            plotView.Model.Annotations.Add(polylineAnnotation);

            //斜椭圆的参数方程
            //x = a * cost * cosθ - b * sint * sinθ + X,
            //y = a * cost * sinθ + b * sint * cosθ + Y.
            //θ为椭圆倾斜角,
            //a,b分别为长、短半轴；
            //t为参数

            {
                PolygonAnnotation polygonAnnotation = new PolygonAnnotation();
                polygonAnnotation.Fill = OxyColor.FromArgb(0, 0, 0, 0);
                polygonAnnotation.Stroke = OxyColor.FromArgb(255, 255, 0, 0);
                polygonAnnotation.StrokeThickness = 1;
                for (double angle = -1 * Math.PI; angle < Math.PI; angle += 0.001)
                {
                    DataPoint point = new DataPoint(
                        Math.Sqrt(a2) * Math.Cos(angle) * Math.Cos(dipAngle) - Math.Sqrt(b2) * Math.Sin(angle) * Math.Sin(dipAngle) + Xc,
                        Math.Sqrt(a2) * Math.Cos(angle) * Math.Sin(dipAngle) + Math.Sqrt(b2) * Math.Sin(angle) * Math.Cos(dipAngle) + Yc
                        );
                    polygonAnnotation.Points.Add(point);
                }
                plotView.Model.Annotations.Add(polygonAnnotation);

            }


            {
                PolygonAnnotation polygonAnnotation = new PolygonAnnotation();
                polygonAnnotation.Fill = OxyColor.FromArgb(0, 0, 0, 0);
                polygonAnnotation.Stroke = OxyColor.FromArgb(255, 0, 255, 0);
                polygonAnnotation.StrokeThickness = 1;
                for (double angle = -1 * Math.PI; angle < Math.PI; angle += 0.001)
                {
                    DataPoint point = new DataPoint(
                        Math.Sqrt(b2) * Math.Cos(angle) * Math.Cos(dipAngle) - Math.Sqrt(a2) * Math.Sin(angle) * Math.Sin(dipAngle) + Xc,
                        Math.Sqrt(b2) * Math.Cos(angle) * Math.Sin(dipAngle) + Math.Sqrt(a2) * Math.Sin(angle) * Math.Cos(dipAngle) + Yc
                        );
                    polygonAnnotation.Points.Add(point);
                }
                plotView.Model.Annotations.Add(polygonAnnotation);
            }

            //EllipseAnnotation ellipseAnnotation = new EllipseAnnotation();
            //ellipseAnnotation.Fill = OxyColor.FromArgb(0, 0, 0, 0);
            //ellipseAnnotation.Stroke = OxyColor.FromArgb(255, 255, 0, 0);
            //ellipseAnnotation.StrokeThickness = 1;
            //ellipseAnnotation.X = Xc;
            //ellipseAnnotation.Y = Yc;
            //ellipseAnnotation.Width = Math.Sqrt(a2) * 2;
            //ellipseAnnotation.Height = Math.Sqrt(b2) * 2;
            //plotView.Model.Annotations.Add(ellipseAnnotation);


            plotView.Model.InvalidatePlot(true);
        }

        void PrepareData(out List<StandardData> standardData, out TestData testData)
        {
            Random random = new Random();
            standardData = new List<StandardData>();
            double salinity = random.NextDouble() * 5 + 20;
            double waterTemp = random.NextDouble() * 5 + 20;
            double salinityInc;
            double waterTempInc;
            for (int i = 0; i < 100; ++i)
            {
                StandardData item = new StandardData() {
                    Salinity = salinity,
                    Watertemp = waterTemp
                };
                salinityInc = random.NextDouble() * 6 - 3;
                waterTempInc = random.NextDouble() * 6 - 3;
                salinity += salinityInc;
                waterTemp += waterTempInc;
                standardData.Add(item);
            }


            testData = new TestData()
            {
                Salinity = random.Next(15, 30),
                Watertemp = random.Next(15, 30),
                Depth = random.Next(0, 3000)
            };
        }

        void calculationEllipse(double r, double s, double t, double u, double v, double w, out double Xc, out double Yc, out double dipAngle, out double a2, out double b2)
        {
            //rx ^ 2 + sy ^ 2 + txy + ux + vy + w = 0
            //Ax ^ 2 + Cy ^ 2 + Bxy + Dx + Ey + 1 = 0
            double A = r / w;
            double B = t / w;
            double C = s / w;
            double D = u / w;
            double E = v / w;

            //椭圆几何中心
            Xc = (B * E - 2 * C * D) / (4 * A * C - B * B);
            Yc = (B * D - 2 * A * E) / (4 * A * C - B * B);

            //a倾角
            dipAngle = Math.Atan(B / (A - C)) / 2;

            //半轴
            double m2 = 2 * (A * Xc * Xc + C * Yc * Yc + B * Xc * Yc - 1) / (A + C + Math.Sqrt((A - C) * (A - C) + B * B));
            double n2 = 2 * (A * Xc * Xc + C * Yc * Yc + B * Xc * Yc - 1) / (A + C - Math.Sqrt((A - C) * (A - C) + B * B));

            if(dipAngle < 0)
            {
                dipAngle = dipAngle + Math.PI / 2;
            }

            if (m2 > n2)
            {
                a2 = m2;
                b2 = n2;
            }
            else
            {
                a2 = n2;
                b2 = m2;
            }

            int z = 0;
            ++z;
        }

        void calculationConvexHull(List<StandardData> standardData, TestData testData, out double n, out List<int> standardDataConvexHullIndices, out List<TestDataConvexHull> testDataConvexHullIndices)
        {
            string inputJson = "";
            JObject obj = new JObject();
            JArray arrStandardData = new JArray();
            for (int i = 0; i < standardData.Count; ++i)
            {
                JObject objStandardData = new JObject();
                objStandardData.Add("salinity", standardData[i].Salinity);
                objStandardData.Add("temperature", standardData[i].Watertemp);
                objStandardData.Add("depth", 0);
                arrStandardData.Add(objStandardData);
            }
            obj.Add("standard_station_data", arrStandardData);

            JArray arrTestData = new JArray();
            JObject objTestData = new JObject();
            objTestData.Add("salinity", testData.Salinity);
            objTestData.Add("temperature", testData.Watertemp);
            objTestData.Add("depth", testData.Depth);
            arrTestData.Add(objTestData);
            obj.Add("test_station_data", arrTestData);

            inputJson = obj.ToString();

            var module = DLLWrapper.Load(AppDomain.CurrentDomain.BaseDirectory + @"TSDPointClusterTestEvaluationIndexCalculation\TSDPointClusterTestEvaluationIndexCalculation.dll");
            TSDPointClusterTestEvaluationIndexCalculation TSDPointClusterTestEvaluationIndexCalculationV1 = (TSDPointClusterTestEvaluationIndexCalculation)DLLWrapper.GetFunctionAddress(module, "TSDPointClusterTestEvaluationIndexCalculation_v1", typeof(TSDPointClusterTestEvaluationIndexCalculation));
            StringBuilder outputJson = new StringBuilder(1024);
            UIntPtr size = (UIntPtr)outputJson.Capacity;
            bool success = TSDPointClusterTestEvaluationIndexCalculationV1(inputJson, outputJson, ref size);
            if (!success)
            {
            }

            DLLWrapper.Release(module);
            string output = outputJson.ToString();
            JObject outputObj = JObject.Parse(output);

            n = outputObj["N"].Value<double>();
            standardDataConvexHullIndices = new List<int>();
            foreach(var index in outputObj["standard_data_convex_hull_indices"])
            {
                standardDataConvexHullIndices.Add(index.Value<int>());
            }

            //return Double.Parse(outputObj["N"].ToString());
            testDataConvexHullIndices = new List<TestDataConvexHull>();
            foreach (var index in outputObj["test_data_convex_hull_indices"])
            {
                TestDataConvexHull convexHull = new TestDataConvexHull();
                convexHull.Depth = index["depth"].Value<double>();
                foreach(var indices in (JArray)index["indices"])
                {
                    convexHull.Indices.Add(indices.Value<int>());
                }
                foreach(var min_ellipse in (JArray)index["min_ellipse"])
                {
                    convexHull.MinEllipse.Add(min_ellipse.Value<double>());
                }

                testDataConvexHullIndices.Add(convexHull);
            }
        }
    }
}


//if (avgWaterTemp.Count > 0)
//{
//    //优质数据集散点图
//    {
//        ScatterSeries ss = new ScatterSeries() { Title = "标准数据", MarkerType = MarkerType.Circle, MarkerFill = OxyColor.FromRgb(0, 0, 255) };
//        for (int i = 0; i < avgWaterTemp.Count; ++i)
//        {
//            ss.Points.Add(new ScatterPoint(avgSalinity[i], avgWaterTemp[i]));
//        }
//        TSDModel.Series.Add(ss);
//    }

//    if (recordDepth.Count > 0)
//    {
//        ScatterSeries ss = new ScatterSeries() { Title = "被检数据", MarkerType = MarkerType.Diamond, MarkerFill = OxyColor.FromRgb(255, 0, 0) };
//        for (int i = 0; i < recordDepth.Count; ++i)
//        {
//            ScatterPoint pt = new ScatterPoint(recordSalinity[i], recordWaterTemp[i]);
//            pt.Tag = "深度：" + recordDepth[i].ToString();
//            ss.Points.Add(pt);
//        }
//        TSDModel.Series.Add(ss);
//    }
//}
//TSDModel.InvalidatePlot(true);
