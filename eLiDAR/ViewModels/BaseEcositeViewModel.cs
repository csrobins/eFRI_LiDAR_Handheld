using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
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
            get => _ecosite.HUMUSFORMCODE;
            set
            {
                _ecosite.HUMUSFORMCODE = value;
                NotifyPropertyChanged("HUMUS_FORM");
            }
        }

        public int DRAINAGE
        {
            get => _ecosite.DRAINAGECLASSCODE;
            set
            {
                _ecosite.DRAINAGECLASSCODE = value;
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

        public string EFFECTIVE_PORE_PATTERN
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
            get => _ecosite.DEPTHTODISTINCTMOTTLES;
            set
            {
                _ecosite.DEPTHTODISTINCTMOTTLES = value;
                NotifyPropertyChanged("DEPTH_TO_DISTINCT_MOTTLES");
            }
        }

        public int DEPTH_TO_PROMINENT_MOTTLES
        {
            get => _ecosite.DEPTHTOPROMINENTMOTTLES;
            set
            {
                _ecosite.DEPTHTOPROMINENTMOTTLES = value;
                NotifyPropertyChanged("DEPTH_TO_PROMINENT_MOTTLES");
            }
        }

        public int DEPTH_TO_GLEY
        {
            get => _ecosite.DEPTHTOGLEY;
            set
            {
                _ecosite.DEPTHTOGLEY = value;
                NotifyPropertyChanged("DEPTH_TO_GLEY");
            }
        }

        public int DEPTH_TO_BEDROCK
        {
            get => _ecosite.DEPTHTOBEDROCK;
            set
            {
                _ecosite.DEPTHTOBEDROCK = value;
                NotifyPropertyChanged("DEPTH_TO_BEDROCK");
            }
        }

        public int DEPTH_TO_CARBONATES
        {
            get => _ecosite.DEPTHTOCARBONATES;
            set
            {
                _ecosite.DEPTHTOCARBONATES = value;
                NotifyPropertyChanged("DEPTH_TO_CARBONATES");
            }
        }
        public string MOISTURE_REGIME_DEPTH_CLASS
        {
            get => _ecosite.MOISTURE_REGIME_DEPTH_CLASS;
            set
            {
                _ecosite.MOISTURE_REGIME_DEPTH_CLASS = value;
                NotifyPropertyChanged("MOISTURE_REGIME_DEPTH_CLASS");
            }
        }

        public string MOISTURE_REGIME
        {
            get => _ecosite.MOISTUREREGIMECODE;
            set
            {
                _ecosite.MOISTUREREGIMECODE = value;
                NotifyPropertyChanged("MOISTURE_REGIME");
            }
        }

        public string MODE_OF_DEPOSITION1
        {
            get => _ecosite.MODEOFDEPOSITIONCODE1;
            set
            {
                _ecosite.MODEOFDEPOSITIONCODE1 = value;
                NotifyPropertyChanged("MODE_OF_DEPOSITION1");
            }
        }

        public string MODE_OF_DEPOSITION2
        {
            get => _ecosite.MODEOFDEPOSITIONCODE2;
            set
            {
                _ecosite.MODEOFDEPOSITIONCODE2 = value;
                NotifyPropertyChanged("MODE_OF_DEPOSITION2");
            }
        }

        public int FUNCTIONAL_ROOTING_DEPTH
        {
            get => _ecosite.FUNCTIONALROOTINGDEPTH;
            set
            {
                _ecosite.FUNCTIONALROOTINGDEPTH = value;
                NotifyPropertyChanged("FUNCTIONAL_ROOTING_DEPTH");
            }
        }

        public int MAXIMUM_ROOTING_DEPTH
        {
            get => _ecosite.MAXIMUMROOTINGDEPTH;
            set
            {
                _ecosite.MAXIMUMROOTINGDEPTH = value;
                NotifyPropertyChanged("MAXIMUM_ROOTING_DEPTH");
            }
        }

        public int DEPTH_TO_ROOT_RESTRICTION
        {
            get => _ecosite.DEPTHTOROOTRESTRICTION;
            set
            {
                _ecosite.DEPTHTOROOTRESTRICTION = value;
                NotifyPropertyChanged("DEPTH_TO_ROOT_RESTRICTION");
            }
        }

        public int DEPTH_TO_WATER_TABLE
        {
            get => _ecosite.DEPTHTOWATERTABLE;
            set
            {
                _ecosite.DEPTHTOWATERTABLE = value;
                NotifyPropertyChanged("DEPTH_TO_WATER_TABLE");
            }
        }

        public int DEPTH_TO_COARSE_FRAGS
        {
            get => _ecosite.DEPTHTOIMPASSABLECOARSEFRAGMENTS;
            set
            {
                _ecosite.DEPTHTOIMPASSABLECOARSEFRAGMENTS = value;
                NotifyPropertyChanged("DEPTH_TO_COARSE_FRAGS");
            }
        }

        public string PROBLEMATIC_SITE_PROTOCOL_CLASS
        {
            get => _ecosite.PROBLEMATICSITE;
            set
            {
                _ecosite.PROBLEMATICSITE = value;
                NotifyPropertyChanged("PROBLEMATIC_SITE_PROTOCOL_CLASS");
            }
        }

        public string SEEPAGE
        {
            get => _ecosite.DEPTHTOSEEPAGE;
            set
            {
                _ecosite.DEPTHTOSEEPAGE = value;
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
            get => _ecosite.PITAZIMUTH;
            set
            {
                _ecosite.PITAZIMUTH = value;
                NotifyPropertyChanged("AZIMUTH");
            }
        }

        public Single DISTANCE
        {
            get => _ecosite.PITDISTANCE;
            set
            {
                _ecosite.PITDISTANCE = value;
                NotifyPropertyChanged("DISTANCE");
            }
        }

        public string COMMENTS
        {
            get => _ecosite.SUBSTRATENOTE;
            set
            {
                _ecosite.SUBSTRATENOTE = value;
                NotifyPropertyChanged("COMMENTS");
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
