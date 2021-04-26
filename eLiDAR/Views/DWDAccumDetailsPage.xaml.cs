using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class DWDAccumDetailsPage : ContentPage {

        public DWDAccumDetailsPage(string soilID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new DWDDetailsViewModel(Navigation, soilID, true);
                NavigationPage.SetHasNavigationBar(this, false);
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
            ((DWDDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((DWDDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }

    }
}
