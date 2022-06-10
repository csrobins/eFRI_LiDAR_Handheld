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
    public class BaseSmallTreeViewModel : INotifyPropertyChanged {

        public SMALLTREE _smallTree;
        public INavigation _navigation;
        public ISmallTreeRepository _smallTreeRepository;
        public string _fk;

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
                SmallTreeValidator _fullvalidator = new SmallTreeValidator(true);
                ValidationResult fullvalidationResults = _fullvalidator.Validate(_smallTree);
                ParseValidater _parser = new ParseValidater();
                (ERRORCOUNT, ERRORMSG) = _parser.Parse(fullvalidationResults);
            }
        }
        public string SMALLTREEID
        {
            get => _smallTree.SMALLTREEID;
            set
            {
                _smallTree.SMALLTREEID = value;
                NotifyPropertyChanged("SMALLTREEID");
            }
        }

        public string PLOTID
        {
            get => _smallTree.PLOTID;
            set
            {
                _smallTree.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }

        public int SPECIES
        {
            get => _smallTree.SPECIESCODE;
            set
            {
                if (!_smallTree.SPECIESCODE.Equals(value)) { IsChanged = true; }
                _smallTree.SPECIESCODE = value;
                NotifyPropertyChanged("SPECIES");
               
            }
        }

        public int HT_CLASS1_COUNT
        {
            get => _smallTree.HT_CLASS1_COUNT;
            set
            {
                if (!_smallTree.HT_CLASS1_COUNT.Equals(value)) { _smallTree.HT_CLASS1_COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("HT_CLASS1_COUNT");
                
            }
        }

        public int HT_CLASS2_COUNT
        {
            get => _smallTree.HT_CLASS2_COUNT;
            set
            {
                if (!_smallTree.HT_CLASS2_COUNT.Equals(value)) { _smallTree.HT_CLASS2_COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("HT_CLASS2_COUNT");
                
            }
        }

        public int HT_CLASS3_COUNT
        {
            get => _smallTree.HT_CLASS3_COUNT;
            set
            {
                if (!_smallTree.HT_CLASS3_COUNT.Equals(value)) { _smallTree.HT_CLASS3_COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("HT_CLASS3_COUNT");
               
            }
        }

        public int HT_CLASS4_COUNT
        {
            get => _smallTree.HT_CLASS4_COUNT;
            set
            {
                if (!_smallTree.HT_CLASS4_COUNT.Equals(value)) { _smallTree.HT_CLASS4_COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("HT_CLASS4_COUNT");
                
            }
        }

        public int HT_CLASS5_COUNT
        {
            get => _smallTree.HT_CLASS5_COUNT;
            set
            {
                if (!_smallTree.HT_CLASS5_COUNT.Equals(value)) { _smallTree.HT_CLASS5_COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("HT_CLASS5_COUNT");
                
            }
        }

        public int HT_CLASS6_COUNT
        {
            get => _smallTree.HT_CLASS6_COUNT;
            set
            {
                if (!_smallTree.HT_CLASS6_COUNT.Equals(value)) { _smallTree.HT_CLASS6_COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("HT_CLASS6_COUNT");
                
            }
        }

        public int HT_CLASS7_COUNT
        {
            get => _smallTree.HT_CLASS7_COUNT;
            set
            {
                if (!_smallTree.HT_CLASS7_COUNT.Equals(value)) { _smallTree.HT_CLASS7_COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("HT_CLASS7_COUNT");
                
            }
        }

        public int HT_CLASS8_COUNT
        {
            get => _smallTree.HT_CLASS8_COUNT;
            set
            {
                if (!_smallTree.HT_CLASS8_COUNT.Equals(value)) { _smallTree.HT_CLASS8_COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("HT_CLASS8_COUNT");
                
            }
        }
        public int COUNT
        {
            get => _smallTree.COUNT;
            set
            {
                if (!_smallTree.COUNT.Equals(value)) { _smallTree.COUNT = value; IsChanged = true; }

                NotifyPropertyChanged("COUNT");
                
            }
        }
        public double HEIGHT
        {
            get => _smallTree.HEIGHT;
            set
            {
                if (!_smallTree.HEIGHT.Equals(value)) { _smallTree.HEIGHT = value; IsChanged = true; }

                NotifyPropertyChanged("HEIGHT");
                
            }
        }

        public int ERRORCOUNT
        {
            get => _smallTree.ERRORCOUNT;
            set
            {
                _smallTree.ERRORCOUNT = value;
                NotifyPropertyChanged("ERRORCOUNT");

            }
        }
        public string ERRORMSG
        {
            get => _smallTree.ERRORMSG;
            set
            {
                _smallTree.ERRORMSG = value;
                NotifyPropertyChanged("ERRORMSG");
     
            }
        }

        List<SMALLTREELIST> _smallTreeList;
        public List<SMALLTREELIST> SmallTreeList
        {
            get => _smallTreeList;
            set
            {
                _smallTreeList = value;
                NotifyPropertyChanged("SmallTreeList");
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
