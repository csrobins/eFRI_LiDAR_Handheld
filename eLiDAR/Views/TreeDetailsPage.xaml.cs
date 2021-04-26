using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class TreeDetailsPage : ContentPage {

        public TreeDetailsPage(string treeID) {
            InitializeComponent();
            this.BindingContext = new TreeDetailsViewModel(Navigation,treeID);
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((TreeDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((TreeDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
    }
}
