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
                NotifyPropertyChanged("IsChanged");
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
                _plot.VSNPLOTTYPECODE = value;
                NotifyPropertyChanged("PLOT_TYPE");
                IsChanged = true;
            }
        }

        public string PLOTNUM
        {
            get => _plot.VSNPLOTNAME;
            set
            {
                _plot.VSNPLOTNAME = value;
                NotifyPropertyChanged("PLOTNUM");
                IsChanged = true;
            }
        }
        public string MEASURETYPECODE
        {
            get => _plot.MEASURETYPECODE;
            set
            {
                _plot.MEASURETYPECODE = value;
                NotifyPropertyChanged("MEASURETYPECODE");
                IsChanged = true;
            }
        }
        public int NONSTANDARDTYPECODE
        {
            get => _plot.NONSTANDARDTYPECODE ;
            set
            {
                _plot.NONSTANDARDTYPECODE = value;
                NotifyPropertyChanged("NONSTANDARDTYPECODE");
                IsChanged = true;
            }
        }
        public int GROWTHPLOTNUMBER
        {
            get => _plot.GROWTHPLOTNUMBER;
            set
            {
                _plot.GROWTHPLOTNUMBER  = value;
                NotifyPropertyChanged("GROWTHPLOTNUMBER");
                IsChanged = true;
            }
        }

        //public int ADMINISTRATIVE
        //{
        //    get => _plot.ADMINISTRATIVE;
        //    set
        //    {
        //        _plot.ADMINISTRATIVE = value;
        //        NotifyPropertyChanged("ADMINISTRATIVE");
        //    }
        //}

        //public string FOREST_DISTRICT
        //{
        //    get => _plot.FOREST_DISTRICT;
        //    set
        //    {
        //        _plot.FOREST_DISTRICT = value;
        //        NotifyPropertyChanged("FOREST_DISTRICT");
        //    }
        //}

        //public int FMU
        //{
        //    get => _plot.FMU;
        //    set
        //    {
        //        _plot.FMU = value;
        //        NotifyPropertyChanged("FMU");
        //    }
        //}

        //public int MANAGEMENT_UNIT
        //{
        //    get => _plot.MANAGEMENT_UNIT;
        //    set
        //    {
        //        _plot.MANAGEMENT_UNIT = value;
        //        NotifyPropertyChanged("MANAGEMENT_UNIT");
        //    }
        //}

        //public string IMAGE_ANNOTATION
        //{
        //    get => _plot.IMAGE_ANNOTATION;
        //    set
        //    {
        //        _plot.IMAGE_ANNOTATION = value;
        //        NotifyPropertyChanged("IMAGE_ANNOTATION");
        //    }
        //}

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
                _plot.PLOTOVERVIEWDATE = value;
                NotifyPropertyChanged("PLOT_DATE");
                IsChanged = true;
            }
        }

        public string MEASUREMENT_TYPE
        {
            get => _plot.MEASURETYPECODE;
            set
            {
                _plot.MEASURETYPECODE = value;
                NotifyPropertyChanged("MEASUREMENT_TYPE");
                IsChanged = true;
            }
        }

        public int LEAD_SPP
        {
            get => _plot.LEAD_SPP;
            set
            {
                _plot.LEAD_SPP = value;
                NotifyPropertyChanged("LEAD_SPP");
                IsChanged = true;
            }
        }

        public int ORIGIN
        {
            get => _plot.ORIGIN;
            set
            {
                _plot.ORIGIN = value;
                NotifyPropertyChanged("ORIGIN");
                IsChanged = true;
            }
        }

        public string CANOPY_STRUCTURE
        {
            get => _plot.CANOPYSTRUCTURECODE;
            set
            {
                _plot.CANOPYSTRUCTURECODE = value;
                NotifyPropertyChanged("CANOPY_STRUCTURE");
                IsChanged = true;
            }
        }
 
        public string MATURITY
        {
            get => _plot.MATURITYCLASSCODE;
            set
            {
                _plot.MATURITYCLASSCODE = value;
                NotifyPropertyChanged("MATURITY");
                IsChanged = true;
            }
        }

        public string MATURITYCLASSRATIONALE
        {
            get => _plot.MATURITYCLASSRATIONALE;
            set
            {
                _plot.MATURITYCLASSRATIONALE = value;
                NotifyPropertyChanged("MATURITYCLASSRATIONALE");
                IsChanged = true;
            }
        }

        public int CROWN_CLOSURE
        {
            get => _plot.CROWN_CLOSURE;
            set
            {
                _plot.CROWN_CLOSURE = value;
                NotifyPropertyChanged("CROWN_CLOSURE");
                IsChanged = true;
            }
        }

        public string FIELD_CREW1
        {
            get => _plot.FIELD_CREW1;
            set
            {
                _plot.FIELD_CREW1 = value;
                NotifyPropertyChanged("FIELD_CREW1");
                IsChanged = true;
            }
        }

        public string FIELD_CREW2
        {
            get => _plot.FIELD_CREW2;
            set
            {
                _plot.FIELD_CREW2 = value;
                NotifyPropertyChanged("FIELD_CREW2");
                IsChanged = true;
            }
        }
        public string FIELD_CREW3
        {
            get => _plot.FIELD_CREW3;
            set
            {
                _plot.FIELD_CREW3 = value;
                NotifyPropertyChanged("FIELD_CREW3");
                IsChanged = true;
            }
        }
        public string DECLINATION
        {
            get => _plot.DECLINATION;
            set
            {
                _plot.DECLINATION = value;
                NotifyPropertyChanged("DECLINATION");
                IsChanged = true;
            }
        }

        public int UTM_ZONE
        {
            get => _plot.UTMZONE;
            set
            {
                _plot.UTMZONE = value;
                NotifyPropertyChanged("UTM_ZONE");
                IsChanged = true;
            }
        }

        public double UTM_EASTING
        {
            get => _plot.EASTING;
            set
            {
                _plot.EASTING = value;
                NotifyPropertyChanged("UTM_EASTING");
                IsChanged = true;
            }
        }

        public double UTM_NORTHING
        {
            get => _plot.NORTHING;
            set
            {
                _plot.NORTHING = value;
                NotifyPropertyChanged("UTM_NORTHING");
                IsChanged = true;
            }
        }

        public string DATUM
        {
            get => _plot.DATUM;
            set
            {
                _plot.DATUM = value;
                NotifyPropertyChanged("DATUM");
                IsChanged = true;
            }
        }

        public string COMMENTS
        {
            get => _plot.PLOTOVERVIEWNOTE;
            set
            {
                _plot.PLOTOVERVIEWNOTE = value;
                NotifyPropertyChanged("COMMENTS");
                IsChanged = true;
            }
        }
        public DateTime FORESTHEALTHDATE
        {
            get => _plot.FORESTHEALTHDATE;
            set
            {
                _plot.FORESTHEALTHDATE = value;
                NotifyPropertyChanged("FORESTHEALTHDATE");
                IsChanged = true;
            }
        }
        public string FORESTHEALTHNOTE
        {
            get => _plot.FORESTHEALTHNOTE;
            set
            {
                _plot.FORESTHEALTHNOTE = value;
                NotifyPropertyChanged("FORESTHEALTHNOTE");
                IsChanged = true;
            }
        }
        public string FORESTHEALTHPERSON
        {
            get => _plot.FORESTHEALTHPERSON;
            set
            {
                _plot.FORESTHEALTHPERSON = value;
                NotifyPropertyChanged("FORESTHEALTHPERSON");
                IsChanged = true;
            }
        }
        public int SMALLTREESHRUBAREA
        {
            get => _plot.SMALLTREESHRUBAREA;
            set
            {
                _plot.SMALLTREESHRUBAREA = value;
                NotifyPropertyChanged("SMALLTREESHRUBAREA");
                IsChanged = true;
            }
        }

        public DateTime SMALLTREESHRUBDATE
        {
            get => _plot.SMALLTREESHRUBDATE;
            set
            {
                _plot.SMALLTREESHRUBDATE = value;
                NotifyPropertyChanged("SMALLTREESHRUBDATE");
                IsChanged = true;
            }
        }

        public string SMALLTREESHRUBNOTE
        {
            get => _plot.SMALLTREESHRUBNOTE;
            set
            {
                _plot.SMALLTREESHRUBNOTE = value;
                NotifyPropertyChanged("SMALLTREESHRUBNOTE");
                IsChanged = true;
            }
        }
        public string SMALLTREEPERSON
        {
            get => _plot.SMALLTREEPERSON;
            set
            {
                _plot.SMALLTREEPERSON = value;
                NotifyPropertyChanged("SMALLTREEPERSON");
                IsChanged = true;
            }
        }
        public DateTime UNDERSTORYVEGETATIONDATE
        {
            get => _plot.UNDERSTORYVEGETATIONDATE;
            set
            {
                _plot.UNDERSTORYVEGETATIONDATE = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONDATE");
                IsChanged = true;
            }
        }
        public int UNDERSTORYVEGETATIONAREA
        {
            get => _plot.UNDERSTORYVEGETATIONAREA;
            set
            {
                _plot.UNDERSTORYVEGETATIONAREA = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONAREA");
                IsChanged = true;
            }
        }

        public string UNDERSTORYVEGEATIONNOTE
        {
            get => _plot.UNDERSTORYVEGETATIONNOTE;
            set
            {
                _plot.UNDERSTORYVEGETATIONNOTE = value;
                NotifyPropertyChanged("UNDERSTORYVEGEATIONNOTE");
                IsChanged = true;
            }
        }
        public string UNDERSTORYVEGETATIONPERSON
        {
            get => _plot.UNDERSTORYVEGETATIONPERSON;
            set
            {
                _plot.UNDERSTORYVEGETATIONPERSON = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONPERSON");
                IsChanged = true;
            }
        }
        public DateTime UNDERSTORYCENSUSDATE
        {
            get => _plot.UNDERSTORYCENSUSDATE;
            set
            {
                _plot.UNDERSTORYCENSUSDATE = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSDATE");
                IsChanged = true;
            }
        }
        public string UNDERSTORYCENSUSNOTE
        {
            get => _plot.UNDERSTORYCENSUSNOTE;
            set
            {
                _plot.UNDERSTORYCENSUSNOTE = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSNOTE");
                IsChanged = true;
            }
        }
        public string UNDERSTORYCENSUSPERSON
        {
            get => _plot.UNDERSTORYCENSUSPERSON;
            set
            {
                _plot.UNDERSTORYCENSUSPERSON = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSPERSON");
                IsChanged = true;
            }
        }
        public DateTime DOWNWOODYDEBRISDATE
        {
            get => _plot.DOWNWOODYDEBRISDATE;
            set
            {
                _plot.DOWNWOODYDEBRISDATE = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISDATE");
                IsChanged = true;
            }
        }
        public string DOWNWOODYDEBRISNOTE
        {
            get => _plot.DOWNWOODYDEBRISNOTE;
            set
            {
                _plot.DOWNWOODYDEBRISNOTE = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISNOTE");
                IsChanged = true;
            }
        }
        public string DOWNWOODYDEBRISPERSON
        {
            get => _plot.DOWNWOODYDEBRISPERSON;
            set
            {
                _plot.DOWNWOODYDEBRISPERSON = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISPERSON");
                IsChanged = true;
            }
        }
        public DateTime DEFORMITYDATE
        {
            get => _plot.DEFORMITYDATE;
            set
            {
                _plot.DEFORMITYDATE = value;
                NotifyPropertyChanged("DEFORMITYDATE");
                IsChanged = true;
            }
        }
        public string DEFORMITYNOTE
        {
            get => _plot.DEFORMITYNOTE;
            set
            {
                _plot.DEFORMITYNOTE = value;
                NotifyPropertyChanged("DEFORMITYNOTE");
                IsChanged = true;
            }
        }
        public string DEFORMITYPERSON
        {
            get => _plot.DEFORMITYPERSON;
            set
            {
                _plot.DEFORMITYPERSON = value;
                NotifyPropertyChanged("DEFORMITYPERSON");
                IsChanged = true;
            }
        }
        public DateTime STANDINFODATE
        {
            get => _plot.STANDINFODATE;
            set
            {
                _plot.STANDINFODATE = value;
                NotifyPropertyChanged("STANDINFODATE");
                IsChanged = true;
            }
        }

        public string STANDINFONOTE
        {
            get => _plot.STANDINFONOTE;
            set
            {
                _plot.STANDINFONOTE = value;
                NotifyPropertyChanged("STANDINFONOTE");
                IsChanged = true;
            }
        }
        public string STANDINFOPERSON
        {
            get => _plot.STANDINFOPERSON;
            set
            {
                _plot.STANDINFOPERSON = value;
                NotifyPropertyChanged("STANDINFOPERSON");
                IsChanged = true;
            }
        }
        public int DISTURBANCECODE
        {
            get => _plot.DISTURBANCECODE;
            set
            {
                _plot.DISTURBANCECODE = value;
                NotifyPropertyChanged("DISTURBANCECODE");
                IsChanged = true;
            }
        }
        public int PERCENTAFFECTED
        {
            get => _plot.PERCENTAFFECTED;
            set
            {
                _plot.PERCENTAFFECTED = value;
                NotifyPropertyChanged("PERCENTAFFECTED");
                IsChanged = true;
            }
        }
        public int PERCENTMORTALITY
        {
            get => _plot.PERCENTMORTALITY;
            set
            {
                _plot.PERCENTMORTALITY = value;
                NotifyPropertyChanged("PERCENTMORTALITY");
                IsChanged = true;
            }
        }
        public int ACCESSCONDITIONCODE
        {
            get => _plot.ACCESSCONDITIONCODE;
            set
            {
                _plot.ACCESSCONDITIONCODE = value;
                NotifyPropertyChanged("ACCESSCONDITIONCODE");
                IsChanged = true;
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
