using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eLiDAR.Views
{
    public partial class AddDWD : ContentPage {
        public AddDWD(string fk)
        {
            try
            {
                InitializeComponent();
                BindingContext = new AddDWDViewModel(Navigation, fk);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
              //  Log.Fatal(e);
            };
        }
    }
}
