using eLiDAR.ViewModels;
using eLiDAR.Views;
using System;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PhotoDetailsPage : ContentPage {

        public PhotoDetailsPage(string ID) {
            try
            {
                InitializeComponent();
                this.BindingContext = new PhotoDetailsViewModel(Navigation, ID);
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
            ((PhotoDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((PhotoDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    }
}
