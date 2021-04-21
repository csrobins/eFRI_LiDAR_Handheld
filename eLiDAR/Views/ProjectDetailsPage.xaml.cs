using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class ProjectDetailsPage : ContentPage {
        
        public ProjectDetailsPage(string projectID) {
            InitializeComponent();
            this.BindingContext = new ProjectDetailsViewModel(Navigation, projectID);
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((ProjectDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((ProjectDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }

    }
}
