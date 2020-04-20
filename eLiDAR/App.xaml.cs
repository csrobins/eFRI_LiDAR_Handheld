using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eLiDAR.Views;
using System;
using eLiDAR.Domain.Global;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace eLiDAR
{
    public partial class App : Application
    {
       
        public App()
        {
            InitializeComponent();
            //   MainPage = new eLiDAR.AppShell();
            DependencyService.Register<AppModel>();

            MainPage = new MainShell();       
        }
       

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static string AppTheme
        {
            get; set;
        }

    }
}
