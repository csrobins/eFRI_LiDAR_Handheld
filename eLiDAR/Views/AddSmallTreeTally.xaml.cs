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
    public partial class AddSmallTreeTally : ContentPage
    {
        public AddSmallTreeTally(string fk)
        {
            try
            {
                InitializeComponent();
                this.BindingContext = new AddSmallTreeTallyViewModel(Navigation, fk);
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);                
                //var page = new ErrorPage();
                //page.BindingContext = ex;
                //this.Navigation.PushAsync(page);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((AddSmallTreeTallyViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((AddSmallTreeTallyViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);

        }


    }
}

