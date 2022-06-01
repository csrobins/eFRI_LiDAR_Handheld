using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eLiDAR.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace eLiDAR.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SmallTreeTallyList : ContentPage
    {
        public SmallTreeTallyList(string plotID)
        {
            InitializeComponent();
            _viewmodel = new SmallTreeTallyListViewModel(Navigation, plotID);
            this.BindingContext = _viewmodel;
            //       MyListView.ItemsSource = null;
            //      _viewmodel.FetchSmallTree();
            //     MyListView.ItemsSource = _viewmodel.SmallTreeList;

        }
        private SmallTreeTallyListViewModel _viewmodel;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            _viewmodel.FetchSmallTreeTally();
        }


    }
}