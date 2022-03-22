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
        private bool _IsChanged = false;
        public bool IsChanged
        {
            get => _IsChanged;
            set
            {
                _IsChanged = value;
            }
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
                IsChanged = true;
            }
        }

        public float FROM
        {
            get => _soil.DEPTHFROM;
            set
            {
                if (Math.Abs(FROM - value) >= 0.001) // Some threshold value suitable for your scenario
                {

                    _soil.DEPTHFROM = value;
                    NotifyPropertyChanged("FROM");
                    IsChanged = true;
                }
            }
        }
        private bool _isorganic; 
        public bool IsOrganic
        {
            get =>  _isorganic;
            set
            {
                _isorganic = value;
                IsNotOrganic = !value;
                NotifyPropertyChanged("IsOrganic");
            }
        }

        private bool _isnotorganic;
        public bool IsNotOrganic
        {
            get => _isnotorganic;
            set
            {
                _isnotorganic = value;
                NotifyPropertyChanged("IsNotOrganic");
            }
        }

        public float TO
        {
            get => _soil.DEPTHTO;
            set
            {
                if (Math.Abs(TO - value) >= 0.001) // Some threshold value suitable for your scenario
                {

                    _soil.DEPTHTO = value;
                    NotifyPropertyChanged("TO");
                    IsChanged = true;
                }
            }
        }
        public int HORIZONNUMBER
        {
            get => _soil.HORIZONNUMBER;
            set
            {
                _soil.HORIZONNUMBER = value;
                NotifyPropertyChanged("HORIZONNUMBER");
                IsChanged = true;
            }
        }
        public string HORIZON
        {
            get => _soil.HORIZON;
            set
            {
                _soil.HORIZON  = value;
                if (value == "Of" || value == "Of1" || value == "Of2" || value == "Of3" || value == "Of4" || value == "Om" || value == "Om1" || value == "Om2" || value == "Oh" || value == "Oh1" || value == "Oh2")
                {
                    IsOrganic = true;
                }
                else { IsOrganic = false; }
                NotifyPropertyChanged("HORIZON");
                IsChanged = true;
            }
        }

        public string VON_POST
        {
            get => _soil.DECOMPOSITIONCODE;
            set
            {
                _soil.DECOMPOSITIONCODE = value;
                NotifyPropertyChanged("VON_POST");
                IsChanged = true;
            }
        }

        public string TEXTURE
        {
            get => _soil.MINERALTEXTURECODE;
            set
            {
                _soil.MINERALTEXTURECODE = value;
                NotifyPropertyChanged("TEXTURE");
                IsChanged = true;
            }
        }

        public string PORE_PATTERN
        {
            get => _soil.POREPATTERNCODE;
            set
            {
                _soil.POREPATTERNCODE = value;
                NotifyPropertyChanged("PORE_PATTERN");
                IsChanged = true;
            }
        }

        public string STRUCTURE
        {
            get => _soil.STRUCTURE;
            set
            {
                _soil.STRUCTURE = value;
                NotifyPropertyChanged("STRUCTURE");
                IsChanged = true;
            }
        }

        public string COLOUR
        {
            get => _soil.MATRIXCOLOUR;
            set
            {
                _soil.MATRIXCOLOUR = value;
                NotifyPropertyChanged("COLOUR");
                IsChanged = true;
            }
        }
        public string GLEYCOLOUR
        {
            get => _soil.GLEYCOLOUR;
            set
            {
                _soil.GLEYCOLOUR = value;
                NotifyPropertyChanged("GLEYCOLOUR");
                IsChanged = true;
            }
        }
        public string MOTTLE_COLOUR
        {
            get => _soil.MOTTLECOLOUR;
            set
            {
                _soil.MOTTLECOLOUR = value;
                NotifyPropertyChanged("MOTTLE_COLOUR");
                IsChanged = true;
            }
        }

        public int PERCENT_GRAVEL
        {
            get => _soil.PERCENTGRAVEL;
            set
            {
                _soil.PERCENTGRAVEL = value;
                NotifyPropertyChanged("PERCENT_GRAVEL");
                IsChanged = true;
            }
        }

        public int PERCENT_COBBLE
        {
            get => _soil.PERCENTCOBBLE;
            set
            {
                _soil.PERCENTCOBBLE = value;
                NotifyPropertyChanged("PERCENT_COBBLE");
                IsChanged = true;
            }
        }

        public int PERCENT_STONE
        {
            get => _soil.PERCENTSTONE;
            set
            {
                _soil.PERCENTSTONE = value;
                NotifyPropertyChanged("PERCENT_STONE");
                IsChanged = true;
            }
        }
        public int ERRORCOUNT
        {
            get => _soil.ERRORCOUNT;
            set
            {
                _soil.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
                IsChanged = true;
            }
        }
        public string ERRORMSG
        {
            get => _soil.ERRORMSG;
            set
            {
                _soil.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
                IsChanged = true;
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
