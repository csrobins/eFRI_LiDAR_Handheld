using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class DeformityDetailsPage : ContentPage {

        public DeformityDetailsPage(string ID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new DeformityDetailsViewModel(Navigation, ID);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
    }
}
