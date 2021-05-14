using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_MVVM_Normal.Utility;

namespace WPF_MVVM_Normal.ViewModel
{
    [Export(typeof(IPluginViewModel))]
    [ExportMetadata("Type", new[]{ "新窗口" })]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SecondViewModel : ViewModelBase, IPluginViewModel
    {
        private ICommand _window_Loaded;
        public ICommand Window_Loaded
        {
            get
            {
                if (_window_Loaded == null)
                {
                    _window_Loaded = new RelayCommand<RoutedEventArgs>(e => {

                        //(从设计的角度来说，应该ViewModel向ViewModel发送消息，这里简单起见，自己给自己发送消息)

                        //根据来源(Sender)区分消息(这种设计思想不好，NotificationMessage比string类型好的地方就是提供了Sender用来记录发送方，另外Target仅仅提供提示功能)
                        Messenger.Default.Register<NotificationMessage>(this, message => {
                            if(message.Sender is SecondViewModel)
                            {
                                MessageBox.Show(message.Notification);
                            }
                        });

                        //根据令牌(Token)区分消息(这是常用的方式)
                        Messenger.Default.Register<string>(this, "区分消息的令牌", message => {
                            MessageBox.Show(message);
                        });

                        //根据Messenger不同实例(之前都使用相同实例Messenger.Default)区分消息
                        Messenger messenger = new Messenger();
                        SimpleIoc.Default.Register(() => messenger, "SecondViewModel使用的Messenger");
                        messenger.Register<string>(this, message => {
                            MessageBox.Show(message);
                        });

                        //根据变更属性的属性名区分消息
                        Messenger.Default.Register<PropertyChangedMessage<string>>(this, message => { 
                            if(message.PropertyName == "TextWithSendMessage")
                            {
                                ResultWithSendMessage = message.NewValue;
                            }
                        });
                    });
                }
                return _window_Loaded;
            }
        }

        private ICommand _window_Unloaded;
        public ICommand Window_Unloaded
        {
            get
            {
                if (_window_Unloaded == null)
                {
                    _window_Unloaded = new RelayCommand<RoutedEventArgs>(e => {
                        //(注意应该指定跟之前的Register对应)
                        Messenger.Default.Unregister(this);
                    });
                }
                return _window_Unloaded;
            }
        }

        private ICommand _sendNotificationMessage;
        public ICommand SendNotificationMessage
        {
            get
            {
                if(_sendNotificationMessage == null)
                {
                    _sendNotificationMessage = new RelayCommand(() => {
                        //this指定来源(Sender)
                        NotificationMessage nm = new NotificationMessage(this, "这是一个NotificationMessage");
                        Messenger.Default.Send<NotificationMessage>(nm);
                    });
                }
                return _sendNotificationMessage;
            }
        }

        private ICommand _sendStringMessage;
        public ICommand SendStringMessage
        {
            get
            {
                if(_sendStringMessage == null)
                {
                    _sendStringMessage = new RelayCommand(() => {
                        Messenger.Default.Send<string>("这是一个带令牌的字符串Message", "区分消息的令牌");
                    });
                }
                return _sendStringMessage;
            }
        }

        private ICommand _sendCustomMessengerMessage;
        public ICommand SendCustomMessengerMessage
        {
            get
            {
                if(_sendCustomMessengerMessage == null)
                {
                    _sendCustomMessengerMessage = new RelayCommand(() => {
                        Messenger messenger = SimpleIoc.Default.GetInstance<Messenger>("SecondViewModel使用的Messenger");
                        messenger.Send<string>("这是一个采用自定义Messenger实例发送的字符串Message");
                    });
                }
                return _sendCustomMessengerMessage;
            }
        }

        private string _textWithSendMessage;
        public string TextWithSendMessage
        {
            get
            {
                return _textWithSendMessage;
            }
            set
            {
                if(_textWithSendMessage != value)
                {
                    string oldValue = _textWithSendMessage;
                    _textWithSendMessage = value;
                    RaisePropertyChanged(() => TextWithSendMessage, oldValue, value, true);//内部自动调用Messenger.Default.Send
                }
            }
        }

        private string _resultWithSendMessage;
        public string ResultWithSendMessage
        {
            get
            {
                return _resultWithSendMessage;
            }
            set
            {
                if(_resultWithSendMessage != value)
                {
                    _resultWithSendMessage = value;
                    RaisePropertyChanged(() => ResultWithSendMessage);
                }
            }
        }
    }
}
