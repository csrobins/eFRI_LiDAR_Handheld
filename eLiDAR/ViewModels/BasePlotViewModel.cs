using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using eLiDAR.Validator;
using FluentValidation.Results;

namespace eLiDAR.ViewModels
{
    public class BasePlotViewModel : INotifyPropertyChanged {

        public PLOT _plot;
        public INavigation _navigation;
        public IValidator _plotValidator;
        public IPlotRepository _plotRepository;
        public string _selectedprojectid;
        private bool _ischanged = false;

        public event PropertyChangedEventHandler PropertyChanged2;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName]string propertyName = null)
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
        public bool IsChanged
        {
            get => _ischanged;
            set
            {
                _ischanged = value;
                PlotValidator _fullvalidator = new PlotValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_plot);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);

                PlotValidator _validator = new PlotValidator();
                ValidationResult validationResults = _validator.Validate(_plot);

                if (validationResults.IsValid)
                {
                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                } 

                NotifyPropertyChanged("IsChanged");
            }
        }

        private bool _IsValid = false;
        public bool IsValid
        {
            get
            {
                return _IsValid;
            }
            set
            {
                _IsValid = value;
                NotifyPropertyChanged("IsValid");
            }
        }

        public string PLOTID
        {
            get => _plot.PLOTID;
            set
            {
                _plot.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }

        public string PROJECTID
        {
            get => _plot.PROJECTID;
            set
            {
                _plot.PROJECTID = value;
                NotifyPropertyChanged("PROJECTID");
            }
        }

        public string PLOT_TYPE
        {
            get => _plot.VSNPLOTTYPECODE;
            set
            {
                if (_plot.VSNPLOTTYPECODE != value) 
                {
                    IsChanged = true; 
                }
                _plot.VSNPLOTTYPECODE = value;
                NotifyPropertyChanged("PLOT_TYPE");
            
            }
        }

        public string PLOTNUM
        {
            get => _plot.VSNPLOTNAME;
            set
            {
                if (!_plot.VSNPLOTNAME.Equals(value)) { IsChanged = true; }
                _plot.VSNPLOTNAME = value;
                NotifyPropertyChanged("PLOTNUM");
               
            }
        }
        public string MEASURETYPECODE
        {
            get => _plot.MEASURETYPECODE;
            set
            {
                if (!_plot.MEASURETYPECODE.Equals(value)) { IsChanged = true; }
                _plot.MEASURETYPECODE = value;
                NotifyPropertyChanged("MEASURETYPECODE");
               
            }
        }
        public int NONSTANDARDTYPECODE
        {
            get => _plot.NONSTANDARDTYPECODE ;
            set
            {
                if (!_plot.NONSTANDARDTYPECODE.Equals(value)) { IsChanged = true; }
                _plot.NONSTANDARDTYPECODE = value;
                NotifyPropertyChanged("NONSTANDARDTYPECODE");
      
            }
        }
        public int GROWTHPLOTNUMBER
        {
            get => _plot.GROWTHPLOTNUMBER;
            set
            {
                if (!_plot.GROWTHPLOTNUMBER.Equals(value)) { IsChanged = true; }
                _plot.GROWTHPLOTNUMBER  = value;
                NotifyPropertyChanged("GROWTHPLOTNUMBER");
           
            }
        }
        public string EXISTINGPLOTNAME
        {
            get => _plot.EXISTINGPLOTNAME;
            set
            {
                if (_plot.EXISTINGPLOTNAME != value) { IsChanged = true; }
                _plot.EXISTINGPLOTNAME = value;
                NotifyPropertyChanged("EXISTINGPLOTNAME");
             
            }
        }
        public string EXISTINGPLOTTYPECODE
        {
            get => _plot.EXISTINGPLOTTYPECODE;
            set
            {
                if (!_plot.EXISTINGPLOTTYPECODE.Equals(value)) { IsChanged = true; }
                _plot.EXISTINGPLOTTYPECODE = value;
                NotifyPropertyChanged("EXISTINGPLOTTYPECODE");
       
            }
        }
        public double DISTANCETARGETMOVED
        {
            get => _plot.DISTANCETARGETMOVED;
            set
            {
                if (!_plot.DISTANCETARGETMOVED.Equals(value)) { IsChanged = true; }
                _plot.DISTANCETARGETMOVED = value;
                NotifyPropertyChanged("DISTANCETARGETMOVED");
             
            }
        }
        public int AZIMUTHTARGETMOVED
        {
            get => _plot.AZIMUTHTARGETMOVED;
            set
            {
                if (!_plot.AZIMUTHTARGETMOVED.Equals(value)) { IsChanged = true; }
                _plot.AZIMUTHTARGETMOVED = value;
                NotifyPropertyChanged("AZIMUTHTARGETMOVED");
              
            }
        }

        public string PLOTKEY
        {
            get => _plot.PLOTKEY;
            set
            {
                _plot.PLOTKEY = value;
                NotifyPropertyChanged("PLOTKEY");
            }
        }

        public DateTime PLOT_DATE
        {
            get {
                DateTime date1 = new DateTime(_plot.PLOTOVERVIEWDATE.Ticks );
                DateTime date2 = new DateTime(2020, 1, 1, 12, 0, 0);
                int result = DateTime.Compare(date1, date2);
                if (result < 0)
                {
                    return DateTime.Today;
                }
                else {return  _plot.PLOTOVERVIEWDATE; }
            }
            set
            {
                if (!_plot.PLOTOVERVIEWDATE.Equals(value)) { IsChanged = true; }
                _plot.PLOTOVERVIEWDATE = value;
                _plot.MEASUREYEAR = _plot.PLOTOVERVIEWDATE.Year; 
                NotifyPropertyChanged("PLOT_DATE");
               
            }
        }

        public string MEASUREMENT_TYPE
        {
            get => _plot.MEASURETYPECODE;
            set
            {
                if (!_plot.MEASURETYPECODE.Equals(value)) { IsChanged = true; }
                _plot.MEASURETYPECODE = value;
                NotifyPropertyChanged("MEASUREMENT_TYPE");
  
            }
        }

        public int LEAD_SPP
        {
            get => _plot.LEAD_SPP;
            set
            {
                if (!_plot.LEAD_SPP.Equals(value)) { IsChanged = true; }
                _plot.LEAD_SPP = value;
                NotifyPropertyChanged("LEAD_SPP");
          
            }
        }

        public int MAINCANOPYORIGINCODE1
        {
            get => _plot.MAINCANOPYORIGINCODE1;
            set
            {
                if (!_plot.MAINCANOPYORIGINCODE1.Equals(value)) { IsChanged = true; }
                _plot.MAINCANOPYORIGINCODE1 = value;
                NotifyPropertyChanged("MAINCANOPYORIGINCODE1");

            }
        }
        public int MAINCANOPYORIGINCODE2
        {
            get => _plot.MAINCANOPYORIGINCODE2;
            set
            {
                if (!_plot.MAINCANOPYORIGINCODE2.Equals(value)) { IsChanged = true; }
                _plot.MAINCANOPYORIGINCODE2 = value;
                NotifyPropertyChanged("MAINCANOPYORIGINCODE2");

            }
        }

        public string CANOPYSTRUCTURECODE1
        {
            get => _plot.CANOPYSTRUCTURECODE1;
            set
            {
                if (!_plot.CANOPYSTRUCTURECODE1.Equals(value)) { IsChanged = true; }
                _plot.CANOPYSTRUCTURECODE1 = value;
                NotifyPropertyChanged("CANOPYSTRUCTURECODE1");
              
            }
        }

        public string CANOPYSTRUCTURECODE2
        {
            get => _plot.CANOPYSTRUCTURECODE2;
            set
            {
                if (!_plot.CANOPYSTRUCTURECODE2.Equals(value)) { IsChanged = true; }
                _plot.CANOPYSTRUCTURECODE2 = value;
                NotifyPropertyChanged("CANOPYSTRUCTURECODE2");

            }
        }

        public string MATURITYCLASSCODE1
        {
            get => _plot.MATURITYCLASSCODE1;
            set
            {
                if (!_plot.MATURITYCLASSCODE1.Equals(value)) { IsChanged = true; }
                _plot.MATURITYCLASSCODE1 = value;
                NotifyPropertyChanged("MATURITYCLASSCODE1");
       
            }
        }

        public string MATURITYCLASSRATIONALE1
        {
            get => _plot.MATURITYCLASSRATIONALE1;
            set
            {
                if (!_plot.MATURITYCLASSRATIONALE1.Equals(value)) { IsChanged = true; }
                _plot.MATURITYCLASSRATIONALE1 = value;
                NotifyPropertyChanged("MATURITYCLASSRATIONALE1");
       
            }
        }
        public string MATURITYCLASSCODE2
        {
            get => _plot.MATURITYCLASSCODE2;
            set
            {
                if (!_plot.MATURITYCLASSCODE2.Equals(value)) { IsChanged = true; }
                _plot.MATURITYCLASSCODE2 = value;
                NotifyPropertyChanged("MATURITYCLASSCODE2");

            }
        }

        public string MATURITYCLASSRATIONALE2
        {
            get => _plot.MATURITYCLASSRATIONALE2;
            set
            {
                if (!_plot.MATURITYCLASSRATIONALE2.Equals(value)) { IsChanged = true; }
                _plot.MATURITYCLASSRATIONALE2 = value;
                NotifyPropertyChanged("MATURITYCLASSRATIONALE2");
  
            }
        }
        public int CROWN_CLOSURE
        {
            get => _plot.CROWN_CLOSURE;
            set
            {
                if (!_plot.CROWN_CLOSURE.Equals(value)) { IsChanged = true; }
                _plot.CROWN_CLOSURE = value;
                NotifyPropertyChanged("CROWN_CLOSURE");
         
            }
        }

        public string FIELD_CREW1
        {
            get => _plot.FIELD_CREW1;
            set
            {
                if (!_plot.FIELD_CREW1.Equals(value)) { IsChanged = true; }
                _plot.FIELD_CREW1 = value;
                NotifyPropertyChanged("FIELD_CREW1");
            
            }
        }

        public string FIELD_CREW2
        {
            get => _plot.FIELD_CREW2;
            set
            {
                if (!_plot.FIELD_CREW2.Equals(value)) { IsChanged = true; }
                _plot.FIELD_CREW2 = value;
                NotifyPropertyChanged("FIELD_CREW2");
                
            }
        }
        public string FIELD_CREW3
        {
            get => _plot.FIELD_CREW3;
            set
            {
                if (!_plot.FIELD_CREW3.Equals(value)) { IsChanged = true; }
                _plot.FIELD_CREW3 = value;
                NotifyPropertyChanged("FIELD_CREW3");
             
            }
        }
        public string FIELD_CREW4
        {
            get => _plot.FIELD_CREW4;
            set
            {
                if (!_plot.FIELD_CREW4.Equals(value)) { IsChanged = true; }
                _plot.FIELD_CREW4 = value;
                NotifyPropertyChanged("FIELD_CREW4");
            
            }
        }
        public string FIELD_CREW5
        {
            get => _plot.FIELD_CREW5;
            set
            {
                if (!_plot.FIELD_CREW5.Equals(value)) { IsChanged = true; }
                _plot.FIELD_CREW5 = value;
                NotifyPropertyChanged("FIELD_CREW5");
          
            }
        }
        public string FIELD_CREW6
        {
            get => _plot.FIELD_CREW6;
            set
            {
                if (!_plot.FIELD_CREW6.Equals(value)) { IsChanged = true; }
                _plot.FIELD_CREW6 = value;
                NotifyPropertyChanged("FIELD_CREW6");
         
            }
        }
        public int DECLINATION
        {
            get => _plot.DECLINATION;
            set
            {
                if (!_plot.DECLINATION.Equals(value)) { IsChanged = true; }
                _plot.DECLINATION = value;
                NotifyPropertyChanged("DECLINATION");
          
            }
        }

        public int UTM_ZONE
        {
            get => _plot.UTMZONE;
            set
            {
                if (!_plot.UTMZONE.Equals(value)) { IsChanged = true; }
                _plot.UTMZONE = value;
                NotifyPropertyChanged("UTM_ZONE");
    
            }
        }

        public double UTM_EASTING
        {
            get => _plot.EASTING;
            set
            {
                if (!_plot.EASTING.Equals(value)) { IsChanged = true; }
                _plot.EASTING = value;
                NotifyPropertyChanged("UTM_EASTING");

            }
        }

        public double UTM_NORTHING
        {
            get => _plot.NORTHING;
            set
            {
                if (!_plot.NORTHING.Equals(value)) { IsChanged = true; }
                _plot.NORTHING = value;
                NotifyPropertyChanged("UTM_NORTHING");
    
            }
        }

        public string DATUM
        {
            get => _plot.DATUM;
            set
            {
                if (_plot.DATUM != value) 
                { 
                    IsChanged = true;
                }
                _plot.DATUM = value;
                NotifyPropertyChanged("DATUM");
    
            }
        }

        public string COMMENTS
        {
            get => _plot.PLOTOVERVIEWNOTE;
            set
            {
                if (_plot.PLOTOVERVIEWNOTE != value) 
                {
                    IsChanged = true;
                }
                _plot.PLOTOVERVIEWNOTE = value;
                NotifyPropertyChanged("COMMENTS");
       
            }
        }
        public DateTime FORESTHEALTHDATE
        {
            get => _plot.FORESTHEALTHDATE;
            set
            {
                if (!_plot.FORESTHEALTHDATE.Equals(value)) { IsChanged = true; }
                _plot.FORESTHEALTHDATE = value;
                NotifyPropertyChanged("FORESTHEALTHDATE");
           
            }
        }
        public string FORESTHEALTHNOTE
        {
            get => _plot.FORESTHEALTHNOTE;
            set
            {
                if (_plot.FORESTHEALTHNOTE != value) 
                {
                    IsChanged = true;
                }
                _plot.FORESTHEALTHNOTE = value;
                NotifyPropertyChanged("FORESTHEALTHNOTE");
 
            }
        }
        public string FORESTHEALTHPERSON
        {
            get => _plot.FORESTHEALTHPERSON;
            set
            {
                if (_plot.FORESTHEALTHPERSON != value) 
                {
                    IsChanged = true;
                }
                _plot.FORESTHEALTHPERSON = value;
                NotifyPropertyChanged("FORESTHEALTHPERSON");

            }
        }
        public int SMALLTREESHRUBAREA
        {
            get => _plot.SMALLTREESHRUBAREA;
            set
            {
                if (!_plot.SMALLTREESHRUBAREA.Equals(value)) { IsChanged = true; }
                _plot.SMALLTREESHRUBAREA = value;
                NotifyPropertyChanged("SMALLTREESHRUBAREA");
            
            }
        }

        public DateTime SMALLTREESHRUBDATE
        {
            get => _plot.SMALLTREESHRUBDATE;
            set
            {
                if (!_plot.SMALLTREESHRUBDATE.Equals(value)) { IsChanged = true; }
                _plot.SMALLTREESHRUBDATE = value;
                NotifyPropertyChanged("SMALLTREESHRUBDATE");
              
            }
        }

        public string SMALLTREESHRUBNOTE
        {
            get => _plot.SMALLTREESHRUBNOTE;
            set
            {
                if (_plot.SMALLTREESHRUBNOTE != value) 
                { 
                    IsChanged = true;
                }
                _plot.SMALLTREESHRUBNOTE = value;
                NotifyPropertyChanged("SMALLTREESHRUBNOTE");

            }
        }
        public string SMALLTREEPERSON
        {
            get => _plot.SMALLTREEPERSON;
            set
            {
                if (_plot.SMALLTREEPERSON != value) { IsChanged = true; }
                _plot.SMALLTREEPERSON = value;
                NotifyPropertyChanged("SMALLTREEPERSON");

            }
        }
        public DateTime UNDERSTORYVEGETATIONDATE
        {
            get => _plot.UNDERSTORYVEGETATIONDATE;
            set
            {
                if (!_plot.UNDERSTORYVEGETATIONDATE.Equals(value)) { IsChanged = true; }
                _plot.UNDERSTORYVEGETATIONDATE = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONDATE");
            
            }
        }
        public int UNDERSTORYVEGETATIONAREA
        {
            get => _plot.UNDERSTORYVEGETATIONAREA;
            set
            {
                if (!_plot.UNDERSTORYVEGETATIONAREA.Equals(value)) { IsChanged = true; }
                _plot.UNDERSTORYVEGETATIONAREA = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONAREA");
       
            }
        }

        public string UNDERSTORYVEGEATIONNOTE
        {
            get => _plot.UNDERSTORYVEGETATIONNOTE;
            set
            {
                if (_plot.UNDERSTORYVEGETATIONNOTE != value) { IsChanged = true; }
                _plot.UNDERSTORYVEGETATIONNOTE = value;
                NotifyPropertyChanged("UNDERSTORYVEGEATIONNOTE");
       
            }
        }
        public string UNDERSTORYVEGETATIONPERSON
        {
            get => _plot.UNDERSTORYVEGETATIONPERSON;
            set
            {
                if (_plot.STEMMAPPINGNOTE != value) { IsChanged = true; }
                _plot.UNDERSTORYVEGETATIONPERSON = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONPERSON");
 
            }
        }
        public DateTime UNDERSTORYCENSUSDATE
        {
            get => _plot.UNDERSTORYCENSUSDATE;
            set
            {
                if (!_plot.UNDERSTORYCENSUSDATE.Equals(value)) { IsChanged = true; }
                _plot.UNDERSTORYCENSUSDATE = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSDATE");
       
            }
        }
        public string UNDERSTORYCENSUSNOTE
        {
            get => _plot.UNDERSTORYCENSUSNOTE;
            set
            {
                if (_plot.UNDERSTORYCENSUSNOTE != value) { IsChanged = true; }
                _plot.UNDERSTORYCENSUSNOTE = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSNOTE");
           
            }
        }
        public string UNDERSTORYCENSUSPERSON
        {
            get => _plot.UNDERSTORYCENSUSPERSON;
            set
            {
                if (_plot.UNDERSTORYCENSUSPERSON != value) { IsChanged = true; }
                _plot.UNDERSTORYCENSUSPERSON = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSPERSON");
       
            }
        }
        public DateTime DOWNWOODYDEBRISDATE
        {
            get => _plot.DOWNWOODYDEBRISDATE;
            set
            {
                if (!_plot.DOWNWOODYDEBRISDATE.Equals(value)) { IsChanged = true; }
                _plot.DOWNWOODYDEBRISDATE = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISDATE");
        
            }
        }
        public double LINELENGTH1
        {
            get => _plot.LINELENGTH1;
            set
            {
                if (!_plot.LINELENGTH1.Equals(value)) { IsChanged = true; }
                _plot.LINELENGTH1 = value;
                NotifyPropertyChanged("LINELENGTH1");
           
            }
        }
        public double LINELENGTH2
        {
            get => _plot.LINELENGTH2;
            set
            {
                if (!_plot.LINELENGTH2.Equals(value)) { IsChanged = true; }
                _plot.LINELENGTH2 = value;
                NotifyPropertyChanged("LINELENGTH2");

            }
        }

        public string DOWNWOODYDEBRISNOTE
        {
            get => _plot.DOWNWOODYDEBRISNOTE;
            set
            {
                if (_plot.DOWNWOODYDEBRISNOTE != value) { IsChanged = true; }
                _plot.DOWNWOODYDEBRISNOTE = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISNOTE");
  
            }
        }
        public string DOWNWOODYDEBRISPERSON
        {
            get => _plot.DOWNWOODYDEBRISPERSON;
            set
            {
                if (_plot.DOWNWOODYDEBRISPERSON != value) { IsChanged = true; }
                _plot.DOWNWOODYDEBRISPERSON = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISPERSON");
       
            }
        }
        public DateTime DEFORMITYDATE
        {
            get => _plot.DEFORMITYDATE;
            set
            {
                if (!_plot.DEFORMITYDATE.Equals(value)) { IsChanged = true; }
                _plot.DEFORMITYDATE = value;
                NotifyPropertyChanged("DEFORMITYDATE");
         
            }
        }
        public string DEFORMITYNOTE
        {
            get => _plot.DEFORMITYNOTE;
            set
            {
                if (_plot.DEFORMITYNOTE != value) { IsChanged = true; }
                _plot.DEFORMITYNOTE = value;
                NotifyPropertyChanged("DEFORMITYNOTE");
             
            }
        }
        public string DEFORMITYPERSON
        {
            get => _plot.DEFORMITYPERSON;
            set
            {
                if (_plot.DEFORMITYPERSON != value) { IsChanged = true; }
                _plot.DEFORMITYPERSON = value;
                NotifyPropertyChanged("DEFORMITYPERSON");
             
            }
        }
        public DateTime STANDINFODATE
        {
            get => _plot.STANDINFODATE;
            set
            {
                if (!_plot.STANDINFODATE.Equals(value)) { IsChanged = true; }
                _plot.STANDINFODATE = value;
                NotifyPropertyChanged("STANDINFODATE");
       
            }
        }

        public string STANDINFONOTE
        {
            get => _plot.STANDINFONOTE;
            set
            {
                if (_plot.STANDINFONOTE != value) { IsChanged = true; }
                _plot.STANDINFONOTE = value;
                NotifyPropertyChanged("STANDINFONOTE");
          
            }
        }
        public string STANDINFOPERSON
        {
            get => _plot.STANDINFOPERSON;
            set
            {
                if (_plot.STANDINFOPERSON != value) { IsChanged = true; }
                _plot.STANDINFOPERSON = value;
                NotifyPropertyChanged("STANDINFOPERSON");
                
            }
        }
        public int DISTURBANCECODE1
        {
            get => _plot.DISTURBANCECODE1;
            set
            {
                if (!_plot.DISTURBANCECODE1.Equals(value)) { IsChanged = true; }
                _plot.DISTURBANCECODE1 = value;
                NotifyPropertyChanged("DISTURBANCECODE1");
        
            }
        }
        public int DISTURBANCECODE2
        {
            get => _plot.DISTURBANCECODE2;
            set
            {
                if (!_plot.DISTURBANCECODE2.Equals(value)) { IsChanged = true; }
                _plot.DISTURBANCECODE2 = value;
                NotifyPropertyChanged("DISTURBANCECODE2");

            }
        }
        public int PERCENTAFFECTED
        {
            get => _plot.PERCENTAFFECTED;
            set
            {
                if (!_plot.PERCENTAFFECTED.Equals(value)) { IsChanged = true; }
                _plot.PERCENTAFFECTED = value;
                NotifyPropertyChanged("PERCENTAFFECTED");
        
            }
        }
        public int PERCENTMORTALITY
        {
            get => _plot.PERCENTMORTALITY;
            set
            {
                if (!_plot.PERCENTMORTALITY.Equals(value)) { IsChanged = true; }
                _plot.PERCENTMORTALITY = value;
                NotifyPropertyChanged("PERCENTMORTALITY");
    
            }
        }
        public int ACCESSCONDITIONCODE
        {
            get => _plot.ACCESSCONDITIONCODE;
            set
            {
                if (!_plot.ACCESSCONDITIONCODE.Equals(value)) { IsChanged = true; }
                _plot.ACCESSCONDITIONCODE = value;
                NotifyPropertyChanged("ACCESSCONDITIONCODE");
    
            }
        }
        public DateTime AGEDATE
        {
            get => _plot.AGEDATE;
            set
            {
                if (!_plot.AGEDATE.Equals(value)) { IsChanged = true; }
                _plot.AGEDATE = value;
                NotifyPropertyChanged("AGEDATE");
   
            }
        }
        public string AGENOTE
        {
            get => _plot.AGENOTE;
            set
            {
                if (_plot.AGENOTE != value) 
                { 
                    IsChanged = true;
                }
                _plot.AGENOTE = value;
                NotifyPropertyChanged("AGENOTE");

            }
        }

        public string AGEPERSON
        {
            get => _plot.AGEPERSON;
            set
            {
                if (_plot.AGEPERSON != value)
                {
                    IsChanged = true;
                }
                _plot.AGEPERSON = value;
                NotifyPropertyChanged("AGEPERSON");
            }
        }

        public DateTime STEMMAPPINGDATE
        {
            get => _plot.STEMMAPPINGDATE;
            set
            {
                if (!_plot.STEMMAPPINGDATE.Equals(value)) { IsChanged = true; }
                _plot.STEMMAPPINGDATE = value;
                NotifyPropertyChanged("STEMMAPPINGDATE");

            }
        }
        public string STEMMAPPINGNOTE
        {
            get => _plot.STEMMAPPINGNOTE;
            set
            {
                if (_plot.STEMMAPPINGNOTE != value) 
                { 
                    IsChanged = true;
                }
                _plot.STEMMAPPINGNOTE = value;
                NotifyPropertyChanged("STEMMAPPINGNOTE");
            }
        }

        public string STEMMAPPINGPERSON
        {
            get => _plot.STEMMAPPINGPERSON;
            set
            {
                if (_plot.STEMMAPPINGPERSON != value)
                {
                    IsChanged = true;
                }
                _plot.STEMMAPPINGPERSON = value;
                NotifyPropertyChanged("STEMMAPPINGPERSON");
            }
        }
        public int ERRORCOUNT
        {
            get => _plot.ERRORCOUNT;
            set
            {
                _plot.ERRORCOUNT  = value;
                NotifyPropertyChanged("ERRORCOUNT");

            }
        }
        public string ERRORMSG
        {
            get => _plot.ERRORMSG;
            set
            {
                _plot.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");

            }
        }

        List<PLOT> _plotList;
        public List<PLOT> PlotList
        {
            get => _plotList;
            set
            {
                _plotList = value;
                NotifyPropertyChanged("PlotList");
            }
        }
        List<PERSON> _personList;
        public List<PERSON> PersonList
        {
            get => _personList;
            set
            {
                _personList = value;
                NotifyPropertyChanged("PersonList");
            }
        }
        List<PLOTLIST> _plotListFull;
        public List<PLOTLIST> PlotListFull
        {
            get => _plotListFull;
            set
            {
                _plotListFull = value;
                NotifyPropertyChanged("PlotListFull");
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
