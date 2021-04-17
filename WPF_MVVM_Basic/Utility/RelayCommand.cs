using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WPF_MVVM_Basic.Utility
{
    /// <summary>
    /// 绑定到Command的对象必须派生自ICommand
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Func<bool> _canExecute;
        private readonly Action _execute;


        public RelayCommand(Action execute)
            : this(execute, null)
        {}

        public RelayCommand(Action execute, Func<bool> canExecute)
        { 
            if(execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }

        #region ICommand
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if(_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if(_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
        #endregion
    }
}
