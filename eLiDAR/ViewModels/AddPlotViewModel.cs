using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Services;
using eLiDAR.Validator;
using eLiDAR.Views;
using Xamarin.Forms;


namespace eLiDAR.ViewModels {
    public class AddPlotViewModel : BasePlotViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand ViewAllCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }
        public List<PickerItems> ListFMU { get; set; }
        public List<PickerItems> ListSpecies { get; set; }
        public List<PickerItems> ListCanopyOrigin { get; set; }
        public List<PickerItemsString> ListCanopyStructure {get; set; }
        public List<PickerItemsString> ListMaturityClass { get; set; }
        public List<PickerItems> ListNonStandardType { get; set; }

        public AddPlotViewModel(INavigation navigation){
            _navigation = navigation;
            _plotValidator = new PlotValidator();
            _plot = new PLOT();
            _plotRepository = new PlotRepository();
            AddCommand = new Command(async () => await AddPlot("")); 
            ViewAllCommand = new Command(async () => await ShowList());
            ListFMU = PickerService.ForestItems().OrderBy(c => c.NAME).ToList();
        }

        public AddPlotViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _plotValidator = new PlotValidator();
            _plot = new PLOT();
            _plotRepository = new PlotRepository();
            _selectedprojectid = fk;
            AddCommand = new Command(async () => await AddPlot(_selectedprojectid));
            ViewAllCommand = new Command(async () => await ShowList());
            ListFMU = PickerService.ForestItems().OrderBy(c => c.NAME).ToList();
            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.NAME).ToList();
            ListCanopyOrigin= PickerService.CanopyOriginItems().OrderBy(c => c.NAME).ToList();
            ListCanopyStructure = PickerService.CanopyStructureItems().OrderBy(c => c.NAME).ToList();
            ListMaturityClass = PickerService.MaturityClassItems().OrderBy(c => c.NAME).ToList();

            CommentsCommand = new Command(async () => await ShowComments());

        }
        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new PlotComments(_plot));
        }
        private PickerItems _selectedSpecies = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedSpecies
        {
            get
            {
                _selectedSpecies = PickerService.GetItem(ListSpecies, _plot.LEAD_SPP);
                return _selectedSpecies;
            }
            set
            {
                SetProperty(ref _selectedSpecies, value);
                _plot.LEAD_SPP = (int)_selectedSpecies.ID;
            }
        }
        private PickerItems _selectedCanopyOrigin = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedCanopyOrigin
        {
            get
            {
                _selectedCanopyOrigin = PickerService.GetItem(ListCanopyOrigin, _plot.ORIGIN);
                return _selectedCanopyOrigin;
            }
            set
            {
                SetProperty(ref _selectedCanopyOrigin, value);
                _plot.ORIGIN = (int)_selectedCanopyOrigin.ID;
            }
        }
        private PickerItemsString _selectedMaturityClass = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMaturityClass
        {
            get
            {
                _selectedMaturityClass = PickerService.GetItem(ListMaturityClass, _plot.MATURITY);
                return _selectedMaturityClass;
            }
            set
            {
                SetProperty(ref _selectedMaturityClass, value);
                _plot.MATURITY  = _selectedMaturityClass.ID;
            }
        }
        private PickerItemsString _selectedCanopyStructure = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedCanopyStructure
        {
            get
            {
                _selectedCanopyStructure = PickerService.GetItem(ListCanopyStructure, _plot.CANOPY_STRUCTURE);
                return _selectedCanopyStructure;
            }
            set
            {
                SetProperty(ref _selectedCanopyStructure, value);
                _plot.CANOPY_STRUCTURE  = _selectedCanopyStructure.ID;
            }
        }

        private PickerItems _selectedFMU = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedFMU
        {
            get
            {
                //_selectedSpecies.ID = PickerService.GetIndex(ListSpecies, _tree.SPECIES);
                //_selectedSpecies.NAME = PickerService.GetValue(ListSpecies, _tree.SPECIES);
                _selectedFMU = PickerService.GetItem(ListFMU, _plot.FMU);
                return _selectedFMU;
            }
            set
            {
                SetProperty(ref _selectedFMU, value);
                _plot.FMU = (int)_selectedFMU.ID;
            }
        }

        async Task ShowList(){ 
            await _navigation.PushAsync(new PlotList());
        }
        async Task AddPlot(string fk)
        {
            _plot.PROJECTID = fk;
            var validationResults = _plotValidator.Validate(_plot);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Plot", "Do you want to save plot details?", "OK", "Cancel");
                if (isUserAccept)
                {

                    _plotRepository.InsertPlot(_plot, fk);
                    
                    await _navigation.PopAsync(); 
                 //   await _navigation.PushAsync(new PlotList(fk));
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Add Plot", validationResults.Errors[0].ErrorMessage, "Ok");
            }
        }
        public bool IsViewAll => _plotRepository.GetAllData().Count > 0 ? true : false;
    }
}
