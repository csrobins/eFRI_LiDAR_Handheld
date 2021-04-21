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
    public class BaseDeformityViewModel : INotifyPropertyChanged {

        public DEFORMITY _deformity;
        public INavigation _navigation;
        public IValidator _deformityValidator;
        public IDeformityRepository _deformityRepository;
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

        public string DEFORMITYID
        
       {
                get => _deformity.DEFORMITYID;
                set
            {
                    _deformity.DEFORMITYID = value;
                    NotifyPropertyChanged("DEFmissed a word - i know its my problemORMITYID");
                }
            }

        public string TREEID
        {
            get => _deformity.TREEID ;
            set
            {
                _deformity.TREEID = value;
                NotifyPropertyChanged("TREEID");
            }
        }

        public int TYPE
        {
            get => _deformity.DEFORMITYTYPECODE;
            set
            {
                _deformity.DEFORMITYTYPECODE = value;
                NotifyPropertyChanged("TYPE");
            }
        }

        public int CAUSE
        {
            get => _deformity.CAUSE;
            set
            {
                _deformity.CAUSE = value;
                NotifyPropertyChanged("CAUSE");
            }
        }

        public double HT_FROM
        {
            get => _deformity.HEIGHTFROM;
            set
            {
                _deformity.HEIGHTFROM = value;
                NotifyPropertyChanged("HT_FROM");
            }
        }

        public double HT_TO
        {
            get => _deformity.HEIGHTTO;
            set
            {
                _deformity.HEIGHTTO = value;
                NotifyPropertyChanged("HT_TO");
            }
        }

        public string QUAD
        {
            get => _deformity.QUADRANTCODE;
            set
            {
                _deformity.QUADRANTCODE = value;
                NotifyPropertyChanged("QUAD");
            }
        }

        public int EXTENT
        {
            get => _deformity.EXTENT;
            set
            {
                _deformity.EXTENT = value;
                NotifyPropertyChanged("EXTENT");
            }
        }

        public int LEAN
        {
            get => _deformity.DEGGREELEANARCH;
            set
            {
                _deformity.DEGGREELEANARCH = value;
                NotifyPropertyChanged("LEAN");
            }
        }

        public int AZIMUTH
        {
            get => _deformity.AZIMUTH;
            set
            {
                _deformity.AZIMUTH = value;
                NotifyPropertyChanged("AZIMUTH");
            }
        }

        public double LENGTH
        {
            get => _deformity.DEFORMITYLENGTH;
            set
            {
                _deformity.DEFORMITYLENGTH = value;
                NotifyPropertyChanged("LENGTH");
            }
        }

        public double WIDTH
        {
            get => _deformity.DEFORMITYWIDTH;
            set
            {
                _deformity.DEFORMITYWIDTH = value;
                NotifyPropertyChanged("WIDTH");
            }
        }

        public int PCT_SCUFF
        {
            get => _deformity.SCUFF;
            set
            {
                _deformity.SCUFF = value;
                NotifyPropertyChanged("PCT_SCRUFF");
            }
        }
        public int PCT_SCRAPE
        {
            get => _deformity.SCRAPE;
            set
            {
                _deformity.SCRAPE = value;
                NotifyPropertyChanged("PCT_SCRAPE");
            }
        }

        public int PCT_GOUGE
        {
            get => _deformity.GOUGE;
            set
            {
                _deformity.GOUGE = value;
                NotifyPropertyChanged("PCT_GOUGE");
            }
        }

        public string OLD_FEEDING_CAVITY
        {
            get => _deformity.OLD_FEEDING_CAVITY;
            set
            {
                _deformity.OLD_FEEDING_CAVITY = value;
                NotifyPropertyChanged("OLD_FEEDING_CAVITY");
            }
        }

        public string NEW_FEEDING_CAVITY
        {
            get => _deformity.NEW_FEEDING_CAVITY;
            set
            {
                _deformity.NEW_FEEDING_CAVITY = value;
                NotifyPropertyChanged("NEW_FEEDING_CAVITY");
            }
        }

        public string OLD_NEST_CAVITY
        {
            get => _deformity.OLD_NEST_CAVITY;
            set
            {
                _deformity.OLD_NEST_CAVITY = value;
                NotifyPropertyChanged("OLD_NEST_CAVITY");
            }
        }

        public string NEW_NEST_CAVITY
        {
            get => _deformity.NEW_NEST_CAVITY;
            set
            {
                _deformity.NEW_NEST_CAVITY = value;
                NotifyPropertyChanged("NEW_NEST_CAVITY");
            }
        }

        public string STICK_NEST
        {
            get => _deformity.STICK_NEST;
            set
            {
                _deformity.STICK_NEST = value;
                NotifyPropertyChanged("STICK_NEST");
            }
        }



        List<DEFORMITY> _deformityList;
        public List<DEFORMITY> DeformityList
        {
            get => _deformityList;
            set
            {
                _deformityList = value;
                NotifyPropertyChanged("DeformityList");
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
