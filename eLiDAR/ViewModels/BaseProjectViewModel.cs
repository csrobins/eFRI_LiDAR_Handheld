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
    public class BaseProjectViewModel : INotifyPropertyChanged {

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
                if (NAME != value) { IsChanged = true; }
                _project.NAME = value;
                NotifyPropertyChanged("NAME");
               
            }
        }  
        public string DESCRIPTION
        {
            get => _project.DESCRIPTION; 
            set {
                if (DESCRIPTION != value) { IsChanged = true; }
                _project.DESCRIPTION = value; 
                NotifyPropertyChanged("DESCRIPTION");
               
            }
        } 
        public DateTime PROJECT_DATE
        {
            get => _project.PROJECT_DATE; 
            set {
                if (PROJECT_DATE != value) { IsChanged = true; }
                _project.PROJECT_DATE = value; 
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

        #region INotifyPropertyChanged    
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = ""){
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
