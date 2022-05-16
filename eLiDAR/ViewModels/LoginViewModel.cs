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

namespace eLiDAR.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged   {

        public INavigation _navigation;
        private Utils util;
        private SETTINGS settings;
        private DatabaseHelper databasehelper;
        public ICommand LoginCommand { get; private set; }
    
        public string SelectedTheme { get; set; }
        public LoginViewModel(INavigation navigation)
        {
            _navigation = navigation;
            util = new Utils();
            LoginCommand = new Command(async () => await DoLogin());
            databasehelper = new DatabaseHelper();
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
      
        async Task DoLogin()
        {
            USER _user = new USER();
            _user = databasehelper.GetUser(Username);
            if (_user != null)
            {
                var test = util.MD5Hash(Pwd);

                if (_user.PWD == util.MD5Hash(Pwd))
                {
                    util.IsLoggedIn = true;
                    util.LoggedInAs = Username;
                    util.PutURI = _user.PUT;
                    util.PostURI = _user.POST;
                    util.GetURI = _user.GET;
                    util.KEY = _user.KEY;
                    await _navigation.PopAsync(true);
                }
                else
                {
                    util.IsLoggedIn = false;
                    util.LoggedInAs = null;
                    util.PutURI = null;
                    util.PostURI = null;
                    util.GetURI = null;
                    await Application.Current.MainPage.DisplayAlert("Login failed", "Incorrect username/password", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", "Incorrect username/password", "OK");
                util.IsLoggedIn = false;
                util.LoggedInAs = null;
                util.PutURI = null;
                util.PostURI = null;
                util.GetURI = null;

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
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyPropertyChanged("Username");
            }
        }
        private string _pwd;
        public string Pwd
        {
            get => _pwd;
            set
            {
                _pwd = value;
                NotifyPropertyChanged("Pwd");
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
