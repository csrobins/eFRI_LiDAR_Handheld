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
    public class BaseSmallTreeViewModel : INotifyPropertyChanged {

        public SMALLTREE _smallTree;
        public INavigation _navigation;
        public IValidator _soilValidator;
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
                _smallTree.SPECIESCODE = value;
                NotifyPropertyChanged("SPECIES");
                IsChanged = true;
            }
        }

        public int HT_CLASS1_COUNT
        {
            get => _smallTree.HT_CLASS1_COUNT;
            set
            {
                _smallTree.HT_CLASS1_COUNT = value;
                NotifyPropertyChanged("HT_CLASS1_COUNT");
                IsChanged = true;
            }
        }

        public int HT_CLASS2_COUNT
        {
            get => _smallTree.HT_CLASS2_COUNT;
            set
            {
                _smallTree.HT_CLASS2_COUNT = value;
                NotifyPropertyChanged("HT_CLASS2_COUNT");
                IsChanged = true;
            }
        }

        public int HT_CLASS3_COUNT
        {
            get => _smallTree.HT_CLASS3_COUNT;
            set
            {
                _smallTree.HT_CLASS3_COUNT = value;
                NotifyPropertyChanged("HT_CLASS3_COUNT");
                IsChanged = true;
            }
        }

        public int HT_CLASS4_COUNT
        {
            get => _smallTree.HT_CLASS4_COUNT;
            set
            {
                _smallTree.HT_CLASS4_COUNT = value;
                NotifyPropertyChanged("HT_CLASS4_COUNT");
                IsChanged = true;
            }
        }

        public int HT_CLASS5_COUNT
        {
            get => _smallTree.HT_CLASS5_COUNT;
            set
            {
                _smallTree.HT_CLASS5_COUNT = value;
                NotifyPropertyChanged("HT_CLASS5_COUNT");
                IsChanged = true;
            }
        }

        public int HT_CLASS6_COUNT
        {
            get => _smallTree.HT_CLASS6_COUNT;
            set
            {
                _smallTree.HT_CLASS6_COUNT = value;
                NotifyPropertyChanged("HT_CLASS6_COUNT");
                IsChanged = true;
            }
        }

        public int HT_CLASS7_COUNT
        {
            get => _smallTree.HT_CLASS7_COUNT;
            set
            {
                _smallTree.HT_CLASS7_COUNT = value;
                NotifyPropertyChanged("HT_CLASS7_COUNT");
                IsChanged = true;
            }
        }

        public int HT_CLASS8_COUNT
        {
            get => _smallTree.HT_CLASS8_COUNT;
            set
            {
                _smallTree.HT_CLASS8_COUNT = value;
                NotifyPropertyChanged("HT_CLASS8_COUNT");
                IsChanged = true;
            }
        }

        List<SMALLTREE> _smallTreeList;
        public List<SMALLTREE> SmallTreeList
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
