using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace eLiDAR.Models
{
    [Table("PROJECT")]
    public class PROJECT
    {
        //private Guid _guid;
        [PrimaryKey]
        public string PROJECTID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime PROJECT_DATE { get; set; }
    }
    [Table("PLOT")]
    public class PLOT
    {
        [PrimaryKey]
        public string PLOTID { get; set; }
        public string PROJECTID { get; set; }
        public string PLOT_TYPE { get; set; }
        public string PLOTNUM { get; set; }
        public string ADMINISTRATIVE { get; set; }
        public string FOREST_DISTRICT { get; set; }
        public string FMU { get; set; }
        public string MANAGEMENT_UNIT { get; set; }
        public string IMAGE_ANNOTATION { get; set; }
        public string PLOTKEY { get; set; }
        public DateTime PLOT_DATE { get; set; }
        public string MEASUREMENT_TYPE { get; set; }
        public string LEAD_SPP { get; set; }
        public string ORIGIN { get; set; }
        public string CANOPY_STRUCTURE { get; set; }
        public string MATURITY { get; set; }
        public int CROWN_CLOSURE { get; set; }
        public string FIELD_CREW1 { get; set; }
        public string FIELD_CREW2 { get; set; }
        public string DECLINATION { get; set; }
        public int UTM_ZONE { get; set; }
        public double UTM_EASTING { get; set; }
        public double UTM_NORTHING { get; set; }
        public string DATUM { get; set; }
        public string NOTES { get; set; }

    }
    [Table("TREE")]
    public class TREE
    {
        [PrimaryKey]
        public string TREEID { get; set; }
        public string PLOTID { get; set; }
        public int TREENUM { get; set; }
        public int SPECIES { get; set; }
        public string TAG_TYPE { get; set; }
        public string ORIGIN { get; set; }
        public string STATUS { get; set; }
        public int VIGOUR { get; set; }
        public Single HT_TO_DBH { get; set; }
        public Single DBH { get; set; }
        public Single HT { get; set; }
        public Boolean DBH_IN { get; set; }
        public Boolean CROWN_IN { get; set; }
        public int LIVE_CROWN_RATIO { get; set; }
        public string CROWN_CLASS { get; set; }
        public int CROWN_DAMAGE { get; set; }
        public int DEFOLIATING_INSECT { get; set; }
        public int FOLIAR_DISEASE { get; set; }
        public string STEM_QUALITY { get; set; }
        public int BARK_RETENTION { get; set; }
        public int WOOD_CONDITION { get; set; }
        public int DECAY_CLASS { get; set; }
        public int MORT_CAUSE { get; set; }
        public Boolean BROKEN_TOP { get; set; }
        public int AGE { get; set; }
        public double AZIMUTH { get; set; }
        public double DISTANCE { get; set; }
        public double CROWN_WIDTH1 { get; set; }
        public double CROWN_WIDTH2 { get; set; }

    }
}

