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
    [Table("STEMMAP")]
    public class STEMMAP
    {
        [PrimaryKey]
        public string STEMMAPID { get; set; }
        public string TREEID { get; set; }
        public int AZIMUTH { get; set; }
        public double DISTANCE { get; set; }
        public double CROWN_AXIS_LONG { get; set; }
        public double CROWN_AXIS_SHORT { get; set; }
    }
    [Table("ECOSITE")]
    public class ECOSITE
    {
        [PrimaryKey]
        public string ECOSITEID { get; set; }
        public string PLOTID { get; set; }
        public int HUMUS_FORM { get; set; }
        public int DRAINAGE { get; set; }
        public string STRATIFIED { get; set; }
        public int EFFECTIVE_PORE_PATTERN { get; set; }
        public string ELC_SUBSTRATE_TYPE { get; set; }
        public int DEPTH_TO_DISTINCT_MOTTLES { get; set; }
        public int DEPTH_TO_PROMINENT_MOTTLES { get; set; }
        public int DEPTH_TO_GLEY { get; set; }
        public int DEPTH_TO_BEDROCK { get; set; }
        public int DEPTH_TO_CARBONATES { get; set; }
        public int MOISTURE_REGIME_DEPTH_CLASS { get; set; }
        public int MOISTURE_REGIME { get; set; }
        public string MODE_OF_DEPOSITION1 { get; set; }
        public string MODE_OF_DEPOSITION2 { get; set; }
        public int FUNCTIONAL_ROOTING_DEPTH { get; set; }
        public int MAXIMUM_ROOTING_DEPTH { get; set; }
        public int DEPTH_TO_ROOT_RESTRICTION { get; set; }
        public int DEPTH_TO_WATER_TABLE { get; set; }
        public int DEPTH_TO_COARSE_FRAGS { get; set; }
        public int PROBLEMATIC_SITE_PROTOCOL_CLASS { get; set; }
        public string SEEPAGE { get; set; }
        public string SOIL_PIT_PHOTO { get; set; }
        public string PRI_ECO { get; set; }
        public int PRI_ECO_PCT { get; set; }
        public string SEC_ECO { get; set; }
        public int SEC_ECO_PCT { get; set; }
        public int AZIMUTH { get; set; }
        public int DISTANCE { get; set; }
    }
}

