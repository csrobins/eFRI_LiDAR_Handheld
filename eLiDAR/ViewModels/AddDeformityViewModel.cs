﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Validator;
using FluentValidation;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class AddDeformityViewModel: BaseDeformityViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItems> ListCause { get; set; }
        public List<PickerItems> ListType { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public AddDeformityViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _deformity = new DEFORMITY();
            _deformity.TREEID  = selectedID;
            _deformityRepository = new DeformityRepository();
            _fk = selectedID;
            AddCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            ListCause = PickerService.CauseItems().ToList();
            ListType = PickerService.TypeItems().OrderBy(x => x.ID).ToList();
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());

        }
        private bool _IsValidSingle;
        public bool IsValidSingle
        {
            get => _IsValidSingle;
            set
            {
                _IsValidSingle = value;
                OnPropertyChanged();
            }
        }
        void FetchDetails(string fk){
            _deformity = _deformityRepository.GetDeformityData(fk);
        }

        private PickerItems _selectedCause = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedCause
        {
            get
            {
                _selectedCause = PickerService.GetItem(ListCause, _deformity.CAUSE );
                return _selectedCause;
            }
            set
            {
                SetProperty(ref _selectedCause, value);
                _deformity.CAUSE  = (int)_selectedCause.ID;
            }
        }
        private PickerItems _selectedType = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedType
        {
            get
            {
                _selectedType = PickerService.GetItem(ListType, _deformity.DEFORMITYTYPECODE);
                return _selectedType;
            }
            set
            {
                SetProperty(ref _selectedType, value);
                _deformity.DEFORMITYTYPECODE = (int)_selectedType.ID;
            }
        }

        private Task Update() {
            try
            {

                _deformity.IsDeleted = "N";
                _deformity.Created = System.DateTime.UtcNow;
               _deformity.LastModified = _deformity.Created;
               _deformityRepository.InsertDeformity(_deformity,_fk);
                        //  This is just to slow down the database
               _deformityRepository.GetDeformityData(_deformity.DEFORMITYID);
                return Task.CompletedTask;

            }
            catch (Exception e)
            {
                var myerror = e.Message;
                return Task.CompletedTask;// error
                                          //  Log.Fatal(e);
            };
        }
        async Task Delete() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Deformity Details", "Delete Deformity Details", "OK", "Cancel");
            if (isUserAccept) {
                _deformityRepository.DeleteDeformity (_deformity);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Deformity details for plot " + _deformityRepository.GetTitle(_fk);
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
                DeformityValidator _validator = new DeformityValidator();
                ValidationResult validationResults = _validator.Validate(_deformity);
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
               //     await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Deformity", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
        //        await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}