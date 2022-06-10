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
    internal class BaseSmallTreeTallyViewModel : INotifyPropertyChanged
    {

        public SMALLTREETALLY _smallTreeTally;
        public INavigation _navigation;
        public ISmallTreeTallyRepository _smallTreeTallyRepository;
        public string _fk;

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
                SmallTreeTallyValidator _fullvalidator = new SmallTreeTallyValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_smallTreeTally);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);
            }
        }
        public string SMALLTREEID
        {
            get => _smallTreeTally.SMALLTREETALLYID;
            set
            {
                _smallTreeTally.SMALLTREETALLYID = value;
                NotifyPropertyChanged("SMALLTREETALLYID");
            }
        }

        public string PLOTID
        {
            get => _smallTreeTally.PLOTID;
            set
            {
                _smallTreeTally.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }

        public int SPECIES
        {
            get => _smallTreeTally.SPECIESCODE;
            set
            {
                if (!_smallTreeTally.SPECIESCODE.Equals(value)) { IsChanged = true; }
                _smallTreeTally.SPECIESCODE = value;
                NotifyPropertyChanged("SPECIES");
               
            }
        }

        
        public int COUNT
        {
            get => _smallTreeTally.COUNT;
            set
            {
                if (!_smallTreeTally.COUNT.Equals(value)) { _smallTreeTally.COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("COUNT");
               
            }
        }
        public double HEIGHT
        {
            get => _smallTreeTally.HEIGHT;
            set
            {
                if (!_smallTreeTally.HEIGHT.Equals(value)) { _smallTreeTally.HEIGHT = value; IsChanged = true; }

                NotifyPropertyChanged("HEIGHT");
               
            }
        }
        public double DBH
        {
            get => _smallTreeTally.DBH;
            set
            {
                if (!_smallTreeTally.DBH.Equals(value)) { _smallTreeTally.DBH = value; IsChanged = true; }

                NotifyPropertyChanged("DBH");
              
            }
        }

        public int ERRORCOUNT
        {
            get => _smallTreeTally.ERRORCOUNT;
            set
            {
                _smallTreeTally.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");

            }
        }
        public string ERRORMSG
        {
            get => _smallTreeTally.ERRORMSG;
            set
            {
                _smallTreeTally.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");

            }
        }

        List<SMALLTREETALLYLIST> _smallTreeTallyList;
        public List<SMALLTREETALLYLIST> SmallTreeTallyList
        {
            get => _smallTreeTallyList;
            set
            {
                _smallTreeTallyList = value;
                NotifyPropertyChanged("SmallTreeTallyList");
            }
        }

        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
