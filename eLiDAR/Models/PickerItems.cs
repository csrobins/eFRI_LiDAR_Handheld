using System;
using System.Collections.Generic;
using System.Text;

namespace eLiDAR.Models
{
    public class PickerItems
    {
        public int ID { get; set; }
        public string NAME { get; set; }
    }
    public class PickerItemsString
    {
        public string ID { get; set; }
        public string NAME { get; set; }
    }
    public class SpeciesList
    {
        public int spp { get; set; }
        public double BA { get; set; }
        public string sppname { get; set; }

    }
}
