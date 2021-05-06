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
using Xamarin.Forms.Internals;
using eLiDAR.Utilities;

namespace eLiDAR.ViewModels {
    public class AddTreeViewModel : BaseTreeViewModel {

        private Utils util = new Utils(); 
        public ICommand AddCommand { get; private set; }
        public ICommand ViewAllCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }
        public ICommand StemMapCommand { get; private set; }
        public ICommand DeformityCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }

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
        private bool _AllowtoLeave = false;
        public AddTreeViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            //_treeValidator = new TreeValidator();
            _tree = new TREE();
            _treeRepository = new TreeRepository();
            _fk = fk;
            _tree.PLOTID = fk;
            AddCommand = new Command(async () => await AddTree(_fk));
            ViewAllCommand = new Command(async () => await ShowList());
            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.NAME).ToList();
            ListVigour = PickerService.VigourItems().ToList();
            ListCrownDamage = PickerService.CrownDamageItems().ToList();
            ListDefoliatingInsect = PickerService.DefoliatingInsectItems().ToList();
            ListFoliarDisease = PickerService.FoliarDiseaseItems().ToList();
            ListBarkRetention = PickerService.BarkRetentionItems().ToList();
            ListWoodCondition = PickerService.WoodConditionItems().ToList();
            ListMortalityCause = PickerService.MortalityCauseItems().ToList();
            ListDecayClass = PickerService.DecayClassItems().ToList();
            ListCrownPosition = PickerService.CrownPositionItems().ToList();
            CommentsCommand = new Command(async () => await ShowComments());
            StemMapCommand = new Command(async () => await ShowStemMap());
            DeformityCommand = new Command(async () => await ShowDeformity());
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());

            // DoDefaults
            _tree.HEIGHTTODBH = 1.3F;
            _tree.DBHIN  = "Y";
            _tree.CROWNIN = "Y";
            _tree.TREESTATUSCODE = "L";
            if (util.AllowAutoNumber) { _tree.TREENUMBER = _treeRepository.GetNextNumber(fk); }

        }
        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            _AllowtoLeave = true;
            await _navigation.PushAsync(new TreeComments(_tree));
        }
        async Task ShowStemMap()
        {
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowtoLeave = true;
                await _navigation.PushAsync(new StemMapDetailsPage(_tree.TREEID));
            }
          
        }
        private async Task<bool> TrySave()
        {
            TreeValidator _validator = new TreeValidator();
            ValidationResult validationResults = _validator.Validate(_tree);
            if (validationResults.IsValid)
            {
                _ = AddTree(_fk);
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
                _AllowtoLeave = true;
                await _navigation.PushAsync(new DeformityList(_tree.TREEID));
            }
            
        }

        // These are for the picker item sources that present a an item different than the code
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
            get {
                _selectedSpecies = PickerService.GetItem(ListSpecies, _tree.SPECIESCODE);
                return _selectedSpecies;
            }
            set {
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
                _tree.DECAYCLASS  = (int)_selectedDecayClass.ID;
            }
        }


        async Task ShowList(){ 
            await _navigation.PushAsync(new TreeListPage(_fk));
        }
        private Task AddTree(string _fk)
        {
            if (_tree.TREEID == null)
            {
                _tree.Created = System.DateTime.UtcNow;
                _tree.IsDeleted = "N";
                _tree.LastModified = _tree.Created;
                _treeRepository.InsertTree(_tree, _fk);
            }
            else
            {
                _tree.LastModified = System.DateTime.UtcNow;
                _treeRepository.UpdateTree(_tree);
            }
            return Task.CompletedTask;
        }
        public bool IsViewAll => _treeRepository.GetAllData().Count > 0 ? true : false;
        private void OnAppearing()
        {
            Shell.Current.Navigating += Current_Navigating;
        }
        private void OnDisappearing()
        {
            _AllowtoLeave = false;
            Shell.Current.Navigating -= Current_Navigating;
        }
        private async void Current_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            if (e.CanCancel)
            {
                if (!_AllowtoLeave)
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
                ValidationResult validationResults = _validator.Validate(_tree);
                if (validationResults.IsValid)
                {
                    _ = AddTree(_fk);
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
          //      await Shell.Current.GoToAsync("..", true);
                await _navigation.PopAsync(true);
            }
        }
    }

}
