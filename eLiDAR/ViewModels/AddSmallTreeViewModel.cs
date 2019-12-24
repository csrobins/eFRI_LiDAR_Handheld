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
    public class AddSmallTreeViewModel: BaseSmallTreeViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public List<PickerItems> ListSpecies { get; set; }

        public AddSmallTreeViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _smallTree = new SMALLTREE();
            _smallTree.SMALLTREEID   = selectedID;
            _smallTreeRepository = new SmallTreeRepository();
            _fk = selectedID;
            AddCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
            ListSpecies = PickerService.SpeciesItems().ToList().OrderBy(c => c.NAME).ToList();
        }
        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                _selectedSpecies = PickerService.GetItem(ListSpecies, _smallTree.SPECIES);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _smallTree.SPECIES   = (int)_selectedSpecies.ID;
            }
        }
        void FetchDetails(string fk){
            _smallTree = _smallTreeRepository.GetSmallTreeData(fk);
        }
        async Task Update() {
            try
            {
                SmallTreeValidator _smallTreeValidator = new SmallTreeValidator();
                ValidationResult validationResults = _smallTreeValidator.Validate(_smallTree);

                if (validationResults.IsValid)
                {
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Small Tree Details", "Save Small Tree Details", "OK", "Cancel");
                    if (isUserAccept)
                    {
                     _smallTreeRepository.InsertSmallTree(_smallTree,_fk);
                     await _navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Small Tree", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        async Task Delete() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Small Tree Details", "Delete Small Tree Details", "OK", "Cancel");
            if (isUserAccept) {
                _smallTreeRepository.DeleteSmallTree(_smallTree.SMALLTREEID );
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Small Trees for plot " + _smallTreeRepository.GetTitle(_fk);
            set
            {
            }
        }
    }
}