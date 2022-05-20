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
    public class SoilColourViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged2;
        public INavigation _navigation;
        public SOIL _thissoil;
       
        public ICommand ClearCommand { get; private set; }

        private string _master;
        private string _suffix1;
        private string _suffix2;
        private string _tempcolour;
        private bool _ischanged;

        public SoilColourViewModel(INavigation navigation, SOIL _soil, bool ischanged)
        {
            _navigation = navigation;
            _thissoil = _soil;
            _ischanged = ischanged;
            
            ClearCommand = new Command(() => ClearItems());
            _tempcolour = _thissoil.MATRIXCOLOUR;
            SetCalc();
    }
        void ClearItems()
        {
            _thissoil.MATRIXCOLOUR = "";
            _ = _navigation.PopAsync();
       
        }
      
        public string SoilTitle
        {
            get => "Soil Colour" ;
            set
            {
            }
        }
        void Calc()
        {
            COLOUR = MASTER + "-" + SUFFIX1 + "-" + SUFFIX2;  
        }
        void SetCalc()
        {
            if (COLOUR != null && COLOUR != "")
            {
                // Need to parse the horizon into parts
                int len;
                int lastlen;
                int horizlen;
                len = COLOUR.IndexOf("-", 0);
                lastlen = COLOUR.LastIndexOf("-");
                horizlen = COLOUR.Length;
                if (horizlen >= len) { MASTER = _tempcolour.Substring(0, len); }
                if (horizlen >= len + 2) { SUFFIX1 = _tempcolour.Substring(len+1, lastlen-len-1); }
                if (horizlen >= lastlen + 2) { SUFFIX2 = _tempcolour.Substring(lastlen + 1); }

            }
        }
        public string COLOUR
        {
            get
            {
                return _thissoil.MATRIXCOLOUR;
            }

            set
            {
                if (_thissoil.MATRIXCOLOUR != value) { _ischanged = true; }
                _thissoil.MATRIXCOLOUR  = value;
                NotifyPropertyChanged("COLOUR");
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

        public string SUFFIX1
        {
            get => _suffix1;
            set
            {
                _suffix1 = value;
                NotifyPropertyChanged("SUFFIX1");
                Calc();

            }
        }
        public string SUFFIX2
        {
            get => _suffix2;
            set
            {
                _suffix2 = value;
                NotifyPropertyChanged("SUFFIX2");
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
