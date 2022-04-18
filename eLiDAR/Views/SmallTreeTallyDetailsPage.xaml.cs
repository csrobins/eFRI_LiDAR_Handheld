using eLiDAR.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eLiDAR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmallTreeTallyDetailsPage : ContentPage
    {
        public SmallTreeTallyDetailsPage(string smallTallyTreeID)
        {
            try
            {
                InitializeComponent();
                this.BindingContext = new SmallTreeTallyDetailsViewModel(Navigation, smallTallyTreeID);
                NavigationPage.SetHasNavigationBar(this, false);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            }
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((SmallTreeTallyDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((SmallTreeTallyDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    }
}