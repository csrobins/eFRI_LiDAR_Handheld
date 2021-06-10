using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eLiDAR.Views;
using System;
using eLiDAR.Domain.Global;

namespace eLiDAR
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //   MainPage = new eLiDAR.AppShell();
            DependencyService.Register<AppModel>();

            try
            {
                MainPage = new MainShell();
            }
            catch (Exception ex) 
            {
                var msg = ex.Message; 
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        public static string AppTheme
        {
            get; set;
        }
    }
}
