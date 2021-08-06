using System.Collections.Generic;
using System.Linq;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using eLiDAR.Utilities;
using System;

namespace eLiDAR.ViewModels {
    public class PlotCrewViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public PLOT _plot;
        public PlotRepository _plotRepository;
        public List<PickerItemsString> ListPerson { get; set; }

        public PlotCrewViewModel(INavigation navigation, PLOT _thisplot)
        {
            try
            {
                _navigation = navigation;
                _plot = new PLOT();
                _plot = _thisplot;
                _plotRepository = new PlotRepository();
                Utils util = new Utils();
                // DO DEFAULTS
                if (_plot.SMALLTREESHRUBDATE == System.DateTime.MinValue) { _plot.SMALLTREESHRUBDATE = System.DateTime.Now; }
                if (_plot.DEFORMITYDATE == System.DateTime.MinValue) { _plot.DEFORMITYDATE = System.DateTime.Now; }
                if (_plot.DOWNWOODYDEBRISDATE == System.DateTime.MinValue) { _plot.DOWNWOODYDEBRISDATE = System.DateTime.Now; }
                if (_plot.UNDERSTORYVEGETATIONDATE == System.DateTime.MinValue) { _plot.UNDERSTORYVEGETATIONDATE = System.DateTime.Now; }
                if (_plot.UNDERSTORYCENSUSDATE == System.DateTime.MinValue) { _plot.UNDERSTORYCENSUSDATE = System.DateTime.Now; }
                if (_plot.AGEDATE  == System.DateTime.MinValue) { _plot.AGEDATE = System.DateTime.Now; }
                if (_plot.UNDERSTORYVEGETATIONAREA == 0) { _plot.UNDERSTORYVEGETATIONAREA = Constants.DefaultUnderstoryVegArea; }
                if (_plot.SMALLTREESHRUBAREA == 0) { _plot.SMALLTREESHRUBAREA = Constants.DefaultSmallTreeArea; }
                if (_plot.LINELENGTH1 == 0 && _plot.VSNPLOTTYPECODE.Contains("C")) { _plot.LINELENGTH1 = Constants.DefaultDWDLineLength; }
                if (_plot.LINELENGTH2 == 0 && _plot.VSNPLOTTYPECODE.Contains("C")) { _plot.LINELENGTH2 = Constants.DefaultDWDLineLength; }
                if (util.UseDefaultPerson)
                {
                    if (_plot.STANDINFOPERSON == null) { _plot.STANDINFOPERSON = util.DefaultPerson; }
                    if (_plot.SMALLTREEPERSON == null) { _plot.SMALLTREEPERSON = util.DefaultPerson; }
                    if (_plot.AGEPERSON == null) { _plot.AGEPERSON = util.DefaultPerson; }
                    if (_plot.FIELD_CREW1 == null) { _plot.FIELD_CREW1 = util.DefaultPerson; }
                    if (_plot.FORESTHEALTHPERSON == null) { _plot.FORESTHEALTHPERSON = util.DefaultPerson; }
                }
                //   ListPerson = FillPersonPicker().OrderBy(c => c.NAME).ToList();
                ListPerson = PickerService.FillPersonPicker(_plotRepository.GetPersonList(_plot.PROJECTID)).OrderBy(c => c.NAME).ToList();
            }
            catch (System.Exception ex)
            {
                var msg = ex.Message;
            }
        }

