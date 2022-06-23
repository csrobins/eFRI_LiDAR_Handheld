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
    public class TreeDetailsViewModel : BaseTreeViewModel {

        public ICommand UpdateTreeCommand { get; private set; }
        public ICommand DeleteTreeCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand StemMapCommand { get; private set; }
        public ICommand DeformityCommand { get; private set; }
        public ICommand AgeCommand { get; private set; }
        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItems> ListVigour { get; set; }
        public List<PickerItems> ListCrownDamage { get; set; }
        public List<PickerItems> ListDefoliatingInsect { get; set; }
        public List<PickerItems> ListFoliarDisease { get; set; }
        public List<PickerItems> ListBarkRetention { get; set; }
        public List<PickerItems> ListWoodCondition { get; set; }
        public List<PickerItems> ListMortalityCause { get; set; }
        public List<PickerItems> ListDecayClass { get; set; }
        public List<PickerItems> ListCrownPosition { get; set; }
        public List<PickerItemsString> ListCrownClass { get; set; }
        public List<PickerItemsString> ListStemQuality { get; set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        private bool _AllowToLeave = false;
        public TreeDetailsViewModel(INavigation navigation, string selectedTreeID) {
            _navigation = navigation;
            _tree = new TREE();
            _tree.TREEID = selectedTreeID;
            _treeRepository = new TreeRepository();

            UpdateTreeCommand = new Command(async () => await UpdateTree());
            DeleteTreeCommand = new Command(async () => await DeleteTree());
            CommentsCommand = new Command(async () => await ShowComments());
            ResetCommand = new Command(async () => await ResetValues());
            StemMapCommand = new Command(async () => await ShowStemMap());
            DeformityCommand = new Command(async () => await ShowDeformity());
            AgeCommand = new Command(async () => await ShowAge());
            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.ID).ToList();
            ListVigour = PickerService.VigourItems().OrderBy(c => c.NAME).ToList();
            ListCrownDamage = PickerService.CrownDamageItems().OrderBy(c => c.NAME).ToList();
            ListDefoliatingInsect = PickerService.DefoliatingInsectItems().OrderBy(c => c.NAME).ToList();
            ListFoliarDisease = PickerService.FoliarDiseaseItems().OrderBy(c => c.NAME).ToList();
            ListBarkRetention = PickerService.BarkRetentionItems().OrderBy(c => c.NAME).ToList();
            ListWoodCondition = PickerService.WoodConditionItems().OrderBy(c => c.NAME).ToList();
            ListMortalityCause = PickerService.MortalityCauseItems().OrderBy(c => c.NAME).ToList();
            ListDecayClass = PickerService.DecayClassItems().OrderBy(c => c.NAME).ToList();
            ListCrownPosition = PickerService.CrownPositionItems().OrderBy(c => c.NAME).ToList();
            ListCrownClass = PickerService.CrownClassItems().OrderBy(c => c.NAME).ToList();
            ListStemQuality = PickerService.StemQualityItems().OrderBy(c => c.NAME).ToList();

            FetchTreeDetails();
            IsChanged = false;
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
        }

        async Task ResetValues()
        {
            // for resetting tree values
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree Details", "Do you want to reset tree values for this tree?", "OK", "Cancel");
            if (isUserAccept)
            {
                CROWN_CLASS = "";
                STEM_QUALITY = "";

                BROKEN_TOP = "";
                PickerItems reset = new PickerItems { ID = 0, NAME = "" };
                SelectedBarkRetention =reset;
                SelectedWoodCondition =reset;
                SelectedCrownDamage = reset;
                SelectedMortalityCause = reset;
                SelectedDecayClass = reset;
  

            }
        }
        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new TreeComments(_tree));
            IsChanged = true;
        }
        async Task ShowStemMap()
        {
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new StemMapDetailsPage(_tree.TREEID));
              //  IsChanged = true;
            }
        }
        async Task ShowAge()
        {
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new TreeAge(_tree));
                IsChanged = true;
            }

        }
        private async Task<bool> TrySave()
        {
            TreeValidator _validator = new TreeValidator();
            ValidationResult validationResults = _validator.Validate(_tree);
            if (validationResults.IsValid)
            {
                _ = UpdateTree();
                return true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Update Tree", validationResults.Errors[0].ErrorMessage, "Ok");
                return false;
            }
        }
        async Task ShowDeformity()
        {
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new DeformityList(_tree.TREEID));
            }
        }
        private PickerItemsString _selectedCrownClass = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCrownClass
        {
            get
            {
                _selectedCrownClass = PickerService.GetItem(ListCrownClass, _tree.CROWNCLASSCODE);
                return _selectedCrownClass;
            }
            set
            {
                SetProperty(ref _selectedCrownClass, value);
                _tree.CROWNCLASSCODE = _selectedCrownClass.ID;
            }
        }
        private PickerItemsString _selectedStemQuality = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedStemQuality
        {
            get
            {
                _selectedStemQuality = PickerService.GetItem(ListStemQuality, _tree.STEMQUALITYCODE);
                return _selectedStemQuality;
            }
            set
            {
                SetProperty(ref _selectedStemQuality, value);
                _tree.STEMQUALITYCODE = _selectedStemQuality.ID;

            }
        }
        private PickerItems _selectedCrownPosition = new PickerItems { ID = 0, NAME = "" };
        public PickerItems CrownPosition
        {
            get
            {
                _selectedCrownPosition = PickerService.GetItem(ListCrownPosition, _tree.CROWN_POSITION);
                return _selectedCrownPosition;
            }
            set
            {
                SetProperty(ref _selectedCrownPosition, value);
                _tree.CROWN_POSITION = (int)_selectedCrownPosition.ID;
            }
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
        // These are for the picker item sources that present a an item different than the code
        private PickerItems _selectedVigour = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedVigour
        {
            get
            {
                _selectedVigour = PickerService.GetItem(ListVigour, _tree.VIGOURCODE); ;
                return _selectedVigour;
            }
            set
            {
                SetProperty(ref _selectedVigour, value);
                _tree.VIGOURCODE = (int)_selectedVigour.ID;
            }
        }
        private PickerItems _selectedCrownDamage = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedCrownDamage
        {
            get
            {
                _selectedCrownDamage = PickerService.GetItem(ListCrownDamage, _tree.CROWNDAMAGECODE);
                return _selectedCrownDamage;
            }
            set
            {
                SetProperty(ref _selectedCrownDamage, value);
                _tree.CROWNDAMAGECODE = (int)_selectedCrownDamage.ID;
            }
        }
        private PickerItems _selectedDefoliatingInsect = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDefoliatingInsect
        {
            get
            {
                _selectedDefoliatingInsect = PickerService.GetItem(ListDefoliatingInsect, _tree.DEFOLIATING_INSECT);
                return _selectedDefoliatingInsect;
            }
            set
            {
                SetProperty(ref _selectedDefoliatingInsect, value);
                _tree.DEFOLIATING_INSECT = (int)_selectedDefoliatingInsect.ID;
            }
        }
        private PickerItems _selectedFoliarDisease = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedFoliarDisease
        {
            get
            {
                _selectedFoliarDisease = PickerService.GetItem(ListFoliarDisease, _tree.FOLIAR_DISEASE);
                return _selectedFoliarDisease;
            }
            set
            {
                SetProperty(ref _selectedFoliarDisease, value);
                _tree.FOLIAR_DISEASE = (int)_selectedFoliarDisease.ID;
            }
        }
        private PickerItems _selectedBarkRetention = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedBarkRetention
        {
            get
            {
                _selectedBarkRetention = PickerService.GetItem(ListBarkRetention, _tree.BARKRETENTIONCODE);
                return _selectedBarkRetention;
            }
            set
            {
                SetProperty(ref _selectedBarkRetention, value);
                _tree.BARKRETENTIONCODE = (int)_selectedBarkRetention.ID;
            }
        }
        private PickerItems _selectedWoodCondition = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedWoodCondition
        {
            get
            {
                _selectedWoodCondition = PickerService.GetItem(ListWoodCondition, _tree.WOODCONDITIONCODE);
                return _selectedWoodCondition;
            }
            set
            {
                SetProperty(ref _selectedWoodCondition, value);
                _tree.WOODCONDITIONCODE = (int)_selectedWoodCondition.ID;
            }
        }
        private PickerItems _selectedMortalityCause = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedMortalityCause
        {
            get
            {
                _selectedMortalityCause = PickerService.GetItem(ListMortalityCause, _tree.MORTALITYCAUSECODE);
                return _selectedMortalityCause;
            }
            set
            {
                SetProperty(ref _selectedMortalityCause, value);
                _tree.MORTALITYCAUSECODE = (int)_selectedMortalityCause.ID;
            }
        }
        private PickerItems _selectedDecayClass = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDecayClass
        {
            get
            {
                _selectedDecayClass = PickerService.GetItem(ListDecayClass, _tree.DECAYCLASS);
                return _selectedDecayClass;
            }
            set
            {
                SetProperty(ref _selectedDecayClass, value);
                _tree.DECAYCLASS = (int)_selectedDecayClass.ID;
                if (_tree.DECAYCLASS < 4 && IsNotLiveTree) { IsDecayClass1to3 = true; } else { IsDecayClass1to3 = false; }
            }
        }


        void FetchTreeDetails(){
            _tree = _treeRepository.GetTreeData(_tree.TREEID);
        }

        private Task UpdateTree() {
           
            try
            {
                _tree.LastModified = System.DateTime.UtcNow;
                _treeRepository.UpdateTree(_tree);
                NotifyPropertyChanged("TreeListFull");
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                return Task.CompletedTask;                         //  Log.Fatal(e);
            };

        }

        async Task DeleteTree() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree Details", "Delete Tree Details", "OK", "Cancel");
            if (isUserAccept) {
                if (_treeRepository.AllowDelete(_tree))
                {
                    _treeRepository.DeleteTree(_tree);
                    _AllowToLeave = true;
                    await _navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Cannot delete tree", "This tree has related stem map or deformity data.  Delete those records first before deleting the tree.", "OK", "Cancel");
                }
            }
        }
        public string Title
        {
            get => "Tree Details for tree " + _tree.TREENUMBER;
            set
            {
            }
        }
        private void OnAppearing()
        {
            _AllowToLeave = false;
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
                if (!_AllowToLeave)
                {
                    e.Cancel();
                    await GoBack();
                }
            }
        }

        private async Task GoBack()
        {
            // display Alert for confirmation
            if (IsChanged)
            {
                TreeValidator _validator = new TreeValidator();
                TreeValidator _fullvalidator = new TreeValidator(true);

                ValidationResult validationResults = _validator.Validate(_tree);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_tree);

                ParseValidater _parser = new ParseValidater();
                (_tree.ERRORCOUNT, _tree.ERRORMSG) = _parser.Parse(fullvalidationResults);
                if (validationResults.IsValid)
                {
                    _ = UpdateTree();
                    Shell.Current.Navigating -= Current_Navigating;
             //       await Shell.Current.GoToAsync("..", true);
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
           //     await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }
}