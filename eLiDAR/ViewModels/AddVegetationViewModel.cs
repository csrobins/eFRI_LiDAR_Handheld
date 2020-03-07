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
using SupportWidgetXF.Models.Widgets;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class AddVegetationViewModel: BaseVegetationViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public AddVegetationViewModel(INavigation navigation, string selectedID) {
            _navigation = navigation;
            _vegetation = new VEGETATION();
            _vegetation.VEGETATIONID  = selectedID;
            _vegetationRepository = new VegetationRepository();
            _fk = selectedID;
            AddCommand = new Command(async () => await Update());
            DeleteCommand = new Command(async () => await Delete());
  

            // Added to handle the autocomplete picker as s check
//            ImageItems = new ObservableCollection<GalleryImageXF>();
 //           ImageItemsSet = new ObservableCollection<string>();

            ItemDemo = new List<IAutoDropItem>();
            ItemDemo.Add(new YourClass("Robert Downey Jr.", "Iron Man - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Chris Evans", "Captain America - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Scarlett Johansson", "Black Widow - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Tom Hiddleston", "Loki - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Mark Ruffalo", "The Hulk - Marvel Universe", "marvel"));
            ItemDemo.Add(new YourClass("Ben Affleck", "BatMan - DC Universe", "dc"));
            ItemDemo.Add(new YourClass("Henry Cavill", "Superman - DC Universe", "dc"));
            ItemDemo.Add(new YourClass("Gal Gadot", "Wonder Woman - DC Universe", "dc"));
            ItemDemo.Add(new YourClass("Ezra Miller", "The Flash - DC Universe", "dc"));
            ItemDemo.Add(new YourClass("Jason Momoa", "Aquaman - DC Universe", "dc"));


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
            _vegetation = _vegetationRepository.GetVegetationData(fk);
        }
        async Task Update() {
            try
            {
                VegetationValidator _vegetationValidator = new VegetationValidator();
                ValidationResult validationResults = _vegetationValidator.Validate(_vegetation);

                if (validationResults.IsValid)
                {
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Vegetation Details", "Save Vegetation Details", "OK", "Cancel");
                    if (isUserAccept)
                    {
                     _vegetationRepository.InsertVegetation(_vegetation,_fk);
                        //  This is just to slow down the database
                     _vegetationRepository.GetVegetationData(_vegetation.VEGETATIONID );
                     await _navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Vegetation", validationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        async Task Delete() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Vegetation Details", "Delete Vegetation Details", "OK", "Cancel");
            if (isUserAccept) {
                _vegetationRepository.DeleteVegetation (_vegetation.VEGETATIONID );
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Vegetation layer for plot " + _vegetationRepository.GetTitle(_fk);
            set
            {
            }
        }
        private List<IAutoDropItem> _ItemDemo;
        public List<IAutoDropItem> ItemDemo
        {
            get => _ItemDemo;
            set
            {
                _ItemDemo = value;
                OnPropertyChanged();
            }
        }
    }
}