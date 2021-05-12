using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using eLiDAR.Models;
using Xamarin.Essentials; 

namespace eLiDAR.Utilities
{
    public class Utils
    {
        private bool _borealspecies { get; set; }
        private string keyvalboreal = "UseBorealSpecies";
        private string keyvaldevicetheme = "UseDeviceTheme";
        private string keyvaldarktheme = "UseDarkTheme";
        private string keynotifysave = "IsNotifySave";
        private string keyallowplotdeletion = "IsAllowPlotDeletion";
        private string keyallowprojectdeletion = "IsAllowProjectDeletion";
        private string keydopartialsynch = "DoPartialSynch";
        private string keyallowautonumber = "AllowAutoNumber";
        private string keynumericlist = "UseNumericList";
        private string keyalphalist = "UseAlphaList";
        private string keyerrorlist = "ErrorList";

        public Guid getGUID()
        {
            // Create and display the value of two GUIDs.
            return Guid.NewGuid();
        }
        internal string ErrorList
        {
            get
            {
                return Preferences.Get(keyerrorlist, "");
            }
            set
            {
                Preferences.Set(keyerrorlist, value);
            }
        }

        internal bool DoPartialSynch
        {
            get
            {
                return Preferences.Get(keydopartialsynch, false);
            }
            set
            {
                Preferences.Set(keydopartialsynch, value);
            }
        }
        internal bool UseNumericList
        {
            get
            {
                return Preferences.Get(keynumericlist, false);
            }
            set
            {
                Preferences.Set(keynumericlist, value);
            }
        }
        internal bool UseAlphaList
        {
            get
            {
                return Preferences.Get(keyalphalist, false);
            }
            set
            {
                Preferences.Set(keyalphalist, value);
            }
        }
        internal bool AllowAutoNumber
        {
            get
            {
                return Preferences.Get(keyallowautonumber, true);
            }
            set
            {
                Preferences.Set(keyallowautonumber, value);
            }
        }

        internal bool AllowPlotDeletion
        {
            get
            {
                return Preferences.Get(keyallowplotdeletion, false);
            }
            set
            {
                Preferences.Set(keyallowplotdeletion, value);
            }
        }
        internal bool AllowProjectDeletion
        {
            get
            {
                return Preferences.Get(keyallowprojectdeletion, false);
            }
            set
            {
                Preferences.Set(keyallowprojectdeletion, value);
            }
        }

        internal bool BorealSpecies
        {
            get
            {
                return Preferences.Get(keyvalboreal, false); 
            }
            set 
            {
                Preferences.Set(keyvalboreal, value);
            }
        }
        internal bool NotifySave
        {
            get
            {
                return Preferences.Get(keynotifysave, false);
            }
            set
            {
                Preferences.Set(keynotifysave, value);
            }
        }
        internal bool DeviceTheme
        {
            get
            {
                return Preferences.Get(keyvaldevicetheme, false);
            }
            set
            {
                Preferences.Set(keyvaldevicetheme, value);
                if (value)
                {
                    DarkTheme = false;
                }
            }
        }
        internal bool DarkTheme
        {
            get
            {
                return Preferences.Get(keyvaldarktheme, false);
            }
            set
            {
                Preferences.Set(keyvaldarktheme, value);
                if (value) 
                {
                    DeviceTheme = false;
                }
               
            }
        }



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
