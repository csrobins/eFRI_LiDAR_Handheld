using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class VegetationDetailsPage : ContentPage {

        public VegetationDetailsPage(string vegetationID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new VegetationDetailsViewModel(Navigation, vegetationID);
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
            ((VegetationDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((VegetationDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
        }
    }
}
