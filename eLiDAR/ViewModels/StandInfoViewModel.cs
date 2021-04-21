using System.Collections.Generic;
using System.Linq;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class StandInfoViewModel : INotifyPropertyChanged 
    {
        public INavigation _navigation;
        public PLOT _plot;
        public PlotRepository _plotRepository;
        public List<PickerItemsString> ListPerson { get; set; }
        public List<PickerItemsString> ListCanopyStructure { get; set; }
        public List<PickerItems> ListCanopyOrigin { get; set; }
        public List<PickerItemsString> ListMaturityClass { get; set; }
        public List<PickerItems> ListDisturbanceCode { get; set; }
        public StandInfoViewModel(INavigation navigation, PLOT _thisplot)
        {
            _navigation = navigation;
            _plot = new PLOT();
            _plot = _thisplot;
            _plotRepository = new PlotRepository();
            if (_plot.STANDINFODATE  == System.DateTime.MinValue ) { _plot.STANDINFODATE = System.DateTime.Now; }

            ListPerson = FillPersonPicker().OrderBy(c => c.NAME).ToList();
            ListCanopyOrigin = PickerService.CanopyOriginItems().OrderBy(c => c.NAME).ToList();
            ListCanopyStructure = PickerService.CanopyStructureItems().OrderBy(c => c.NAME).ToList();
            ListMaturityClass = PickerService.MaturityClassItems().OrderBy(c => c.NAME).ToList();
            ListDisturbanceCode = PickerService.DisturbanceItems().OrderBy(c => c.NAME).ToList();
        }
        public string Title
        {
            get => "Stand Information for plot " + _plot.VSNPLOTNAME;
            set
            {
            }
        }
        private List<PickerItemsString> FillPersonPicker()
        {
            var list = new List<PickerItemsString>();
            foreach (var newperson in _plotRepository.GetPersonList(_plot.PROJECTID))
            {
                var newitem = new PickerItemsString() { ID = newperson.LASTNAME + ", " + newperson.FIRSTNAME, NAME = newperson.LASTNAME + ", " + newperson.FIRSTNAME };
                list.Add(newitem);
            };
            return list;
        }

        private PickerItemsString _selectedStandInfoPerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedStandInfoPerson
        {
            get
            {
                _selectedStandInfoPerson = PickerService.GetItem(ListPerson, _plot.STANDINFOPERSON);
                return _selectedStandInfoPerson;
            }
            set
            {
                SetProperty(ref _selectedStandInfoPerson, value);
                _plot.STANDINFOPERSON = _selectedStandInfoPerson.ID;
            }
        }
        private PickerItems _selectedDisturbance = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDisturbance
        {
            get
            {
                _selectedDisturbance = PickerService.GetItem(ListDisturbanceCode, _plot.DISTURBANCECODE );
                return _selectedDisturbance;
            }
            set
            
            {
                if (value == null) { return; }
                if (value == _selectedDisturbance) { return; }
                SetProperty(ref _selectedDisturbance, value);
                _plot.DISTURBANCECODE  = (int)_selectedDisturbance.ID;
          //      OnPropertyChanged();
            }
        }
        private PickerItems _selectedCanopyOrigin = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedCanopyOrigin
        {
            get
            {
                _selectedCanopyOrigin = PickerService.GetItem(ListCanopyOrigin, _plot.ORIGIN);
                return _selectedCanopyOrigin;
            }
            set
            {
                try
                {
                    if (value == _selectedCanopyOrigin) { return; }
                    SetProperty(ref _selectedCanopyOrigin, value);
                    _plot.ORIGIN = (int)_selectedCanopyOrigin.ID;
                }
                catch (System.Exception e)
                {
                    var myerror = e.Message; // error
                                             //  Log.Fatal(e);
                };
            }
        }
        private PickerItemsString _selectedCanopyStructure = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCanopyStructure
        {
            get
            {
                _selectedCanopyStructure = PickerService.GetItem(ListCanopyStructure, _plot.CANOPYSTRUCTURECODE);
                return _selectedCanopyStructure;
            }
            set
            {
                if (value == null) { return; }
                SetProperty(ref _selectedCanopyStructure, value);
                _plot.CANOPYSTRUCTURECODE = _selectedCanopyStructure.ID;
            }
        }
        private PickerItemsString _selectedMaturityClass = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMaturityClass
        {
            get
            {
                _selectedMaturityClass = PickerService.GetItem(ListMaturityClass, _plot.MATURITYCLASSCODE);
                return _selectedMaturityClass;
            }
            set
            {
                SetProperty(ref _selectedMaturityClass, value);
                _plot.MATURITYCLASSCODE = _selectedMaturityClass.ID;
            }
        }
        public int PERCENTAFFECTED
        {
            get => _plot.PERCENTAFFECTED;
            set
            {
                _plot.PERCENTAFFECTED = value;
                NotifyPropertyChanged("PERCENTAFFECTED");
                //  IsChanged = true;
            }
        }
        public int PERCENTMORTALITY
        {
            get => _plot.PERCENTMORTALITY;
            set
            {
                _plot.PERCENTMORTALITY = value;
                NotifyPropertyChanged("PERCENTMORTALITY");
                //   IsChanged = true;
            }
        }
        public System.DateTime STANDINFODATE
        {
            get => _plot.STANDINFODATE;
            set
            {
                _plot.STANDINFODATE = value;
                NotifyPropertyChanged("STANDINFODATE");
              
            }
        }

        public string STANDINFONOTE
        {
            get => _plot.STANDINFONOTE;
            set
            {
                _plot.STANDINFONOTE = value;
                NotifyPropertyChanged("STANDINFONOTE");
               
            }
        }
        public string MATURITYCLASSRATIONALE
        {
            get => _plot.MATURITYCLASSRATIONALE;
            set
            {
                _plot.MATURITYCLASSRATIONALE = value;
                NotifyPropertyChanged("MATURITYCLASSRATIONALE");
                
            }
        }

        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName] string propertyName = null)
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
        public event PropertyChangedEventHandler PropertyChanged2;
        #endregion
    }
}
