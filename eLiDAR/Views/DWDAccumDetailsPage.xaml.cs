using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class DWDAccumDetailsPage : ContentPage {

        public DWDAccumDetailsPage(string soilID) {
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
