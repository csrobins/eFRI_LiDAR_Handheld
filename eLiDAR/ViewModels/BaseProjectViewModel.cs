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
    public abstract class BaseProjectViewModel : INotifyPropertyChanged {

        public PROJECT _project;
        public INavigation _navigation;
        public IValidator _projectValidator;
        public IProjectRepository _projectRepository;
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
            get => _project.PROJECTID;
            set
            {
                _project.PROJECTID = value ;
                NotifyPropertyChanged("PROJECTID");
            }
        }
        public string NAME
        {
            get => _project.NAME;
            set{
                if (NAME != value) { _project.NAME = value; IsChanged = true; }

                NotifyPropertyChanged("NAME");
               
            }
        }  
        public string DESCRIPTION
        {
            get => _project.DESCRIPTION; 
            set {
                if (DESCRIPTION != value) { _project.DESCRIPTION = value; IsChanged = true; }

                NotifyPropertyChanged("DESCRIPTION");
               
            }
        } 
        public DateTime PROJECT_DATE
        {
            get => _project.PROJECT_DATE;
            set
            {
                if (!_project.PROJECT_DATE.Equals(value)) { _project.PROJECT_DATE = value; IsChanged = true; }

                NotifyPropertyChanged("PROJECT_DATE");
            }
        }

        List<PROJECT> _projectList;
        public List<PROJECT> ProjectList
        {
            get => _projectList;
            set
            {
                _projectList = value;
                NotifyPropertyChanged("ProjectList");
            }
        }
        protected virtual bool SetPropertyValue<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (value == null ? field != null : !value.Equals(field))
            {
                field = value;

                var handler = this.PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
                return true;
            }
            return false;
        }
        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = ""){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
