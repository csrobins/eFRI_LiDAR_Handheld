using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;

namespace eLiDAR.ViewModels
{
    public class BaseSoilViewModel : INotifyPropertyChanged {

        public SOIL _soil;
        public INavigation _navigation;
        public IValidator _soilValidator;
        public ISoilRepository _soilRepository;
        public string _fk;
        public string _selectedid;


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

        public string SOILID
        {
            get => _soil.SOILID;
            set
            {
                _soil.SOILID = value;
                NotifyPropertyChanged("SOILID");
            }
        }

        public string PLOTID
        {
            get => _soil.PLOTID;
            set
            {
                _soil.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }

        public int LAYER
        {
            get => _soil.HORIZONNUMBER;
            set
            {
                _soil.HORIZONNUMBER = value;
                NotifyPropertyChanged("LAYER");
            }
        }

        public int FROM
        {
            get => _soil.DEPTHFROM;
            set
            {
                _soil.DEPTHFROM = value;
                NotifyPropertyChanged("FROM");
            }
        }

        public int TO
        {
            get => _soil.DEPTHTO;
            set
            {
                _soil.DEPTHTO = value;
                NotifyPropertyChanged("TO");
            }
        }

        public string HORIZON
        {
            get => _soil.HORIZON;
            set
            {
                _soil.HORIZON = value;
                NotifyPropertyChanged("HORIZON");
            }
        }

        public string VON_POST
        {
            get => _soil.DECOMPOSITIONCODE;
            set
            {
                _soil.DECOMPOSITIONCODE = value;
                NotifyPropertyChanged("VON_POST");
            }
        }

        public string TEXTURE
        {
            get => _soil.MINERALTEXTURECODE;
            set
            {
                _soil.MINERALTEXTURECODE = value;
                NotifyPropertyChanged("TEXTURE");
            }
        }

        public string PORE_PATTERN
        {
            get => _soil.POREPATTERNCODE;
            set
            {
                _soil.POREPATTERNCODE = value;
                NotifyPropertyChanged("PORE_PATTERN");
            }
        }

        public string STRUCTURE
        {
            get => _soil.STRUCTURE;
            set
            {
                _soil.STRUCTURE = value;
                NotifyPropertyChanged("STRUCTURE");
            }
        }

        public string COLOUR
        {
            get => _soil.MATRIXCOLOUR;
            set
            {
                _soil.MATRIXCOLOUR = value;
                NotifyPropertyChanged("COLOUR");
            }
        }

        public string MOTTLE_COLOUR
        {
            get => _soil.MOTTLECOLOUR;
            set
            {
                _soil.MOTTLECOLOUR = value;
                NotifyPropertyChanged("MOTTLE_COLOUR");
            }
        }

        public int PERCENT_GRAVEL
        {
            get => _soil.PERCENTGRAVEL;
            set
            {
                _soil.PERCENTGRAVEL = value;
                NotifyPropertyChanged("PERCENT_GRAVEL");
            }
        }

        public int PERCENT_COBBLE
        {
            get => _soil.PERCENTCOBBLE;
            set
            {
                _soil.PERCENTCOBBLE = value;
                NotifyPropertyChanged("PERCENT_COBBLE");
            }
        }

        public int PERCENT_STONE
        {
            get => _soil.PERCENTSTONE;
            set
            {
                _soil.PERCENTSTONE = value;
                NotifyPropertyChanged("PERCENT_STONE");
            }
        }

        List<SOIL> _soilList;
        public List<SOIL> SoilList
        {
            get => _soilList;
            set
            {
                _soilList = value;
                NotifyPropertyChanged("SoilMapList");
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
