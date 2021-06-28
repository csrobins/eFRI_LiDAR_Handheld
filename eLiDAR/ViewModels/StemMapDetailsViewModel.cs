using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Services;
using eLiDAR.Validator;
using eLiDAR.Views;
using FluentValidation;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class StemMapDetailsViewModel: BaseStemMapViewModel {

        public ICommand UpdateTreeCommand { get; private set; }
        public ICommand DeleteTreeCommand { get; private set; }
      //  public ICommand CommentsCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public StemMapDetailsViewModel(INavigation navigation, string selectedTreeID) {
            _navigation = navigation;
            _stemmap = new STEMMAP();
            _stemmap.TREEID = selectedTreeID;
            _stemMapRepository = new StemMapRepository();
            _fk = selectedTreeID;
            UpdateTreeCommand = new Command(async () => await UpdateTree());
            DeleteTreeCommand = new Command(async () => await DeleteTree());
       //     CommentsCommand = new Command(async () => await ShowComments());
            // Get the tree if it exists
            if (_stemMapRepository.IsStemMapExists(_fk))
            {
                FetchTreeDetails(_fk);
            }
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }


        void FetchTreeDetails(string fk){
            _stemmap = _stemMapRepository.GetTreeData(fk);
        }
       

        //async Task ShowComments()
        //{
        //    // launch the form - filtered to a specific tree
        //    _AllowToLeave = true;
        //    await _navigation.PushAsync(new PlotCrew(_plotid));
        //}

        private Task UpdateTree() {

            try
            {
                if (_stemMapRepository.IsStemMapExists(_fk))
                {
                    _stemmap.LastModified = System.DateTime.UtcNow;
                    _stemMapRepository.UpdateTree(_stemmap);
                }
                else
                {
                    _stemmap.Created = System.DateTime.UtcNow;
                    _stemmap.LastModified = _stemmap.Created;
                    _stemmap.IsDeleted = "N";
                    _stemMapRepository.InsertTree(_stemmap, _fk);
                }
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                var myerror = e.Message;
                return Task.CompletedTask;
            }
        }
        async Task DeleteTree() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Stem Map Details", "Delete Stem Map Details", "OK", "Cancel");
            if (isUserAccept) {
                _stemMapRepository.DeleteTree(_stemmap);
                await _navigation.PopAsync();
            }
        }
        public bool DoCrownWidth
        {
            get =>  _stemMapRepository.IsRequiresCrownWidth(_stemmap.TREEID);
            set
            {
            }
        }
        public string Title
        {
            get => "Stem Map details for tree " + _stemMapRepository.GetTitle(_fk);
            set
            {
            }
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
                StemMapValidator _validator = new StemMapValidator();
                ValidationResult validationResults = _validator.Validate(_stemmap);
                if (validationResults.IsValid)
                {
                    _ = UpdateTree();
                    Shell.Current.Navigating -= Current_Navigating;
               //     await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Tree", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
         //       await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}