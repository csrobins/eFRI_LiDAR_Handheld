using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using eLiDAR.Helpers;
using eLiDAR.Models;
using Xamarin.Forms;
using eLiDAR.Utilities;
using System.Windows.Input;
using eLiDAR.API;
using System.Threading.Tasks;
using eLiDAR.Styles;
using eLiDAR.Domain.Global;
using eLiDAR.Services;
using System.Linq;

namespace eLiDAR.ViewModels
{
    public class DefaultViewModel : INotifyPropertyChanged   {

        public INavigation _navigation;
        private Utils util;
     //   private DatabaseHelper databasehelper;
        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItemsString> ListPerson { get; set; }
        PlotRepository _plotRepository = new PlotRepository(); 
        public DefaultViewModel(INavigation navigation)
        {
            _navigation = navigation;
            util = new Utils();
            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.ID).ToList();
            ListPerson = PickerService.FillPersonPicker(_plotRepository.GetPersonList()).OrderBy(c => c.NAME).ToList();
       //     databasehelper = new DatabaseHelper();
           
     //   FetchSettings();
        }
 
        //void FetchSettings() 
        //{
        //    settings = databasehelper.GetSettingsData();
        //    NotifyPropertyChanged("LastSynched");
        //    NotifyPropertyChanged("PROJECT_ROWS_SYNCHED");
        //    NotifyPropertyChanged("PLOT_ROWS_SYNCHED");
        //    NotifyPropertyChanged("TREE_ROWS_SYNCHED");
        //    NotifyPropertyChanged("PROJECT_ROWS_PULLED");
        //    NotifyPropertyChanged("PLOT_ROWS_PULLED");
        //    NotifyPropertyChanged("TREE_ROWS_PULLED");

        //}

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

        private PickerItemsString _selectedPerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedPerson
        {
            get
            {
                _selectedPerson = PickerService.GetItem(ListPerson, util.DefaultPerson);
                return _selectedPerson;
            }
            set
            {
                SetProperty(ref _selectedPerson, value);
                util.DefaultPerson = _selectedPerson.ID;
            }
        }
        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                _selectedSpecies = PickerService.GetItem(ListSpecies, util.DefaultSpecies);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                util.DefaultSpecies = (int)_selectedSpecies.ID;
            }
        }
        public bool UseDefaultSpecies
        {
            get => util.UseDefaultSpecies;
            set
            {
                util.UseDefaultSpecies = value;
                NotifyPropertyChanged("UseDefaultSpecies");
            }
        }
        public string DefaultStatus
        {
            get => util.DefaultStatus;
            set
            {
                util.DefaultStatus = value;
                NotifyPropertyChanged("DefaultStatus");
            }
        }
        public bool UseDefaultDeclination
        {
            get => util.UseDefaultDeclination;
            set
            {
                util.UseDefaultDeclination = value;
                NotifyPropertyChanged("UseDefaultDeclination");
            }
        }
        public int DefaultDeclination
        {
            get => util.DefaultDeclination;
            set
            {
                util.DefaultDeclination = value;
                NotifyPropertyChanged("DefaultDeclination");
            }
        }
        public string DefaultVSNStatus
        {
            get => util.DefaultVSNStatus;
            set
            {
                util.DefaultVSNStatus = value;
                NotifyPropertyChanged("DefaultVSNStatus");
            }
        }
        public string DefaultOrigin
        {
            get => util.DefaultOrigin;
            set
            {
                util.DefaultOrigin = value;
                NotifyPropertyChanged("DefaultOrigin");
            }
        }
        public bool UseDefaultPerson
        {
            get => util.UseDefaultPerson;
            set
            {
                util.UseDefaultPerson = value;
                NotifyPropertyChanged("UseDefaultPerson");
            }
        }
        public bool UseDefaultStatus
        {
            get => util.UseDefaultStatus;
            set
            {
                util.UseDefaultStatus = value;
                NotifyPropertyChanged("UseDefaultStatus");
            }
        }
        public bool UseDefaultOrigin
        {
            get => util.UseDefaultOrigin;
            set
            {
                util.UseDefaultOrigin = value;
                NotifyPropertyChanged("UseDefaultOrigin");
            }
        }
        public bool UseDefaultVSNStatus
        {
            get => util.UseDefaultVSNStatus;
            set
            {
                util.UseDefaultVSNStatus = value;
                NotifyPropertyChanged("UseDefaultVSNStatus");
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
