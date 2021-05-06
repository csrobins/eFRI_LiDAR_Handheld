using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Utilities;
using eLiDAR.Validator;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;


namespace eLiDAR.ViewModels {
    public class PersonDetailsViewModel : BasePersonViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
   //     public ICommand ViewAllCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public PersonDetailsViewModel(INavigation navigation, string id)
        {
            _navigation = navigation;
            _personValidator = new PersonValidator();
            _person = new PERSON();
            _personRepository = new PersonRepository();
            _selectedpersonid = id;
            AddCommand = new Command(async () => await UpdatePerson());
            DeleteCommand = new Command(async () => await DeletePerson());
      //      ViewAllCommand = new Command(async () => await ShowList());
            FetchDetails();
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }

        //async Task ShowList(){ 
        //    await _navigation.PushAsync(new PersonList());
        //}

        void FetchDetails()
        {
            _person = _personRepository.GetPersonData(_selectedpersonid );
        }
        async Task DeletePerson()
        {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Person Details", "Delete Person", "OK", "Cancel");
            if (isUserAccept)
            {
                _personRepository.DeletePerson(_person);
                await _navigation.PopAsync();
            }
        }
        private Task UpdatePerson()
        {
            _person.LastModified = System.DateTime.UtcNow;
            _personRepository.UpdatePerson(_person);
            return Task.CompletedTask;
        }

        public bool IsViewAll => _personRepository.GetAllPersonData().Count > 0 ? true : false;
        public string Title
        {
            get => "Details for " + LASTNAME + ", " + FIRSTNAME;
        }
        private void OnAppearing()
        {
            Shell.Current.Navigating += Current_Navigating;
        }
        private void OnDisappearing()
        {
            Shell.Current.Navigating -= Current_Navigating;
        }
        private async void Current_Navigating(object sender,  ShellNavigatingEventArgs e)
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
                PersonValidator _validator = new PersonValidator();
                ValidationResult validationResults = _validator.Validate(_person);
                if (validationResults.IsValid)
                {
                    _ = UpdatePerson();
                    Shell.Current.Navigating -= Current_Navigating;
              //      await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Person", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
           //     await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}
