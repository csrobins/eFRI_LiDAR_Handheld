using System;
using eLiDAR.Helpers;
using eLiDAR.Models;
using eLiDAR.ViewModels;
using Xamarin.Forms;

namespace eLiDAR.Views {
    public partial class TreeList : ContentPage {
        public TreeList() {
            InitializeComponent();
            this.BindingContext = new TreeListViewModel(Navigation);
        }

        public TreeList(string plotID)
        {
            InitializeComponent();
            this.BindingContext = new TreeListViewModel(Navigation, plotID);
        } 
        }
    }
