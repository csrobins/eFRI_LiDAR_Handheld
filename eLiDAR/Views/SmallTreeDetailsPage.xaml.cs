using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class SmallTreeDetailsPage : ContentPage {

        public SmallTreeDetailsPage(string smallTreeID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new SmallTreeDetailsViewModel(Navigation, smallTreeID);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
    }
}
