using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using Xamarin.Forms;
using eLiDAR.Utilities;
using System.Windows.Input;
using eLiDAR.API;
using System.Threading.Tasks;


namespace eLiDAR.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged {

        public INavigation _navigation;
        private Utils util;
        private SETTINGS settings;
        private DatabaseHelper databasehelper;
        public ICommand SynchCommand { get; private set; }
        private SynchManager _synchmanager;
        public SettingsViewModel(INavigation navigation)
        {
            _navigation = navigation;
            util = new Utils();
            SynchCommand = new Command(async () => await Synchrun());
            _synchmanager = new SynchManager();
            databasehelper = new DatabaseHelper(); 
            FetchSettings();
        }
         async Task Synchrun()
        {

            bool success = await  _synchmanager.RunSynch();
            if (success)
            {
                msg = "Synch succeeded";
                await Application.Current.MainPage.DisplayAlert("Synch", "The synch operation completed successfully.", "OK");
            }
            else { 
                msg = "Not all tables synched!";
                await Application.Current.MainPage.DisplayAlert("Synch did not fnish", "The synch operation was not completed.", "OK");
            }
            FetchSettings(); 
        }
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
