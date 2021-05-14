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

namespace WPF_DatePicker
{
    /// <summary>
    /// Simple.xaml 的交互逻辑
    /// </summary>
    public partial class Simple : Window
    {
        public Simple()
        {
            InitializeComponent();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            DateTime? date = picker.SelectedDate;

            if(date == null)
            {
                this.Title = "No date";
            }
            else
            {
                this.Title = date.Value.ToShortDateString();
            }
        }

        /// <summary>
        /// 拒绝特定的日期选择，比如周末
        ///
        /// 可使用支持单个或多个日期选择的Calender事件加以测试。如果支持多个选择，那么尝试在整个星期的日期上拖动鼠标。除不允许的周末日期外，其他所有日期将
        /// 保持突出显示，而周末日期将被自动取消选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calendarNoWeekend_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //Check all the newly added items.
            foreach(DateTime selectedDate in e.AddedItems)
            {
                if((selectedDate.DayOfWeek == DayOfWeek.Saturday) ||
                    (selectedDate.DayOfWeek == DayOfWeek.Sunday))
                {
                    //lblError.Text = "Weekends are not allowed";
                    // Remove the selected date.
                    ((Calendar)sender).SelectedDates.Remove(selectedDate);
                }
            }

        }

        /// <summary>
        /// 通常，当用户打开日历视图时会丢弃非法值，但可以选择填充一些文本以向用户通知发生了问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void datePickerDateValidationError_DateValidationError(object sender, DatePickerDateValidationErrorEventArgs e)
        {
            //lblError.Text = "," + e.Text +
            //    "' is not a valid value because " + e.Exception.Message;
        }
    }
}