        public string Title
        {
            get => "Notes and personnel for plot " + _plot.VSNPLOTNAME + " elements";
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

        private PickerItemsString _selectedDeformityPerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedDeformityPerson
        {
            get
            {
                _selectedDeformityPerson = PickerService.GetItem(ListPerson, _plot.DEFORMITYPERSON);
                return _selectedDeformityPerson;
            }
            set
            {
                SetProperty(ref _selectedDeformityPerson, value);
                _plot.DEFORMITYPERSON = _selectedDeformityPerson.ID;
            }
        }
        private PickerItemsString _selectedDWDPerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedDWDPerson
        {
            get
            {
                _selectedDWDPerson = PickerService.GetItem(ListPerson, _plot.DOWNWOODYDEBRISPERSON);
                return _selectedDWDPerson;
            }
            set
            {
                SetProperty(ref _selectedDWDPerson, value);
                _plot.DOWNWOODYDEBRISPERSON = _selectedDWDPerson.ID;
            }
        }
        private PickerItemsString _selectedUnderstoryVegPerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedUnderstoryVegPerson
        {
            get
            {
                _selectedUnderstoryVegPerson = PickerService.GetItem(ListPerson, _plot.UNDERSTORYVEGETATIONPERSON);
                return _selectedUnderstoryVegPerson;
            }
            set
            {
                SetProperty(ref _selectedUnderstoryVegPerson, value);
                _plot.UNDERSTORYVEGETATIONPERSON = _selectedUnderstoryVegPerson.ID;
            }
        }
        private PickerItemsString _selectedAgePerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedAgePerson
        {
            get
            {
                _selectedAgePerson = PickerService.GetItem(ListPerson, _plot.AGEPERSON);
                return _selectedAgePerson;
            }
            set
            {
                SetProperty(ref _selectedAgePerson, value);
                _plot.AGEPERSON = _selectedAgePerson.ID;
            }
        }
        private PickerItemsString _selectedStemMappingPerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedStemMappingPerson
        {
            get
            {
                _selectedStemMappingPerson = PickerService.GetItem(ListPerson, _plot.STEMMAPPINGPERSON);
                return _selectedStemMappingPerson;
            }
            set
            {
                SetProperty(ref _selectedStemMappingPerson, value);
                _plot.STEMMAPPINGPERSON = _selectedStemMappingPerson.ID;
            }
        }
        private PickerItemsString _selectedUnderstoryCensusPerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedUnderstoryCensusPerson
        {
            get
            {
                _selectedUnderstoryCensusPerson = PickerService.GetItem(ListPerson, _plot.UNDERSTORYCENSUSPERSON);
                return _selectedUnderstoryCensusPerson;
            }
            set
            {
                SetProperty(ref _selectedUnderstoryCensusPerson, value);
                _plot.UNDERSTORYCENSUSPERSON = _selectedUnderstoryCensusPerson.ID;
            }
        }

        private PickerItemsString _selectedSmallTreePerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedSmallTreePerson
        {
            get
            {
                _selectedSmallTreePerson = PickerService.GetItem(ListPerson, _plot.SMALLTREEPERSON);
                return _selectedSmallTreePerson;
            }
            set
            {
                SetProperty(ref _selectedSmallTreePerson, value);
                _plot.SMALLTREEPERSON = _selectedSmallTreePerson.ID;
            }
        }
        public int SMALLTREESHRUBAREA
        {
            get => _plot.SMALLTREESHRUBAREA;
            set
            {
                _plot.SMALLTREESHRUBAREA = value;
                NotifyPropertyChanged("SMALLTREESHRUBAREA");

            }
        }

        public System.DateTime SMALLTREESHRUBDATE
        {
            get => _plot.SMALLTREESHRUBDATE;
            set
            {
                _plot.SMALLTREESHRUBDATE = value;
                NotifyPropertyChanged("SMALLTREESHRUBDATE");

            }
        }

        public bool IsPlotTypeB 
            {
            get 
              {
              if (_plot.VSNPLOTTYPECODE == "B" || _plot.VSNPLOTTYPECODE == "C") {return true;}
              else { return false; }
              }
           }
        public bool IsPlotTypeC
        {
            get
            {
                if (_plot.VSNPLOTTYPECODE == "C") { return true; }
                else { return false; }
            }
        }

        public string SMALLTREESHRUBNOTE
        {
            get => _plot.SMALLTREESHRUBNOTE;
            set
            {
                _plot.SMALLTREESHRUBNOTE = value;
                NotifyPropertyChanged("SMALLTREESHRUBNOTE");
        
            }
        }
        public double LINELENGTH1
        {
            get => _plot.LINELENGTH1 ;
            set
            {
                _plot.LINELENGTH1 = value;
                NotifyPropertyChanged("LINELENGTH1");

            }
        }
        public double LINELENGTH2
        {
            get => _plot.LINELENGTH2;
            set
            {
                _plot.LINELENGTH2 = value;
                NotifyPropertyChanged("LINELENGTH2");

            }
        }
        public string SMALLTREEPERSON
        {
            get => _plot.SMALLTREEPERSON;
            set
            {
                _plot.SMALLTREEPERSON = value;
                NotifyPropertyChanged("SMALLTREEPERSON");
     
            }
        }
        public System.DateTime UNDERSTORYVEGETATIONDATE
        {
            get => _plot.UNDERSTORYVEGETATIONDATE;
            set
            {
                _plot.UNDERSTORYVEGETATIONDATE = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONDATE");
             
            }
        }
        public int UNDERSTORYVEGETATIONAREA
        {
            get => _plot.UNDERSTORYVEGETATIONAREA;
            set
            {
                _plot.UNDERSTORYVEGETATIONAREA = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONAREA");
           
            }
        }

        public string UNDERSTORYVEGETATIONNOTE
        {
            get => _plot.UNDERSTORYVEGETATIONNOTE;
            set
            {
                _plot.UNDERSTORYVEGETATIONNOTE = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONNOTE");
           
            }
        }
        public string UNDERSTORYVEGETATIONPERSON
        {
            get => _plot.UNDERSTORYVEGETATIONPERSON;
            set
            {
                _plot.UNDERSTORYVEGETATIONPERSON = value;
                NotifyPropertyChanged("UNDERSTORYVEGETATIONPERSON");
                
            }
        }
        public System.DateTime UNDERSTORYCENSUSDATE
        {
            get => _plot.UNDERSTORYCENSUSDATE;
            set
            {
                _plot.UNDERSTORYCENSUSDATE = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSDATE");
           
            }
        }
        public string UNDERSTORYCENSUSNOTE
        {
            get => _plot.UNDERSTORYCENSUSNOTE;
            set
            {
                _plot.UNDERSTORYCENSUSNOTE = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSNOTE");
  
            }
        }
        public string UNDERSTORYCENSUSPERSON
        {
            get => _plot.UNDERSTORYCENSUSPERSON;
            set
            {
                _plot.UNDERSTORYCENSUSPERSON = value;
                NotifyPropertyChanged("UNDERSTORYCENSUSPERSON");
                
            }
        }
        public System.DateTime DOWNWOODYDEBRISDATE
        {
            get => _plot.DOWNWOODYDEBRISDATE;
            set
            {
                _plot.DOWNWOODYDEBRISDATE = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISDATE");
       
            }
        }
        public string DOWNWOODYDEBRISNOTE
        {
            get => _plot.DOWNWOODYDEBRISNOTE;
            set
            {
                _plot.DOWNWOODYDEBRISNOTE = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISNOTE");
           
            }
        }
        public string DOWNWOODYDEBRISPERSON
        {
            get => _plot.DOWNWOODYDEBRISPERSON;
            set
            {
                _plot.DOWNWOODYDEBRISPERSON = value;
                NotifyPropertyChanged("DOWNWOODYDEBRISPERSON");
            
            }
        }
        public System.DateTime DEFORMITYDATE
        {
            get => _plot.DEFORMITYDATE;
            set
            {
                _plot.DEFORMITYDATE = value;
                NotifyPropertyChanged("DEFORMITYDATE");
           
            }
        }
        public string DEFORMITYNOTE
        {
            get => _plot.DEFORMITYNOTE;
            set
            {
                _plot.DEFORMITYNOTE = value;
                NotifyPropertyChanged("DEFORMITYNOTE");
             
            }
        }
        public string DEFORMITYPERSON
        {
            get => _plot.DEFORMITYPERSON;
            set
            {
                _plot.DEFORMITYPERSON = value;
                NotifyPropertyChanged("DEFORMITYPERSON");
           
            }
        }
        public DateTime AGEDATE
        {
            get => _plot.AGEDATE;
            set
            {
                _plot.AGEDATE = value;
                NotifyPropertyChanged("AGEDATE");
               
            }
        }
        public string AGENOTE
        {
            get => _plot.AGENOTE;
            set
            {
                _plot.AGENOTE = value;
                NotifyPropertyChanged("AGENOTE");
                
            }
        }

        public string AGEPERSON
        {
            get => _plot.AGEPERSON;
            set
            {
                _plot.AGEPERSON = value;
                NotifyPropertyChanged("AGEPERSON");
                
            }
        }

        public DateTime STEMMAPPINGDATE
        {
            get => _plot.STEMMAPPINGDATE;
            set
            {
                _plot.STEMMAPPINGDATE = value;
                NotifyPropertyChanged("STEMMAPPINGDATE");
              
            }
        }
        public string STEMMAPPINGNOTE
        {
            get => _plot.STEMMAPPINGNOTE;
            set
            {
                _plot.STEMMAPPINGNOTE = value;
                NotifyPropertyChanged("STEMMAPPINGNOTE");
               
            }
        }

        public string STEMMAPPINGPERSON
        {
            get => _plot.STEMMAPPINGPERSON;
            set
            {
                _plot.STEMMAPPINGPERSON = value;
                NotifyPropertyChanged("STEMMAPPINGPERSON");
               
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
