using eLiDAR.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace eLiDAR.Views
{
    public partial class AddSoil : ContentPage {
        private AddSoilViewModel _viewmodel;
        public AddSoil(string fk)
        {
            try
            {
                InitializeComponent();
                _viewmodel = new AddSoilViewModel(Navigation, fk);
                BindingContext =_viewmodel;
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
            if (_viewmodel != null) { _viewmodel.Refresh(); }
            ((AddSoilViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((AddSoilViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }

    }
    
}
