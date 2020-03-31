using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
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
            get => _plot.PLOT_TYPE;
            set
            {
                _plot.PLOT_TYPE = value;
                NotifyPropertyChanged("PLOT_TYPE");
            }
        }

        public string PLOTNUM
        {
            get => _plot.PLOTNUM;
            set
            {
                _plot.PLOTNUM = value;
                NotifyPropertyChanged("PLOTNUM");
            }
        }

        public int ADMINISTRATIVE
        {
            get => _plot.ADMINISTRATIVE;
            set
            {
                _plot.ADMINISTRATIVE = value;
                NotifyPropertyChanged("ADMINISTRATIVE");
            }
        }

        public string FOREST_DISTRICT
        {
            get => _plot.FOREST_DISTRICT;
            set
            {
                _plot.FOREST_DISTRICT = value;
                NotifyPropertyChanged("FOREST_DISTRICT");
            }
        }

        public int FMU
        {
            get => _plot.FMU;
            set
            {
                _plot.FMU = value;
                NotifyPropertyChanged("FMU");
            }
        }

        public int MANAGEMENT_UNIT
        {
            get => _plot.MANAGEMENT_UNIT;
            set
            {
                _plot.MANAGEMENT_UNIT = value;
                NotifyPropertyChanged("MANAGEMENT_UNIT");
            }
        }

        public string IMAGE_ANNOTATION
        {
            get => _plot.IMAGE_ANNOTATION;
            set
            {
                _plot.IMAGE_ANNOTATION = value;
                NotifyPropertyChanged("IMAGE_ANNOTATION");
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
                DateTime date1 = new DateTime(_plot.PLOT_DATE.Ticks );
                DateTime date2 = new DateTime(2020, 1, 1, 12, 0, 0);
                int result = DateTime.Compare(date1, date2);
                if (result < 0)
                {
                    return DateTime.Today;
                }
                else {return  _plot.PLOT_DATE; }
            }
            set
            {
                _plot.PLOT_DATE = value;
                NotifyPropertyChanged("PLOT_DATE");
            }
        }

        public string MEASUREMENT_TYPE
        {
            get => _plot.MEASUREMENT_TYPE;
            set
            {
                _plot.MEASUREMENT_TYPE = value;
                NotifyPropertyChanged("MEASUREMENT_TYPE");
            }
        }

        public int LEAD_SPP
        {
            get => _plot.LEAD_SPP;
            set
            {
                _plot.LEAD_SPP = value;
                NotifyPropertyChanged("LEAD_SPP");
            }
        }

        public int ORIGIN
        {
            get => _plot.ORIGIN;
            set
            {
                _plot.ORIGIN = value;
                NotifyPropertyChanged("ORIGIN");
            }
        }

        public string CANOPY_STRUCTURE
        {
            get => _plot.CANOPY_STRUCTURE;
            set
            {
                _plot.CANOPY_STRUCTURE = value;
                NotifyPropertyChanged("CANOPY_STRUCTURE");
            }
        }

        public string MATURITY
        {
            get => _plot.MATURITY;
            set
            {
                _plot.MATURITY = value;
                NotifyPropertyChanged("MATURITY");
            }
        }

        public int CROWN_CLOSURE
        {
            get => _plot.CROWN_CLOSURE;
            set
            {
                _plot.CROWN_CLOSURE = value;
                NotifyPropertyChanged("CROWN_CLOSURE");
            }
        }

        public string FIELD_CREW1
        {
            get => _plot.FIELD_CREW1;
            set
            {
                _plot.FIELD_CREW1 = value;
                NotifyPropertyChanged("FIELD_CREW1");
            }
        }

        public string FIELD_CREW2
        {
            get => _plot.FIELD_CREW2;
            set
            {
                _plot.FIELD_CREW2 = value;
                NotifyPropertyChanged("FIELD_CREW2");
            }
        }
        public string FIELD_CREW3
        {
            get => _plot.FIELD_CREW3;
            set
            {
                _plot.FIELD_CREW3 = value;
                NotifyPropertyChanged("FIELD_CREW3");
            }
        }
        public string DECLINATION
        {
            get => _plot.DECLINATION;
            set
            {
                _plot.DECLINATION = value;
                NotifyPropertyChanged("DECLINATION");
            }
        }

        public int UTM_ZONE
        {
            get => _plot.UTM_ZONE;
            set
            {
                _plot.UTM_ZONE = value;
                NotifyPropertyChanged("UTM_ZONE");
            }
        }

        public double UTM_EASTING
        {
            get => _plot.UTM_EASTING;
            set
            {
                _plot.UTM_EASTING = value;
                NotifyPropertyChanged("UTM_EASTING");
            }
        }

        public double UTM_NORTHING
        {
            get => _plot.UTM_NORTHING;
            set
            {
                _plot.UTM_NORTHING = value;
                NotifyPropertyChanged("UTM_NORTHING");
            }
        }

        public string DATUM
        {
            get => _plot.DATUM;
            set
            {
                _plot.DATUM = value;
                NotifyPropertyChanged("DATUM");
            }
        }

        public string COMMENTS
        {
            get => _plot.COMMENTS;
            set
            {
                _plot.COMMENTS = value;
                NotifyPropertyChanged("COMMENTS");
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
