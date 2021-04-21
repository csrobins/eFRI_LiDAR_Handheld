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


namespace eLiDAR.ViewModels {
    public class AddPlotViewModel : BasePlotViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand ViewAllCommand { get; private set; }
        public ICommand CommentsCommand { get; private set; }
        public ICommand StandInfoCommand { get; private set; }
        public ICommand ForestHealthCommand { get; private set; }
        public ICommand PlotCrewCommand { get; private set; }

        public List<PickerItems> ListFMU { get; set; }
        public List<PickerItems> ListSpecies { get; set; }
       
        public List<PickerItems> ListNonStandardType { get; set; }
        public List<PickerItemsString> ListMeasurementType { get; set; }
        public List<PickerItemsString> ListNonStandardTypeCode { get; set; }
        public List<PickerItemsString> ListPerson { get; set; }
        public List<PickerItems> ListGrowthPlot { get; set; }
        public AddPlotViewModel(INavigation navigation, string fk)
        {
            _navigation = navigation;
            _plotValidator = new PlotValidator();
            _plot = new PLOT();
            _plotRepository = new PlotRepository();
            _selectedprojectid = fk;
            _plot.VSNPLOTNAME = "VSN";
            _plot.IsDeleted = "N";
            _plot.DATUM = "NAD83";
            _plot.PROJECTID = fk;
            _plot.PLOTOVERVIEWDATE = System.DateTime.Now;
            AddCommand = new Command(async () => await AddPlot(_selectedprojectid));
            ViewAllCommand = new Command(async () => await ShowList());
            ListFMU = PickerService.ForestItems().OrderBy(c => c.NAME).ToList();
            ListSpecies = PickerService.SpeciesItems().OrderBy(c => c.NAME).ToList();
            
            ListMeasurementType = PickerService.MeasurementTypeItems().OrderBy(c => c.NAME).ToList();
            ListNonStandardType = PickerService.NonStandardTypeItems().OrderBy(c => c.NAME).ToList();
            ListPerson = FillPersonPicker().OrderBy(c => c.NAME).ToList();
            ListGrowthPlot = PickerService.GrowthPlotItems().OrderBy(c => c.NAME).ToList();
            CommentsCommand = new Command(async () => await ShowComments());
            StandInfoCommand = new Command(async () => await ShowStandInfo());
            ForestHealthCommand = new Command(async () => await ShowForestHealth());
            PlotCrewCommand = new Command(async () => await ShowPlotCrew());

        }
        async Task ShowComments()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new PlotComments(_plot));
        }
        async Task ShowStandInfo()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new StandInfo(_plot));
        }
        async Task ShowForestHealth()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new ForestHealth(_plot));
        }
        async Task ShowPlotCrew()
        {
            // launch the form - filtered to a specific tree
            await _navigation.PushAsync(new PlotCrew(_plot));
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
        private PickerItems _selectedNonStandardType = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedNonStandardType
        {
            get
            {
                _selectedNonStandardType = PickerService.GetItem(ListNonStandardType, _plot.NONSTANDARDTYPECODE);
                return _selectedNonStandardType;
            }
            set
            {
                SetProperty(ref _selectedNonStandardType, value);
                _plot.NONSTANDARDTYPECODE = (int)_selectedNonStandardType.ID;
            }
        }

        private List<PickerItemsString> FillPersonPicker()
        {
            var list = new List<PickerItemsString>();
            foreach (var newperson in _plotRepository.GetPersonList(_plot.PROJECTID))
            {
                var newitem = new PickerItemsString() { ID = newperson.LASTNAME + ", " + newperson.FIRSTNAME, NAME = newperson.LASTNAME + ", " + newperson.FIRSTNAME };
                list.Add(newitem);
            };
            return list;
        }

        private PickerItemsString _selectedForestHealthPerson = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedForestHealthPerson
        {
            get
            {
                _selectedForestHealthPerson = PickerService.GetItem(ListPerson, _plot.FORESTHEALTHPERSON);
                return _selectedForestHealthPerson;
            }
            set
            {
                SetProperty(ref _selectedForestHealthPerson, value);
                _plot.FORESTHEALTHPERSON = _selectedForestHealthPerson.ID;
            }
        }

        private PickerItemsString _selectedMeasurementType = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedMeasurementType
        {
        get
        {
            _selectedMeasurementType = PickerService.GetItem(ListMeasurementType, _plot.MEASURETYPECODE);
            return _selectedMeasurementType;
        }
        set
        {
            SetProperty(ref _selectedMeasurementType, value);
            _plot.MEASURETYPECODE = _selectedMeasurementType.ID;
        }
        }
        private PickerItems _selectedGrowthPlot = new PickerItems { ID = 0, NAME = "" };
        public PickerItems SelectedGrowthPlot
        {
            get
            {
                _selectedGrowthPlot = PickerService.GetItem(ListGrowthPlot, _plot.GROWTHPLOTNUMBER);
                return _selectedGrowthPlot;
            }
            set
            {
                SetProperty(ref _selectedGrowthPlot, value);
                _plot.GROWTHPLOTNUMBER = (int)_selectedGrowthPlot.ID;
            }
        }

        async Task ShowList(){ 
            await _navigation.PushAsync(new PlotList(_selectedprojectid ));
        }
        async Task AddPlot(string fk)
        {
            _plot.PROJECTID = fk;
//            var validationResults = _plotValidator.Validate((FluentValidation.IValidationContext)_plot);
  
            PlotValidator _plotValidator = new PlotValidator();
            ValidationResult validationResults = _plotValidator.Validate(_plot);

            if (validationResults.IsValid)
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Plot", "Do you want to save plot details?", "OK", "Cancel");
                if (isUserAccept)
                {
                    _plot.Created = System.DateTime.UtcNow;
                    _plot.LastModified = _plot.Created;
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
