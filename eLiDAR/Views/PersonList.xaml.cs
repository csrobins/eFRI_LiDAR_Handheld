using System;
using System.Linq;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class PersonList : ContentPage {
        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Do your thing
            //this.BindingContext.  
            MyListView.ItemsSource = null;
            _viewmodel.Fetch();

            MyListView.ItemsSource = _viewmodel.PersonList;
        }
        private PersonListViewModel _viewmodel;
     
        public PersonList(string projectID)
        {
            InitializeComponent();
            _viewmodel = new PersonListViewModel(Navigation, projectID);
            this.BindingContext = _viewmodel;
            _viewmodel.Fetch();

        }
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            MyListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                MyListView.ItemsSource = _viewmodel.PersonList;
            else
                MyListView.ItemsSource = _viewmodel.PersonList.Where(i => i.LASTNAME.ToUpper().Contains(e.NewTextValue.ToUpper() ));

            MyListView.EndRefresh();
        }
    }
    }
