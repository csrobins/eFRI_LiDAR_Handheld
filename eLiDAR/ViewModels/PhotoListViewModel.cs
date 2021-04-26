
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
            _navigation = navigation;
            _photoRepository = new PhotoRepository();
            _selectedplotid = _fk;
            AddCommand = new Command(async () => await ShowAdd(_fk));
            
            ShowFilteredCommand = new Command<PHOTO>(async (x) => await ShowVegetation(x));
            CheckTable();
            FetchVegetation();
        }
        void CheckTable()
        {
            // run this to prepopulate table with photos the first time through
            if (_photoRepository.IsPhotoTableEmpty(_selectedplotid))
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
                for (int i = 0; i < 8; i++)
                {
                    PHOTO _newphoto3 = new PHOTO();
                    _newphoto3.PHOTOTYPE = "Stand Information";
                    _newphoto3.PHOTONUMBER = i + 2;
                    _newphoto3.AZIMUTH = 45 * i;
                    _newphoto3.DISTANCE = Constants.DefaultPhoto1Distance;
                    _newphoto3.PLOTID = _selectedplotid;
                    _newphoto3.Created = System.DateTime.UtcNow;
                    _newphoto3.LastModified = _newphoto3.Created;
                    _newphoto3.IsDeleted = "N";
                    _photoRepository.InsertPhoto(_newphoto3);
                }
                for (int i = 0; i < 8; i++)
                {
                    PHOTO _newphoto4 = new PHOTO();
                    _newphoto4.PHOTOTYPE = "Stand Information";
                    _newphoto4.PHOTONUMBER = i + 10;
                    _newphoto4.AZIMUTH = 45 * i;
                    _newphoto4.DISTANCE = Constants.DefaultPhoto2Distance;
                    _newphoto4.PLOTID = _selectedplotid;
                    _newphoto4.Created = System.DateTime.UtcNow;
                    _newphoto4.LastModified = _newphoto4.Created;
                    _newphoto4.IsDeleted = "N";
                    _photoRepository.InsertPhoto(_newphoto4);
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
