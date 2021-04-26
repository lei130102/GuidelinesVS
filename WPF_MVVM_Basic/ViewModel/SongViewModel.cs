using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM_Basic.Model;
using WPF_MVVM_Basic.Utility;

namespace WPF_MVVM_Basic.ViewModel
{
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new[] { "歌曲" })]
    public class SongViewModel : INotifyPropertyChanged, IPluginViewModel
    {
        private Song _song;

        public SongViewModel()
        {
            _song = new Song { Title = "Unknown", Artist = "Unknown" };

            IsProgressRelateEnable = true;
            IsWaitingDisplay = false;
        }

        //实际上也可以创建Song属性，然后绑定Song.Title和Song.Artist
        public string Title
        {
            get
            {
                return _song.Title;
            }
            set
            {
                if(_song.Title != value)
                {
                    _song.Title = value;

                    RaisePropertyChanged("Title");
                }
            }
        }

        public string Artist
        {
            get
            {
                return _song.Artist;
            }
            set
            {
                if(_song.Artist != value)
                {
                    _song.Artist = value;

                    RaisePropertyChanged("Artist");
                }
            }
        }

        private string _text = "星期一";
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if(_text != value)
                {
                    _text = value;

                    RaisePropertyChanged("Text");
                }
            }
        }

        private bool _isProgressRelateEnable;
        /// <summary>
        /// 是否使进度框相关的可用
        /// </summary>
        public bool IsProgressRelateEnable
        {
            get
            {
                return _isProgressRelateEnable;
            }
            set
            {
                if(_isProgressRelateEnable != value)
                {
                    _isProgressRelateEnable = value;

                    RaisePropertyChanged("IsProgressRelateEnable");
                }
            }
        }

        private bool _isWaitingDisplay;
        /// <summary>
        /// 是否显示进度框
        /// </summary>
        public bool IsWaitingDisplay
        {
            get
            {
                return _isWaitingDisplay;
            }
            set
            {
                if(_isWaitingDisplay != value)
                {
                    _isWaitingDisplay = value;

                    RaisePropertyChanged("IsWaitingDisplay");
                }
            }
        }

        private int _processRange;
        /// <summary>
        /// 进度比例
        /// </summary>
        public int ProcessRange
        {
            get
            {
                return _processRange;
            }
            set
            {
                if(_processRange != value)
                {
                    _processRange = value;

                    RaisePropertyChanged("ProcessRange");
                }
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            //take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ICommand _changeValue;
        public ICommand ChangeValue
        {
            get
            {
                if(_changeValue == null)
                {
                    _changeValue = new RelayCommand(() => {

                        Title = "Title被修改";
                        Artist = "Artist被修改";

                        IPluginView view = PluginCatalogService.Instance.GetPlugin<IPluginView>("新窗口");
                        Window window = view as Window;
                        if (window != null)
                        {
                            window.Show();
                        }

                    }, () => true);
                }


                return _changeValue;
            }
        }

        private ICommand _showProgressCmd;
        public ICommand ShowProgressCmd
        {
            get
            {
                if(_showProgressCmd == null)
                {
                    _showProgressCmd = new RelayCommand(()=> {

                        BackgroundTask backgroundTask = new BackgroundTask();
                        backgroundTask.UpdateUI += new EventHandler<BackgroundTask.UpdateUIEventArgs>(updateUI);
                        backgroundTask.Start();

                        IsProgressRelateEnable = false;
                        IsWaitingDisplay = true;
                    });
                }
                return _showProgressCmd;
            }
        }

        private void updateUI(object sender, BackgroundTask.UpdateUIEventArgs e)
        {
            //注意这里
            DispatcherHelper.CheckBeginInvokeOnUI(() => { 
                if(e.IsFinish)
                {
                    IsProgressRelateEnable = true;
                    IsWaitingDisplay = false;
                }
                else
                {
                    ProcessRange = e.Process;
                }
            });
        }
    }
}
