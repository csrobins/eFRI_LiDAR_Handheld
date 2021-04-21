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

        public List<PickerItems> ListDrainage { get; set; }
        public List<PickerItemsString> ListPorePattern { get; set; }
        public List<PickerItemsString> ListMoistureRegime { get; set; }
        public List<PickerItemsString> ListDepthClass { get; set; }
        public List<PickerItemsString> ListDeposition { get; set; }

        public List<PickerItems> ListHumusForm { get; set; }
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
            ListDrainage = PickerService.DrainageItems().ToList();
            ListPorePattern = PickerService.PorePatternItems().ToList();
            ListMoistureRegime = PickerService.MoistureRegimeItems().ToList();
            ListHumusForm = PickerService.HumusFormItems().ToList();
            ListDepthClass = PickerService.DepthClassItems().ToList();
            ListDeposition = PickerService.DepositionItems().ToList();

            // Get the ecosite if it exists
            if (_ecositeRepository.IsEcositeExists(_fk))
            {
                FetchDetails(_fk);
            }
        }
        async Task ShowPhoto()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new CameraPage(_ecosite));
        }
        async Task ShowEcosite()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new EcositeCode(_ecosite));
        }

        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new EcositeComments(_ecosite));
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

        async Task Update() {
            try
            {
                EcositeValidator _ecositeValidator = new EcositeValidator();
                ValidationResult validationResults = _ecositeValidator.Validate(_ecosite);

                if (validationResults.IsValid)
                {
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Ecosite Details", "Save Ecosite Details", "OK", "Cancel");
                    if (isUserAccept)
                    {
                        if (_ecositeRepository.IsEcositeExists(_fk))
                        {
                            _ecosite.LastModified = System.DateTime.UtcNow;
                            _ecositeRepository.UpdateEcosite(_ecosite);

                        }
                        else
                        {
                            _ecosite.Created = System.DateTime.UtcNow;
                            _ecosite.LastModified = _ecosite.Created;
                            _ecositeRepository.InsertEcosite(_ecosite, _fk);
                        }
                        await _navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Ecosite", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        async Task Delete() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Ecosite Details", "Delete Ecosite Details", "OK", "Cancel");
            if (isUserAccept) {
                _ecositeRepository.DeleteEcosite(_ecosite.ECOSITEID);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Ecosite details for plot " + _ecositeRepository.GetTitle(_fk);
            set
            {
            }
        }
    }
}