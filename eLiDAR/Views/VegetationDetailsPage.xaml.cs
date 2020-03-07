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
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
    }
}
