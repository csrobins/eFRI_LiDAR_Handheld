using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class SoilDetailsPage : ContentPage {

        public SoilDetailsPage(string soilID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new SoilDetailsViewModel(Navigation, soilID);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
    }
}
