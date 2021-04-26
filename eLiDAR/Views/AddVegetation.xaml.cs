using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eLiDAR.Views
{
    public partial class AddVegetation : ContentPage {
        public AddVegetation(string fk)
        {
            try
            {
                InitializeComponent();
                BindingContext = new AddVegetationViewModel(Navigation, fk);
                NavigationPage.SetHasNavigationBar(this, false);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((AddVegetationViewModel)this.BindingContext).OnAppearingCommand.Execute(null);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((AddVegetationViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    
    }
}
