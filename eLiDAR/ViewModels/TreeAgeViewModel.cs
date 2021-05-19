using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Validator;
using eLiDAR.Views;
using FluentValidation;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class TreeAgeViewModel : BaseTreeViewModel {

        public ICommand UpdateTreeCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }

        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItemsString> ListCoreStatus { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        private bool _dosave = true;
        public TreeAgeViewModel(INavigation navigation, string selectedTreeID) {
            _navigation = navigation;
            _tree = new TREE();
            _tree.TREEID = selectedTreeID;
            _treeRepository = new TreeRepository();
           
            UpdateTreeCommand = new Command(async () => await UpdateTree());
            CommentsCommand = new Command(async () => await ShowComments());

            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.NAME).ToList();
            ListCoreStatus = PickerService.CoreStatusItems().OrderBy(c => c.NAME).ToList();
            FetchTreeDetails();
            //defaults
            if (_tree.HEIGHTTOCORE == 0) { _tree.HEIGHTTOCORE = 1.3F; }
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }
        public TreeAgeViewModel(INavigation navigation, TREE tree)
        {
            _navigation = navigation;
            _tree = tree;
//            _tree.TREEID = selectedTreeID;
            _treeRepository = new TreeRepository();
            _dosave = false;
            UpdateTreeCommand = new Command(async () => await UpdateTree());
            CommentsCommand = new Command(async () => await ShowComments());

            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.NAME).ToList();
            ListCoreStatus = PickerService.CoreStatusItems().OrderBy(c => c.NAME).ToList();
            //FetchTreeDetails();
            //defaults
            if (_tree.HEIGHTTOCORE == 0) { _tree.HEIGHTTOCORE = 1.3F; }
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }

        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new TreeComments(_tree));
        }
      

        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                //_selectedSpecies.ID = PickerService.GetIndex(ListSpecies, _tree.SPECIES);
                //_selectedSpecies.NAME = PickerService.GetValue(ListSpecies, _tree.SPECIES);
                _selectedSpecies = PickerService.GetItem(ListSpecies, _tree.SPECIESCODE);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _tree.SPECIESCODE = (int)_selectedSpecies.ID;
            }
        }
        private PickerItemsString _selectedCoreStatus = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCoreStatus
        {
            get
            {
                _selectedCoreStatus = PickerService.GetItem(ListCoreStatus, _tree.CORESTATUSCODE);
                return _selectedCoreStatus;
            }
            set
            {
                SetProperty(ref _selectedCoreStatus, value);
                _tree.CORESTATUSCODE = _selectedCoreStatus.ID;
            }
        }


        void FetchTreeDetails(){
            _tree = _treeRepository.GetTreeData(_tree.TREEID);
        }

        private Task UpdateTree() {
            //var validationResults = _treeValidator.Validate(_tree);
            //TreeValidator _treeValidator = new TreeValidator();
            //ValidationResult validationResults = new ValidationResult();
            //validationResults =  _treeValidator.Validate(_tree);
            try
            {
               
                 _tree.LastModified = System.DateTime.UtcNow;
               if (_dosave)
                {
                    _treeRepository.UpdateTree(_tree);
                }
                NotifyPropertyChanged("TreeListFull");
                return Task.CompletedTask;
                //     await _navigation.PopAsync();

            }
            catch (Exception e)
            {
                var myerror = e.Message;
                return Task.CompletedTask;// error
                                          //  Log.Fatal(e);
            };

        }

        public string Title
        {
            get => "Tree Age Details for tree " + _tree.TREENUMBER.ToString() + ", Species:" + _tree.SPECIESCODE.ToString() + ", DBH:" + _tree.DBH.ToString();
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
                TreeAgeValidator _validator = new TreeAgeValidator();
                ValidationResult validationResults = _validator.Validate(_tree);
                if (validationResults.IsValid)
                {
                    _ = UpdateTree();
                    Shell.Current.Navigating -= Current_Navigating;
          //          await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Tree Age", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            else
            {
                Shell.Current.Navigating -= Current_Navigating;
          //      await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}