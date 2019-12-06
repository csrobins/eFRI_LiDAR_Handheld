using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Services;
using eLiDAR.Validator;
using FluentValidation;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class TreeDetailsViewModel: BaseTreeViewModel {

        public ICommand UpdateTreeCommand { get; private set; }
        public ICommand DeleteTreeCommand { get; private set; }
        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItems> ListVigour { get; set; }
        public List<PickerItems> ListCrownDamage { get; set; }
        public List<PickerItems> ListDefoliatingInsect { get; set; }
        public List<PickerItems> ListFoliarDisease { get; set; }
        public List<PickerItems> ListBarkRetention { get; set; }
        public List<PickerItems> ListWoodCondition { get; set; }
        public List<PickerItems> ListMortalityCause { get; set; }
        public List<PickerItems> ListDecayClass { get; set; }
        public TreeDetailsViewModel(INavigation navigation, string selectedTreeID) {
            _navigation = navigation;
            _tree = new TREE();
            _tree.TREEID = selectedTreeID;
            _treeRepository = new TreeRepository();

            UpdateTreeCommand = new Command(async () => await UpdateTree());
            DeleteTreeCommand = new Command(async () => await DeleteTree());
            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.NAME).ToList();
            ListVigour = PickerService.VigourItems().ToList();
            ListCrownDamage = PickerService.CrownDamageItems().ToList();
            ListDefoliatingInsect = PickerService.DefoliatingInsectItems().ToList();
            ListFoliarDisease = PickerService.FoliarDiseaseItems().ToList();
            ListBarkRetention = PickerService.BarkRetentionItems().ToList();
            ListWoodCondition = PickerService.WoodConditionItems().ToList();
            ListMortalityCause = PickerService.MortalityCauseItems().ToList();
            ListDecayClass = PickerService.DecayClassItems().ToList();
            FetchTreeDetails();
        }

        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                //_selectedSpecies.ID = PickerService.GetIndex(ListSpecies, _tree.SPECIES);
                //_selectedSpecies.NAME = PickerService.GetValue(ListSpecies, _tree.SPECIES);
                _selectedSpecies = PickerService.GetItem(ListSpecies, _tree.SPECIES);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _tree.SPECIES = (int)_selectedSpecies.ID;
            }
        }
        // These are for the picker item sources that present a an item different than the code
        private PickerItems _selectedVigour = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedVigour
        {
            get
            {
                _selectedVigour = PickerService.GetItem(ListVigour, _tree.VIGOUR); ;
                return _selectedVigour;
            }
            set
            {
                SetProperty(ref _selectedVigour, value);
                _tree.VIGOUR = (int)_selectedVigour.ID;
            }
        }
        private PickerItems _selectedCrownDamage = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedCrownDamage
        {
            get
            {
                _selectedCrownDamage = PickerService.GetItem(ListCrownDamage, _tree.CROWN_DAMAGE);
                return _selectedCrownDamage;
            }
            set
            {
                SetProperty(ref _selectedCrownDamage, value);
                _tree.CROWN_DAMAGE = (int)_selectedCrownDamage.ID;
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
                _selectedBarkRetention = PickerService.GetItem(ListBarkRetention, _tree.BARK_RETENTION);
                return _selectedBarkRetention;
            }
            set
            {
                SetProperty(ref _selectedBarkRetention, value);
                _tree.BARK_RETENTION = (int)_selectedBarkRetention.ID;
            }
        }
        private PickerItems _selectedWoodCondition = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedWoodCondition
        {
            get
            {
                _selectedWoodCondition = PickerService.GetItem(ListWoodCondition, _tree.WOOD_CONDITION);
                return _selectedWoodCondition;
            }
            set
            {
                SetProperty(ref _selectedWoodCondition, value);
                _tree.WOOD_CONDITION = (int)_selectedWoodCondition.ID;
            }
        }
        private PickerItems _selectedMortalityCause = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedMortalityCause
        {
            get
            {
                _selectedMortalityCause = PickerService.GetItem(ListMortalityCause, _tree.MORT_CAUSE);
                return _selectedMortalityCause;
            }
            set
            {
                SetProperty(ref _selectedMortalityCause, value);
                _tree.MORT_CAUSE = (int)_selectedMortalityCause.ID;
            }
        }
        private PickerItems _selectedDecayClass = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDecayClass
        {
            get
            {
                _selectedDecayClass = PickerService.GetItem(ListDecayClass, _tree.DECAY_CLASS);
                return _selectedDecayClass;
            }
            set
            {
                SetProperty(ref _selectedDecayClass, value);
                _tree.DECAY_CLASS = (int)_selectedDecayClass.ID;
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
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Tree Details", "Update Tree Details", "OK", "Cancel");
                    if (isUserAccept)
                    {
                        _treeRepository.UpdateTree(_tree);
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
            get => "Tree Details for tree " + _tree.TREENUM;
            set
            {
            }
        }
    }
}