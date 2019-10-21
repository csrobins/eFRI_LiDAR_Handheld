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
    public class BaseProjectViewModel : INotifyPropertyChanged {

        public PROJECT _project;
        public INavigation _navigation;
        public IValidator _projectValidator;
        public IProjectRepository _projectRepository;
      

        public string PROJECTID
        {
            get => _project.PROJECTID;
            set
            {
                _project.PROJECTID = value ;
                NotifyPropertyChanged("PROJECTID");
            }
        }
        public string PROJECT  
        {  
            get => _project.NAME; 
            set{
                _project.NAME = value;
                NotifyPropertyChanged("NAME");
            }
        }  
        public string DESCRIPTION
        {
            get => _project.DESCRIPTION; 
            set { 
                _project.DESCRIPTION = value; 
                NotifyPropertyChanged("DESCRIPTION");
            }
        } 
        public DateTime PROJECT_DATE
        {
            get => _project.PROJECT_DATE; 
            set { 
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
