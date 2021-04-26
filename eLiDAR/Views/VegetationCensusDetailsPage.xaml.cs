using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class VegetationCensusDetailsPage : ContentPage {

        public VegetationCensusDetailsPage(string vegetationID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new VegetationCensusDetailsViewModel(Navigation, vegetationID);
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
            ((VegetationCensusDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((VegetationCensusDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    }
}
