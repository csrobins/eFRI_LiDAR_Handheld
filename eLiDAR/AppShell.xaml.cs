
using System;
using System.Collections.Generic;
using System.Windows.Input;
using eLiDAR.Pages;
using Xamarin.Forms;

namespace eLiDAR
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public ICommand TakePhotoCommand => new Command(GoToCamera);
        public ICommand LoginCommand => new Command(GoToLogin);

        public AppShell()
        {
            InitializeComponent();

            BindingContext = this;

            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute("login", typeof(LoginPage));
        }

        void GoToCamera()
        {
            Shell.Current.GoToAsync("//photo?payload=4.x", true);
            Shell.Current.FlyoutIsPresented = false;
        }

        void GoToLogin()
        {
            Shell.Current.Navigation.PushModalAsync(new LoginPage(), true);
            Shell.Current.FlyoutIsPresented = false;
        }
    }
}
