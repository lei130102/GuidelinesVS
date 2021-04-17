using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_MVVM_Basic.Model;
using WPF_MVVM_Basic.Utility;

namespace WPF_MVVM_Basic.ViewModel
{
    public class SongViewModel : INotifyPropertyChanged
    {
        private Song _song;

        public SongViewModel()
        {
            _song = new Song { Title = "Unknown", Artist = "Unknown" };
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
                    }, () => true);
                }
                return _changeValue;
            }
        }
    }
}
