using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using Xamarin.Forms;

namespace eLiDAR.ViewModels
{
    public class BaseEcositeViewModel : INotifyPropertyChanged {

        public ECOSITE _ecosite;
        public INavigation _navigation;
        public IValidator _treeValidator;
        public IEcositeRepository _ecositeRepository;
        public string _fk;

        public event PropertyChangedEventHandler PropertyChanged2;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName]string propertyName =null)
        {
            if (EqualityComparer<T>.Default.Equals(backfield, value))
            {
                return false;
            }
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public string ECOSITEID
        {
            get => _ecosite.ECOSITEID;
            set
            {
                _ecosite.ECOSITEID = value;
                NotifyPropertyChanged("ECOSITEID");
            }
        }

        public string PLOTID
        {
            get => _ecosite.PLOTID;
            set
            {
                _ecosite.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }

        public int HUMUS_FORM
        {
            get => _ecosite.HUMUS_FORM;
            set
            {
                _ecosite.HUMUS_FORM = value;
                NotifyPropertyChanged("HUMUS_FORM");
            }
        }

        public int DRAINAGE
        {
            get => _ecosite.DRAINAGE;
            set
            {
                _ecosite.DRAINAGE = value;
                NotifyPropertyChanged("DRAINAGE");
            }
        }

        public string STRATIFIED
        {
            get => _ecosite.STRATIFIED;
            set
            {
                _ecosite.STRATIFIED = value;
                NotifyPropertyChanged("STRATIFIED");
            }
        }

        public int EFFECTIVE_PORE_PATTERN
        {
            get => _ecosite.EFFECTIVE_PORE_PATTERN;
            set
            {
                _ecosite.EFFECTIVE_PORE_PATTERN = value;
                NotifyPropertyChanged("EFFECTIVE_PORE_PATTERN");
            }
        }

        public string ELC_SUBSTRATE_TYPE
        {
            get => _ecosite.ELC_SUBSTRATE_TYPE;
            set
            {
                _ecosite.ELC_SUBSTRATE_TYPE = value;
                NotifyPropertyChanged("ELC_SUBSTRATE_TYPE");
            }
        }

        public int DEPTH_TO_DISTINCT_MOTTLES
        {
            get => _ecosite.DEPTH_TO_DISTINCT_MOTTLES;
            set
            {
                _ecosite.DEPTH_TO_DISTINCT_MOTTLES = value;
                NotifyPropertyChanged("DEPTH_TO_DISTINCT_MOTTLES");
            }
        }

        public int DEPTH_TO_PROMINENT_MOTTLES
        {
            get => _ecosite.DEPTH_TO_PROMINENT_MOTTLES;
            set
            {
                _ecosite.DEPTH_TO_PROMINENT_MOTTLES = value;
                NotifyPropertyChanged("DEPTH_TO_PROMINENT_MOTTLES");
            }
        }

        public int DEPTH_TO_GLEY
        {
            get => _ecosite.DEPTH_TO_GLEY;
            set
            {
                _ecosite.DEPTH_TO_GLEY = value;
                NotifyPropertyChanged("DEPTH_TO_GLEY");
            }
        }

        public int DEPTH_TO_BEDROCK
        {
            get => _ecosite.DEPTH_TO_BEDROCK;
            set
            {
                _ecosite.DEPTH_TO_BEDROCK = value;
                NotifyPropertyChanged("DEPTH_TO_BEDROCK");
            }
        }

        public int DEPTH_TO_CARBONATES
        {
            get => _ecosite.DEPTH_TO_CARBONATES;
            set
            {
                _ecosite.DEPTH_TO_CARBONATES = value;
                NotifyPropertyChanged("DEPTH_TO_CARBONATES");
            }
        }
        public int MOISTURE_REGIME_DEPTH_CLASS
        {
            get => _ecosite.MOISTURE_REGIME_DEPTH_CLASS;
            set
            {
                _ecosite.MOISTURE_REGIME_DEPTH_CLASS = value;
                NotifyPropertyChanged("MOISTURE_REGIME_DEPTH_CLASS");
            }
        }

        public int MOISTURE_REGIME
        {
            get => _ecosite.MOISTURE_REGIME;
            set
            {
                _ecosite.MOISTURE_REGIME = value;
                NotifyPropertyChanged("MOISTURE_REGIME");
            }
        }

        public string MODE_OF_DEPOSITION1
        {
            get => _ecosite.MODE_OF_DEPOSITION1;
            set
            {
                _ecosite.MODE_OF_DEPOSITION1 = value;
                NotifyPropertyChanged("MODE_OF_DEPOSITION1");
            }
        }

        public string MODE_OF_DEPOSITION2
        {
            get => _ecosite.MODE_OF_DEPOSITION2;
            set
            {
                _ecosite.MODE_OF_DEPOSITION2 = value;
                NotifyPropertyChanged("MODE_OF_DEPOSITION2");
            }
        }

        public int FUNCTIONAL_ROOTING_DEPTH
        {
            get => _ecosite.FUNCTIONAL_ROOTING_DEPTH;
            set
            {
                _ecosite.FUNCTIONAL_ROOTING_DEPTH = value;
                NotifyPropertyChanged("FUNCTIONAL_ROOTING_DEPTH");
            }
        }

        public int MAXIMUM_ROOTING_DEPTH
        {
            get => _ecosite.MAXIMUM_ROOTING_DEPTH;
            set
            {
                _ecosite.MAXIMUM_ROOTING_DEPTH = value;
                NotifyPropertyChanged("MAXIMUM_ROOTING_DEPTH");
            }
        }

        public int DEPTH_TO_ROOT_RESTRICTION
        {
            get => _ecosite.DEPTH_TO_ROOT_RESTRICTION;
            set
            {
                _ecosite.DEPTH_TO_ROOT_RESTRICTION = value;
                NotifyPropertyChanged("DEPTH_TO_ROOT_RESTRICTION");
            }
        }

        public int DEPTH_TO_WATER_TABLE
        {
            get => _ecosite.DEPTH_TO_WATER_TABLE;
            set
            {
                _ecosite.DEPTH_TO_WATER_TABLE = value;
                NotifyPropertyChanged("DEPTH_TO_WATER_TABLE");
            }
        }

        public int DEPTH_TO_COARSE_FRAGS
        {
            get => _ecosite.DEPTH_TO_COARSE_FRAGS;
            set
            {
                _ecosite.DEPTH_TO_COARSE_FRAGS = value;
                NotifyPropertyChanged("DEPTH_TO_COARSE_FRAGS");
            }
        }

        public int PROBLEMATIC_SITE_PROTOCOL_CLASS
        {
            get => _ecosite.PROBLEMATIC_SITE_PROTOCOL_CLASS;
            set
            {
                _ecosite.PROBLEMATIC_SITE_PROTOCOL_CLASS = value;
                NotifyPropertyChanged("PROBLEMATIC_SITE_PROTOCOL_CLASS");
            }
        }

        public string SEEPAGE
        {
            get => _ecosite.SEEPAGE;
            set
            {
                _ecosite.SEEPAGE = value;
                NotifyPropertyChanged("SEEPAGE");
            }
        }

        public string SOIL_PIT_PHOTO
        {
            get => _ecosite.SOIL_PIT_PHOTO;
            set
            {
                _ecosite.SOIL_PIT_PHOTO = value;
                NotifyPropertyChanged("SOIL_PIT_PHOTO");
            }
        }

        public string PRI_ECO
        {
            get => _ecosite.PRI_ECO;
            set
            {
                _ecosite.PRI_ECO = value;
                NotifyPropertyChanged("PRI_ECO");
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

        public int AZIMUTH
        {
            get => _ecosite.AZIMUTH;
            set
            {
                _ecosite.AZIMUTH = value;
                NotifyPropertyChanged("AZIMUTH");
            }
        }

        public int DISTANCE
        {
            get => _ecosite.DISTANCE;
            set
            {
                _ecosite.DISTANCE = value;
                NotifyPropertyChanged("DISTANCE");
            }
        }
               
        List<ECOSITE> _ecositeList;
        public List<ECOSITE> EcositeList
        {
            get => _ecositeList;
            set
            {
                _ecositeList = value;
                NotifyPropertyChanged("StemMapList");
            }
        }

        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = ""){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
