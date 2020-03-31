using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eLiDAR.Views
{
    public partial class AddDeformity : ContentPage {
       
        public AddDeformity(string fk)
        {
            try
            {
                InitializeComponent();
                BindingContext = new AddDeformityViewModel(Navigation, fk);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
              //  Log.Fatal(e);
            };
        }
    }
}
