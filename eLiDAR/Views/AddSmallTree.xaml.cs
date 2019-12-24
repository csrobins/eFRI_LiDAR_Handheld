using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eLiDAR.Views
{
    public partial class AddSmallTree : ContentPage {
        public AddSmallTree(string fk)
        {
            try
            {
                InitializeComponent();
                BindingContext = new AddSmallTreeViewModel(Navigation, fk);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
              //  Log.Fatal(e);
            };
        }
    }
}
