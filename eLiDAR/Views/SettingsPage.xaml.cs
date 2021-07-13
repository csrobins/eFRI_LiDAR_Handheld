using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLiDAR.Utilities;  
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using eLiDAR.ViewModels;
 

namespace eLiDAR.Views
{
     
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
          //  CheckSpecies();
        }
        private SettingsViewModel _viewmodel;
        protected override void OnAppearing()
        {
            try
            {
                _viewmodel = new SettingsViewModel(Navigation);
                this.BindingContext = _viewmodel;
            }
            catch (Exception ex) 
            { 
                var msg = ex.Message;
            }
            
        }
        
        void Handle_Clicked_2(object sender, System.EventArgs e)
        {
            Application.Current.Resources["CurrentTheme"] = Application.Current.Resources["BaseStyle"];
        }

        void Handle_Clicked_3(object sender, System.EventArgs e)
        {
            Application.Current.Resources["CurrentTheme"] = Application.Current.Resources["SecondaryShell"];
        }
    }
}