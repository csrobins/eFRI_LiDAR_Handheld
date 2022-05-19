using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Models;
using eLiDAR.Views;
using Xamarin.Forms;
using eLiDAR.Services;
using System.ComponentModel;
using eLiDAR.Helpers;
using System.Runtime.CompilerServices;
using eLiDAR.API;
using System.Linq;
using eLiDAR.Utilities;
using System.Collections.ObjectModel;

namespace eLiDAR.ViewModels {
    public class PlotSynchViewModel : INotifyPropertyChanged
    {

        public ICommand SynchPlotCommand { get; private set; }
        public ICommand SinglePlotCommand { get; private set; }
        public List<PickerItemsString> ListPlots { get; set; }
        private Utils util;
        private DatabaseHelper databasehelper;
        private SynchManager _synchmanager;
        private DatabaseHelper _databasehelper;
        private INavigation _navigation;
        public ICommand SynchCommand { get; private set; }
        private ObservableCollection<PLOT> _plotlist;
        public PlotSynchViewModel(INavigation navigation)
        {
            util = new Utils();
            _navigation = navigation;
            _databasehelper = new DatabaseHelper();
            _synchmanager = new SynchManager();
            databasehelper = new DatabaseHelper();
            ListPlots = Services.PickerService.FillPlotPicker(databasehelper.GetAllPlotData()).ToList().OrderBy(c => c.NAME).ToList();
            SynchPlotCommand = new Command<PLOT>(async (x) => await SynchPlotrun(x));
            SinglePlotCommand = new Command(async() => await SynchPlotrun());
            SynchCommand = new Command(async () => await Synchrun());
            if (!IsSynchBusy) { IsSynchEnabled = true; }
            _plotlist = new ObservableCollection<PLOT>(databasehelper.GetPlotDataforSynch(System.DateTime.MinValue).ToList());
            //FetchPlots();
         //   MessagingCenter.Subscribe<PlotSynchList>(this, "Update listview", (sender) =>
         //   {
         //       FetchPlots();
         //   });
        }

        async Task Synchrun()
        {
            IsSynchBusy = true;
            bool success = await _synchmanager.RunLoad();
            IsSynchBusy = false;
            if (success)
            {
                msg = "Synch succeeded";
                await Application.Current.MainPage.DisplayAlert("Synch", "The synch operation completed successfully.", "OK");
            }
            else
            {
                msg = "Not all tables synched!";
                await Application.Current.MainPage.DisplayAlert("Synch did not finish", "The synch operation was not completed.", "OK");
            }
           
        }
        private bool _issynchbusy;
        public bool IsSynchBusy
        {
            get => _issynchbusy;
            set
            {
                _issynchbusy = value;
                if (value)
                {
                    IsSynchEnabled = false;
                }
                else
                { IsSynchEnabled = true; }
                NotifyPropertyChanged("IsSynchBusy");
            }
        }
        public void FetchPlots()
        {
            PlotList.Clear();
          //  PlotList = ObservableCollection<PLOT>(_databasehelper.GetPlotDataforSynch(System.DateTime.MinValue));
            foreach (PLOT p in _databasehelper.GetPlotDataforSynch(System.DateTime.MinValue) as List<PLOT>)
            {
                PlotList.Add(p);
            }
        //    MessagingCenter.Send(this, "Update listview");
            NotifyPropertyChanged("PlotList"); 

        }
        private bool _isplotsynchenabled;
        public bool IsPlotSynchEnabled
        {
            get => _isplotsynchenabled;
            set
            {
                if (_selectedPlot != null)
                {
                    if (_selectedPlot.ID != "" && IsSynchEnabled) { _isplotsynchenabled = value; }
                    else { _isplotsynchenabled = false; }

                }
                else { _isplotsynchenabled = false; }
                //_issynchenabled = true;
                NotifyPropertyChanged("IsPlotSynchEnabled");
            }
        }
        public string Title
        {
            get => "Plot List for Synching. " + PlotList.Count.ToString()+ " plots.";
            set
            {
            }
        }

      
        public ObservableCollection<PLOT> PlotList
        {
            get
            {
     
                return _plotlist;
            } 
            set
            {
                SetProperty(ref _plotlist, value);
                NotifyPropertyChanged("PlotList");
                
            }
        }
 
        private string _msg;
        public string msg
        {
            get => _msg;
            set
            {
                _msg = value;
                NotifyPropertyChanged("msg");
            }
        }
 
        async Task SynchPlotrun(PLOT _plot)
        {
            IsPlotSynchBusy = true;
            bool success = await _synchmanager.RunSynch(_plot.PLOTID);
            IsPlotSynchBusy = false;
            if (success)
            {
                msg = "Plot Synch succeeded for " + _plot.VSNPLOTNAME;
                _databasehelper.SetPlotSynch(_plot.PLOTID,null,true);  
                await Application.Current.MainPage.DisplayAlert("Synch", msg, "OK");
            }
            else
            {
                msg = "Not all tables synched!";
                await Application.Current.MainPage.DisplayAlert("Synch did not finish", msg, "OK");
            }
            FetchPlots();
        }
        private PickerItemsString _selectedPlot = new PickerItemsString { ID = "", NAME = "" };
        public PickerItemsString SelectedPlot
        {
            get
            {
                //     _selectedPlot = PickerService.GetItem(ListPlots, _smallTree.SPECIESCODE);
                return _selectedPlot;
            }
            set
            {
                SetProperty(ref _selectedPlot, value);
                if (value != null)
                {
                    if (value.ID != "") { IsPlotSynchEnabled = true; }
                    else { IsPlotSynchEnabled = false; }
                }
                else { IsPlotSynchEnabled = false; }
                _selectedPlot = value;
            }
        }
        async Task SynchPlotrun()
        {
            IsPlotSynchBusy = true;
            bool success = await _synchmanager.RunSynch(_selectedPlot.ID);
            IsPlotSynchBusy = false;
            if (success)
            {
                msg = "Plot Synch succeeded for " + _selectedPlot.NAME;
                _databasehelper.SetPlotSynch(_selectedPlot.ID, null, true);
                await Application.Current.MainPage.DisplayAlert("Synch", msg, "OK");
            }
            else
            {
                msg = "Not all tables synched!";
                await Application.Current.MainPage.DisplayAlert("Synch did not finish", msg, "OK");
            }
            FetchPlots();
        }

        private bool _isplotsynchbusy;
        public bool IsPlotSynchBusy
        {
            get => _isplotsynchbusy;
            set
            {
                _isplotsynchbusy = value;
                if (value)
                {
                    IsSynchEnabled = false;
                }
                else
                { IsSynchEnabled = true; }
                NotifyPropertyChanged("IsPlotSynchBusy");
            }
        }
        private bool _issynchenabled;
        public bool IsSynchEnabled
        {
            get => _issynchenabled;
            set
            {
                if (util.IsLoggedIn) { _issynchenabled = value; }
                else { _issynchenabled = false; }
                //_issynchenabled = true;
                NotifyPropertyChanged("IsSynchEnabled");
            }
        }
        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged2;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backfield, value))
            {
                return false;
            }
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
