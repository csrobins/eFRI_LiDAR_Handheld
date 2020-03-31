using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Services;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using eLiDAR.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class EcositeCodeViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public ECOSITE _ecosite;
        public List<PickerItemsString> ListEcosite = PickerService.EcositeItems().ToList();
        private string _getecosite1;
        private string _getecosite2;
        public EcositeCodeViewModel(INavigation navigation, ECOSITE _thisecosite)
        {
            _navigation = navigation;
            _ecosite = _thisecosite;
        }
        void SetEcositeCode(int ecocode)
        {
         if (ecocode == 1)
            {
                GetEcosite1 = PickerService.GetItem(ListEcosite, PRI_ECO).NAME;
            }
            else 
            {
                GetEcosite2 = PickerService.GetItem(ListEcosite, SEC_ECO).NAME;

            }
        }
        public string GetEcosite1
        {
            get
            {
                return _getecosite1;
            }
            set
            {
                _getecosite1 = value;
                NotifyPropertyChanged("GetEcosite1");
            }
        }
        public string GetEcosite2
        {
            get
            {
                return _getecosite2;
            }
            set
            {
                _getecosite2 = value;
                NotifyPropertyChanged("GetEcosite2");
            }
        }
        public string CommentsTitle
        {
            get => "Ecosite codes" ;
            set
            {
            }
        }
        public string PRI_ECO
        {
            get => _ecosite.PRI_ECO;
            set
            {
                _ecosite.PRI_ECO = value;
                NotifyPropertyChanged("PRI_ECO");
                SetEcositeCode(1); 
            }
        }

        public int PRI_ECO_PCT
        {
            get => _ecosite.PRI_ECO_PCT;
            set
            {
                _ecosite.PRI_ECO_PCT = value;
                NotifyPropertyChanged("PRI_ECO_PCT");
            }
        }

        public string SEC_ECO
        {
            get => _ecosite.SEC_ECO;
            set
            {
                _ecosite.SEC_ECO = value;
                NotifyPropertyChanged("SEC_ECO");
                SetEcositeCode(2);
            }
        }
        public int SEC_ECO_PCT
        {
            get => _ecosite.SEC_ECO_PCT;
            set
            {
                _ecosite.SEC_ECO_PCT = value;
                NotifyPropertyChanged("SEC_ECO_PCT");
            }
        }

        public string COMMENTS
        {
            get => _ecosite.COMMENTS;
            set
            {
                _ecosite.COMMENTS = value;
                NotifyPropertyChanged("COMMENTS");
            }
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
