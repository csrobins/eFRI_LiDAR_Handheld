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
    public class EcositeDetailsViewModel: BaseEcositeViewModel {

        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItems> ListDrainage { get; set; }
        public List<PickerItems> ListPorePattern { get; set; }
        public List<PickerItems> ListMoistureRegime { get; set; }
        public List<PickerItems> ListHumusForm { get; set; }
        public EcositeDetailsViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _ecosite = new ECOSITE();
            _ecosite.PLOTID  = selectedID;
            _ecositeRepository = new EcositeRepository();
            _fk = selectedID;
            UpdateCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            ListDrainage = PickerService.DrainageItems().ToList();
            ListPorePattern = PickerService.PorePatternItems().ToList();
            ListMoistureRegime = PickerService.MoistureRegimeItems().ToList();
            ListHumusForm = PickerService.HumusFormItems().ToList();

            // Get the ecosite if it exists
            if (_ecositeRepository.IsEcositeExists(_fk))
            {
                FetchDetails(_fk);
            }
        }

        private PickerItems _selectedDrainage = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedDrainage
        {
            get
            {
                _selectedDrainage = PickerService.GetItem(ListDrainage, _ecosite.DRAINAGE);
                return _selectedDrainage;
            }
            set
            {
                SetProperty(ref _selectedDrainage, value);
                _ecosite.DRAINAGE  = (int)_selectedDrainage.ID;
            }
        }

        private PickerItems _selectedPorePattern = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedPorePattern
        {
            get
            {
                _selectedPorePattern = PickerService.GetItem(ListPorePattern, _ecosite.EFFECTIVE_PORE_PATTERN);
                return _selectedPorePattern;
            }
            set
            {
                SetProperty(ref _selectedPorePattern, value);
                _ecosite.EFFECTIVE_PORE_PATTERN  = (int)_selectedPorePattern.ID;
            }
        }

        private PickerItems _selectedMoistureRegime = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedMoistureRegime
        {
            get
            {
                _selectedMoistureRegime = PickerService.GetItem(ListMoistureRegime, _ecosite.MOISTURE_REGIME);
                return _selectedMoistureRegime;
            }
            set
            {
                SetProperty(ref _selectedMoistureRegime, value);
                _ecosite.MOISTURE_REGIME  = (int)_selectedMoistureRegime.ID;
            }
        }

        private PickerItems _selectedHumusForm = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedHumusForm
        {
            get
            {
                _selectedHumusForm = PickerService.GetItem(ListHumusForm, _ecosite.HUMUS_FORM);
                return _selectedHumusForm;
            }
            set
            {
                SetProperty(ref _selectedHumusForm, value);
                _ecosite.HUMUS_FORM = (int)_selectedHumusForm.ID;
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
                            _ecositeRepository.UpdateEcosite(_ecosite);

                        }
                        else
                        {
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