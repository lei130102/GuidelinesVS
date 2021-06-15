using StoreDatabase;
using System;
using System.Collections.Generic;
using System.Globalization;
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

//自定义验证规则

//应用自定义验证规则的方法和应用自定义转换器的方法类似。该方法定义继承自ValidationRule(位于System.Windows.Controls命名空间)的类，并为了
//执行验证而重写Validate()方法。如有必要，可添加接受其他细节的属性，可使用这些属性影响验证(例如，用于检查文本的验证规则可包含Boolean类型的
//CaseSensitive属性)


namespace WPF_DataBinding
{
    //下面是一条完整的验证规则，该规则将decimal数值限制在指定的最小值和最大值之间。因为这条验证规则用于货币数值，所以在默认情况下，最小值是0，
    //而最大值是decimal类型能够容纳的最大值。然而，为了获得最大的灵活性，可通过属性来配置这些细节：
    public class PositivePriceRule : ValidationRule
    {
        private decimal min = 0;
        private decimal max = Decimal.MaxValue;

        public decimal Min
        {
            get { return min; }
            set { min = value; }
        }

        public decimal Max
        {
            get { return max; }
            set { max = value; }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal price = 0;

            try
            {
                if(((string)value).Length > 0)
                {
                    // Allow number styles with currency symbols like $.
                    price = Decimal.Parse((string)value, System.Globalization.NumberStyles.Any);

                    //注意，验证逻辑使用了Decimal.Parse()方法的一个重载版本，该重载版本接受一个NumberStyles枚举值。这是因为验证总在转换之前
                    //进行。如果为同一字段同时应用验证器和转换器，就需要确保当存在货币符号时能够成功地进行验证。验证逻辑的成败通过返回的ValidationResult对象标识。
                    //IsValid属性指示验证是否成功，并且如果验证不成功，ErrorContent属性会提供描述问题的对象。在这个示例中，错误内容被设置为将会显示在用户界面中的
                    //字符串，这是最常用的方法
                }
            }
            catch(Exception ex)
            {
                return new ValidationResult(false, "Illegal characters.");
            }

            if((price < Min) || (price > Max))
            {
                return new ValidationResult(false, "Not in the range " + Min + " to " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }

    /// <summary>
    /// ValidationTest.xaml 的交互逻辑
    /// </summary>
    public partial class ValidationTest : Window
    {
        public ValidationTest()
        {
            InitializeComponent();
        }

        private ICollection<Product> products;

        private void cmdGetProducts_Click(object sender, RoutedEventArgs e)
        {
            products = App.StoreDb.GetProducts();
            lstProducts.ItemsSource = products;
        }

        private void cmdUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            // Make sure update has taken place.
            FocusManager.SetFocusedElement(this, (Button)sender);
        }

        private void validationError(object sender, ValidationErrorEventArgs e)
        {
            if(e.Action == ValidationErrorEventAction.Added)
            {
                MessageBox.Show(e.Error.ErrorContent.ToString());
            }
        }


        private void cmdGetExceptions_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            GetErrors(sb, gridProductDetails);
            string message = sb.ToString();
            if (message != "")
                MessageBox.Show(message);
        }

        private void GetErrors(StringBuilder sb, DependencyObject obj)
        {
            foreach(object child in LogicalTreeHelper.GetChildren(obj))
            {
                // Ignore strings and dependency objects that aren't elements. 
                TextBox element = child as TextBox;
                if (element == null)
                    continue;

                if(Validation.GetHasError(element))
                {
                    sb.Append(element.Text + " has errors:\r\n");
                    foreach(ValidationError error in Validation.GetErrors(element))
                    {
                        sb.Append(" " + error.ErrorContent.ToString());
                        sb.Append("\r\n");
                    }
                }

                // Check the children of this object.
                GetErrors(sb, element);
            }
        }
    }
}

//一旦完善自定义的验证规则，就准备好通过将验证规则添加到Binding.ValidationRules集合中来将之关联到元素。下面是使用PositivePriceRule验证规则的一个示例，
//该规则的Maximum属性值被设置为999.99：

//<TextBlock Margin="7" Grid.Row="2">Unit Cost:</TextBlock>
//<TextBox Margin="5" Grid.Row="2" Grid.Column="1">
//  <TextBox.Text>
//      <Binding Path="UnitCost">
//          <Binding.ValidationRules>
//              <local:PositivePriceRule Max="999.99"/>
//          </Binding.ValidationRules>
//      </Binding>
//  </TextBox.Text>
//</TextBox>

//通常，会为使用同类规则的每个元素定义不同的验证规则对象。因为可能希望独立地调整验证属性(例如，PositivePriceRule验证规则中的最小值和最大值)。如果确实希望为多个绑定使用
//完全相同的验证规则，可将验证规则定义为资源，并在每个绑定中简单地使用StaticResource标记扩展指向该资源

//您可能已经猜到了，Binding.ValidationRules集合可包含任意数量地验证规则。将值提交到源时，WPF将按顺序检查每个验证规则(请记住，当文本框失去焦点时文本框地值被提交到源，
//除非使用UpdateSourceTrigger属性另行指定)。如果所有验证都成功了，WPF接着会调用转换器(如果存在的话)并为源应用值

//注意：
//如果在PositivePriceRule验证规则之后添加ExceptionValidationRule验证规则，那么会首先评估PositivePriceRule验证规则。PositivePriceRule验证规则将捕获
//由于超出范围造成的错误。然而，当输入的内容不能被转换为decimal类型的数值时(如一系列字母)，ExceptionValidationRule验证规则会捕获类型转换错误

//当使用PositivePriceRule验证规则执行验证时，其行为和使用ExceptionValidationRule验证规则的行为相同——文本框使用红色轮廓，设置hasError和Errors属性，并引发Error事件。
//为给用户提供一些更有帮助作用的反馈信息，需要添加一些代码或自定义ErrorTemplate模板。后续您将学习如何使用这两种方法

//提示：
//自定义验证规则可非常特殊，从而可用于约束特定的属性，或更为通用，从而可在各种情况下重用。例如，可很容易地创建一条自定义验证规则，借助于.NET提供地System.Text.RegularExpression.Regex类，
//使用指定地正则表达式验证字符串。根据使用地正则表达式，可对各种基于模式地文本数据使用这条验证规则，如电子邮件地址、电话号码、IP地址以及邮政编码







//响应验证错误

//在上个示例中，有关用户接收到错误地唯一指示是在违反规则地文本框周围地红色轮廓。为提供更多信息，可处理Error事件，当存储或清除错误时会引发该事件，但前提是首先必须
//确保已将Binding.NotifyOnValidationError属性设置为true:
//<Binding Path="UnitCost" NotifyOnValidationError="True">

//Error事件是使用冒泡策略的路由事件，所以可通过在父容器中关联事件处理程序来为多个控件处理Error事件，如下所示：
//<Grid Name="gridProductDetails" Validation.Error="validationError">

//下面的代码对这个事件进行响应，并显示一个具有错误信息的消息框(一个干扰程度更小的选项是显示工具提示或在窗口的某个地方显示错误信息)：
//private void validationError(object sender, ValidationErrorEventArgs e)
//{
// Check that the error is being added (not cleared).
//if(e.Action == ValidationErrorEventAction.Added)
//{
//  MessageBox.Show(e.Error.ErrorContent.ToString());
//}
//}

//ValidationErrorEventArgs.Error属性提供了一个ValidationError对象，该对象将几个有用的细节捆绑在一起，包括引起问题的异常(Exception)、
//违反的验证规则(ValidationRule)、关联的绑定对象(BindingInError)以及ValidationRule对象返回的任何自定义信息(ErrorContent)。

//如果正使用自定义的验证规则，几乎总会选择在ValidationError.ErrorContent属性中放置错误信息。如果使用ExceptionValidationRule验证规则，ErrorContent属性
//将返回相应异常的Message属性。然而，存在一个问题，如果是因为数据类型不能转换为正确的值而引起的异常，ErrorContent属性会如您所期望的那样工作，并报告发生了
//问题。但如果在数据对象的属性设置器中抛出了异常，那么异常会被封装到TargetInvocationException对象中，并且ErrorContent属性提供来自TargetInvocationException.Message
//属性的文本，内容是"Exception has been thrown by the target of an invocation."，这段文本内容实际没有什么作用

//因此，如果正在使用属性设置器引发异常，就需要添加代码来检查TargetInvocationException对象的InnerException属性。如果不是null，就可以检索原始异常对象，并使用原始
//异常对象的Message属性而不是使用ValidationError.ErrorContent属性









//获取错误列表

//在某些情况下， 您可能希望获取当前窗口(或窗口中的给定容器)中所有未处理错误的列表。这项任务较简单——需要做的所有工作就是遍历元素树，测试每个元素的Validation.HasError属性

//下面的代码例程演示一个专门查找TextBox对象中非法数据的示例。这个示例使用递归代码遍历整个元素层次。同时将错误信息聚集到一条单独的消息中，然后显示给用户：
//private void cmdOK_Click(object sender, RoutedEventArgs e)
//{
//    string message;
//    if(FormHasErrors(message))
//    {
//        // Errors still exist.
//        MessageBox.Show(message);
//    }
//    else
//    {
//        // There are no errors. You can continue on to complete the task
//        // (for example, apply the edit to the data source.).
//    }
//}
//
//private bool FormHasErrors(out string message)
//{
//    StringBuilder sb = new StringBuilder();
//    GetErrors(sb, gridProductDetails);
//    message = sb.ToString();
//    return message != "";
//}

//private void GetErrors(StringBuilder sb, DependencyObject obj)
//{
//  foreach(object child in LogicalTreeHelper.GetChildren(obj))
//  {
//      TextBox element = child as TextBox;
//      if(element == null)
//          continue;
//      if(Validation.GetHasError(element))
//      {
//          sb.Append(element.Text + " has errors:\r\n");
//          foreach(ValidationError error in Validation.GetErrors(element))
//          {
//              sb.Append(" " + error.ErrorContent.ToString());
//              sb.Append("\r\n");
//          }
//          // Check the children of this object for errors.
//          GetErrors(sb, element);
//      }
//  }
//}

//在更复杂的实现中，FormHasErrors()方法可能创建包含错误信息对象的集合。cmdOk_Click()事件处理程序接着负责构造恰当的信息









//显示不同的错误指示符号

//为最大限度地利用WPF验证，您可能希望创建自己的错误模板，以适当的方式标识错误。乍一看，这像是一种报告错误的低级方法——毕竟，可使用标准的控件模板详细地自定义控件
//的构成。然而，错误模板和普通控件模板是不同的。

//错误模板使用的是装饰层，装饰层是位于普通窗口内容之上的绘图层。使用装饰层，可添加可视化装饰来指示错误，而不用替换控件背后的控件模板或改变窗口的布局。文本框的标准错误
//模板通过在相应文本框的上面添加红色的Border元素(背后的文本框没有发生变化)来指示发生了错误。可使用错误模板添加其他细节，如图像、文本或其他能吸引用户关注问题的图形细节

//下面的标记显示了一个示例。该例定义了一个错误模板，该模板使用绿色边框并在具有非法输入的控件的旁边添加一个星号。该模板被封装进一条样式规则中，从而可自动将之应用到当前
//窗口的所有文本框：
/*
<Style TargetType="{x:Type TextBox}">
    <Setter Property="Validation.ErrorTemplate">
        <Setter.Value>
            <ControlTemplate>
                <DockPanel LastChildFill="True">
                    <TextBlock DockPanel.Dock="Right" Foreground="Red" FontSize="14" FontWeight="Bold">*</TextBlock>
                    <Border BorderBrush="Green" BorderThickness="1">
                        <AdornedElementPlaceholder></AdornedElementPlaceholder>
                    </Border>
                </DockPanel>
            </ControlTemplate>
        </Setter.Value>
    </Setter>
</Style>
 */
//AdornedElementPlaceholder是支持这种技术的粘合剂。他代表控件自身，位于元素层中。通过使用AdornedElementPlaceholder元素，能在文本框的背后安排自己的内容

//因此，在该例中，边框被直接放在文本框上，而不管文本框的尺寸是多少。在这个示例中，星号放在右边，最令人满意的是，新的错误模板内容叠加在已存在的内容之上，
//从而不会在原始窗口的布局中触发任何改变(实际上，如果不小心在装饰层中包含了过多内容，最终会改变窗口的其他部分)

//提示：
//如果希望使错误模板叠加显示在元素之上(而不是位于元素的周围)，可在Grid控件的同一个单元格中同时放置自定义的内容和AdornerElementPlaceholder元素。此外，也可以
//不用AdornerElementPlaceholder元素，但这样就会丧失在元素之后精确定位自定义内容的能力



//错误模板仍存在一个问题——没有提供任何有关错误的附加信息。为显示这些细节，需要使用数据绑定提取他们。一个好方法是使用第一个错误的错误信息，并将其用作自定义错误
//指示器的工具提示文本。下面的模板实现了这一功能：
/*
<ControlTemplate>
    <DockPanel LastChidFill = "True">
        <TextBlock DockPanel.Dock="Right" Foreground="Red" FontSize="14" FontWeight="Bold"
            ToolTip="{Binding ElementName=adornerPlaceholder,Path=AdornedElement.(Validation.Errors)[0].ErrorContent}">*</TextBlock>
        <Border BorderBrush="Green" BorderThickness="1">
            <AdornedElementPlaceholder Name="adornerPlaceholder">
            </AdornedElementPlaceholder>
        </Border>
    </DockPanel>
</ControlTemplate>
 */

//绑定表达式的Path有些复杂，并且需要更仔细地加以检查。绑定表达式地源是AdornedElementPlaceholder元素，该元素是在控件模板中定义地
//ToolTip="{Binding ElementName=adornerPlaceholder, ...

//AdornedElementPlaceholder类通过AdornedElement属性提供了指向背后元素(在这个示例中是存在错误的TextBox对象)的引用：
//ToolTip="{Binding ElementName=adornerPlaceholder, Path=AdornedElement ...

//为检索实际错误，需要检查这个元素的Validation.Error属性。然而，需要用圆括号包围Validation.Errors属性，从而指示他是附加属性而不是TextBox类的属性：
//ToolTip="{Binding ElementName=adornerPlaceholder, Path=AdornedElement.(Validation.Errors) ...

//最后，需要使用索引器从集合中检索第一个ValidationError对象，然后提取该对象的ErrorContent属性：
//ToolTip = "{Binding ElementName=adornerPlaceholder, Path=AdornedElement.(Validation.Errors)[0].ErrorContent}"

//现在当将鼠标移到星号上时，就可以看到错误信息
//此外，您可能希望在Border或TextBox元素本身的工具提示中显示错误信息，从而当用户将鼠标移到控件上的任何部分时都会显示错误信息。可实现这一功能而无须借助于自定义错误模板——只需要
//一个用于TextBox控件的触发器，当Validation.HasError属性变为true时应用该触发器，并且应用具有错误消息的工具提示。下面是一个例子：
/*
<Style TargetType="{x:Type TextBox}">
    ...
    <Style.Triggers>
        <Trigger Property="Validation.HasError" Value="True">
            <Setter Property="ToolTip" Value="{Binding RelativeSource={RelativeSource Self}, Path=(Validation.Errors)[0].ErrorContent}"/>
        </Trigger>
    </Style.Triggers>
</Style>
 */








//验证多个值

//使用到目前为止看到的方法可验证单个数值。然而， 在许多情况下需要执行包含两个或更多个绑定值的验证。例如，如果Project对象的StartDate在其EndDate之后，该对象就是无效的，
//对于Order对象，其Status不应当为Shipped，并且其ShipDate不能为空。Product对象的ManufacturingCost不应当大于RetailPrice，等等

//可采用多种方式设计应用程序以处理这些限制。在某些情况下，构建智能的用户界面是有意义的(例如，如果有些字段基于其他字段的信息判断得出是不合适的，可选择禁用他们)。在其他情况下，
//会将这一逻辑构建到数据类自身(然而，如果数据在某些情况下是有效的，只是在特定的编辑任务中不能接受，那么这种方法行不通)。最后，可通过WPF数据绑定系统使用绑定组(binding group)
//创建应用这种规则的自定义验证规则。

//绑定组背后的基本思想很简单。创建继承自ValidationRule类的自定义验证规则，如前面所述。但不是将该规则应用到单个绑定表达式，而将其附加到包含所有绑定控件的容器上
//(通常，也就是将DataContext设置为数据对象的同一容器)。然后当提交编辑时，WPF会使用该验证规则验证整个数据对象，这就是所谓的项级别验证(item-level validation)

//例如，下面的标记通过设置BindingGroup属性为Grid面板(该面板包含了所有元素)创建了一个绑定组。然后添加一个名为NoBlankProductRule的验证规则。该规则自动应用到绑
//定的Product对象，该对象存储在Grid.DataContext属性中。
/*
<Grid Name="gridProductDetails"
    DataContext="{Binding ElementName=lstProducts, Path=SelectedItem}">
    <Grid.BindingGroup>
        <BindingGroup x:Name="productBindingGroup">
            <local:NoBlankProductRule></local:NoBlankProductRule>
        </BindingGroup>
    </Grid.BindingGroup>

    <TextBlock Margin="7">Model Number:</TextBlock>

    <TextBox Margin="5" Grid.Column="1" Text="{Binding Path=ModelNumber}"></TextBox>
</Grid>
 */

//在到目前为止看到的验证规则中，Validate()方法接收审查的单个数值。但当使用绑定组时，Validate()方法接收一个BindingGroup对象，BindingGroup对象封装了绑定数据对象
//(在这个示例中是Product对象)

//下面是NoBlankProductRule类中Validate()方法的开始部分：
/*
public override ValidationResult Validate(object value, CultureInfo cultureInfo)
{
    BindingGroup bindingGroup = (BindingGroup)value;
    Product product = (Product)bindingGroup.Items[0];
    ...
}
 */
//您将注意到，代码从BindingGroup.Items集合检索第一个对象。在这个示例中，在该集合中只有一个数据对象。但可以创建应用到多个不同对象的绑定组(尽管不常见)。
//在这种情况下，您会收到包含所有数据对象的集合。

//注意：
//为创建应用于多个数据对象的绑定组，必须设置BindingGroup.Name属性来为绑定组提供一个描述性的名称。然后在绑定表达式中设置BindGroupName属性使他们相互匹配：
//Text="{Binding Path=ModelNumber, BindingGroupName=MyBindingGroup}"
//这样一来，每个绑定表达式明确选择绑定组，并且可为针对不同数据对象的表达式使用相同的绑定组


//Validate()方法使用绑定组的方式还有一个意想不到的区别。在默认情况下，接收到的数据对象是针对原始对象的，没有应用任何新的修改。为得到希望验证的数值，需要
//调用BindingGroup.GetValue()方法并传递数据对象和属性名：
//string newModelName = (string)bindingGroup.GetValue(product, "ModelName");
//这种设计具有一定意义。不为数据对象实际应用新值，从而使WPF能够确保在这些修改生效前，不会触发其他更新或应用程序中的同步任务

//下面是NoBlandProductRule类的完整代码：
/*
public class NoBlankProductRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        BindingGroup bindingGroup = (BindingGroup)value;

        // This product has the original values.
        Product product = (Product)bindingGroup.Items[0];
        // Check the new values.
        string newModelName = (string)bindingGroup.GetValue(product, "ModelName");
        string newModelNumber = (string)bindingGroup.GetValue(product, "ModelNumber");

        if((newModelName == "") && (newModelNumber == ""))
        {
            return new ValidationResult(false, "A product requires a ModelName or ModelNumber.");
        }
        else
        {
            return new ValidationResult(true, null);
        }
    }
}
*/
//当使用顶级别验证时，通常需要创建与此类似的紧耦合验证规则。这是因为归纳验证逻辑通常并不容易(换句话说，不太可能为不同的数据对象应用类似但稍微不同的验证逻辑)。
//当调用GetValue()方法时，还需要使用特定的属性名。所以，为顶级别验证创建的验证规则不可能像为验证单个值所创建的验证规则那样整洁和精炼，而且可重用性更差。

//如上所述，当前示例尚未完全完成。绑定组使用事务处理编辑系统，这意味着在运行验证逻辑之前需要正式地提交编辑。完成该操作的最简便做法是调用BindingGroup.CommitEdit()
//方法。可使用当单击按钮或当编辑控件失去焦点时运行的事件处理程序来完成该工作
//如下所示：
/*
<Grid Name="gridProductDetails" TextBox.LostFocus="txt_LostFocus" DataContext="{Binding ElementName=lstProducts, Path=SelectedItem}">
 */
//下面是事件处理代码：
/*
private void txt_LostFocus(object sender, RoutedEventArgs e)
{
    productBindingGroup.CommitEdit();
}
 */
//如果验证失败，整个Grid面板都被认为是无效的，并在其周围显示红色的细边框。就像类似TextBox的编辑控件那样，可通过修改Validation.ErrorTemplate改变Grid面板的外观

//注意，对于将在之后讨论的DataGrid控件，项级别验证能更加无缝地工作。当用户从一个单元格移到另一个单元格时，处理编辑的事物方面和触发字段导航事务，并且当用户从一行
//移到另一行时调用BindingGroup.CommitEdit()方法。




//数据提供者

//在您已经看到的大多数示例中，都是通过代码设置元素的DataCotext属性或列表控件的ItemsSource属性，从而提供顶级的数据源。这通常是最灵活的方法，当数据对象是通过另一个
//类(如StoreDB类)构造时尤其如此。然而，还有其他选择。

//一种技术是作为窗口(或其他一些容器)的资源定义数据对象。如果能以声明方式构造对象，这种方法工作得很好，但如果需要在运行时连接到外部数据源(如数据库)，这种技术就没那么有意义了。
//然而，有些开发人员仍在运行时使用这种方法(通常是为了避免编写事件处理代码)。基本思想是创建封装器对象，在其构造函数中获取所需的数据。
//例如，可按如下方式创建资源部分：
/*
<Window.Resources>
    <ProductListSource x:Key="products"></ProductListSource>
</Window.Resources>
 */
//在此，ProductListSource类继承自ObservableCollection<Products>类。因此，他能存储产品列表。他还在构造函数中提供了一些基本逻辑，调用StoreDB.GetProducts()方法
//来填充自身。
//现在，其他元素可在他们的绑定中使用这个资源了：
//<ListBox ItemsSource="{StaticResource products}" ...>
//这种方法初看起来很诱人，但却存在一些风险。当添加错误处理时，需要将错误处理代码放在ProductListSource类中。甚至可能需要显示一条信息，向用户解释问题。
//正如能够看到的，这种方法将数据模型、数据访问代码以及用户界面代码混合在一起，所以当需要访问外部资源(文件和数据库等)时，这种方法不是很合理

//在某种程度上，数据提供者(data provider)是这种模型的扩展。可以通过数据提供者直接绑定在标记的资源部分定义的对象。然而，不是直接绑定到数据对象自身，而是绑定到能够检索
//或构建数据对象的数据提供者。如果数据提供者功能完备——例如，当发生异常时能够引发事件并提供用于配置与其操作相关细节的属性，这时这种方法是合理的。但是，WPF中的数据
//提供者还没有达到这种标准。他们很有限，在使用外部数据时(例如，当从数据库或文件中提取信息时)会遇到问题，导致不值得使用这种方法。对于一些较简单情况，使用他们可能是有意义的
//例如，可使用数据提供者将几个提供输入的控件捆绑成用于计算结果的类。然而，对于这种情况，除了能够减少有利于标记的事件处理代码之外，他们几乎没有提供其他功能。

//所有数据提供者都继承自System.Windows.Data.DataSourceProvider类。目前，WPF只提供了以下两个数据提供者：
//1.ObjectDataProvider，该数据提供者通过调用另一个类中的方法获取信息
//2.XmlDataProvider，该数据提供者直接从XML文件获取信息
//这两个对象的目标都是让用户能在XAML中实例化数据对象，而不必使用事件处理代码。

//注意，还有一种选择：可作为XAML中的资源明确创建视图对象，将控件绑定到视图，并使用代码为视图填充数据。尽管有些开发人员更喜欢尝试这种选择，不过这种选择主要用于
//希望通过应用排序和过滤定制视图的情况




//ObjectDataProvider
//ObjectDataProvider数据提供者可从应用程序的另一个类中获取信息。他添加了以下功能：
//1.他能创建需要的对象并为构造函数传递参数
//2.他能调用所创建对象中的方法，并向他传递方法参数
//3.他能异步地创建数据对象(换句话说，他能在窗口加载之前一直等待，此后在后台完成工作)
//例如，下面是一个基本的ObjectDataProvider数据提供者，他创建了StoreDB类的一个实例，调用该实例的GetProducts()方法，并且在窗口的其他部分获取数据：
/*
<Window.Resources>
    <ObjectDataProvider x:Key="productsProvider" ObjectType="{x:Type local:StoreDB}" MethodName="GetProducts"></ObjectDataProvider>
</Window.Resources>
 */
//现在可创建绑定，从ObjectDataProvider数据提供者获取数据源：
/*
<ListBox Name="lstProducts" DisplayMemberPath="ModelName"
    ItemsSource="{Binding Source={StaticResource productsProvider}}"></ListBox>
 */
//上面的标签像是绑定到ObjectDataProvider数据提供者，但ObjectDataProvider数据提供者的智能程度足够高，他知道实际上需要绑定到从GetProducts()方法返回的产品列表
//注意：与所有数据提供者类似，ObjectDataProvider是专门针对检索数据而设计的，而不是针对更新数据。换句话说，无法强制ObjectDataProvider数据提供者调用StoreDB类中
//的另一个方法来触发更新。这只是WPF中数据提供者类不如其他框架中的其他实现(如ASP.NET中的数据源控件)成熟的一个例子


//1.错误处理
//正如前面提到的，这个示例有一个极大的限制。当创建这个窗口时，XAML解析器创建窗口并调用GetProducts()方法，从而可以设置绑定。如果GetProducts()方法返回期望的数据，
//一切都会运行得很好，但如果抛出未处理的异常(例如，如果数据库太忙碌以至于不可访问)，结果就不是很理想了。这时，异常从窗口构造函数中的InitializeComponent()调用上传。
//显示此窗口的代码需要捕获这个错误，这在概念上有些混乱，并且无法继续执行并显示窗口——即使在构造函数中捕获了异常，窗口的其他部分也不会被正确地初始化
//
//但没有较容易的方法能解决这个问题。ObjectDataProvider类提供了IsInitialLoadEnabled属性，当第一次创建窗口是可将该属性设置为false，阻止调用GetProducts()方法。
//如果将该属性设置为false，可在后面调用Refresh()方法触发调用。但如果使用这种技术，绑定表达式就会失败，因为列表不能检索到他的数据源，这与大多数数据绑定错误是不同的
//(失败后不会给出提示，不会引发异常)
//
//那么，解决方法是什么呢？可在代码中构造ObjectDataProvider数据提供者，但这会丧失声明式绑定的优点(而这可能是首先使用ObjectDataProvider数据提供者的原因)。另一种
//解决方法是配置ObjectDataProvider数据提供者，使其异步地执行工作，如后面所述。对于这种情况，发生异常时，不会通告失败信息(但仍会在Debug窗口中显示跟踪消息来详细地
//描述错误)


//2.异步支持
//大多数开发人员会发现，没多少理由使用ObjectDataProvider数据提供者。通常，简单地直接绑定到数据对象，并添加少许代码来调用查询数据的类(如StoreDB类)更加容易。然而，有一个理由
//可能会让您使用ObjectDataProvider数据提供者——利用他执行异步数据查询
//<ObjectDataProvider IsAsynchronous="True" ...>
//这看似很简单。只要将ObjectDataProvider.IsAsynchronous属性设置为true，ObjectDataProvider数据提供者就会在后台进程中执行工作。因此，当在后台执行工作时，用户界面就会有
//反应。一旦数据对象构造完毕并从方法返回，所有绑定元素就可以使用ObjectDataProvider数据提供者了
//
//提示，如果不希望使用ObjectDataProvider数据提供者，仍可异步加载数据访问代码。技巧是使用WPF对多线程应用程序的支持。一个有用的工具是以后介绍的BackgroundWorker组件。
//如果使用BackgroundWorker组件，还可得到取消支持和进度报告的优点。然而，将BackgroundWorker组件添加到用户界面比简单设置ObjectDataProvider.IsAsynchronous属性需要
//完成更多工作










//异步数据绑定

//WPF还通过每个绑定对象的IsAsync属性来提供异步支持。不过，相对于ObjectDataProvider数据提供者的异步支持，这一功能的用处很小。当把Binding.IsAsync属性设置为true时，
//WPF异步地从数据对象检索绑定的属性。然而，数据对象自身仍然是同步创建的。

//例如，假设为StoreDB实例创建了如下异步绑定：
//<TextBox Text="{Binding Path=ModelNumber, IsAsync=True}"/>

//尽管使用的是异步绑定，但当代码查询数据库时仍然必须等待。一旦创建产品集合，绑定就会异步地从当前产品对象查询Product.ModelNumber属性。这一行为几乎没什么优点，因为执行
//Product类中的属性过程只需要很短的时间。实际上，所有设计良好的数据对象都像这样由轻量级的属性构建，这也是WPF团队对于提供Binding.IsAsync属性持完全保留意见的一个原因！

//利用Binding.IsAsync属性的唯一方法是构建在属性获取的过程中添加耗时逻辑的特殊类。例如，考虑一个绑定到数据模型的分析应用程序。数据对象可能包含一部分信息，使用耗时算法
//对其进行计算。可使用异步绑定绑定到这种属性，并使用同步绑定绑定到其他所有属性。通过这种方法，应用程序中的一些信息会立即显示，而其他信息会在准备就绪后显示。

//WPF还提供了基于异步绑定构建的优先绑定功能。通过优先绑定可使用优先列表提供几个异步绑定。具有最高优先权的绑定最先执行，但如果他正在被评估，就改用低优先级的绑定。
//下面是一个示例：
/*
<TextBox>
    <TextBox.Text>
        <PriorityBinding>
            <Binding Path="SlowSpeedProperty" IsAsync="True"/>
            <Binding Path="MediumSpeedProperty" IsAsync="True"/>
            <Binding Path="FastSpeedProperty"/>
        </PriorityBinding>
    </TextBox.Text>
</TextBox>
 */
//上面的标记假定当前数据上下文包含一个对象，该对象具有三个属性：SlowSpeedProperty、MediumSpeedProperty和FastSpeedProperty。
//绑定的放置顺序很重要。因此，如果SlowSpeedProperty可用，就总是用他设置文本。但如果第一个绑定仍在读取SlowSpeedProperty属性
//(换句话说，在该属性的获取过程中具有耗时逻辑)，就改用MediumSpeedProperty属性。如果MediumSpeedProperty属性也不可用，就使用
//FastSpeedProperty属性。为使这种方法奏效，除了列表最后的最快、优先级最低的绑定外，必须将其他所有绑定设置为异步绑定。最后一个
//绑定可以是异步的(对于这种情况，在检索到值之前，文本框一直显示为空)，也可以是同步的(对于这种情况，窗口会被冻结，直到同步绑定
//完成其工作为止)






//XmlDataProvider
//XmlDataProvider数据提供者提供了一种简捷方式，用于从单独的文件、Web站点或应用程序资源中提取XML数据，并使应用程序中的元素能使用提取到的数据。
//XmlDataProvider数据提供者被设计为只读的(换句话说，他不具有提交数据修改的能力)，而且不能处理来自其他源(如数据库记录、Web服务消息等)的XML数据。
//所以，XmlDataProvider是一款专用工具
//
//如果以前通过.NET使用过XML，就会知道.NET提供了丰富的库，用于读写以及操作XML。可使用精简的读取器类和写入器类(从而可以遍历XML文件，并使用自定义的代码处理每个元素)，
//可使用XPath或DOM查找特定内容，并且可使用串行化器类从XML表示方法转换整个对象，并且可将对象转换回XML表示方法。其中的每种方法都有各自的优缺点，但所有这些方法都比
//XmlDataProvider数据提供者的功能强大
//
//如果预见到需要修改XML或需要将XML数据转换成能在代码中使用的对象表示形式，最好使用.NET中已有的XML扩展支持。事实是数据以XML的表示方式存储，然后变成了与构造用户界面
//的方式不相关的低级细节(用户界面可简单地绑定到数据对象，就像在本章前面所介绍的由数据库支持的示例一样)。然而，如果确实需要一种快捷的方法来提取XML内容，并且需求较为简单，
//XmlDataProvider数据提供者将是合理的选择
//为使用XmlDataProvider数据提供者，首先要定义他并通过设置Source属性将他指向恰当的文件：
//<XmlDataProvider x:Key="productsProvider" Source="store.xml"></XmlDataProvider>
//
//也可通过代码设置Source属性(如果不能确定需要使用的文件名，这是很重要的)。默认情况下，除非显式地将XmlDataProvider.IsAsynchronous属性设置为false，否则XmlDataProvider
//数据提供者会异步地加载XML内容
//
//下面是在这个示例中使用地简单XML文件地一部分。他在顶级的Products元素中封装了整个文档，并在单独的Product元素中放置每个产品。针对每个产品的单个属性则以嵌套元素的形式提供：
/*
<Products> 
    <Product>
        <ProductID>355</ProductID>
        <CategoryID>16</CategoryID>
        <ModelNumber>RU007</ModelNumber>
        <ModelName>Rain Racer 2000</ModelName>
        <ProductImage>image.gif</ProductImage>
        <UnitCost>1499.99</UnitCost>
        <Description>Looks like an ordinary bumbershoot ...</Description>
    </Product>
    <Product>
        <ProductID>356</ProductID>
        <CategoryID>20</CategoryID>
        <ModelNumber>STKY1</ModelNumber>
        <ModelName>Edible Tape</ModelName>
        <ProductImage>image.git</ProductImage>
        <UnitCost>3.99</UnitCost>
        <Description>The latest in personal survival gear ... </Description>
    </Product>
</Products>
 */
//为从XML文件中提取信息，需要使用XPath表达式。XPath是一个强大的标准，通过他可从文档中检索感兴趣的部分。尽管全面讨论XPath超出了本书的范围，但勾画出其本质是很容易的。

//XPath使用类似路径的表示方法。例如，路径"/"标识XML文档的根，而/Products标识名为<Products>的根元素。路径/Products/Product选择<Products>元素中的每个<Product>元素。

//当在XmlDataProvider数据提供者中使用XPath时，第一个任务是确定根节点。在当前示例中，这意味着选择包含所有数据的<Products>元素(如果希望关注XML文档中的特定部分，可使用
//不同的顶级元素)
/*
<XmlDataProvider x:Key="productsProvider" Source="store.xml" XPath="/Products"></XmlDataProvider>
 */
//下一步是绑定列表。当使用XmlDataProvider数据提供者时，使用Binding.XPath属性而不是Binding.Path属性。这样可灵活地以所需的深度挖掘XML文档。
//下面的标记提取所有<Product>元素：
/*
 <ListBox Name="lstProducts" Margin="5" DisplayMemberPath="ModelName" ItemsSource="{Binding Source={StaticResource products}, XPath=Product}"></ListBox>
 */
//当在绑定中设置XPath属性时，需要记住表达式相对于XML文档的当前位置。因此，不需要在列表绑定中提供完整的路径/Products/Product。而只是使用相对路径Product，这个相对路径
//从XmlDataProvider数据提供者选择的<Products>节点开始。

//最后，需要关联显示产品细节的每个元素。同样，您编写的XPath表达式相对于当前节点(他时针对当前产品的<Product>元素)进行计算。下面的示例绑定到<ModelNumber>元素：
/*
 <TextBox Text="{Binding XPath=ModelNumber}"></TextBox>
 */
//一旦完成这些更改，就会得到一个基于XML的示例，该例与前面介绍的基于对象的绑定基本相同。唯一的区别是所有数据都被看成普通文本。为将他转换为不同的数据类型或不同的表达形式，
//需要使用值转换器
