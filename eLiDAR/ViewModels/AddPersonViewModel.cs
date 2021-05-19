using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Validator;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;


namespace eLiDAR.ViewModels {
    public class AddPersonViewModel : BasePersonViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand ViewAllCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
       
        public AddPersonViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _personValidator = new PersonValidator();
            _person = new PERSON();
            _personRepository = new PersonRepository();
            _selectedprojectid = fk;
            _person.PROJECTID = fk;
            AddCommand = new Command(() => AddPerson(_selectedprojectid));
            ViewAllCommand = new Command(async () => await ShowList());
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }
        private void OnAppearing()
        {
            Shell.Current.Navigating += Current_Navigating;
        }
        private void OnDisappearing()
        {
            Shell.Current.Navigating -= Current_Navigating;
        }
        private async void Current_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            if (e.CanCancel)
            {
                e.Cancel();
                await GoBack();
            }
        }
        private async Task GoBack()
        {
            // display Alert for confirmation
            if (IsChanged)
            {
                PersonValidator _personValidator = new PersonValidator();
                ValidationResult validationResults = _personValidator.Validate(_person);
                if (validationResults.IsValid)
                {
                    AddPerson(_selectedprojectid );
                    Shell.Current.Navigating -= Current_Navigating;
               //     await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Person", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
           //     await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
        async Task ShowList(){ 
            await _navigation.PushAsync(new PersonList(_selectedprojectid ));
        }
        private void AddPerson(string fk)
        {
                _person.Created = System.DateTime.UtcNow;
                _person.LastModified = _person.Created;
                _person.IsDeleted = "N";
                _personRepository.InsertPerson(_person);
                    
      //              await _navigation.PopAsync(); 
                 //   await _navigation.PushAsync(new PlotList(fk));
   
        }
        public bool IsViewAll => _personRepository.GetAllPersonData().Count > 0 ? true : false;
    }
}
