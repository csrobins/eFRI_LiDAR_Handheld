using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class ProjectDetailsPage : ContentPage {
        
        public ProjectDetailsPage(string projectID) {
            InitializeComponent();
            this.BindingContext = new ProjectDetailsViewModel(Navigation, projectID);
        }
    }
}
