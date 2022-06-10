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
    public class BasePersonViewModel : INotifyPropertyChanged {

        public PERSON _person;
        public INavigation _navigation;
        public IValidator _personValidator;
        public IPersonRepository _personRepository;
        public string _selectedprojectid;
        public string _selectedpersonid;
        private bool _IsChanged = false; 

        public bool IsChanged
        {
            get => _IsChanged;
            set
            {
                _IsChanged = value;
            }
        }
        public string PROJECTID
        {
            get => _person.PROJECTID;
            set
            {
                _person.PROJECTID = value ;
                NotifyPropertyChanged("PROJECTID");
            }
        }
        public string PERSONID
        {
            get => _person.PERSONID;
            set
            {
                _person.PERSONID = value;
          
                NotifyPropertyChanged("PERSONID");
            }
        }
        public string FIRSTNAME  
        {  
            get => _person.FIRSTNAME; 
            set{
                if (_person.FIRSTNAME != value) 
                {
                    _person.FIRSTNAME = value;
                    IsChanged = true; 
                }

               
                NotifyPropertyChanged("FIRSTNAME");
            }
        }  
        public string LASTNAME
        {
            get => _person.LASTNAME; 
            set {
                if (_person.LASTNAME != value)
                {
                    _person.LASTNAME = value;
                    IsChanged = true; 
                }

               
                NotifyPropertyChanged("LASTNAME");
            }
        } 
   
        List<PERSON> _personList;
        public List<PERSON> PersonList
        {
            get => _personList;
            set
            {
                _personList = value;
                NotifyPropertyChanged("PersonList");
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
