using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using eLiDAR.Models;

namespace eLiDAR.Utilities
{
    public class Utils
    {
        

        public Guid getGUID()
        {
            // Create and display the value of two GUIDs.
            return Guid.NewGuid();
        }
        //bool IsIndexed;
    }

    internal class PickerCell : ViewCell
    {
        private Label _label { get; set; }

        private View _picker { get; set; }

        private Grid _base;

        internal string Label
        {
            get
            {
                return _label.Text;
            }
            set
            {
                _label.Text = value;
            }
        }

        internal View Picker
        {
            set
            {
                //Remove picker if it exists
                if (_picker != null)
                {
                    _base.Children.Remove(_picker);
                }

                //Set its value
                _picker = value;
                //Add to layout
                _base.Children.Add(_picker, 1, 0);

            }
        }

        internal PickerCell()
        {
            _label = new Label()
            {
                VerticalOptions = LayoutOptions.Center
            };

            _base = new Grid()
            {
                ColumnDefinitions = new ColumnDefinitionCollection() {
                new ColumnDefinition () { Width = new GridLength (2, GridUnitType.Star) },
                new ColumnDefinition () { Width = new GridLength (8, GridUnitType.Star) }
            },
                Padding = 15
            };

            _base.Children.Add(_label, 0, 0);

            this.View = _base;
        }
    }
   

}
