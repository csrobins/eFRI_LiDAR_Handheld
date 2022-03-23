
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Views;

using Xamarin.Forms;

namespace eLiDAR.ViewModels {
    public class PhotoListViewModel : BasePhotoViewModel {

        public ICommand AddCommand { get; private set; }
        public ICommand ShowFilteredCommand { get; private set; }
  
        public PhotoListViewModel(INavigation navigation, string _fk)
        {
            _photo = new PHOTO();
            _navigation = navigation;
            _photoRepository = new PhotoRepository();
            _selectedplotid = _fk;
            AddCommand = new Command(async () => await ShowAdd(_fk));
            
            ShowFilteredCommand = new Command<PHOTO>(async (x) => await ShowVegetation(x));
          //  CheckTable();
            FetchVegetation();
        }

        
        public async Task CheckTable()
        {
            // run this to prepopulate table with photos the first time through
            if (_photoRepository.IsPhotoTableEmpty(_selectedplotid))
            {
                bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Add Photo Details", "Do you want to prepopulate the standard 18 Stand Information Photo Details?", "OK", "Cancel");
                if (isUserAccept)
                {
                    PHOTO _newphoto = new PHOTO();
                    _newphoto.PHOTOTYPE = "Stand Information";
                    _newphoto.PHOTONUMBER = 1;
                    _newphoto.AZIMUTH = 0;
                    _newphoto.DISTANCE = 0;
                    _newphoto.PLOTID = _selectedplotid;
                    _newphoto.Created = System.DateTime.UtcNow;
                    _newphoto.LastModified = _newphoto.Created;
                    _newphoto.IsDeleted = "N";
                    _photoRepository.InsertPhoto(_newphoto);
                    PHOTO _newphoto2 = new PHOTO();
                    _newphoto2.PHOTOTYPE = "Stand Information";
                    _newphoto2.PHOTONUMBER = 18;
                    _newphoto2.AZIMUTH = 0;
                    _newphoto2.DISTANCE = 0;
                    _newphoto2.Created = System.DateTime.UtcNow;
                    _newphoto2.LastModified = _newphoto2.Created;
                    _newphoto2.IsDeleted = "N";
                    _newphoto2.PLOTID = _selectedplotid;
                    _photoRepository.InsertPhoto(_newphoto2);
                    // Insert records for the Stand Infor Photos
                    int j = 1;
                    for (int i = 2; i < 18; i += 2)
                    {
                        PHOTO _newphoto3 = new PHOTO();
                        _newphoto3.PHOTOTYPE = "Stand Information";
                        _newphoto3.PHOTONUMBER = i;
                        _newphoto3.AZIMUTH = (i - (j + 1)) * 45;
                        if (j % 2 == 0)
                        {
                            _newphoto3.DISTANCE = Constants.DefaultPhoto2Distance;
                        }
                        else
                        {
                            _newphoto3.DISTANCE = Constants.DefaultPhoto1Distance;
                        }
                        _newphoto3.PLOTID = _selectedplotid;
                        _newphoto3.Created = System.DateTime.UtcNow;
                        _newphoto3.LastModified = _newphoto3.Created;
                        _newphoto3.IsDeleted = "N";
                        _photoRepository.InsertPhoto(_newphoto3);
                        j = j + 1;
                    }
                    j = 2;
                    for (int i = 3; i < 18; i += 2)
                    {
                        PHOTO _newphoto3 = new PHOTO();
                        _newphoto3.PHOTOTYPE = "Stand Information";
                        _newphoto3.PHOTONUMBER = i;
                        _newphoto3.AZIMUTH = (i - (j + 1)) * 45;
                        if (j % 2 == 0)
                        {
                            _newphoto3.DISTANCE = Constants.DefaultPhoto2Distance;
                        }
                        else
                        {
                            _newphoto3.DISTANCE = Constants.DefaultPhoto1Distance;
                        }
                        _newphoto3.PLOTID = _selectedplotid;
                        _newphoto3.Created = System.DateTime.UtcNow;
                        _newphoto3.LastModified = _newphoto3.Created;
                        _newphoto3.IsDeleted = "N";
                        _photoRepository.InsertPhoto(_newphoto3);
                        j = j + 1;
                    }
                    FetchVegetation(); 
                }
            }
        }

        public void FetchVegetation(){
           PhotoList = _photoRepository.GetFilteredData(_selectedplotid);
        }

        async Task ShowAdd(string _fk)
        {
            await _navigation.PushAsync(new AddPhoto(_fk));
        }
        
        async void ShowDetails(string selectedVegetationID)
            {
                await _navigation.PushAsync(new PhotoDetailsPage(selectedVegetationID));
            }
        
        private PHOTO _selectedVegetationItem;
        public PHOTO SelectedVegetationItem {
            get  {
                try
                {
                   return _selectedVegetationItem;
                }
                catch (System.Exception ex) {
                    return null;
                }
            }
            set {
                if (value != null){
                    _selectedVegetationItem = value;
                    NotifyPropertyChanged("SelectedVegetationItem");
                    ShowDetails(value.PHOTOID );
                }
            }
        }
        async Task ShowVegetation(PHOTO  _vegetation)
        {
            // launch the form - filtered to a specific projectid
            await _navigation.PushAsync(new PhotoDetailsPage(_vegetation.PHOTOID));
        }
        public string Title
        {
            get => "Photo details for plot " + _photoRepository.GetPlotTitle(_selectedplotid) + ".  " + PhotoList.Count.ToString() + " photos.";
            set
            {
            }
        }
    }
}
