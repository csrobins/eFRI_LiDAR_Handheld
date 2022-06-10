using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using Xamarin.Forms;
using eLiDAR.Validator;
using FluentValidation.Results;

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
            else
            {
                IsChanged = true;
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
                SoilValidator _fullvalidator = new SoilValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_soil);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);
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
                if (!_soil.HORIZONNUMBER.Equals(value)) { _soil.HORIZONNUMBER = value; IsChanged = true; }

                NotifyPropertyChanged("LAYER");
            }
        }

        public float FROM
        {
            get => _soil.DEPTHFROM;
            set
            {
                if (Math.Abs(FROM - value) >= 0.001) // Some threshold value suitable for your scenario
                {
                    if (!_soil.DEPTHFROM.Equals(value)) { _soil.DEPTHFROM = value; IsChanged = true; }

                    NotifyPropertyChanged("FROM");
                   
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
                    if (!_soil.DEPTHTO.Equals(value)) { _soil.DEPTHTO = value; IsChanged = true; }

                    NotifyPropertyChanged("TO");
                  
                }
            }
        }
        public int HORIZONNUMBER
        {
            get => _soil.HORIZONNUMBER;
            set
            {
                if (!_soil.HORIZONNUMBER.Equals(value)) { _soil.HORIZONNUMBER = value; IsChanged = true; }

                NotifyPropertyChanged("HORIZONNUMBER");
               
            }
        }
        public string HORIZON
        {
            get => _soil.HORIZON;
            set
            {
                if ( _soil.HORIZON != value) { IsChanged = true; }
                _soil.HORIZON  = value;
                if (value == "Of" || value == "Of1" || value == "Of2" || value == "Of3" || value == "Of4" || value == "Om" || value == "Om1" || value == "Om2" || value == "Oh" || value == "Oh1" || value == "Oh2")
                {
                    IsOrganic = true;
                }
                else { IsOrganic = false; }
                NotifyPropertyChanged("HORIZON");
                
            }
        }

        public string VON_POST
        {
            get => _soil.DECOMPOSITIONCODE;
            set
            {
                if (_soil.DECOMPOSITIONCODE != value) { IsChanged = true; }
                _soil.DECOMPOSITIONCODE = value;
                NotifyPropertyChanged("VON_POST");
                
            }
        }

        public string TEXTURE
        {
            get => _soil.MINERALTEXTURECODE;
            set
            {
                if (_soil.MINERALTEXTURECODE !=value) { IsChanged = true; }
                _soil.MINERALTEXTURECODE = value;
                NotifyPropertyChanged("TEXTURE");
         
            }
        }

        public string PORE_PATTERN
        {
            get => _soil.POREPATTERNCODE;
            set
            {
                if (_soil.POREPATTERNCODE != value) { IsChanged = true; }
                _soil.POREPATTERNCODE = value;
                NotifyPropertyChanged("PORE_PATTERN");
       
            }
        }

        public string STRUCTURE
        {
            get => _soil.STRUCTURE;
            set
            {
                if (_soil.STRUCTURE != value) { IsChanged = true; }
                _soil.STRUCTURE = value;
                NotifyPropertyChanged("STRUCTURE");
                
            }
        }

        public string COLOUR
        {
            get => _soil.MATRIXCOLOUR;
            set
            {
                if (_soil.MATRIXCOLOUR !=value) { IsChanged = true; }
                _soil.MATRIXCOLOUR = value;
                NotifyPropertyChanged("COLOUR");
             
            }
        }
        public string GLEYCOLOUR
        {
            get => _soil.GLEYCOLOUR;
            set
            {
                if (_soil.GLEYCOLOUR !=value) { IsChanged = true; }
                _soil.GLEYCOLOUR = value;
                NotifyPropertyChanged("GLEYCOLOUR");
              
            }
        }
        public string MOTTLE_COLOUR
        {
            get => _soil.MOTTLECOLOUR;
            set
            {
                if (_soil.MOTTLECOLOUR !=value) { IsChanged = true; }
                _soil.MOTTLECOLOUR = value;
                NotifyPropertyChanged("MOTTLE_COLOUR");
              
            }
        }

        public int PERCENT_GRAVEL
        {
            get => _soil.PERCENTGRAVEL;
            set
            {
                if (!_soil.PERCENTGRAVEL.Equals(value)) { _soil.PERCENTGRAVEL = value; IsChanged = true; }

                NotifyPropertyChanged("PERCENT_GRAVEL");
              
            }
        }

        public int PERCENT_COBBLE
        {
            get => _soil.PERCENTCOBBLE;
            set
            {
                if (!_soil.PERCENTCOBBLE.Equals(value)) { _soil.PERCENTCOBBLE = value; IsChanged = true; }

                NotifyPropertyChanged("PERCENT_COBBLE");
             
            }
        }

        public int PERCENT_STONE
        {
            get => _soil.PERCENTSTONE;
            set
            {
                if (!_soil.PERCENTSTONE.Equals(value)) { _soil.PERCENTSTONE = value; IsChanged = true; }

                NotifyPropertyChanged("PERCENT_STONE");
           
            }
        }
        public int ERRORCOUNT
        {
            get => _soil.ERRORCOUNT;
            set
            {
               
                _soil.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");
           
            }
        }
        public string ERRORMSG
        {
            get => _soil.ERRORMSG;
            set
            {
                _soil.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
           
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
