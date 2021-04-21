using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views
{
    public partial class AddPerson : ContentPage {
      
        public AddPerson(string fk)
        {
            InitializeComponent();
            BindingContext = new AddPersonViewModel(Navigation, fk);
            NavigationPage.SetHasNavigationBar(this, false);
        }
       
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((AddPersonViewModel)this.BindingContext).OnAppearingCommand.Execute(null);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ((AddPersonViewModel)this.BindingContext).OnDisappearingCommand.Execute(null);
       
        }
    }
}
