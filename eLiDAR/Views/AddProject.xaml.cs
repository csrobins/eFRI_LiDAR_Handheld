using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views
{
    public partial class AddProject : ContentPage {
        public AddProject() {
            InitializeComponent();
            BindingContext = new AddProjectViewModel(Navigation);
        }
    }
}
