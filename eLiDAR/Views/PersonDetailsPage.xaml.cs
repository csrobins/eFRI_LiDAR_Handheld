using eLiDAR.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eLiDAR.Views
{
    public partial class PersonDetailsPage : ContentPage
    {
        public PersonDetailsPage(string ID)
        {
            InitializeComponent();
            this.BindingContext = new PersonDetailsViewModel(Navigation, ID);
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            ((PersonDetailsViewModel)this.BindingContext).OnAppearingCommand.Execute(null);
  
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((PersonDetailsViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
            // execute OnDisappearingCommand        
            // informing ViewModel
        }
      

    }
}
