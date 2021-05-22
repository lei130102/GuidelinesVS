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

namespace WPF_Shape.View
{
    //DrawingCanvas类没有提供用于绘制、选择以及移动正方形的逻辑，这是因为该功能是在应用程序层中控制的。因为可能有几个不同的绘图工具都使用同一个
    //DrawingCanvas类，所以这样做是合理的。根据用户单击的按钮，用户可绘制不同类型的形状，或使用不同的笔画颜色和填充颜色。所有这些细节都是特定于
    //窗口的。DrawingCanvas提供了用于驻留、渲染以及跟踪可视化对象的功能
    public class DrawingCanvas : Panel
    {
        /// <summary>
        /// 存储的可视化对象的集合
        /// </summary>
        private List<Visual> visuals = new List<Visual>();

        /// <summary>
        /// 提供对每个可视化对象的访问
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        /// <summary>
        /// 可视化对象的个数
        /// </summary>
        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }

        #region 简化在可视化对象集合的恰当位置插入可视化对象的自定义代码
        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);

            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }

        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);

            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }
        #endregion

        #region 命中测试
        //为了允许用户移动和删除已经绘制的正方形，需要编写代码以截获鼠标单击，并查找位于可单击位置的可视化对象，该任务被称为命中测试(hit testing)
        //为支持命中测试，最好为DrawingCanvas类添加GetVisual()方法。该方法使用一个点作为参数并返回匹配的Drawing Visual对象。为此使用了
        //VisualTreeHelper.HitTest()静态方法
        public DrawingVisual GetVisual(Point point)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);
            return hitResult.VisualHit as DrawingVisual;
        }
        //在本例中，代码忽略了所有非DrawingVisual类型的命中对象，包括DrawingCanvas对象本身。如果没有正方形被单击，GetVisual()方法返回null引用
        #endregion

        #region 复杂的命中测试
        //在上面的示例中，命中测试代码始终返回最上面的可视化对象(如果单击空白处，就返回null引用)。然而，VisualTreeHelper类提供了HitTest()方法的两个重载版本，
        //从而可以执行更加复杂的命中测试。使用这些方法，可以检索位于特定点的所有可视化对象，即使他们被其他元素隐藏在后面也同样如此，还可以找到位于给定几何图形
        //中的所有可视化对象
        //为了使用这个更高级的命中测试行为，需要创建回调函数。之后VisualTreeHelper类自上而下遍历所有可视化对象(与创建他们的顺序相反)。每当发现匹配的对象时，
        //就会调用回调函数并传递相关细节。然后可以选择停止查找(如果已经查找到足够的层次)，或继续查找直到遍历完所有的可视化对象为止
        private List<DrawingVisual> hits = new List<DrawingVisual>();
        public List<DrawingVisual> GetVisuals(Geometry region)//region对象用于命中测试
        {
            hits.Clear();//情况命中测试结果的集合
            GeometryHitTestParameters parameters = new GeometryHitTestParameters(region);
            HitTestResultCallback callback = new HitTestResultCallback(this.HitTestCallback);
            //通过调用VisualTreeHelper.HitTest()方法启动命中测试过程
            VisualTreeHelper.HitTest(this, null, callback, parameters);
            //当该过程结束时，该方法返回包含所有找到的可视化对象的集合
            return hits;
        }

        //在本例中，通过单独定义的HitTestResultCallback()方法实现回调函数。
        //HitTestResultCallback()和GetVisuals()方法都使用命中集合，所以命中集合作为成员字段进行定义。然而，可以通过为回调函数使用匿名方法来避免这一点，
        //匿名方法可在GetVisuals()方法内声明
        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            //回调方法实现了命中测试行为。通常HitTestResult对象只提供一个属性(VisualHit)，但可以根据执行命中测试的类型，将他转换成两个派生类型中的任意
            //一个。

            //如果使用一个点进行命中测试，可将HitTestResult对象转换为PointHitTestResult对象，该类提供了一个不起眼的PointHint属性，该属性返回用于执行
            //命中测试的原始点

            //如果使用Geometry对象进行命中测试，可将HitTestResult对象转换为GeometryHitTestResult对象，并访问IntersectionDetail属性。IntersectionDetail
            //属性告知您几何图形是否完全封装了可视化对象(FullyInside)，几何图形是否与可视化元素只是相互重叠(Intersets)，或者用于命中测试的几何图形是否落
            //在可视化元素的内部(FullyContains)

            //在本例中，只有当可视化对象完全位于命中测试区域时，才会对命中数量计数。
            GeometryHitTestResult geometryResult = (GeometryHitTestResult)result;
            DrawingVisual visual = result.VisualHit as DrawingVisual;
            if(visual != null && geometryResult.IntersectionDetail == IntersectionDetail.FullyInside)
            {
                hits.Add(visual);
            }

            //最后，在回调函数的末尾，可返回两个HitTestResultBehavior枚举值中的一个：
            //返回Continue表示继续查找命中，返回Stop则表示结束查找过程
            return HitTestResultBehavior.Continue;
        }
        #endregion
    }




    /// <summary>
    /// VisualLayer.xaml 的交互逻辑
    /// </summary>
    public partial class VisualLayer : Window
    {
        public VisualLayer()
        {
            InitializeComponent();

            DrawingVisual v = new DrawingVisual();
            DrawSquare(v, new Point(10, 10), false);
        }

        // Variables for dragging shapes.
        private bool isDragging = false;
        private Vector clickOffset;
        private DrawingVisual selectedVisual;

        // Variables for drawing the selection square.
        private bool isMultiSelecting = false;
        private Point selectionSquareTopLeft;

        private void drawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point pointClicked = e.GetPosition(drawingSurface);

            if(cmdAdd.IsChecked == true)
            {//创建正方形
                DrawingVisual visual = new DrawingVisual();
                DrawSquare(visual, pointClicked, false);
                drawingSurface.AddVisual(visual);
            }
            else if(cmdDelete.IsChecked == true)
            {//删除正方形
                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if(visual != null)
                {
                    drawingSurface.DeleteVisual(visual);
                }
            }
            else if(cmdSelectMove.IsChecked == true)
            {//选择正方形和拖拽

                //需要通过一种方法对拖动进行跟踪。在窗口中添加了三个字段用于该目的——isDragging、clickOffset和selectedVisual
                //private bool isDragging = false;
                //private DrawingVisual selectedVisual;
                //private Vector clickOffset;
                //当用户单击某个形状时，isDragging字段被设置为true，selectedVisual字段被设置为被单击的可视化对象，而clickOffset字段记录了用户单击点
                //和正方形左上角点之间的距离

                DrawingVisual visual = drawingSurface.GetVisual(pointClicked);
                if(visual != null)
                {
                    // Calculate the top-left corner of the square.
                    // This is done by looking at the current bounds and
                    // removing half the border (pen thickness).
                    // An alternate solution would be to store the top-left
                    // point of every visual in a collection in the DrawingCanvas,
                    // and provide this point when hit testing.
                    Point topLeftCorner = new Point(
                        visual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                        visual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
                    //还调用了DrawSquare()方法，使用新颜色重新渲染DrawingVisual对象
                    DrawSquare(visual, topLeftCorner, true);

                    clickOffset = topLeftCorner - pointClicked;
                    isDragging = true;

                    if(selectedVisual != null && selectedVisual != visual)
                    {
                        // The selection has changed. Clear the previous selection.
                        //重新绘制以前选中的正方形，使该正方形恢复其正常外观
                        ClearSelection();
                    }
                    selectedVisual = visual;
                }
            }
            else if(cmdSelectMultiple.IsChecked == true)
            {//绘制选择框
                //为了创建选择框，窗口只需要为DrawingCanvas面板添加另一个DrawingVisual对象即可，此外还有isMultiSelecting标志和selectionSquareTopLeft字段
                //当绘制选择框时，isMultiSelecting标志跟踪正在进行的选择操作，selectionSquareTopLeft字段跟踪当前选择框的左上角
                //private DrawingVisual selectionSquare;
                //private bool isMultiSelecting = false;
                //private Point selectionSquareTopLeft;

                selectionSquare = new DrawingVisual();

                drawingSurface.AddVisual(selectionSquare);

                selectionSquareTopLeft = pointClicked;
                isMultiSelecting = true;

                // Make sure we get the MouseLeftButtonUp event if the user
                // moves off the Canvas. Otherwise, two selection squares could be drawn at once.
                drawingSurface.CaptureMouse();
            }
        }

        // Drawing constants.
        private Brush drawingBrush = Brushes.AliceBlue;
        private Brush selectedDrawingBrush = Brushes.LightGoldenrodYellow;
        private Pen drawingPen = new Pen(Brushes.SteelBlue, 3);
        private Size squareSize = new Size(30, 30);
        private DrawingVisual selectionSquare;

        /// <summary>
        /// Rendering the square
        ///
        /// 注意，DrawSquare()方法为正方形定义了内容——但正方形实际上没有在窗口中进行绘制，因此，不用担心会无意中在其他正方形的上面绘制应当位于下面的正方形。
        /// WPF管理绘图过程，确保按照从GetVisualChild()方法返回的顺序绘制可视化对象(该顺序是在可视化集合中定义的顺序)
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="topLeftCorner"></param>
        /// <param name="isSelected"></param>
        private void DrawSquare(DrawingVisual visual, Point topLeftCorner, bool isSelected)
        {
            //为使用DrawingVisual类绘制内容，需要调用DrawingVisual.RenderOpen()方法。该方法返回一个可用于定义可视化内容的
            //DrawingContext对象。绘制完毕后，需要调用DrawingContext.Close()方法，例如：
            //DrawingVisual visual = new DrawingVisual();
            //DrawingContext dc = visual.RenderOpen();
            ////(Perform drawing here.)
            //dc.Close();

            //本质上，DrawingContext类由各种为可视化对象增加了一些图形细节的方法构成，可调用这些方法来绘制各种图形、应用变换
            //以及改变不透明度等
            ///////////DrawingContext类的方法:
            //DrawLine()                         在指定的位置，使用指定的填充和轮廓绘制特定的形状。通过这些方法绘制的形状和之前看到的形状一样
            //DrawRectangle()
            //DrawRoundedRectangle()
            //DrawEllipse()

            //DrawGeometry()                     绘制更复杂的Geometry对象和Drawing对象
            //DrawDrawing()

            //DrawText()                         在指定的位置绘制文本。通过为该方法传递FormattedText对象，可指定文本、字体、填充以及其他细节。
            //                                   如果设置了FormattedText.MaxTextWidth属性，可使用该方法绘制换行的文本

            //DrawImage()                        在指定的区域(由Rect对象定义)绘制一幅位图图像

            //DrawVideo()                        在特定区域绘制视频内容(封装在MediaPlayer对象中)

            //Pop()                              翻转最后调用的PushXxx()方法。可使用PushXxx()方法暂时应用一个或多个效果，并且Pop()方法会翻转他们

            //PushClip()                         将绘图限制在特定剪裁区域中。这个区域外的内容不被绘制

            //PushEffect()                       为随后的绘制操作应用BitmapEffect对象

            //PushOpacity()                      为了使后续的绘图操作部分透明，应用新的不透明设置或不透明掩码
            //PushOpacityMask()

            //PushTransform()                    设置将应用于后续绘制操作的Transform对象。可使用变换来缩放、移动、旋转或扭曲内容

            //例如创建一个可视化对象，该可视化对象包含没有填充的基本的黑色三角形
            //DrawingVisual visual = new DrawingVisual();
            //using(DrawingContext dc = visual.RenderOpen())
            //{
            //    Pen drawingPen = new Pen(Brushes.Black, 3);
            //    dc.DrawLine(drawingPen, new Point(0, 50), new Point(50, 0));
            //    dc.DrawLine(drawingPen, new Point(50, 0), new Point(100, 50));
            //    dc.DrawLine(drawingPen, new Point(0, 50), new Point(100, 50));
            //}
            //当调用DrawingContext方法时，没有实际绘制可视化对象——而只是定义了可视化外观。当通过调用Close()方法结束绘制时，完成的图画被
            //存储在可视化对象中，并通过只读的DrawingVisual.Drawing属性提供这些图画。WPF会保存Drawing对象，从而当需要时可以重新绘制窗口

            //绘图代码的顺序很重要。后面的绘图操作可在已经存在的图形上绘制内容。PushXxx()方法应用的设置会被应用到后续的绘图操作中。例如，
            //可使用PushOpacity()方法改变不透明等级，该设置会影响所有的后续绘图操作。可使用Pop()方法恢复最近的PushXxx()方法。如果多次调用
            //PushXxx()方法，可一次使用一系列Pop()方法调用关闭他们

            //一旦关闭DrawingContext对象，就不能再修改可视化对象。但可以使用DrawingVisual类的Transform和Opacity属性应用变换或改变整个可视化
            //对象的透明度。如果希望提供全新的内容，可以再次调用RenderOpen()方法并重复绘制过程

            //(许多绘图方法都使用Pen和Brush对象。如果计划使用相同的画笔和填充绘制许多可视化对象，或者如果希望多次渲染同一个可视化对象(为了改变
            //其内容)，就值得事先创建所需的Pen和Brush对象，并在窗口的整个生命周期中保存他们)


            using (DrawingContext dc = visual.RenderOpen())
            {
                Brush brush = drawingBrush;
                if (isSelected)
                {
                    brush = selectedDrawingBrush;
                }
                dc.DrawRectangle(brush, drawingPen, new Rect(topLeftCorner, squareSize));
            }
        }

        private void drawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            if(isMultiSelecting)
            {
                // Display all the squares in this region.
                RectangleGeometry geometry = new RectangleGeometry(
                    new Rect(selectionSquareTopLeft, e.GetPosition(drawingSurface)));

                List<DrawingVisual> visualsInRegion =
                    drawingSurface.GetVisuals(geometry);

                MessageBox.Show(String.Format("You selected {0} square(s).", visualsInRegion.Count));

                isMultiSelecting = false;
                drawingSurface.DeleteVisual(selectionSquare);
                drawingSurface.ReleaseMouseCapture();
            }
        }

        private void ClearSelection()
        {
            Point topLeftCorner = new Point(
                selectedVisual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2,
                selectedVisual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2
                );

            DrawSquare(selectedVisual, topLeftCorner, false);
            selectedVisual = null;
        }

        private void drawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if(isDragging)
            {
                Point pointDragged = e.GetPosition(drawingSurface) + clickOffset;
                DrawSquare(selectedVisual, pointDragged, true);
            }
            else if(isMultiSelecting)
            {
                Point pointDragged = e.GetPosition(drawingSurface);
                DrawSelectionSquare(selectionSquareTopLeft, pointDragged);
            }
        }

        private Brush selectionSquareBrush = Brushes.Transparent;
        private Pen selectionSquarePen = new Pen(Brushes.Black, 2);

        private void DrawSelectionSquare(Point point1, Point point2)
        {
            selectionSquarePen.DashStyle = DashStyles.Dash;

            using (DrawingContext dc = selectionSquare.RenderOpen())
            {
                dc.DrawRectangle(selectionSquareBrush, selectionSquarePen, new Rect(point1, point2));
            }
        }
    }
}
