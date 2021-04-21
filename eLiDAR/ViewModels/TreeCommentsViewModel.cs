using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Services;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using eLiDAR.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class TreeCommentsViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public TREE _tree;
        public TreeCommentsViewModel(INavigation navigation, TREE _thistree)
        {
            _navigation = navigation;
            _tree = _thistree;
        }
        public string CommentsTitle
        {
            get => "Comments for tree " + _tree.TREENUMBER;
            set
            {
            }
        }
        public string COMMENTS
        {
            get => _tree.COMMENTS;
            set
            {
                _tree.COMMENTS = value;
                NotifyPropertyChanged("COMMENTS");
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
