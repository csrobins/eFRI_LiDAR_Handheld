using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class SoilDetailsPage : ContentPage {
        private SoilDetailsViewModel _viewmodel;
        public SoilDetailsPage(string soilID) {
            try
            {
                InitializeComponent();
                _viewmodel = new SoilDetailsViewModel(Navigation, soilID);
                BindingContext = _viewmodel;
                NavigationPage.SetHasNavigationBar(this, false);
             //  this.BindingContext = new SoilDetailsViewModel(Navigation, soilID);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((SoilDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);
            if (_viewmodel != null) { _viewmodel.Refresh(); }

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((SoilDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    }

}
