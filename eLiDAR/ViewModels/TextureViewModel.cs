using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using eLiDAR.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class TextureViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged2;
        public INavigation _navigation;
        public string _thistexture;
        public ICommand ClearCommand { get; private set; }

        private string _master;
        private string _prefix;
        private string _temp;
        private SOIL _soil;
        private ECOSITE _ecosite;
        private bool _issoil = false;

        public TextureViewModel(INavigation navigation, SOIL soil)
        {
            _navigation = navigation;
            _issoil = true;
            _soil = new SOIL();
            _soil = soil;
            TEXTURE = _soil.MINERALTEXTURECODE;
            ClearCommand = new Command(() => ClearItems());
            _temp = _thistexture;
     //       SetCalc();
        }
        public TextureViewModel(INavigation navigation, ECOSITE ecosite)
        {
            _navigation = navigation;
            _ecosite = new ECOSITE();
            _ecosite = ecosite;
            TEXTURE = _ecosite.MINERALTEXTURECODE;
            ClearCommand = new Command(() => ClearItems());
            _temp = _thistexture;
            //       SetCalc();
        }
        void ClearItems()
        {
            TEXTURE = null;
            _ = _navigation.PopAsync();      
        }
      
        public string Title
        {
            get => "Mineral Texture Builder" ;
            set
            {
            }
        }
        void Calc()
        {
            TEXTURE = PREFIX + MASTER;  
        }
        //void SetCalc()
        //{
        //    if (TEXTURE != null && TEXTURE != "")
        //    {
        //        // Need to parse the horizon into parts
        //        int len;
        //        int lastlen;
        //        int horizlen;
        //        len = GLEYCOLOUR.IndexOf("-", 0);
        //        lastlen = GLEYCOLOUR.LastIndexOf("-");
        //        horizlen = GLEYCOLOUR.Length;
        //        if (horizlen >= len) { MASTER = _tempcolour.Substring(0, len); }
        //        if (horizlen >= len + 2) { SUFFIX1 = _tempcolour.Substring(len + 1, lastlen - len - 1); }
        //        if (horizlen >= lastlen + 2) { SUFFIX2 = _tempcolour.Substring(lastlen + 1); }

        //    }
        //}
        public string TEXTURE
        {
            get
            {
                if (_issoil) {return _soil.MINERALTEXTURECODE;}
                else { return _ecosite.MINERALTEXTURECODE; }
            }

            set
            {
                if (_issoil) { _soil.MINERALTEXTURECODE = value; }
                else { _ecosite.MINERALTEXTURECODE = value; }
                NotifyPropertyChanged("TEXTURE");
            }
        }
        public string MASTER
        {
            get => _master;
            set
            {
                _master = value;
                NotifyPropertyChanged("MASTER");
                Calc();
            }
        }

        public string PREFIX
        {
            get => _prefix;
            set
            {
                _prefix = value;
                NotifyPropertyChanged("PREFIX");
                Calc();

            }
        }
       
        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backfield, value))
            {
                return false;
            }
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
