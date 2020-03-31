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
        public int ADMINISTRATIVE { get; set; }
        public string FOREST_DISTRICT { get; set; }
        public int FMU { get; set; }
        public int MANAGEMENT_UNIT { get; set; }
        public string IMAGE_ANNOTATION { get; set; }
        public string PLOTKEY { get; set; }
        public DateTime PLOT_DATE { get; set; }
        public string MEASUREMENT_TYPE { get; set; }
        public int LEAD_SPP { get; set; }
        public int ORIGIN { get; set; }
        public string CANOPY_STRUCTURE { get; set; }
        public string MATURITY { get; set; }
        public int CROWN_CLOSURE { get; set; }
        public string FIELD_CREW1 { get; set; }
        public string FIELD_CREW2 { get; set; }
        public string FIELD_CREW3 { get; set; }
        public string DECLINATION { get; set; }
        public int UTM_ZONE { get; set; }
        public double UTM_EASTING { get; set; }
        public double UTM_NORTHING { get; set; }
        public string DATUM { get; set; }
        public string COMMENTS { get; set; }

    }
    public class PLOTLIST : PLOT
    { 
        public bool IsPlotTypeB {
            get
            {
                if (PLOT_TYPE == "AB" || PLOT_TYPE == "ABC")
                {
                    return true;
                }
                else { return false; }
            }
            set { }
            }
        public bool IsPlotTypeC {
            get
            {
                if (PLOT_TYPE == "AC" || PLOT_TYPE == "ABC")
                {
                    return true;
                }
                else { return false; }
            }
            set { }
        }

    }


    [Table("TREE")]
    public class TREE
    {
        [PrimaryKey]
        public string TREEID { get; set; }
        public string PLOTID { get; set; }
        public int SECTION { get; set; }
        public int TREENUM { get; set; }
        public int SPECIES { get; set; }
        public string TAG_TYPE { get; set; }
        public string ORIGIN { get; set; }
        public string STATUS { get; set; }
        public int VIGOUR { get; set; }
        public Single HT_TO_DBH { get; set; }
        public Single DBH { get; set; }
        public Single HT { get; set; }
        public string DBH_IN { get; set; }
        public string CROWN_IN { get; set; }
        public int LIVE_CROWN_RATIO { get; set; }
        public string CROWN_CLASS { get; set; }
        public int CROWN_POSITION { get; set; }
        public int CROWN_DAMAGE { get; set; }
        public int DEFOLIATING_INSECT { get; set; }
        public int FOLIAR_DISEASE { get; set; }
        public string STEM_QUALITY { get; set; }
        public int BARK_RETENTION { get; set; }
        public int WOOD_CONDITION { get; set; }
        public int DECAY_CLASS { get; set; }
        public int MORT_CAUSE { get; set; }
        public string BROKEN_TOP { get; set; }
        public int AGE { get; set; }
        public Single LENGTH { get; set; }
        public double AZIMUTH { get; set; }
        public double DISTANCE { get; set; }
        public double CROWN_WIDTH1 { get; set; }
        public double CROWN_WIDTH2 { get; set; }
        public string COMMENTS { get; set; }

    }
    public class TREELIST : TREE
    {
        public string PLOT_TYPE { get; set; }
        public bool IsPlotTypeB
        {
            get
            {             
                if (PLOT_TYPE == "AB" || PLOT_TYPE == "ABC")
                {
                    return true;
                }
                else { return false; }
            }
            set { }
        }
        public bool IsPlotTypeC
        {
            get
            {
                if (PLOT_TYPE == "AC" || PLOT_TYPE == "ABC")
                {
                    return true;
                }
                else { return false; }
            }
            set { }
        }

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
        public int OFFSET_AZIMUTH { get; set; }
        public double OFFSET_DISTANCE { get; set; }
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
        public string EFFECTIVE_PORE_PATTERN { get; set; }
        public string ELC_SUBSTRATE_TYPE { get; set; }
        public int DEPTH_TO_DISTINCT_MOTTLES { get; set; }
        public int DEPTH_TO_PROMINENT_MOTTLES { get; set; }
        public int DEPTH_TO_GLEY { get; set; }
        public int DEPTH_TO_BEDROCK { get; set; }
        public int DEPTH_TO_CARBONATES { get; set; }
        public string MOISTURE_REGIME_DEPTH_CLASS { get; set; }
        public string MOISTURE_REGIME { get; set; }
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
        public string COMMENTS { get; set; }

    }
    [Table("SOIL")]
    public class SOIL
    {
        [PrimaryKey]
        public string SOILID { get; set; }
        public string PLOTID { get; set; }
        public int LAYER { get; set; }
        public int FROM { get; set; }
        public int TO { get; set; }
        public string HORIZON { get; set; }
        public string VON_POST { get; set; }
        public string TEXTURE { get; set; }
        public string PORE_PATTERN { get; set; }
        public string STRUCTURE { get; set; }
        public string COLOUR { get; set; }
        public string MOTTLE_COLOUR { get; set; }
        public int PERCENT_GRAVEL { get; set; }
        public int PERCENT_COBBLE { get; set; }
        public int PERCENT_STONE { get; set; }
    }
    [Table("SMALLTREE")]
    public class SMALLTREE
    {
        [PrimaryKey]
        public string SMALLTREEID { get; set; }
        public string PLOTID { get; set; }
        public int SPECIES { get; set; }
        public int HT_CLASS1_COUNT { get; set; }
        public int HT_CLASS2_COUNT { get; set; }
        public int HT_CLASS3_COUNT { get; set; }
        public int HT_CLASS4_COUNT { get; set; }
        public int HT_CLASS5_COUNT { get; set; }
        public int HT_CLASS6_COUNT { get; set; }
        public int HT_CLASS7_COUNT { get; set; }
        public int HT_CLASS8_COUNT { get; set; }
    }
    [Table("VEGETATION")]
    public class VEGETATION
    {
        [PrimaryKey]
        public string VEGETATIONID { get; set; }
        public string PLOTID { get; set; }
        public string SPECIES { get; set; }
        public int QUAD1 { get; set; }
        public int QUAD2 { get; set; }
        public int QUAD3 { get; set; }
        public int QUAD4 { get; set; }
        public int ELCLAYER3 { get; set; }
        public int ELCLAYER4 { get; set; }
        public int ELCLAYER5 { get; set; }
        public int ELCLAYER6 { get; set; }
        public int ELCLAYER7 { get; set; }
    }
    public class DEFORMITY
    {
        [PrimaryKey]
        public string DEFORMITYID { get; set; }
        public string TREEID { get; set; }
        public int TYPE { get; set; }
        public int CAUSE { get; set; }
        public double HT_FROM { get; set; }
        public double HT_TO { get; set; }
        public string QUAD { get; set; }
        public int EXTENT { get; set; }
        public int LEAN { get; set; }
        public int AZIMUTH { get; set; }
        public double LENGTH { get; set; }
        public double WIDTH { get; set; }
        public int PCT_SCUFF { get; set; }
        public int PCT_SCRAPE { get; set; }
        public int PCT_GOUGE { get; set; }
        public string OLD_FEEDING_CAVITY { get; set; }
        public string NEW_FEEDING_CAVITY { get; set; }
        public string OLD_NEST_CAVITY { get; set; }
        public string NEW_NEST_CAVITY { get; set; }
        public string STICK_NEST { get; set; }

    }
    public class DWD
    {
        [PrimaryKey]
        public string DWDID { get; set; }
        public string PLOTID { get; set; }
        public int LINE { get; set; }
        public int DWDNUM { get; set; }
        public int SPECIES { get; set; }
        public double DIAM { get; set; }
        public int DECOMP_CLASS { get; set; }
        public string ORIGIN { get; set; }
        public int TILT_ANGLE { get; set; }
        public double LENGTH { get; set; }
        public double SMALL_DIAM { get; set; }
        public double LARGE_DIAM { get; set; }
        public string GT_50_MOSS { get; set; }
        public string BURNED { get; set; }
        public string HOLLOW { get; set; }
        public string IS_ACCUM { get; set; }
        public double ACCUM_LENGTH { get; set; }
        public double ACCUM_DEPTH { get; set; }
        public int PERCENT_CONIFER { get; set; }
        public int PERCENT_DECID { get; set; }

    }

}

