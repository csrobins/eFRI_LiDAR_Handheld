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
    public class EcositeDetailsViewModel: BaseEcositeViewModel {

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }
        public ICommand EcositeCommand { get; private set; }
        public ICommand PhotoCommand { get; private set; }
        public ICommand SoilCommand { get; private set; }
        public ICommand TextureCommand { get; private set; }
        public Command OnAppearingCommand { get; set; }
        public Command OnDisappearingCommand { get; set; }
        public ICommand ImageCommand { get; private set; }
        public List<PickerItems> ListDrainage { get; set; }
        public List<PickerItemsString> ListPorePattern { get; set; }
        public List<PickerItemsString> ListMoistureRegime { get; set; }
        public List<PickerItemsString> ListDepthClass { get; set; }
        public List<PickerItemsString> ListDeposition { get; set; }
        public List<PickerItemsString> ListPerson { get; set; }


        public List<PickerItems> ListHumusForm { get; set; }
        private bool _AllowToLeave = false;
        public EcositeDetailsViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _ecosite = new ECOSITE();
            _ecosite.PLOTID  = selectedID;
            _ecositeRepository = new EcositeRepository();
            _fk = selectedID;
            UpdateCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            CommentsCommand = new Command(async () => await ShowComments());
            EcositeCommand = new Command(async () => await ShowEcosite());
            PhotoCommand = new Command(async () => await ShowPhoto());
            SoilCommand = new Command(async () => await ShowSoil());
            TextureCommand = new Command(async () => await ShowTexture());
            ListDrainage = PickerService.DrainageItems().ToList();
            ListPorePattern = PickerService.PorePatternItems().ToList();
            ListMoistureRegime = PickerService.MoistureRegimeItems().ToList();
            ListHumusForm = PickerService.HumusFormItems().ToList();
            ListDepthClass = PickerService.DepthClassItems().ToList();
            ListDeposition = PickerService.DepositionItems().ToList();
            ListPerson = PickerService.FillPersonPicker(_ecositeRepository.GetPersonList()).OrderBy(c => c.NAME).ToList();
            ImageCommand = new Command(async () => await ShowImage());
            OnAppearingCommand = new Command(() => OnAppearing());
            OnDisappearingCommand = new Command(() => OnDisappearing());
            // Get the ecosite if it exists
            if (_ecositeRepository.IsEcositeExists(_fk))
            {
                FetchDetails(_fk);
                
            }
            else {
                _ecosite.SUBSTRATEDATE = System.DateTime.Now;
                _ecosite.DEPTHTOCARBONATES = 999;
                _ecosite.DEPTHTOBEDROCK = 999;
                _ecosite.DEPTHTODISTINCTMOTTLES = 999;
                _ecosite.DEPTHTOGLEY = 999;
                _ecosite.DEPTHTOIMPASSABLECOARSEFRAGMENTS = 999;
                _ecosite.DEPTHTOPROMINENTMOTTLES = 999;
                _ecosite.DEPTHTOROOTRESTRICTION  = 999;
                _ecosite.DEPTHTOSEEPAGE = 999;
                _ecosite.DEPTHTOWATERTABLE = 999;
                _ecosite.STRATIFIED = "N";
                _ecosite.PROBLEMATICSITE = "N";
                _ecosite.PRI_ECO_PCT = 100;

            }
            Refresh();
        }
        async Task ShowPhoto()
        {
            // launch the form - filtered to a specific photo list
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new PhotoList(_ecosite.PLOTID)); 
            }       
        }

        async Task ShowSoil()
        {
            // launch the form - filtered to a specific photo list
            bool _issaved = await TrySave();
            if (_issaved)
            {
                _AllowToLeave = true;
                await _navigation.PushAsync(new SoilList(_ecosite.PLOTID));
            }
        }
        async Task ShowImage()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new ImagePage());
        }
        async Task ShowEcosite()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new EcositeCode(_ecosite));
            IsChanged = true;
        }
        async Task ShowTexture()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new Texture(_ecosite));
            IsChanged = true;
        }

        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            _AllowToLeave = true;
            await _navigation.PushAsync(new EcositeComments(_ecosite));
            IsChanged=true;
        }

        private PickerItemsString _selectedSubstratePerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedSubstratePerson
        {
            get
            {
                _selectedSubstratePerson = PickerService.GetItem(ListPerson, _ecosite.SUBSTRATEPERSON);
                return _selectedSubstratePerson;
            }
            set
            {
                SetProperty(ref _selectedSubstratePerson, value);
                _ecosite.SUBSTRATEPERSON = _selectedSubstratePerson.ID;
            }
        }
        private PickerItems _selectedDrainage = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDrainage
        {
            get
            {
                _selectedDrainage = PickerService.GetItem(ListDrainage, _ecosite.DRAINAGECLASSCODE);
                return _selectedDrainage;
            }
            set
            {
                SetProperty(ref _selectedDrainage, value);
                _ecosite.DRAINAGECLASSCODE  = (int)_selectedDrainage.ID;
            }
        }
        private PickerItemsString _selectedDepthClass = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedDepthClass
        {
            get
            {
                _selectedDepthClass = PickerService.GetItem(ListDepthClass, _ecosite.MOISTURE_REGIME_DEPTH_CLASS);
                return _selectedDepthClass;
            }
            set
            {
                SetProperty(ref _selectedDepthClass, value);
                _ecosite.MOISTURE_REGIME_DEPTH_CLASS  = _selectedDepthClass.ID;
            }
        }
        private PickerItemsString _selectedDeposition1 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedDeposition1
        {
            get
            {
                _selectedDeposition1 = PickerService.GetItem(ListDeposition, _ecosite.MODEOFDEPOSITIONCODE1);
                return _selectedDeposition1;
            }
            set
            {
                SetProperty(ref _selectedDeposition1, value);
                _ecosite.MODEOFDEPOSITIONCODE1 = _selectedDeposition1.ID;
            }
        }
        private PickerItemsString _selectedDeposition2 = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedDeposition2
        {
            get
            {
                _selectedDeposition2 = PickerService.GetItem(ListDeposition, _ecosite.MODEOFDEPOSITIONCODE2);
                return _selectedDeposition2;
            }
            set
            {
                SetProperty(ref _selectedDeposition2, value);
                _ecosite.MODEOFDEPOSITIONCODE2  = _selectedDeposition2.ID;
            }
        }
        private PickerItemsString _selectedPorePattern = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedPorePattern
        {
            get
            {
                _selectedPorePattern = PickerService.GetItem(ListPorePattern, _ecosite.EFFECTIVE_PORE_PATTERN);
                return _selectedPorePattern;
            }
            set
            {
                SetProperty(ref _selectedPorePattern, value);
                _ecosite.EFFECTIVE_PORE_PATTERN  = _selectedPorePattern.ID;
            }
        }

        private PickerItemsString _selectedMoistureRegime = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMoistureRegime
        {
            get
            {
                _selectedMoistureRegime = PickerService.GetItem(ListMoistureRegime, _ecosite.MOISTUREREGIMECODE);
                return _selectedMoistureRegime;
            }
            set
            {
                SetProperty(ref _selectedMoistureRegime, value);
                _ecosite.MOISTUREREGIMECODE  = _selectedMoistureRegime.ID;
            }
        }

        private PickerItems _selectedHumusForm = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedHumusForm
        {
            get
            {
                _selectedHumusForm = PickerService.GetItem(ListHumusForm, _ecosite.HUMUSFORMCODE);
                return _selectedHumusForm;
            }
            set
            {
                SetProperty(ref _selectedHumusForm, value);
                _ecosite.HUMUSFORMCODE = (int)_selectedHumusForm.ID;
            }
        }

        void FetchDetails(string fk){
            _ecosite = _ecositeRepository.GetEcositeData(fk);
        }

        private Task Update() {
            try
            {
                if (_ecositeRepository.IsEcositeExists(_fk))
                {
                    _ecosite.LastModified = System.DateTime.UtcNow;
                    _ecositeRepository.UpdateEcosite(_ecosite);
                   
                }
                else
                {
                    _ecosite.IsDeleted = "N";        
                    _ecosite.Created = System.DateTime.UtcNow;
                    _ecosite.LastModified = _ecosite.Created;
                    _ecositeRepository.InsertEcosite(_ecosite, _fk);
                }
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
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Ecosite Details", "Delete Ecosite Details", "OK", "Cancel");
            if (isUserAccept) {
                _ecositeRepository.DeleteEcosite(_ecosite);
                _AllowToLeave = true;
                await _navigation.PopAsync();
            }
        }
        public string EcositeButton
        {
            get
         {
                if (PRI_ECO == null) { return "Ecosite"; }
                else { return PRI_ECO; }
            }
            set
            {
            }
        }
        public string TextureButton
        {
            get
            {
                if (MINERALTEXTURECODE == null) { return "Mineral Texture"; }
            
 
                else { return MINERALTEXTURECODE; }
                
            }
            set
            {
            }
        }
        public void Refresh()
        {
            NotifyPropertyChanged("EcositeButton");
            NotifyPropertyChanged("TextureButton");

        }
        public string Title
        {
            get => "Ecosite details for plot " + _ecositeRepository.GetTitle(_fk);
            set
            {
            }
        }
        private void OnAppearing()
        {
            Shell.Current.Navigating += Current_Navigating;
            Refresh(); 
        }
        private void OnDisappearing()
        {
            _AllowToLeave = false;
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

        private async Task<bool> TrySave()
        {
            EcositeValidator _validator = new EcositeValidator();
            ValidationResult validationResults = _validator.Validate(_ecosite);
            if (validationResults.IsValid)
            {
                _ = Update();
                return true;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Update Site", validationResults.Errors[0].ErrorMessage, "Ok");
                return false;
            }
        }
        private async Task GoBack()
        {
            // display Alert for confirmation
            if (IsChanged)
            {

                EcositeValidator _validator = new EcositeValidator();
                EcositeValidator _fullvalidator = new EcositeValidator(true);

                ValidationResult validationResults = _validator.Validate(_ecosite);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_ecosite);

                ParseValidater _parser = new ParseValidater();
                (_ecosite.ERRORCOUNT, _ecosite.ERRORMSG) = _parser.Parse(fullvalidationResults);


 //               EcositeValidator _validator = new EcositeValidator();
   //             ValidationResult validationResults = _validator.Validate(_ecosite);
                if (validationResults.IsValid)
                {
                    _ = Update();
                    Shell.Current.Navigating -= Current_Navigating;
             //       await Shell.Current.GoToAsync("..", true);
                    await _navigation.PopAsync(true);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Update Site", validationResults.Errors[0].ErrorMessage, "Ok");
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