using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class DWDDetailsPage : ContentPage {

        public DWDDetailsPage(string soilID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new DWDDetailsViewModel(Navigation, soilID);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
    }
}
