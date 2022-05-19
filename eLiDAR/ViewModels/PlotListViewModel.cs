using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Models;
using eLiDAR.Views;
using Xamarin.Forms;
using eLiDAR.Services;


namespace eLiDAR.ViewModels {
    public class PlotListViewModel : BasePlotViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand DeleteAllCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
        public ICommand ShowSiteCommand { get; private set; }
        public ICommand ShowSmallTreeCommand { get; private set; }
        public ICommand ShowSoilCommand { get; private set; }
        public ICommand ShowPhotoCommand { get; private set; }
        public ICommand ShowVegetationCommand { get; private set; }
        public ICommand ShowVegetationCensusCommand { get; private set; }
        public ICommand ShowDWDCommand { get; private set; }
        public ICommand ShowSmallTreeTallyCommand { get; private set; }
        // public ICommand SearchCommand { get; private set; }

        //   public bool IsPlotTypeB { get; private set; }

        public PlotListViewModel(INavigation navigation,string selectedprojectid )
        {
            _plot = new PLOT();
            _navigation = navigation;
            _plotRepository = new PlotRepository();
            _selectedprojectid = selectedprojectid;

            AddCommand = new Command(async () => await ShowAdd(_selectedprojectid));
            DeleteAllCommand = new Command(async () => await DeleteAll());
            ShowFilteredCommand = new Command<PLOT>(async (x) => await ShowTrees(x));
            ShowSiteCommand = new Command<PLOT>(async (x) => await ShowSite(x));
            ShowSmallTreeCommand = new Command<PLOT>(async (x) => await ShowSmallTree(x));
            ShowSmallTreeTallyCommand = new Command<PLOT>(async (x) => await ShowSmallTreeTally(x));
            ShowSoilCommand = new Command<PLOT>(async (x) => await ShowSoil(x));
            ShowPhotoCommand = new Command<PLOT>(async (x) => await ShowPhoto(x));
            ShowVegetationCommand = new Command<PLOT>(async (x) => await ShowVegetation(x));
            ShowVegetationCensusCommand = new Command<PLOT>(async (x) => await ShowVegetationCensus(x));
            ShowDWDCommand = new Command<PLOT>(async (x) => await ShowDWD(x));
          //  SearchCommand = new Command<string>(async (text) => await Search(text));
            FetchPlots();
        }
 
        public void FetchPlots(){
            if (_selectedprojectid == "")
              //  PlotList = "No project selected";
                PlotList = _plotRepository.GetAllData();
            else
         //       PlotList = _plotRepository.GetFilteredData(_selectedprojectid);
                PlotListFull = _plotRepository.GetFilteredDataFull(_selectedprojectid);

        }
    
        async Task ShowAdd(string fk)
        {
            await _navigation.PushAsync(new AddPlot(fk));
        }


        async Task DeleteAll(){
            bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Plot List", "Delete All Plot Details?", "OK", "Cancel");
            if (isUserAccept){
                _plotRepository.DeleteAllPlots();
                await _navigation.PopAsync(true);
            }
        }

        async void ShowDetails(string selectedPlotID){
            await _navigation.PushAsync(new PlotDetailsPage(selectedPlotID));
        }

        bool _isselected;
        public bool IsSelected
        {
            get
            {
                if (_selectedprojectid.Length > 1)
                {
                    _isselected = true;
                    return _isselected;
                }
                else { return false; }
            }
            set
            {
               _isselected = value;
                NotifyPropertyChanged("IsSelected");
                
            }
        }

        PLOTLIST _selectedPlotItem;
        public PLOTLIST SelectedPlotItem {
            get => _selectedPlotItem;
            set {
                if (value != null){
                    _selectedPlotItem = value;
                    NotifyPropertyChanged("SelectedPlotItem");
                    ShowDetails(value.PLOTID);
                }
            }
        }
        async Task ShowTrees(PLOT _plot)
        {
            // launch the form - filtered to a specific projectid
            try
            {
                await _navigation.PushAsync(new TreeListPage(_plot.PLOTID));
            }
            catch (System.Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };

        }
        async Task ShowSite(PLOT _plot)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new EcositeDetailsPage(_plot.PLOTID));
        }
        async Task ShowSoil(PLOT _plot)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new SoilList(_plot.PLOTID));
        }
        async Task ShowPhoto(PLOT _plot)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new PhotoList(_plot.PLOTID));
        }
        async Task ShowSmallTree(PLOT _plot)
        {
            await _navigation.PushAsync(new SmallTreeList(_plot.PLOTID));
        }
        async Task ShowSmallTreeTally(PLOT _plot)
        {
            await _navigation.PushAsync(new SmallTreeTallyList(_plot.PLOTID));
        }

        async Task ShowVegetation(PLOT _plot)
        {
            await _navigation.PushAsync(new VegetationList(_plot.PLOTID));
        }
        async Task ShowVegetationCensus(PLOT _plot)
        {
            await _navigation.PushAsync(new VegetationCensusList(_plot.PLOTID));
        }
        async Task ShowDWD(PLOT _plot)
        {
            await _navigation.PushAsync(new DWDList(_plot.PLOTID));
        }
 
        public string Title
        {
            get => "Plot List for " + _plotRepository.GetProjectTitle(_selectedprojectid) + ".\n  " + PlotListFull.Count.ToString()+ " plots.";
            set
            {
            }
        }
        public List<PROJECT> ProjectList
        {
            get => _plotRepository.GetProjectList();
            set
            {
            }
        }
        private PROJECT _selectedproject ;
        public PROJECT SelectedProject
        {
            get
            {
                _selectedproject = PickerService.GetProjectItem(ProjectList, _selectedprojectid);
                return _selectedproject;
            }
            set
            {
                SetProperty(ref _selectedproject, value);
                _selectedprojectid = _selectedproject.PROJECTID ;
                NotifyPropertyChanged("PlotListFull");
                NotifyPropertyChanged("Title");
                FetchPlots();
                IsSelected = true;
            }
        }
     
    }
}
