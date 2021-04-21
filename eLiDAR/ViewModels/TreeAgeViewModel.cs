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

        async Task UpdateTree() {
            //var validationResults = _treeValidator.Validate(_tree);
            //TreeValidator _treeValidator = new TreeValidator();
            //ValidationResult validationResults = new ValidationResult();
            //validationResults =  _treeValidator.Validate(_tree);
            try
            {
                TreeValidator _treeValidator = new TreeValidator();
                ValidationResult treevalidationResults = _treeValidator.Validate(_tree);

                if (treevalidationResults.IsValid)
                {
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree Age Details", "Update Tree Age Details", "OK", "Cancel");
                    if (isUserAccept)
                    {
                        _tree.LastModified = System.DateTime.UtcNow;
                        _treeRepository.UpdateTree(_tree);
                        NotifyPropertyChanged("TreeListFull"); 
                        await _navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Tree", treevalidationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };

        }

        async Task DeleteTree() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree Details", "Delete Tree Details", "OK", "Cancel");
            if (isUserAccept) {
                _treeRepository.DeleteTree(_tree.TREEID);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Tree Details for tree " + _tree.TREENUMBER;
            set
            {
            }
        }
    }
}