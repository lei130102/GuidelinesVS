using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_DataGrid.Model;

//用户自定义列

//DataGridTextColumn
//DataGridCheckBoxColumn
//DataGridComboBoxColumn
//DataGridHyperlinkColumn
//DataGridTemplateColumn      允许你定义任意类型的内容

//通过AutoGenerateColumns属性关闭自动生成列的功能，你可以完全控制哪些列可以显示出来，以及这些列的数据是如何显示和编辑。

namespace WPF_DataGrid.ViewModel
{
    public class CustomColumnViewModel : ViewModelBase
    {
        public CustomColumnViewModel()
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
