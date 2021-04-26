using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using System.Linq;

namespace eLiDAR.ViewModels
{
    public class BaseVegetationCensusViewModel : INotifyPropertyChanged {

        public VEGETATIONCENSUS _vegetation;
        public INavigation _navigation;
        public IValidator _soilValidator;
        public IVegetationCensusRepository _vegetationCensusRepository;
        public string _fk;
        public List<PickerItemsString> ListVeg  = PickerService.VegItems().ToList();
        private string _getscientific;

        public event PropertyChangedEventHandler PropertyChanged2;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyname = null)
        {
            PropertyChanged2?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }
        protected bool SetProperty<T>(ref T backfield, T value, [CallerMemberName]string propertyName =null)
        {
            if (EqualityComparer<T>.Default.Equals(backfield, value))
            {
                return false;
            }
            backfield = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    
    private bool _IsChanged = false;
    public bool IsChanged
    {
        get => _IsChanged;
        set
        {
            _IsChanged = value;
        }
    }
    public string VEGETATIONCENSUSID
        {
            get => _vegetation.VEGETATIONCENSUSID;
            set
            {
                _vegetation.VEGETATIONCENSUSID = value;
                NotifyPropertyChanged("VEGETATIONCENSUSID");
            }
        }
        public string PLOTID
        {
            get => _vegetation.PLOTID;
            set
            {
                _vegetation.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }
        public string SPECIES
        {
            get => _vegetation.VSNSPECIESCODE;
            set
            {
                _vegetation.VSNSPECIESCODE = value;
               NotifyPropertyChanged("SPECIES");
                SetScientific();
                IsChanged = true;
            }
        }
        public int SPECIMENNUMBER
        {
            get => _vegetation.SPECIMENNUMBER;
            set
            {
                _vegetation.SPECIMENNUMBER = value;
                NotifyPropertyChanged("SPECIMENNUMBER");
                IsChanged = true;
            }
        }


        void SetScientific()
        {
           GetScientific = PickerService.GetItem(ListVeg, SPECIES).NAME;
        }
        public string GetScientific
        {
            get
            {
                return _getscientific;
            }
            set {
                _getscientific = value;
                NotifyPropertyChanged("GetScientific");
            }
            
        }
    
 

        List<VEGETATIONCENSUS> _vegetationList;
        public List<VEGETATIONCENSUS> VegetationList
        {
            get => _vegetationList;
            set
            {
                _vegetationList = value;
                NotifyPropertyChanged("VegetationList");
            }
        }

        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = ""){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
