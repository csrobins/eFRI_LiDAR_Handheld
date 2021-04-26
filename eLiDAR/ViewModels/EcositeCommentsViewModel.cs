using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.Views;
using FluentValidation.Results;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using eLiDAR.Utilities;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace eLiDAR.ViewModels {
    public class EcositeCommentsViewModel : INotifyPropertyChanged  
    {
        public INavigation _navigation;
        public ECOSITE _ecosite;
        public EcositeCommentsViewModel(INavigation navigation, ECOSITE _thisecosite)
        {
            _navigation = navigation;
            _ecosite = _thisecosite;
        }
        public string CommentsTitle
        {
            get => "Comments for soil/site " ;
            set
            {
            }
        }
        public string SUBSTRATENOTE
        {
            get => _ecosite.SUBSTRATENOTE;
            set
            {
                _ecosite.SUBSTRATENOTE = value;
                NotifyPropertyChanged("SUBSTRATENOTE");
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
