﻿using System;
using System.Linq;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace eLiDAR.Views {
    public partial class TreeListPage : ContentPage {
        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                // Do your thing
                //this.BindingContext.  
                MyListView.ItemsSource = null;
                _viewmodel.FetchTrees();
                MyListView.ItemsSource = _viewmodel.TreeStemListFull;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        private TreeListViewModel _viewmodel;


        public TreeListPage(string plotID)
        {
            try
            {
                InitializeComponent();
                _viewmodel = new TreeListViewModel(Navigation, plotID);

                //this.BindingContext = new TreeListViewModel(Navigation, plotID);
                this.BindingContext = _viewmodel;
            }
            catch (Exception e)
            {
                var myerror = e.Message; // error
                                         //  Log.Fatal(e);
            };
        }
        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            MyListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                MyListView.ItemsSource = _viewmodel.TreeStemListFull;
            else
                MyListView.ItemsSource = _viewmodel.TreeStemListFull.Where(i => i.TREENUMBER.Equals(Int32.Parse(e.NewTextValue)));

            MyListView.EndRefresh();
        }
    }
    }
