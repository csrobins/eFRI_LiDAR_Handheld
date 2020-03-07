using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FluentValidation;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using Xamarin.Forms;

namespace eLiDAR.ViewModels
{
    public class BaseTreeViewModel : INotifyPropertyChanged {

        public TREE _tree;
        public INavigation _navigation;
        public IValidator _treeValidator;
        public ITreeRepository _treeRepository;
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
        public string TREEID
        {
            get => _tree.TREEID;
            set
            {
                _tree.TREEID = value;
                NotifyPropertyChanged("TREEID");
            }
        }

        public string PLOTID
        {
            get => _tree.PLOTID;
            set
            {
                _tree.PLOTID = value;
                NotifyPropertyChanged("PLOTID");
            }
        }

        public int TREENUM
        {
            get => _tree.TREENUM;
            set
            {
                _tree.TREENUM = value;
                NotifyPropertyChanged("TREENUM");
            }
        }

        public int SPECIES
        {
            get => _tree.SPECIES;
            set
            {
                _tree.SPECIES = value;
                NotifyPropertyChanged("SPECIES");
            }
        }

        public string TAG_TYPE
        {
            get => _tree.TAG_TYPE;
            set
            {
                _tree.TAG_TYPE = value;
                NotifyPropertyChanged("TAG_TYPE");
            }
        }

        public string ORIGIN
        {
            get => _tree.ORIGIN;
            set
            {
                _tree.ORIGIN = value;
                NotifyPropertyChanged("ORIGIN");
            }
        }

        public string STATUS
        {
            get => _tree.STATUS;
            set
            {
                _tree.STATUS = value;
                NotifyPropertyChanged("STATUS");
            }
        }

        public int VIGOUR
        {
            get => _tree.VIGOUR;
            set
            {
                _tree.VIGOUR = value;
                NotifyPropertyChanged("VIGOUR");
            }
        }

        public float HT_TO_DBH
        {
            get => _tree.HT_TO_DBH;
            set
            {
                _tree.HT_TO_DBH = value;
                NotifyPropertyChanged("HT_TO_DBH");
            }
        }

        public float DBH
        {
            get => _tree.DBH;
            set
            {
                _tree.DBH = value;
                NotifyPropertyChanged("DBH");
            }
        }

        public float HT
        {
            get => _tree.HT;
            set
            {
                _tree.HT = value;
                NotifyPropertyChanged("HT");
            }
        }

        public String DBH_IN
        {
            get => _tree.DBH_IN;
            set
            {
                _tree.DBH_IN = value;
                NotifyPropertyChanged("DBH_IN");
            }
        }

        public String CROWN_IN
        {
            get => _tree.CROWN_IN;
            set
            {
                _tree.CROWN_IN = value;
                NotifyPropertyChanged("CROWN_IN");
            }
        }

        public int LIVE_CROWN_RATIO
        {
            get => _tree.LIVE_CROWN_RATIO;
            set
            {
                _tree.LIVE_CROWN_RATIO = value;
                NotifyPropertyChanged("LIVE_CROWN_RATIO");
            }
        }

        public string CROWN_CLASS
        {
            get => _tree.CROWN_CLASS;
            set
            {
                _tree.CROWN_CLASS = value;
                NotifyPropertyChanged("CROWN_CLASS");
            }
        }

        public int CROWN_DAMAGE
        {
            get => _tree.CROWN_DAMAGE;
            set
            {
                _tree.CROWN_DAMAGE = value;
                NotifyPropertyChanged("CROWN_DAMAGE");
            }
        }

        public int DEFOLIATING_INSECT
        {
            get => _tree.DEFOLIATING_INSECT;
            set
            {
                _tree.DEFOLIATING_INSECT = value;
                NotifyPropertyChanged("DEFOLIATING_INSECT");
            }
        }

        public int FOLIAR_DISEASE
        {
            get => _tree.FOLIAR_DISEASE;
            set
            {
                _tree.FOLIAR_DISEASE = value;
                NotifyPropertyChanged("FOLIAR_DISEASE");
            }
        }

        public string STEM_QUALITY
        {
            get => _tree.STEM_QUALITY;
            set
            {
                _tree.STEM_QUALITY = value;
                NotifyPropertyChanged("STEM_QUALITY");
            }
        }

        public int BARK_RETENTION
        {
            get => _tree.BARK_RETENTION;
            set
            {
                _tree.BARK_RETENTION = value;
                NotifyPropertyChanged("BARK_RETENTION");
            }
        }

        public int WOOD_CONDITION
        {
            get => _tree.WOOD_CONDITION;
            set
            {
                _tree.WOOD_CONDITION = value;
                NotifyPropertyChanged("WOOD_CONDITION");
            }
        }

        public int DECAY_CLASS
        {
            get => _tree.DECAY_CLASS;
            set
            {
                _tree.DECAY_CLASS = value;
                NotifyPropertyChanged("DECAY_CLASS");
            }
        }

        public int MORT_CAUSE
        {
            get => _tree.MORT_CAUSE;
            set
            {
                _tree.MORT_CAUSE = value;
                NotifyPropertyChanged("MORT_CAUSE");
            }
        }

        public String BROKEN_TOP
        {
            get => _tree.BROKEN_TOP;
            set
            {
                _tree.BROKEN_TOP = value;
                NotifyPropertyChanged("BROKEN_TOP");
            }
        }

        public int AGE
        {
            get => _tree.AGE;
            set
            {
                _tree.AGE = value;
                NotifyPropertyChanged("AGE");
            }
        }

        public double AZIMUTH
        {
            get => _tree.AZIMUTH;
            set
            {
                _tree.AZIMUTH = value;
                NotifyPropertyChanged("AZIMUTH");
            }
        }

        public double DISTANCE
        {
            get => _tree.DISTANCE;
            set
            {
                _tree.DISTANCE = value;
                NotifyPropertyChanged("DISTANCE");
            }
        }

        public double CROWN_WIDTH1
        {
            get => _tree.CROWN_WIDTH1;
            set
            {
                _tree.CROWN_WIDTH1 = value;
                NotifyPropertyChanged("CROWN_WIDTH1");
            }
        }

        public double CROWN_WIDTH2
        {
            get => _tree.CROWN_WIDTH2;
            set
            {
                _tree.CROWN_WIDTH2 = value;
                NotifyPropertyChanged("CROWN_WIDTH2");
            }
        }
        List<TREE> _treeListFull;
        public List<TREE> TreeListFull
        {
            //get => _treeList;
            get
            {
                if (_fk == "")
                    return _treeRepository.GetAllData();
                else
                    return _treeRepository.GetFilteredData(_fk);
            }
            set
            {
                _treeListFull = value;
                NotifyPropertyChanged("TreeListFull");
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
