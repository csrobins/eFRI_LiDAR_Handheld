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
            ((AddSmallTreeViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((AddSmallTreeViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    }
}
