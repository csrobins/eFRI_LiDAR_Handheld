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
    public class BaseTreeViewModel : INotifyPropertyChanged {

        public TREE _tree;
        public INavigation _navigation;
        public IValidator _treeValidator;
        public ITreeRepository _treeRepository;
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
        private bool _IsChanged = false;
        public bool IsChanged
        {
            get => _IsChanged;
            set
            {
                _IsChanged = value;
            }
        }
        private bool _islivetree;
        public bool IsLiveTree
        {
            get
            {
                return _islivetree;
            }
            set { 
                _islivetree = value;
                _isnotlivetree = !value;
                NotifyPropertyChanged("IsLiveTree");
                NotifyPropertyChanged("IsNotLiveTree");
            }
        }
        private bool _isdecayclass1to3;
        public bool IsDecayClass1to3
        {
            get
            {
                return _isdecayclass1to3;
            }
            set
            {
                _isdecayclass1to3 = value;
                NotifyPropertyChanged("_isdecayclass1to3");
            }
        }
       
        private bool _isnotlivetree;
        public bool IsNotLiveTree
        {
            get
            {
                return _isnotlivetree;
            }
            set
            {
                _isnotlivetree = value;
                NotifyPropertyChanged("IsLiveNotTree");
            }
        }
        public string TREEID
        {
            get => _tree.TREEID;
            set
            {
                _tree.TREEID = value;
                NotifyPropertyChanged("TREEID");
            }
        }

        public string PLOTID
        {
            get => _tree.PLOTID;
            set
            {
                _tree.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }

        public int TREENUM
        {
            get => _tree.TREENUMBER;
            set
            {
                _tree.TREENUMBER = (int)value;
                NotifyPropertyChanged("TREENUM");
                IsChanged = true;
            }
        }
        public int SECTION
        {
            get => _tree.SECTION;
            set
            {
                _tree.SECTION = (int)value;
                NotifyPropertyChanged("SECTION");
                IsChanged = true;
            }
        }
        public int CROWN_POSITION
        {
            get => _tree.CROWN_POSITION;
            set
            {
                _tree.CROWN_POSITION = value;
                NotifyPropertyChanged("CRWON_POSITION");
                IsChanged = true;
            }
        }

        public int SPECIES
        {
            get => _tree.SPECIESCODE;
            set
            {
                _tree.SPECIESCODE = value;
                NotifyPropertyChanged("SPECIES");
                IsChanged = true;
            }
        }

        public string TAG_TYPE
        {
            get => _tree.TAG_TYPE;
            set
            {
                _tree.TAG_TYPE = value;
                NotifyPropertyChanged("TAG_TYPE");
                IsChanged = true;
            }
        }

        public string ORIGIN
        {
            get => _tree.TREEORIGINCODE;
            set
            {
                _tree.TREEORIGINCODE = value;
                NotifyPropertyChanged("ORIGIN");
                IsChanged = true;
            }
        }

        public string STATUS
        {
            get => _tree.TREESTATUSCODE;
            set
            {
                _tree.TREESTATUSCODE = value;
                if (STATUS == "L" || STATUS == "V" || STATUS == "M" || STATUS == "E")
                {
                    IsLiveTree = true;
                }
                else { IsLiveTree = false; }
                NotifyPropertyChanged("STATUS");
                IsChanged = true;
            }
        }

        public int VIGOUR
        {
            get => _tree.VIGOURCODE;
            set
            {
                _tree.VIGOURCODE = value;
                NotifyPropertyChanged("VIGOUR");
                IsChanged = true;
            }
        }
        public Single LENGTH
        {
            get => _tree.LENGTH;
            set
            {
                _tree.LENGTH = value;
                NotifyPropertyChanged("LENGTH");
                IsChanged = true;
            }
        }
        public float HT_TO_DBH
        {
            get => _tree.HEIGHTTODBH;
            set
            {
                _tree.HEIGHTTODBH = value;
                NotifyPropertyChanged("HT_TO_DBH");
                IsChanged = true;
            }
        }

        public float DBH
        {
            get => _tree.DBH;
            set
            {
                _tree.DBH = value;
                NotifyPropertyChanged("DBH");
                IsChanged = true;
            }
        }

        public float HT
        {
            get => _tree.DIRECTTOTALHEIGHT;
            set
            {
                _tree.DIRECTTOTALHEIGHT = value;
                NotifyPropertyChanged("HT");
                IsChanged = true;
            }
        }
        public float OCUALRTOTALHEIGHT
        {
            get => _tree.OCUALRTOTALHEIGHT;
            set
            {
                _tree.OCUALRTOTALHEIGHT = value;
                NotifyPropertyChanged("OCUALRTOTALHEIGHT");
                IsChanged = true;
            }
        }
        public float HEIGHTTODEADTIP
        {
            get => _tree.HEIGHTTODEADTIP;
            set
            {
                _tree.HEIGHTTODEADTIP = value;
                NotifyPropertyChanged("HEIGHTTODEADTIP");
                IsChanged = true;
            }
        }
        public float DIRECTHEIGHTTOCONTLIVECROWN
        {
            get => _tree.DIRECTHEIGHTTOCONTLIVECROWN;
            set
            {
                _tree.DIRECTHEIGHTTOCONTLIVECROWN = value;
                NotifyPropertyChanged("DIRECTHEIGHTTOCONTLIVECROWN");
                IsChanged = true;
            }
        }
        public float OCULARHEIGHTTOCONTLIVECROWN
        {
            get => _tree.OCULARHEIGHTTOCONTLIVECROWN;
            set
            {
                _tree.OCULARHEIGHTTOCONTLIVECROWN = value;
                NotifyPropertyChanged("OCULARHEIGHTTOCONTLIVECROWN");
                IsChanged = true;
            }
        }
        public float DIRECTOFFSETDISTANCE
        {
            get => _tree.DIRECTOFFSETDISTANCE;
            set
            {
                _tree.DIRECTOFFSETDISTANCE = value;
                NotifyPropertyChanged("DIRECTOFFSETDISTANCE");
                IsChanged = true;
            }
        }
        public int DEGREEOFLEAN
        {
            get => _tree.DEGREEOFLEAN;
            set
            {
                _tree.DEGREEOFLEAN = value;
                NotifyPropertyChanged("DEGREEOFLEAN");
                IsChanged = true;
            }
        }
        public float HEIGHTTOCORE
        {
            get => _tree.HEIGHTTOCORE;
            set
            {
                _tree.HEIGHTTOCORE = value;
                NotifyPropertyChanged("HEIGHTTOCORE");
                IsChanged = true;
            }
        }
        public string CORESTATUSCODE
        {
            get => _tree.CORESTATUSCODE;
            set
            {
                _tree.CORESTATUSCODE = value;
                NotifyPropertyChanged("CORESTATUSCODE");
                IsChanged = true;
            }
        }
        public float SOUNDWOODLENGTH
        {
            get => _tree.SOUNDWOODLENGTH;
            set
            {
                _tree.SOUNDWOODLENGTH = value;
                NotifyPropertyChanged("SOUNDWOODLENGTH");
                IsChanged = true;
            }
        }
        public int FIELDAGE
        {
            get => _tree.FIELDAGE;
            set
            {
                _tree.FIELDAGE = value;
                NotifyPropertyChanged("FIELDAGE");
                IsChanged = true;

            }
        }
        public int OFFICERINGCOUNT
        {
            get => _tree.OFFICERINGCOUNT;
            set
            {
                _tree.OFFICERINGCOUNT = value;
                NotifyPropertyChanged("OFFICERINGCOUNT");
                IsChanged = true;
            }
        }
        public int MISSINGRINGS
        {
            get => _tree.MISSINGRINGS;
            set
            {
                _tree.MISSINGRINGS = value;
                NotifyPropertyChanged("MISSINGRINGS");
                IsChanged = true;
            }
        }

        public string DBH_IN
        {
            get => _tree.DBHIN;
            set
            {
                _tree.DBHIN = value;
                NotifyPropertyChanged("DBH_IN");
                IsChanged = true;
            }
        }

        public string CROWN_IN
        {
            get => _tree.CROWNIN;
            set
            {
                _tree.CROWNIN = value;
                NotifyPropertyChanged("CROWN_IN");
                IsChanged = true;
            }
        }

        public int LIVE_CROWN_RATIO
        {
            get => _tree.LIVE_CROWN_RATIO;
            set
            {
                _tree.LIVE_CROWN_RATIO = (int)value;
                NotifyPropertyChanged("LIVE_CROWN_RATIO");
            }
        }

        public string CROWN_CLASS
        {
            get => _tree.CROWNCLASSCODE;
            set
            {
                _tree.CROWNCLASSCODE = value;
                NotifyPropertyChanged("CROWN_CLASS");
            }
        }

        public int CROWN_DAMAGE
        {
            get => _tree.CROWNDAMAGECODE;
            set
            {
                _tree.CROWNDAMAGECODE = value;
                NotifyPropertyChanged("CROWN_DAMAGE");
            }
        }

        public int DEFOLIATING_INSECT
        {
            get => _tree.DEFOLIATING_INSECT;
            set
            {
                _tree.DEFOLIATING_INSECT = value;
                NotifyPropertyChanged("DEFOLIATING_INSECT");
            }
        }

        public int FOLIAR_DISEASE
        {
            get => _tree.FOLIAR_DISEASE;
            set
            {
                _tree.FOLIAR_DISEASE = value;
                NotifyPropertyChanged("FOLIAR_DISEASE");
            }
        }

        public string STEM_QUALITY
        {
            get => _tree.STEMQUALITYCODE;
            set
            {
                _tree.STEMQUALITYCODE = value;
                NotifyPropertyChanged("STEM_QUALITY");
            }
        }

        public int BARK_RETENTION
        {
            get => _tree.BARKRETENTIONCODE;
            set
            {
                _tree.BARKRETENTIONCODE = value;
                NotifyPropertyChanged("BARK_RETENTION");
                IsChanged = true;
            }
        }

        public int WOOD_CONDITION
        {
            get => _tree.WOODCONDITIONCODE;
            set
            {
                _tree.WOODCONDITIONCODE = value;
                NotifyPropertyChanged("WOOD_CONDITION");
                IsChanged = true;
            }
        }

        public int DECAY_CLASS
        {
            get => _tree.DECAYCLASS;
            set
            {
                _tree.DECAYCLASS = value;
                NotifyPropertyChanged("DECAY_CLASS");
                if (value < 4 && IsNotLiveTree ) {IsDecayClass1to3 = true;} else { IsDecayClass1to3 = false; }
            }
        }

        public int MORT_CAUSE
        {
            get => _tree.MORTALITYCAUSECODE;
            set
            {
                _tree.MORTALITYCAUSECODE = value;
                NotifyPropertyChanged("MORT_CAUSE");
            }
        }

        public string BROKEN_TOP
        {
            get => _tree.BROKENTOP;
            set
            {
                _tree.BROKENTOP = value;
                NotifyPropertyChanged("BROKEN_TOP");
            }
        }

        public double AZIMUTH
        {
            get => _tree.AZIMUTH;
            set
            {
                _tree.AZIMUTH = value;
                NotifyPropertyChanged("AZIMUTH");
            }
        }

        public double DISTANCE
        {
            get => _tree.DISTANCE;
            set
            {
                _tree.DISTANCE = value;
                NotifyPropertyChanged("DISTANCE");
            }
        }

        public string COMMENTS
        {
            get => _tree.COMMENTS;
            set
            {
                _tree.COMMENTS  = value;
                NotifyPropertyChanged("COMMENTS");
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
