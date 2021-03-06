using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM_Normal.Model;
using WPF_MVVM_Normal.Utility;

namespace WPF_MVVM_Normal.ViewModel
{
    //选择派生自ViewModelBase，而非ObservableObject是因为他还提供了消息发送的功能
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new[] { "歌曲" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SongViewModel : ViewModelBase, IPluginViewModel
    {
        private Song _song;

        public SongViewModel()
        {
            _song = new Song { Title = "Unknown", Artist = "Unknown" };

            IsProgressRelateEnable = true;
            IsWaitingDisplay = false;
        }

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

                    RaisePropertyChanged(() => Title);
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

                    RaisePropertyChanged(() => Artist);
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

                    RaisePropertyChanged(() => Text);
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

                    RaisePropertyChanged(() => IsProgressRelateEnable);
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

                    RaisePropertyChanged(() => IsWaitingDisplay);
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

                    RaisePropertyChanged(() => ProcessRange);
                }
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
                        if(window != null)
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
                    _showProgressCmd = new RelayCommand(() => {
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
