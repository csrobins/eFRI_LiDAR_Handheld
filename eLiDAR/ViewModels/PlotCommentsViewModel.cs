using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Servcies;
using eLiDAR.Services;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using eLiDAR.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class PlotCommentsViewModel : INotifyPropertyChanged
    {
        public INavigation _navigation;
        public PLOT _plot;
        public PlotCommentsViewModel(INavigation navigation, PLOT _thisplot)
        {
            _navigation = navigation;
            _plot = _thisplot;
        }
        public string CommentsTitle
        {
            get => "Comments for plot " + _plot.PLOTNUM;
            set
            {
            }
        }
        public string COMMENTS
        {
            get => _plot.COMMENTS;
            set
            {
                _plot.COMMENTS = value;
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
