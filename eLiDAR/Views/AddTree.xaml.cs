using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eLiDAR.Views
{
    public partial class AddTree : ContentPage {
        public AddTree() {
            InitializeComponent();
            BindingContext = new AddTreeViewModel(Navigation);
        }
        public AddTree(string fk)
        {
            try
            {
                InitializeComponent();
                BindingContext = new AddTreeViewModel(Navigation, fk);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
              //  Log.Fatal(e);
            };
        }
    }
}
