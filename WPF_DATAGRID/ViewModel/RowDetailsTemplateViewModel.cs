using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_DataGrid.Model;

//DataGrid行详细信息
//使用DataGrid控件时非常常见的使用场景是能够显示每行的详细信息，通常位于行本身的正下方。 WPF DataGrid控件很好地支持了这一点，也很容易使用。

//控制可见性
//使用RowDetailsVisibilityMode属性，您可以更改上述行为。 它默认为VisibleWhenSelected，详细信息仅在选中行时可见，但您可以将其更改为Visible或Collapsed。 如果将其设置为Visible，则所有详细信息行将始终可见
//如果将其设置为Collapsed，则所有详细信息将始终不可见。

namespace WPF_DataGrid.ViewModel
{
    public class RowDetailsTemplateViewModel : ViewModelBase
    {
        public RowDetailsTemplateViewModel()
        {
            _students = new ObservableCollection<Student>();
            _students.Add(new Student() { ID = "1", Name = "张三", Birthday = new DateTime(1971, 7, 23) });
            _students.Add(new Student() { ID = "2", Name = "李四", Birthday = new DateTime(1991, 9, 2) });
        }

        private ObservableCollection<Student> _students;
        public ObservableCollection<Student> Students
        {
            get
            {
                return _students;
            }
            set
            {
                _students = value;
                RaisePropertyChanged(() => Students);
            }
        }
    }
}
