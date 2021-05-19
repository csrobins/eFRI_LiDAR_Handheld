using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using eLiDAR.Models;
using Xamarin.Essentials;
using eLiDAR.Services;

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
        private string keyusealphaspecies = "UseAlphaSpecies";
        private string keydefaultspecies = "DefaultSpecies";
        private string keyusedefaultspecies = "UseDefaultSpecies";
        private string keydefaultperson = "DefaultPerson";
        private string keyusedefaultperson = "UseDefaultPerson";
        private string keydefaultstatus = "DefaultStatus";
        private string keyusedefaultstatus = "UseDefaultStatus";
        private string keydefaultorigin = "DefaultOrigin";
        private string keyusedefaultorigin = "UseDefaultOrigin";
        private string keydefaultvsnstatus = "DefaultVSNStatus";
        private string keyusedefaultvsnstatus = "UseDefaultVSNStatus";
        private string keydefaultdeclination = "DefaultDeclination";
        private string keyusedefaultdeclination = "UseDefaultDeclination";
        private string keyallowvegcalc = "AllowVegCalc";
        internal bool AllowVegCalc
        {
            get
            {
                return Preferences.Get(keyallowvegcalc, true);
            }
            set
            {
                Preferences.Set(keyallowvegcalc, value);
            }
        }
        internal bool UseDefaultDeclination
        {
            get
            {
                return Preferences.Get(keyusedefaultdeclination, false);
            }
            set
            {
                Preferences.Set(keyusedefaultdeclination, value);
            }
        }
        internal int DefaultDeclination
        {
            get
            {
                return Preferences.Get(keydefaultdeclination, 0);
            }
            set
            {
                Preferences.Set(keydefaultdeclination, value);
            }
        }
        internal bool UseDefaultSpecies
        {
            get
            {
                return Preferences.Get(keyusedefaultspecies, false);
            }
            set
            {
                Preferences.Set(keyusedefaultspecies, value);
            }
        }
        internal int DefaultSpecies
        {
            get
            {
                return Preferences.Get(keydefaultspecies, 0);
            }
            set
            {
                Preferences.Set(keydefaultspecies, value);
            }
        }
        internal bool UseDefaultPerson
        {
            get
            {
                return Preferences.Get(keyusedefaultperson, false);
            }
            set
            {
                Preferences.Set(keyusedefaultperson, value);
            }
        }
        internal string DefaultPerson
        {
            get
            {
                return Preferences.Get(keydefaultperson, null);
            }
            set
            {
                Preferences.Set(keydefaultperson, value);
            }
        }

        internal bool UseDefaultStatus
        {
            get
            {
                return Preferences.Get(keyusedefaultstatus, true);
            }
            set
            {
                Preferences.Set(keyusedefaultstatus, value);
            }
        }
        internal string DefaultStatus
        {
            get
            {
                return Preferences.Get(keydefaultstatus, "L");
            }
            set
            {
                Preferences.Set(keydefaultstatus, value);
            }
        }
        internal bool UseDefaultOrigin
        {
            get
            {
                return Preferences.Get(keyusedefaultorigin, true);
            }
            set
            {
                Preferences.Set(keyusedefaultorigin, value);
            }
        }
        internal string DefaultOrigin
        {
            get
            {
                return Preferences.Get(keydefaultorigin, "N");
            }
            set
            {
                Preferences.Set(keydefaultorigin, value);
            }
        }

        internal bool UseDefaultVSNStatus
        {
            get
            {
                return Preferences.Get(keyusedefaultvsnstatus, true);
            }
            set
            {
                Preferences.Set(keyusedefaultvsnstatus, value);
            }
        }
        internal string DefaultVSNStatus
        {
            get
            {
                return Preferences.Get(keydefaultvsnstatus, null);
            }
            set
            {
                Preferences.Set(keydefaultvsnstatus, value);
            }
        }
        public Guid getGUID()
        {
            // Create and display the value of two GUIDs.
            return Guid.NewGuid();
        }
        public string getSppComp(List<TREE> list)
            // for calculating a species string for a set of trees
        {
            List<SpeciesList> _spplist = new List<SpeciesList>();
            bool added = false;
            double batot = 0;
            foreach(var tree in list)
            {
                if (tree.TREESTATUSCODE == "L" || tree.TREESTATUSCODE == "V")
                {
                    var ba = Math.Pow(tree.DBH,2) * Math.PI;  
                    foreach(var itm in _spplist)
                    {
                        if (itm.spp == tree.SPECIESCODE)
                        {
                            itm.BA = itm.BA + ba;
                            batot = batot + ba;
                            added = true;
                        }  
                    }
                    if (!added)
                    {
                        _spplist.Add(new SpeciesList() { spp = tree.SPECIESCODE, BA = ba });
                        batot = batot + ba;
                        added = false;
                    }
                    added = false;
                }
            }
            // now that list of species is made, sort it, and build a species string
            string sppcomp = "";
            List<SpeciesList> _spplistsort = _spplist.OrderByDescending(x => x.BA).ToList() ;
            List<PickerItems> _masterlist = PickerService.SpeciesMaster().ToList();
            if (batot > 0)
            {
            foreach(var itm in _spplistsort)
                {
                    if (!UseAlphaSpecies)
                    {
                        sppcomp = sppcomp + itm.spp.ToString() + " " + (Math.Round((itm.BA / batot) * 100)).ToString() + "% ";
                    }
                    else
                    {
                        sppcomp = sppcomp + PickerService.GetValue(_masterlist, itm.spp) + " " + (Math.Round((itm.BA / batot) * 100)).ToString() + "% ";
                    }
                }
            }

            return sppcomp;

        }
        internal bool UseAlphaSpecies
        {
            get
            {
                return Preferences.Get(keyusealphaspecies, false);
            }
            set
            {
                Preferences.Set(keyusealphaspecies, value);
            }
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
