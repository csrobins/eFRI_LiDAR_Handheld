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
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            _viewmodel = new SettingsViewModel(Navigation);
            this.BindingContext = _viewmodel;
        }
    }
}