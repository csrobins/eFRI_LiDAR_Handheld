using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class StemMapDetailsPage : ContentPage {

        public StemMapDetailsPage(string treeID) {
            InitializeComponent();
            this.BindingContext = new StemMapDetailsViewModel(Navigation,treeID);
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((StemMapDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((StemMapDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            
        }

    }
}
