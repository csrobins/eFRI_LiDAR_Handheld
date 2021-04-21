using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Services;
using eLiDAR.Validator;
using FluentValidation;
using FluentValidation.Results;
using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class StemMapDetailsViewModel: BaseStemMapViewModel {

        public ICommand UpdateTreeCommand { get; private set; }
        public ICommand DeleteTreeCommand { get; private set; }
        public StemMapDetailsViewModel(INavigation navigation, string selectedTreeID) {
            _navigation = navigation;
            _stemmap = new STEMMAP();
            _stemmap.TREEID = selectedTreeID;
            _stemMapRepository = new StemMapRepository();
            _fk = selectedTreeID;
            UpdateTreeCommand = new Command(async () => await UpdateTree());
            DeleteTreeCommand = new Command(async () => await DeleteTree());
            // Get the tree if it exists
            if (_stemMapRepository.IsStemMapExists(_fk))
            {
                FetchTreeDetails(_fk);
            }
        }


        void FetchTreeDetails(string fk){
            _stemmap = _stemMapRepository.GetTreeData(fk);
        }

        async Task UpdateTree() {
            
            try
            {
                StemMapValidator _stemmapValidator = new StemMapValidator();
                ValidationResult treevalidationResults = _stemmapValidator.Validate(_stemmap);

                if (treevalidationResults.IsValid)
                {
                    bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Stem Map Details", "Save Stem Map Details", "OK", "Cancel");
                    if (isUserAccept)
                    {
                        if (_stemMapRepository.IsStemMapExists(_fk))
                        {
                            _stemmap.LastModified = System.DateTime.UtcNow;
                            _stemMapRepository.UpdateTree(_stemmap);

                        }
                        else
                        {
                            _stemmap.Created = System.DateTime.UtcNow;
                            _stemmap.LastModified = _stemmap.Created;
                            _stemMapRepository.InsertTree(_stemmap, _fk);
                        }
                        await _navigation.PopAsync();
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Add Stem Map", treevalidationResults.Errors[0].ErrorMessage, "Ok");
                }
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        async Task DeleteTree() {
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Stem Map Details", "Delete Stem Map Details", "OK", "Cancel");
            if (isUserAccept) {
                _stemMapRepository.DeleteTree(_stemmap.TREEID);
                await _navigation.PopAsync();
            }
        }
        public string Title
        {
            get => "Stem Map details for tree " + _stemMapRepository.GetTitle(_fk);
            set
            {
            }
        }
    }
}