using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class DeformityDetailsPage : ContentPage {

        public DeformityDetailsPage(string ID)
        {
            try
            {
                InitializeComponent();
                this.BindingContext = new DeformityDetailsViewModel(Navigation, ID);
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
            ((DeformityDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((DeformityDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    
    }
}
