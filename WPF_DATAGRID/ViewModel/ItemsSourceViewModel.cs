using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_DataGrid.Model;

//只要绑定ObservableCollection<T>类型的属性到ItemsSource，就可以自动显示T中每个属性代表的列(固定死的)
//虽然T中的每个属性都没有调用RaisePropertyChanged，或者说T没有派生自INotifyPropertyChanged，但是也会自动更新

namespace WPF_DataGrid.ViewModel
{
    public class ItemsSourceViewModel : ViewModelBase
    {
        public ItemsSourceViewModel()
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

        private ICommand _window_Closing;
        public ICommand Window_Closing
        {
            get
            {
                if(_window_Closing == null)
                {
                    _window_Closing = new RelayCommand<System.ComponentModel.CancelEventArgs>(e => {

                        //窗口即将关闭，Closing事件处理可以取消窗口关闭

                        //检查 _students 的更新情况
                        //测试发现更新全面，即使Student中成员属性没有调用RaisePropertyChanged函数
                        foreach (var student in _students)
                        {
                            string ID = student.ID;
                            string Name = student.Name;
                            string BirthDay = student.Birthday.ToString();

                            string temp = ID + Name + BirthDay;
                        }
                    });
                }
                return _window_Closing;
            }
        }
    }
}
