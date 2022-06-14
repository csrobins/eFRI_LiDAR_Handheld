using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using eLiDAR.Validator;
using FluentValidation.Results;
using System.Linq;

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
            else { IsChanged = true; }
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
                TreeValidator _fullvalidator = new TreeValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_tree);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);
                _IsChanged = value;
            }
        }
        private bool _islivetree = true;
        public bool IsLiveTree
        {
            get
            {
                return _islivetree;
            }
            set { 
                _islivetree = value;
                IsNotLiveTree = !value;
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
                NotifyPropertyChanged("IsDecayClass1to3");
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
                if (_isnotlivetree)
                {
                    if ( DIRECTHEIGHTTOCONTLIVECROWN != 999)
                    { 
                        DIRECTHEIGHTTOCONTLIVECROWN = 999;
                    }
                    if ( OCULARHEIGHTTOCONTLIVECROWN != 999)
                    {
                        OCULARHEIGHTTOCONTLIVECROWN = 999;
                    }
                }
                NotifyPropertyChanged("IsNotLiveTree");
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

        public bool ThisIsPlotTypeB
        {
            get
            {
                List<string> plots = new List<string> { "B", "C" };
                if (plots.Any(_treeRepository.GetPlotType(_tree.PLOTID).Contains))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool ThisIsPlotTypeC
        {
            get
            {
                List<string> plots = new List<string>{"C"};
                if (plots.Any(_treeRepository.GetPlotType(_tree.PLOTID).Contains))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int TREENUM
        {
            get => _tree.TREENUMBER;
            set
            {
                if (!_tree.TREENUMBER.Equals(value)) { _tree.TREENUMBER = (int)value; IsChanged = true; }

                NotifyPropertyChanged("TREENUM");
         
            }
        }
        public int SECTION
        {
            get => _tree.SECTION;
            set
            {
                if (!_tree.SECTION.Equals(value)) { IsChanged = true; }
                _tree.SECTION = (int)value;
                NotifyPropertyChanged("SECTION");
                
            }
        }
        public int CROWN_POSITION
        {
            get => _tree.CROWN_POSITION;
            set
            {
                if (!_tree.CROWN_POSITION.Equals(value)) { IsChanged = true; }
                _tree.CROWN_POSITION = value;
                NotifyPropertyChanged("CRWON_POSITION");
                
            }
        }

        public int SPECIES
        {
            get => _tree.SPECIESCODE;
            set
            {
                if (!_tree.SPECIESCODE.Equals(value)) { IsChanged = true; }
                _tree.SPECIESCODE = value;
                NotifyPropertyChanged("SPECIES");
                
            }
        }

        public string TAG_TYPE
        {
            get => _tree.TAG_TYPE;
            set
            {
                if (_tree.TAG_TYPE !=value) { IsChanged = true; }
                _tree.TAG_TYPE = value;
                NotifyPropertyChanged("TAG_TYPE");
                
            }
        }

        public string ORIGIN
        {
            get => _tree.TREEORIGINCODE;
            set
            {
                if (_tree.TREEORIGINCODE !=value) { IsChanged = true; }
                _tree.TREEORIGINCODE = value;
                NotifyPropertyChanged("ORIGIN");
            }
        }

        public string STATUS
        {
            get => _tree.TREESTATUSCODE;
            set
            {
                if (_tree.TREESTATUSCODE !=value) { IsChanged = true; }
                _tree.TREESTATUSCODE = value;
                if (value == "L" || value == "V" || value == "M" || value == "E")
                {
                    IsLiveTree = true;
                }
                else { IsLiveTree = false; }
                
                NotifyPropertyChanged("STATUS");
               
            }
        }
        public string VSNSTATUSCODE
        {
            get => _tree.VSNSTATUSCODE;
            set
            {
                if (_tree.VSNSTATUSCODE != value) { IsChanged = true; }
                _tree.VSNSTATUSCODE = value;
                NotifyPropertyChanged("VSNSTATUSCODE");
               
            }
        }

        public int VIGOUR
        {
            get => _tree.VIGOURCODE;
            set
            {
                if (!_tree.VIGOURCODE.Equals(value)) { IsChanged = true; }
                _tree.VIGOURCODE = value;
                NotifyPropertyChanged("VIGOUR");
                
            }
        }
        public Single LENGTH
        {
            get => _tree.LENGTH;
            set
            {
                if (Math.Abs(LENGTH - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_tree.LENGTH.Equals(value)) { _tree.LENGTH = value; IsChanged = true; }

                    NotifyPropertyChanged("LENGTH");
                   
                }
            }
        }
        public float HT_TO_DBH
        {
            get => _tree.HEIGHTTODBH;
            set
            {
                if (Math.Abs(HT_TO_DBH - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_tree.HEIGHTTODBH.Equals(value)) { _tree.HEIGHTTODBH = value; IsChanged = true; }

                    NotifyPropertyChanged("HT_TO_DBH");
                    
                }
            }
        }

        public float DBH
        {
            get => _tree.DBH;
            set
            {
                if (!_tree.DBH.Equals(value)) { _tree.DBH = value; IsChanged = true; }

                NotifyPropertyChanged("DBH");
               
            }
        }

        public float HT
        {
            get => _tree.DIRECTTOTALHEIGHT;
            set
            {
               if (Math.Abs(HT - value) >= 0.001) // Some threshold value suitable for your scenario
                 {
                    if (!_tree.DIRECTTOTALHEIGHT.Equals(value)) 
                    {
                        _tree.DIRECTTOTALHEIGHT = value; 
                        IsChanged = true; }

                    NotifyPropertyChanged("HT");
                    
                }
            }
        }
        public float OCUALRTOTALHEIGHT
        {
            get => _tree.OCULARTOTALHEIGHT;
            set
            {
            if (Math.Abs(OCUALRTOTALHEIGHT - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                if (!_tree.OCULARTOTALHEIGHT.Equals(value)) 
                    {
                        _tree.OCULARTOTALHEIGHT = value;
                        IsChanged = true; }
                NotifyPropertyChanged("OCUALRTOTALHEIGHT");
                
                }
            }
        }
        public float HEIGHTTODEADTIP
        {
            get => _tree.HEIGHTTODEADTIP;
            set
            {
                if (Math.Abs(HEIGHTTODEADTIP - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                if (!_tree.HEIGHTTODEADTIP.Equals(value)) 
                    {
                        _tree.HEIGHTTODEADTIP = value;
                        IsChanged = true;
                        NotifyPropertyChanged("HEIGHTTODEADTIP");

                    }
                }
            }
        }
        public float DIRECTHEIGHTTOCONTLIVECROWN
        {
            get => _tree.DIRECTHEIGHTTOCONTLIVECROWN;
            set
            {
                if (Math.Abs(DIRECTHEIGHTTOCONTLIVECROWN - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                if (!_tree.DIRECTHEIGHTTOCONTLIVECROWN.Equals(value)) 
                    {
                        _tree.DIRECTHEIGHTTOCONTLIVECROWN = value; 
                        IsChanged = true; }

                NotifyPropertyChanged("DIRECTHEIGHTTOCONTLIVECROWN");
                }
            }
        }
        public float OCULARHEIGHTTOCONTLIVECROWN
        {
            get => _tree.OCULARHEIGHTTOCONTLIVECROWN;
            set
            {
                if (Math.Abs(OCULARHEIGHTTOCONTLIVECROWN - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_tree.OCULARHEIGHTTOCONTLIVECROWN.Equals(value)) {
                        _tree.OCULARHEIGHTTOCONTLIVECROWN = value; 
                        IsChanged = true; }

                    NotifyPropertyChanged("OCULARHEIGHTTOCONTLIVECROWN");       
                }
            }
        }
        public float DIRECTOFFSETDISTANCE
        {
            get => _tree.DIRECTOFFSETDISTANCE;
            set
            {
        if (Math.Abs(DIRECTOFFSETDISTANCE - value) >= 0.001) // Some threshold value suitable for your scenario
        {
            if (!_tree.DIRECTOFFSETDISTANCE.Equals(value)) 
                    {
                        _tree.DIRECTOFFSETDISTANCE = value;
                        IsChanged = true; }

            NotifyPropertyChanged("DIRECTOFFSETDISTANCE");
            
            }
            }
        }
        public int DEGREEOFLEAN
        {
            get => _tree.DEGREEOFLEAN;
            set
            {
                if (!_tree.DEGREEOFLEAN.Equals(value)) 
                { 
                    _tree.DEGREEOFLEAN = value;
                    IsChanged = true; }
                
                NotifyPropertyChanged("DEGREEOFLEAN");
               
            }
        }
        public float HEIGHTTOCORE
        {
            get => _tree.HEIGHTTOCORE;
            set
            {
                if (Math.Abs(HEIGHTTOCORE - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                   if (!_tree.HEIGHTTOCORE.Equals(value))
                    {
                        _tree.HEIGHTTOCORE = value;
                        IsChanged = true; 
                    }
                    NotifyPropertyChanged("HEIGHTTOCORE");
                }
            }
        }
        public string CORESTATUSCODE
        {
            get => _tree.CORESTATUSCODE;
            set
            {
                if (_tree.CORESTATUSCODE != value) { IsChanged = true; }
                _tree.CORESTATUSCODE = value;
                NotifyPropertyChanged("CORESTATUSCODE");
            }
        }
        public float SOUNDWOODLENGTH
        {
            get => _tree.SOUNDWOODLENGTH;
            set
                {
                if (Math.Abs(SOUNDWOODLENGTH - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_tree.SOUNDWOODLENGTH.Equals(value)) 
                    {
                        _tree.SOUNDWOODLENGTH = value;
                        IsChanged = true;
                    }
                    NotifyPropertyChanged("SOUNDWOODLENGTH");
                }
            }
        }
        public int FIELDAGE
        {
            get => _tree.FIELDAGE;
            set
            {
                if (!_tree.FIELDAGE.Equals(value)) 
                {
                    _tree.FIELDAGE = value;
                    IsChanged = true; }
                NotifyPropertyChanged("FIELDAGE");
            }
        }
        public int OFFICERINGCOUNT
        {
            get => _tree.OFFICERINGCOUNT;
            set
            {
                if (!_tree.OFFICERINGCOUNT.Equals(value)) 
                {
                    _tree.OFFICERINGCOUNT = value;
                    IsChanged = true;
                    NotifyPropertyChanged("OFFICERINGCOUNT");
                }
            }
        }
        public int MISSINGRINGS
        {
            get => _tree.MISSINGRINGS;
            set
            {
                if (!_tree.MISSINGRINGS.Equals(value)) { IsChanged = true; }
                _tree.MISSINGRINGS = value;
                NotifyPropertyChanged("MISSINGRINGS");
                
            }
        }

        public string DBH_IN
        {
            get => _tree.DBHIN;
            set
            {
                if (_tree.DBHIN != value) { IsChanged = true; }

                _tree.DBHIN = value;
                NotifyPropertyChanged("DBH_IN");
            }
        }

        public string CROWN_IN
        {
            get => _tree.CROWNIN;
            set
            {
                if (_tree.CROWNIN !=value) { IsChanged = true; }

                _tree.CROWNIN = value;
                NotifyPropertyChanged("CROWN_IN");
            }
        }

        public int LIVE_CROWN_RATIO
        {
            get => _tree.LIVE_CROWN_RATIO;
            set
            {
                if (!_tree.LIVE_CROWN_RATIO.Equals(value)) 
                {
                    _tree.LIVE_CROWN_RATIO = (int)value;
                    IsChanged = true;
                    NotifyPropertyChanged("LIVE_CROWN_RATIO");
                }

            }
        }

        public string CROWN_CLASS
        {
            get => _tree.CROWNCLASSCODE;
            set
            {
                if (_tree.CROWNCLASSCODE != value) { IsChanged = true; }

                _tree.CROWNCLASSCODE = value;
                NotifyPropertyChanged("CROWN_CLASS");
            }
        }

        public int CROWN_DAMAGE
        {
            get => _tree.CROWNDAMAGECODE;
            set
            {
                if (!_tree.CROWNDAMAGECODE.Equals(value)) { IsChanged = true; }

                _tree.CROWNDAMAGECODE = value;
                NotifyPropertyChanged("CROWN_DAMAGE");
            }
        }

        public int DEFOLIATING_INSECT
        {
            get => _tree.DEFOLIATING_INSECT;
            set
            {
                if (!_tree.DEFOLIATING_INSECT.Equals(value)) { IsChanged = true; }
                _tree.DEFOLIATING_INSECT = value;
                NotifyPropertyChanged("DEFOLIATING_INSECT");
            }
        }

        public int FOLIAR_DISEASE
        {
            get => _tree.FOLIAR_DISEASE;
            set
            {
                if (!_tree.FOLIAR_DISEASE.Equals(value)) { IsChanged = true; }

                _tree.FOLIAR_DISEASE = value;
                NotifyPropertyChanged("FOLIAR_DISEASE");
            }
        }

        public string STEM_QUALITY
        {
            get => _tree.STEMQUALITYCODE;
            set
            {
                if (_tree.STEMQUALITYCODE != value) { IsChanged = true; }

                _tree.STEMQUALITYCODE = value;
                NotifyPropertyChanged("STEM_QUALITY");
            }
        }

        public int BARK_RETENTION
        {
            get => _tree.BARKRETENTIONCODE;
            set
            {
                if (!_tree.BARKRETENTIONCODE.Equals(value)) { IsChanged = true; }

                _tree.BARKRETENTIONCODE = value;
                NotifyPropertyChanged("BARK_RETENTION");
            }
        }

        public int WOOD_CONDITION
        {
            get => _tree.WOODCONDITIONCODE;
            set
            {
                if (!_tree.WOODCONDITIONCODE.Equals(value)) { IsChanged = true; }

                _tree.WOODCONDITIONCODE = value;
                NotifyPropertyChanged("WOOD_CONDITION");
            }
        }

        public int DECAY_CLASS
        {
            get => _tree.DECAYCLASS;
            set
            {
                if (!_tree.DECAYCLASS.Equals(value)) { IsChanged = true; }

                _tree.DECAYCLASS = value;
                NotifyPropertyChanged("DECAY_CLASS");
                if (value < 4 ) {IsDecayClass1to3 = true;} else { IsDecayClass1to3 = false; }
            }
        }

        public int MORT_CAUSE
        {
            get => _tree.MORTALITYCAUSECODE;
            set
            {
                if (!_tree.MORTALITYCAUSECODE.Equals(value))
                {
                    _tree.MORTALITYCAUSECODE = value;
                    IsChanged = true;
                    NotifyPropertyChanged("MORT_CAUSE");

                }

            }
        }

        public string BROKEN_TOP
        {
            get => _tree.BROKENTOP;
            set
            {
                if (_tree.BROKENTOP !=value) 
                {
                    _tree.BROKENTOP = value;
                    IsChanged = true;
                    NotifyPropertyChanged("BROKEN_TOP");
                }
            }
        }


        public string COMMENTS
        {
            get => _tree.COMMENTS;
            set
            {
                if (_tree.COMMENTS != value) { _tree.COMMENTS = value; IsChanged = true; }

                NotifyPropertyChanged("COMMENTS");
            }
        }

        public int ERRORCOUNT
        {
            get => _tree.ERRORCOUNT;
            set
            {
                _tree.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
            }
        }
        public string ERRORMSG
        {
            get => _tree.ERRORMSG;
            set
            {
                _tree.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
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
