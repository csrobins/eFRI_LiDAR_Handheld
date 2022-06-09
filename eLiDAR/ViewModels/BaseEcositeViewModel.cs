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
            else
            {
                IsChanged = true;
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
                EcositeValidator _fullvalidator = new EcositeValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_ecosite);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);
            }
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
                if (!_ecosite.HUMUSFORMCODE.Equals(value)) { IsChanged = true; }
                _ecosite.HUMUSFORMCODE = value;
                NotifyPropertyChanged("HUMUS_FORM");
              
            }
        }

        public int DRAINAGE
        {
            get => _ecosite.DRAINAGECLASSCODE;
            set
            {
                if (!_ecosite.DRAINAGECLASSCODE.Equals(value)) { IsChanged = true; }
                _ecosite.DRAINAGECLASSCODE = value;
                NotifyPropertyChanged("DRAINAGE");
               
            }
        }

        public string STRATIFIED
        {
            get => _ecosite.STRATIFIED;
            set
            {
                if (_ecosite.STRATIFIED !=value) { _ecosite.STRATIFIED = value; IsChanged = true; }

                NotifyPropertyChanged("STRATIFIED");
               
            }
        }

        public string EFFECTIVE_PORE_PATTERN
        {
            get => _ecosite.EFFECTIVE_PORE_PATTERN;
            set
            {
                if (_ecosite.EFFECTIVE_PORE_PATTERN != value) { IsChanged = true; }
                _ecosite.EFFECTIVE_PORE_PATTERN = value;
                NotifyPropertyChanged("EFFECTIVE_PORE_PATTERN");
                
            }
        }

        public string ELC_SUBSTRATE_TYPE
        {
            get => _ecosite.ELC_SUBSTRATE_TYPE;
            set
            {
                if (_ecosite.ELC_SUBSTRATE_TYPE != value) { IsChanged = true; }
                _ecosite.ELC_SUBSTRATE_TYPE = value;
                NotifyPropertyChanged("ELC_SUBSTRATE_TYPE");
               
            }
        }

        public Nullable<int> DEPTH_TO_DISTINCT_MOTTLES
        {
            get => _ecosite.DEPTHTODISTINCTMOTTLES;
            set
            {
                if (!_ecosite.DEPTHTODISTINCTMOTTLES.Equals(value)) { _ecosite.DEPTHTODISTINCTMOTTLES = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTH_TO_DISTINCT_MOTTLES");
              
            }
        }

        public Nullable<int> DEPTH_TO_PROMINENT_MOTTLES
        {
            get => _ecosite.DEPTHTOPROMINENTMOTTLES;
            set
            {
                if (!_ecosite.DEPTHTOPROMINENTMOTTLES.Equals(value)) { _ecosite.DEPTHTOPROMINENTMOTTLES = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTH_TO_PROMINENT_MOTTLES");
               
            }
        }

        public Nullable<int> DEPTH_TO_GLEY
        {
            get => _ecosite.DEPTHTOGLEY;
            set
            {
                if (!_ecosite.DEPTHTOGLEY.Equals(value)) { _ecosite.DEPTHTOGLEY = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTH_TO_GLEY");
              
            }
        }

        public Nullable<int> DEPTH_TO_BEDROCK
        {
            get => _ecosite.DEPTHTOBEDROCK;
            set
            {
                if (!_ecosite.DEPTHTOBEDROCK.Equals(value)) { _ecosite.DEPTHTOBEDROCK = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTH_TO_BEDROCK");
               
            }
        }

        public System.DateTime SUBSTRATEDATE
        {
            get => _ecosite.SUBSTRATEDATE;
            set
            {
                if (!_ecosite.SUBSTRATEDATE.Equals(value)) { _ecosite.SUBSTRATEDATE = value; IsChanged = true; }

                NotifyPropertyChanged("SUBSTRATEDATE");
                
            }
        }
        public Nullable<int> DEPTH_TO_CARBONATES
        {
            get => _ecosite.DEPTHTOCARBONATES;
            set
            {
                if (!_ecosite.DEPTHTOCARBONATES.Equals(value)) { _ecosite.DEPTHTOCARBONATES = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTH_TO_CARBONATES");
               
            }
        }
        public string MOISTURE_REGIME_DEPTH_CLASS
        {
            get => _ecosite.MOISTURE_REGIME_DEPTH_CLASS;
            set
            {
                if (_ecosite.MOISTURE_REGIME_DEPTH_CLASS != value) { IsChanged = true; }
                _ecosite.MOISTURE_REGIME_DEPTH_CLASS = value;
                NotifyPropertyChanged("MOISTURE_REGIME_DEPTH_CLASS");
               
            }
        }

        public string MOISTURE_REGIME
        {
            get => _ecosite.MOISTUREREGIMECODE;
            set
            {
                if (_ecosite.MOISTUREREGIMECODE != value) { IsChanged = true; }
                _ecosite.MOISTUREREGIMECODE = value;
                NotifyPropertyChanged("MOISTURE_REGIME");
              
            }
        }

        public string MODE_OF_DEPOSITION1
        {
            get => _ecosite.MODEOFDEPOSITIONCODE1;
            set
            {
                if (!_ecosite.MODEOFDEPOSITIONCODE1.Equals(value)) { IsChanged = true; }
                _ecosite.MODEOFDEPOSITIONCODE1 = value;
                NotifyPropertyChanged("MODE_OF_DEPOSITION1");
               
            }
        }

        public string MINERALTEXTURECODE
        {
            get => _ecosite.MINERALTEXTURECODE;
            set
            {
                if (_ecosite.MINERALTEXTURECODE != value) { IsChanged = true; }
                _ecosite.MINERALTEXTURECODE = value;
                NotifyPropertyChanged("MINERALTEXTURECODE");
              
            }
        }
        public int MODEOFDEPOSITIONRANK1
        {
            get => _ecosite.MODEOFDEPOSITIONRANK1;
            set
            {
                if (!_ecosite.MODEOFDEPOSITIONRANK1.Equals(value)) { IsChanged = true; }
                _ecosite.MODEOFDEPOSITIONRANK1 = value;
                NotifyPropertyChanged("MODEOFDEPOSITIONRANK1");
               
            }
        }
        public int MODEOFDEPOSITIONRANK2
        {
            get => _ecosite.MODEOFDEPOSITIONRANK2;
            set
            {
                if (!_ecosite.MODEOFDEPOSITIONRANK2.Equals(value)) { IsChanged = true; }
                _ecosite.MODEOFDEPOSITIONRANK2 = value;
                NotifyPropertyChanged("MODEOFDEPOSITIONRANK2");
                
            }
        }
        public string MODE_OF_DEPOSITION2
        {
            get => _ecosite.MODEOFDEPOSITIONCODE2;
            set
            {
                if (_ecosite.MODEOFDEPOSITIONCODE2 != value) { IsChanged = true; }
                _ecosite.MODEOFDEPOSITIONCODE2 = value;
                NotifyPropertyChanged("MODE_OF_DEPOSITION2");
                
            }
        }

        public int FUNCTIONAL_ROOTING_DEPTH
        {
            get => _ecosite.FUNCTIONALROOTINGDEPTH;
            set
            {
                if (!_ecosite.FUNCTIONALROOTINGDEPTH.Equals(value)) { _ecosite.FUNCTIONALROOTINGDEPTH = value; IsChanged = true; }

                NotifyPropertyChanged("FUNCTIONAL_ROOTING_DEPTH");
                
            }
        }

        public int MAXIMUM_ROOTING_DEPTH
        {
            get => _ecosite.MAXIMUMROOTINGDEPTH;
            set
            {
                if (!_ecosite.MAXIMUMROOTINGDEPTH.Equals(value)) { _ecosite.MAXIMUMROOTINGDEPTH = value; IsChanged = true; }

                NotifyPropertyChanged("MAXIMUM_ROOTING_DEPTH");
                
            }
        }

        public Nullable<int> DEPTH_TO_ROOT_RESTRICTION
        {
            get => _ecosite.DEPTHTOROOTRESTRICTION;
            set
            {
                if (!_ecosite.DEPTHTOROOTRESTRICTION.Equals(value)) { _ecosite.DEPTHTOROOTRESTRICTION = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTH_TO_ROOT_RESTRICTION");
               
            }
        }

        public Nullable<int> DEPTHTOWATERTABLE
        {
            get => _ecosite.DEPTHTOWATERTABLE;
            set
            {
                if (!_ecosite.DEPTHTOWATERTABLE.Equals(value)) { _ecosite.DEPTHTOWATERTABLE = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTHTOWATERTABLE");
                
            }
        }

        public Nullable<int> DEPTH_TO_COARSE_FRAGS
        {
            get => _ecosite.DEPTHTOIMPASSABLECOARSEFRAGMENTS;
            set
            {
                if (!_ecosite.DEPTHTOIMPASSABLECOARSEFRAGMENTS.Equals(value)) { _ecosite.DEPTHTOIMPASSABLECOARSEFRAGMENTS = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTH_TO_COARSE_FRAGS");
                
            }
        }

        public string PROBLEMATIC_SITE_PROTOCOL_CLASS
        {
            get => _ecosite.PROBLEMATICSITE;
            set
            {
                if (_ecosite.PROBLEMATICSITE !=value) { IsChanged = true; }
                _ecosite.PROBLEMATICSITE = value;
                NotifyPropertyChanged("PROBLEMATIC_SITE_PROTOCOL_CLASS");
                
            }
        }

        public Nullable<int> DEPTHTOSEEPAGE
        {
            get => _ecosite.DEPTHTOSEEPAGE;
            set
            {
                if (!_ecosite.DEPTHTOSEEPAGE.Equals(value)) { _ecosite.DEPTHTOSEEPAGE = value; IsChanged = true; }

                NotifyPropertyChanged("DEPTHTOSEEPAGE");
                
            }
        }
        public string DECOMPOSITIONCODE
        {
            get => _ecosite.DECOMPOSITIONCODE;
            set
            {
                if (_ecosite.DECOMPOSITIONCODE != value) { _ecosite.DECOMPOSITIONCODE = value; IsChanged = true; }

                NotifyPropertyChanged("DECOMPOSITIONCODE");

            }
        }

        public string SOIL_PIT_PHOTO
        {
            get => _ecosite.SOIL_PIT_PHOTO;
            set
            {
                if (_ecosite.SOIL_PIT_PHOTO !=value) { IsChanged = true; }
                _ecosite.SOIL_PIT_PHOTO = value;
                NotifyPropertyChanged("SOIL_PIT_PHOTO");
                
            }
        }

        public string PRI_ECO
        {
            get => _ecosite.PRI_ECO;
            set
            {
                if (_ecosite.PRI_ECO !=value) { IsChanged = true; }
                _ecosite.PRI_ECO = value;
                NotifyPropertyChanged("PRI_ECO");
                
            }
        }

        public int PRI_ECO_PCT
        {
            get => _ecosite.PRI_ECO_PCT;
            set
            {
                if (!_ecosite.PRI_ECO_PCT.Equals(value)) { _ecosite.PRI_ECO_PCT = value; IsChanged = true; }

                NotifyPropertyChanged("PRI_ECO_PCT");
                
            }
        }

        public string SEC_ECO
        {
            get => _ecosite.SEC_ECO;
            set
            {
                if (_ecosite.SEC_ECO != value) { IsChanged = true; }
                _ecosite.SEC_ECO = value;
                NotifyPropertyChanged("SEC_ECO");
                
            }
        }
        public int SEC_ECO_PCT
        {
            get => _ecosite.SEC_ECO_PCT;
            set
            {
                if (!_ecosite.SEC_ECO_PCT.Equals(value)) { _ecosite.SEC_ECO_PCT = value; IsChanged = true; }

                NotifyPropertyChanged("SEC_ECO_PCT");
                
            }
        }

        public int AZIMUTH
        {
            get => _ecosite.PITAZIMUTH;
            set
            {
                if (!_ecosite.PITAZIMUTH.Equals(value)) { _ecosite.PITAZIMUTH = value; IsChanged = true; }

                NotifyPropertyChanged("AZIMUTH");
                
            }
        }

        public Single DISTANCE
        {
            get => _ecosite.PITDISTANCE;
            set
            {
                if (!_ecosite.PITDISTANCE.Equals(value)) { _ecosite.PITDISTANCE = value; IsChanged = true; }

                NotifyPropertyChanged("DISTANCE");
                
            }
        }

        public string COMMENTS
        {
            get => _ecosite.SUBSTRATENOTE;
            set
            {
                if (_ecosite.SUBSTRATENOTE !=value) { IsChanged = true; }
                _ecosite.SUBSTRATENOTE = value;
                NotifyPropertyChanged("COMMENTS");
               
            }
        }

        public int ERRORCOUNT
        {
            get => _ecosite.ERRORCOUNT;
            set
            {
                _ecosite.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
               
            }
        }
        public string ERRORMSG
        {
            get => _ecosite.ERRORMSG;
            set
            {
                _ecosite.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
               
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
