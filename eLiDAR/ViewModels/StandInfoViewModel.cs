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

//            ListPerson = FillPersonPicker().OrderBy(c => c.NAME).ToList();
            ListPerson = PickerService.FillPersonPicker(_plotRepository.GetPersonList(_plot.PROJECTID)).OrderBy(c => c.NAME).ToList();
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
                var newitem = new PickerItemsString() { ID = newperson.LASTNAME + " " + newperson.FIRSTNAME, NAME = newperson.LASTNAME + ", " + newperson.FIRSTNAME };
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
        private PickerItems _selectedDisturbance1 = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDisturbance1
        {
            get
            {
                _selectedDisturbance1 = PickerService.GetItem(ListDisturbanceCode, _plot.DISTURBANCECODE1 );
                return _selectedDisturbance1;
            }
            set
            
            {
                if (value == null) { return; }
                if (value == _selectedDisturbance1) { return; }
                SetProperty(ref _selectedDisturbance1, value);
                _plot.DISTURBANCECODE1  = (int)_selectedDisturbance1.ID;
          //      OnPropertyChanged();
            }
        }
        private PickerItems _selectedDisturbance2 = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDisturbance2
        {
            get
            {
                _selectedDisturbance2 = PickerService.GetItem(ListDisturbanceCode, _plot.DISTURBANCECODE2);
                return _selectedDisturbance2;
            }
            set

            {
                if (value == null) { return; }
                if (value == _selectedDisturbance2) { return; }
                SetProperty(ref _selectedDisturbance2, value);
                _plot.DISTURBANCECODE2 = (int)_selectedDisturbance2.ID;
                //      OnPropertyChanged();
            }
        }
        private PickerItems _selectedCanopyOrigin1 = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedCanopyOrigin1
        {
            get
            {
                _selectedCanopyOrigin1 = PickerService.GetItem(ListCanopyOrigin, _plot.MAINCANOPYORIGINCODE1);
                return _selectedCanopyOrigin1;
            }
            set
            {
                try
                {
                    if (value == _selectedCanopyOrigin1) { return; }
                    SetProperty(ref _selectedCanopyOrigin1, value);
                    _plot.MAINCANOPYORIGINCODE1 = (int)_selectedCanopyOrigin1.ID;
                }
                catch (System.Exception e)
                {
                    var myerror = e.Message; // error
                                             //  Log.Fatal(e);
                };
            }
        }
        private PickerItems _selectedCanopyOrigin2 = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedCanopyOrigin2
        {
            get
            {
                _selectedCanopyOrigin2 = PickerService.GetItem(ListCanopyOrigin, _plot.MAINCANOPYORIGINCODE2);
                return _selectedCanopyOrigin2;
            }
            set
            {
                try
                {
                    if (value == _selectedCanopyOrigin2) { return; }
                    SetProperty(ref _selectedCanopyOrigin2, value);
                    _plot.MAINCANOPYORIGINCODE2 = (int)_selectedCanopyOrigin2.ID;
                }
                catch (System.Exception e)
                {
                    var myerror = e.Message; // error
                                             //  Log.Fatal(e);
                };
            }
        }
        private PickerItemsString _selectedCanopyStructure1 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCanopyStructure1
        {
            get
            {
                _selectedCanopyStructure1 = PickerService.GetItem(ListCanopyStructure, _plot.CANOPYSTRUCTURECODE1);
                return _selectedCanopyStructure1;
            }
            set
            {
                if (value == null) { return; }
                SetProperty(ref _selectedCanopyStructure1, value);
                _plot.CANOPYSTRUCTURECODE1 = _selectedCanopyStructure1.ID;
            }
        }
        private PickerItemsString _selectedCanopyStructure2 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCanopyStructure2
        {
            get
            {
                _selectedCanopyStructure2 = PickerService.GetItem(ListCanopyStructure, _plot.CANOPYSTRUCTURECODE2);
                return _selectedCanopyStructure2;
            }
            set
            {
                if (value == null) { return; }
                SetProperty(ref _selectedCanopyStructure2, value);
                _plot.CANOPYSTRUCTURECODE2 = _selectedCanopyStructure2.ID;
            }
        }
        private PickerItemsString _selectedMaturityClass1 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMaturityClass1
        {
            get
            {
                _selectedMaturityClass1 = PickerService.GetItem(ListMaturityClass, _plot.MATURITYCLASSCODE1);
                return _selectedMaturityClass1;
            }
            set
            {
                SetProperty(ref _selectedMaturityClass1, value);
                _plot.MATURITYCLASSCODE1 = _selectedMaturityClass1.ID;
            }
        }
        private PickerItemsString _selectedMaturityClass2 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMaturityClass2
        {
            get
            {
                _selectedMaturityClass2 = PickerService.GetItem(ListMaturityClass, _plot.MATURITYCLASSCODE2);
                return _selectedMaturityClass2;
            }
            set
            {
                SetProperty(ref _selectedMaturityClass2, value);
                _plot.MATURITYCLASSCODE2 = _selectedMaturityClass2.ID;
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
        public string MATURITYCLASSRATIONALE1
        {
            get => _plot.MATURITYCLASSRATIONALE1;
            set
            {
                _plot.MATURITYCLASSRATIONALE1 = value;
                NotifyPropertyChanged("MATURITYCLASSRATIONALE1");
                
            }
        }
        public string MATURITYCLASSRATIONALE2
        {
            get => _plot.MATURITYCLASSRATIONALE2;
            set
            {
                _plot.MATURITYCLASSRATIONALE2 = value;
                NotifyPropertyChanged("MATURITYCLASSRATIONALE2");
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
