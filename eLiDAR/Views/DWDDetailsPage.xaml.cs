using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class DWDDetailsPage : ContentPage {

        public DWDDetailsPage(string soilID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new DWDDetailsViewModel(Navigation, soilID,false );
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
        }
    }
}
