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
using eLiDAR.Views;
using System.Linq;

namespace eLiDAR.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged   {

        public INavigation _navigation;
        private Utils util;
        private SETTINGS settings;
        private DatabaseHelper databasehelper;
        public ICommand SynchCommand { get; private set; }
        public ICommand SynchPlotCommand { get; private set; }
        public ICommand DefaultCommand { get; private set; }
       // private SynchManager _synchmanager;
        public ICommand ChangeThemeCommand { get; set; }
        public List<PickerItemsString> ListPlots { get; set; }
        public string SelectedTheme { get; set; }
        public SettingsViewModel(INavigation navigation)
        {
            _navigation = navigation;
            util = new Utils();
    //        SynchCommand = new Command(async () => await Synchrun());
    //        SynchPlotCommand = new Command(async () => await SynchPlotrun());
            DefaultCommand = new Command(async () => await DoDefault());
            //_synchmanager = new SynchManager();
            databasehelper = new DatabaseHelper();
            ListPlots = Services.PickerService.FillPlotPicker(databasehelper.GetAllPlotData()).ToList().OrderBy(c => c.NAME).ToList();
            ChangeThemeCommand = new Command((x) =>
            {
                if (SelectedTheme.ToLower() == "dark")
                {
                    Application.Current.Resources = new DarkTheme();
                }
                else
                {
                    Application.Current.Resources = new LightTheme();
                }
                App.AppTheme = SelectedTheme.ToLower();
            });
            if (!IsSynchBusy) { IsSynchEnabled = true; }
            FetchSettings();
           
        }
        async Task DoDefault()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new DefaultPage());
        }
        public bool UseDarkMode
        {
            get => DependencyService.Get<AppModel>().UseDarkMode;
            set
            {
                DependencyService.Get<AppModel>().UseDarkMode = value;
                OnPropertyChanged(nameof(UseDarkMode));

                if (UseDarkMode && App.AppTheme != "dark")
                {
                    App.Current.Resources = new DarkTheme();
                    App.AppTheme = "dark";
                }
                else if (!UseDarkMode && App.AppTheme == "dark")
                {
                    App.Current.Resources = new LightTheme();
                    App.AppTheme = "light";
                }
            }
        }

        public bool UseDeviceThemeSettings
        {
            get => DependencyService.Get<AppModel>().UseDeviceThemeSettings;
            set
            {
                DependencyService.Get<AppModel>().UseDeviceThemeSettings = value;
                OnPropertyChanged(nameof(UseDeviceThemeSettings));
        
            }
        }

     //  async Task Synchrun()
     //  {
     //      IsSynchBusy = true;
     //      bool success = await  _synchmanager.RunSynch();
     //      IsSynchBusy = false;
     //      if (success)
     //      {
     //          msg = "Synch succeeded";
     //          await Application.Current.MainPage.DisplayAlert("Synch", "The synch operation completed successfully.", "OK");
     //      }
     //      else { 
     //          msg = "Not all tables synched!";
     //          await Application.Current.MainPage.DisplayAlert("Synch did not finish", "The synch operation was not completed.", "OK");
     //      }
     //      FetchSettings(); 
     //  }
     //  async Task SynchPlotrun()
     //  {
     //      IsPlotSynchBusy = true;
     //      bool success = await _synchmanager.RunSynch(_selectedPlot.ID);
     //      IsPlotSynchBusy = false;
     //      if (success)
     //      {
     //          msg = "Plot Synch succeeded for " + _selectedPlot.NAME;
     //          await Application.Current.MainPage.DisplayAlert("Synch", msg, "OK");
     //      }
     //      else
     //      {
     //          msg = "Not all tables synched!";
     //          await Application.Current.MainPage.DisplayAlert("Synch did not finish", msg, "OK");
     //      }
     //      FetchSettings();
     //  }
        void FetchSettings() 
        {
            settings = databasehelper.GetSettingsData();
            NotifyPropertyChanged("LastSynched");
            NotifyPropertyChanged("PROJECT_ROWS_SYNCHED");
            NotifyPropertyChanged("PLOT_ROWS_SYNCHED");
            NotifyPropertyChanged("TREE_ROWS_SYNCHED");
            NotifyPropertyChanged("PROJECT_ROWS_PULLED");
            NotifyPropertyChanged("PLOT_ROWS_PULLED");
            NotifyPropertyChanged("TREE_ROWS_PULLED");
        }
        private void Update()
        {
            databasehelper.UpdateSettings(settings);  
        }

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
        private bool _issynchbusy;
        public bool IsSynchBusy
        {
            get => _issynchbusy;
            set
            {
                _issynchbusy = value;
                if (value) {
                    IsSynchEnabled = false;
                }
                else
                { IsSynchEnabled = true; }
                NotifyPropertyChanged("IsSynchBusy");
            }
        }
        private bool _isplotsynchbusy;
        public bool IsPlotSynchBusy
        {
            get => _isplotsynchbusy;
            set
            {
                _isplotsynchbusy = value;
                if (value)
                {
                    IsSynchEnabled = false;
                }
                else
                { IsSynchEnabled = true; }
                NotifyPropertyChanged("IsPlotSynchBusy");
            }
        }
        private bool _issynchenabled;
        public bool IsSynchEnabled
        {
            get => _issynchenabled;
            set
            {
                if (util.IsLoggedIn) { _issynchenabled = value; }
                else { _issynchenabled = false; }
                //_issynchenabled = true;
                NotifyPropertyChanged("IsSynchEnabled");
            }
        }
        private bool _isplotsynchenabled;
        public bool IsPlotSynchEnabled
        {
            get => _isplotsynchenabled;
            set
            {
                if (_selectedPlot !=null) 
                {
                    if (_selectedPlot.ID != "" && IsSynchEnabled) { _isplotsynchenabled = value; }
                    else { _isplotsynchenabled = false; }

                }
                else { _isplotsynchenabled = false; }
                //_issynchenabled = true;
                NotifyPropertyChanged("IsPlotSynchEnabled");
            }
        }

        private PickerItemsString _selectedPlot = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedPlot
        {
            get
            {
           //     _selectedPlot = PickerService.GetItem(ListPlots, _smallTree.SPECIESCODE);
                return _selectedPlot;
            }
            set
            {
                SetProperty(ref _selectedPlot, value);
                if (value != null)
                {
                    if (value.ID != "") { IsPlotSynchEnabled = true; }
                    else { IsPlotSynchEnabled = false; }
                }
                else { IsPlotSynchEnabled = false; }
                _selectedPlot = value;
            }
        }
        private bool _allowdatechange = false;
        public bool AllowDateChange
        {
            get => _allowdatechange;
            set
            {
                _allowdatechange = value;
                NotifyPropertyChanged("AllowDateChange");
            }
        }
        public bool AllowVegCalc
        {
            get => util.AllowVegCalc;
            set
            {
                util.AllowVegCalc = value;
                NotifyPropertyChanged("AllowVegCalc");
            }
        }
        public bool UseAlphaSpecies
        {
            get => util.UseAlphaSpecies;
            set
            {
                util.UseAlphaSpecies = value;
                NotifyPropertyChanged("UseAlphaSpecies");
            }
        }
        public bool IsNotifySave
        {
            get => util.NotifySave ;
            set
            {
                util.NotifySave  = value;
                NotifyPropertyChanged("IsNotifySave");
            }
        }
        public bool IsAllowPlotDeletion
        {
            get => util.AllowPlotDeletion;
            set
            {
                util.AllowPlotDeletion = value;
                NotifyPropertyChanged("IsAllowPlotDeletion");
            }
        }
        public bool IsAllowProjectDeletion
        {
            get => util.AllowProjectDeletion;
            set
            {
                util.AllowProjectDeletion = value;
                NotifyPropertyChanged("IsAllowProjectDeletion");
            }
        }
        public bool IsAllowAutoNumber
        {
            get => util.AllowAutoNumber;
            set
            {
                util.AllowAutoNumber = value;
                NotifyPropertyChanged("IsAllowAutoNumber");
            }
        }
        public bool UseNumericList
        {
            get => util.UseNumericList;
            set
            {
                util.UseNumericList = value;
                if (value) { UseAlphaList = !value; }
                NotifyPropertyChanged("UseNumericList");
            }
        }
        public bool UseAlphaList
        {
            get => util.UseAlphaList;
            set
            {
                util.UseAlphaList = value;
                if (value) { UseNumericList = !value; }
                NotifyPropertyChanged("UseAlphaList");
            }
        }
        public bool IsLoggedIn
        {
            get
            {
                return util.IsLoggedIn;
            }
            set
            {
                util.IsLoggedIn = value;
                NotifyPropertyChanged("IsLoggedIn");
            }
        }
        public bool DoPartialSynch
        {
            get => util.DoPartialSynch;
            set
            {
                util.DoPartialSynch  = value;
                NotifyPropertyChanged("DoPartialSynch");
            }
        }
        public bool IsBoreal
        {
            get => util.BorealSpecies;
            set
            {
                util.BorealSpecies = value;
                NotifyPropertyChanged("IsBoreal");
            }
        }
        private string _msg;
        public string msg
        {
            get => _msg;
            set
            {
                _msg = value;
                NotifyPropertyChanged("msg");
            }
        }
        public DateTime LastSynched
        {
            get => settings.LastSynched;
            set
            {
                settings.LastSynched = value;
                Update();
                NotifyPropertyChanged("LastSynched");
            }
        }
        public int PROJECT_ROWS_SYNCHED
        {
            get => settings.PROJECT_ROWS_SYNCHED;
            set
            {
                NotifyPropertyChanged("PROJECT_ROWS_SYNCHED");
            }
        }
        public int PLOT_ROWS_SYNCHED
        {
            get => settings.PLOT_ROWS_SYNCHED;
            set
            {
                NotifyPropertyChanged("PLOT_ROWS_SYNCHED");
            }
        }
        public int TREE_ROWS_SYNCHED
        {
            get => settings.TREE_ROWS_SYNCHED;
            set
            {
                NotifyPropertyChanged("TREE_ROWS_SYNCHED");
            }
        }

        public int PROJECT_ROWS_PULLED
        {
            get => settings.PROJECT_ROWS_PULLED;
            set
            {
                NotifyPropertyChanged("PROJECT_ROWS_PULLED");
            }
        }
        public int PLOT_ROWS_PULLED
        {
            get => settings.PLOT_ROWS_PULLED;
            set
            {
                NotifyPropertyChanged("PLOT_ROWS_PULLED");
            }
        }
        public int TREE_ROWS_PULLED
        {
            get => settings.TREE_ROWS_PULLED;
            set
            {
                NotifyPropertyChanged("TREE_ROWS_PULLED");
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
