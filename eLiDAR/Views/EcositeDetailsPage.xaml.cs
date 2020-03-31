using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eLiDAR.Views
{
    public partial class EcositeDetailsPage : ContentPage {
       
        public EcositeDetailsPage(string fk)
        {
            try
            {
                InitializeComponent();
                BindingContext = new EcositeDetailsViewModel(Navigation, fk);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
              //  Log.Fatal(e);
            };
        }
    }
}
