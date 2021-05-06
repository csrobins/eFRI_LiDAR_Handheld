using System.Collections.Generic;
using System.Linq;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class ForestHealthViewModel : INotifyPropertyChanged 
    {
        public INavigation _navigation;
        public PLOT _plot;
        public PlotRepository _plotRepository;
        public List<PickerItemsString> ListPerson { get; set; }
        public ForestHealthViewModel(INavigation navigation, PLOT _thisplot)
        {
            _navigation = navigation;
            _plot = new PLOT();
            _plot = _thisplot;
            _plotRepository = new PlotRepository();
            if (_plot.FORESTHEALTHDATE  == System.DateTime.MinValue ) { _plot.FORESTHEALTHDATE = System.DateTime.Now; }

//            ListPerson = FillPersonPicker().OrderBy(c => c.NAME).ToList();
            ListPerson = PickerService.FillPersonPicker(_plotRepository.GetPersonList(_plot.PROJECTID)).OrderBy(c => c.NAME).ToList();
        }
        public string Title
        {
            get => "Forest Health Information for plot " + _plot.VSNPLOTNAME;
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
        public System.DateTime FORESTHEALTHDATE
        {
            get => _plot.FORESTHEALTHDATE;
            set
            {
                _plot.FORESTHEALTHDATE = value;
                NotifyPropertyChanged("FORESTHEALTHDATE");
      
            }
        }
        public string FORESTHEALTHNOTE
        {
            get => _plot.FORESTHEALTHNOTE;
            set
            {
                _plot.FORESTHEALTHNOTE = value;
                NotifyPropertyChanged("FORESTHEALTHNOTE");
             
            }
        }
        public string DAMAGEDESCRIPTION
        {
            get => _plot.DAMAGEDESCRIPTION;
            set
            {
                _plot.DAMAGEDESCRIPTION = value;
                NotifyPropertyChanged("DAMAGEDESCRIPTION");
            
            }
        }
        public string FORESTHEALTHPERSON
        {
            get => _plot.FORESTHEALTHPERSON;
            set
            {
                _plot.FORESTHEALTHPERSON = value;
                NotifyPropertyChanged("FORESTHEALTHPERSON");
          
            }
        }
        public string FOLLOWUPREQUIRED
        {
            get => _plot.FOLLOWUPREQUIRED;
            set
            {
                _plot.FOLLOWUPREQUIRED = value;
                NotifyPropertyChanged("FOLLOWUPREQUIRED");

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
