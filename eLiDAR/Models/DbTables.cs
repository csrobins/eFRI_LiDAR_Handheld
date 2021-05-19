using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace eLiDAR.Models
{

    [Table("SETTINGS")]
    public class SETTINGS
    {
        [PrimaryKey]
        public int SETTINGSID { get; set; }
        public DateTime LastSynched { get; set; }
        public int PROJECT_ROWS_SYNCHED { get; set; }
        public int PLOT_ROWS_SYNCHED { get; set; }
        public int TREE_ROWS_SYNCHED { get; set; }
        public int PROJECT_ROWS_PULLED { get; set; }
        public int PLOT_ROWS_PULLED { get; set; }
        public int TREE_ROWS_PULLED { get; set; }
    }

    [Table("PROJECT")]
    public class PROJECT
    {
        [PrimaryKey]
        public string PROJECTID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public DateTime PROJECT_DATE { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }

    }
    [Table("PLOT")]
    public class PLOT
    {
        [PrimaryKey]
        public string PLOTID { get; set; }
        public string PROJECTID { get; set; }
        public string VSNPLOTTYPECODE { get; set; }
        public string VSNPLOTNAME { get; set; }


 //       public int ADMINISTRATIVE { get; set; }
 //       public string FOREST_DISTRICT { get; set; }
  //      public int FMU { get; set; }
   //     public int MANAGEMENT_UNIT { get; set; }
   //     public string IMAGE_ANNOTATION { get; set; }
        public string PLOTKEY { get; set; }
        public DateTime PLOTOVERVIEWDATE { get; set; }
        public string MEASURETYPECODE { get; set; }
        public int LEAD_SPP { get; set; }
        public int MAINCANOPYORIGINCODE1 { get; set; }
        public int MAINCANOPYORIGINCODE2 { get; set; }
        public string CANOPYSTRUCTURECODE1 { get; set; }
        public string CANOPYSTRUCTURECODE2 { get; set; }
        public string MATURITYCLASSCODE1 { get; set; }
        public string MATURITYCLASSCODE2 { get; set; }
        public string MATURITYCLASSRATIONALE1 { get; set; }
        public string MATURITYCLASSRATIONALE2 { get; set; }

        public int CROWN_CLOSURE { get; set; }
        public string FIELD_CREW1 { get; set; }
        public string FIELD_CREW2 { get; set; }
        public string FIELD_CREW3 { get; set; }
        public string FIELD_CREW4 { get; set; }
        public string FIELD_CREW5 { get; set; }
        public string FIELD_CREW6 { get; set; }
        public int DECLINATION { get; set; }
        public int UTMZONE { get; set; }
        public double EASTING { get; set; }
        public double NORTHING { get; set; }
        public string DATUM { get; set; }
        public string PLOTOVERVIEWNOTE { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }
        public int NONSTANDARDTYPECODE { get; set; }
        public int ACCESSCONDITIONCODE { get; set; }
        public int MEASUREYEAR { get; set; }
        public int GROWTHPLOTNUMBER { get; set; }
        public string EXISTINGPLOTNAME { get; set; }
        public string EXISTINGPLOTTYPECODE { get; set; }
        public double DISTANCETARGETMOVED { get; set; }
        public int AZIMUTHTARGETMOVED { get; set; }
        public int SITERANK { get; set; }
        public int CROWNDAMAGECODE { get; set; }
        public int VIGOURCODE { get; set; }
        public string DAMAGEDESCRIPTION { get; set; }

        public DateTime FORESTHEALTHDATE { get; set; }
        public string FORESTHEALTHNOTE { get; set; }
        public string FORESTHEALTHPERSON { get; set; }
        public int SMALLTREESHRUBAREA { get; set; }
        public DateTime SMALLTREESHRUBDATE { get; set; }

        public string SMALLTREESHRUBNOTE { get; set; }
        public string SMALLTREEPERSON { get; set; }
        public DateTime UNDERSTORYVEGETATIONDATE { get; set; }
        public int UNDERSTORYVEGETATIONAREA { get; set; }
        public string UNDERSTORYVEGETATIONNOTE { get; set; }
        public string UNDERSTORYVEGETATIONPERSON { get; set; }

        public DateTime UNDERSTORYCENSUSDATE { get; set; }
        public string UNDERSTORYCENSUSNOTE { get; set; }
        public string UNDERSTORYCENSUSPERSON { get; set; }
        public DateTime DOWNWOODYDEBRISDATE { get; set; }
        public string DOWNWOODYDEBRISNOTE { get; set; }
        public string DOWNWOODYDEBRISPERSON { get; set; }
        public DateTime DEFORMITYDATE { get; set; }
        public string DEFORMITYNOTE { get; set; }
        public string DEFORMITYPERSON { get; set; }
        public DateTime STANDINFODATE { get; set; }
        public string STANDINFONOTE { get; set; }
        public string STANDINFOPERSON { get; set; }
        public int DISTURBANCECODE1 { get; set; }
        public int DISTURBANCECODE2 { get; set; }
        public int PERCENTAFFECTED { get; set; }
        public int PERCENTMORTALITY { get; set; }
        public string FOLLOWUPREQUIRED { get; set; }
        public double LINELENGTH1 { get; set; }
        public double LINELENGTH2 { get; set; }
        public DateTime AGEDATE { get; set; }
        public string AGENOTE { get; set; }
        public string AGEPERSON { get; set; }
        public DateTime STEMMAPPINGDATE { get; set; }
        public string STEMMAPPINGNOTE { get; set; }
        public string STEMMAPPINGPERSON { get; set; }

    }
    public class PLOTLIST : PLOT
    { 
        public bool IsPlotTypeB {
            get
            {
                try
                {
                    if (VSNPLOTTYPECODE.Contains("B") || VSNPLOTTYPECODE.Contains("C"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex) { return false; } 
            }
            set { }
            }
        public bool IsPlotTypeC {
            get
            {
                try
                {
                    if (VSNPLOTTYPECODE.Contains("C"))
                    {
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception ex) { return false; }
            }
            set { }
        }

    }
    [Table("PHOTO")]
    public class PHOTO
    {
        [PrimaryKey]
        public string PHOTOID { get; set; }
        public string PLOTID { get; set; }
        public string PHOTOTYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public int PHOTONUMBER { get; set; }
        public string FRAMENUMBER { get; set; }
        public int AZIMUTH { get; set; }
        public Single DISTANCE { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }
    }

    [Table("PERSON")]
    public class PERSON
    {
        [PrimaryKey]
        public string PERSONID { get; set; }
        public string PROJECTID { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }

    }

    [Table("TREE")]
    public class TREE
    {
        [PrimaryKey]
        public string TREEID { get; set; }
        public string PLOTID { get; set; }
        public int SECTION { get; set; }
        public int TREENUMBER { get; set; }
        public int SPECIESCODE { get; set; }
        public string TAG_TYPE { get; set; }
        public string TREEORIGINCODE { get; set; }
        public string TREESTATUSCODE { get; set; }
        public string VSNSTATUSCODE { get; set; }
        public int VIGOURCODE { get; set; }
        public Single HEIGHTTODBH { get; set; }
        public Single DBH { get; set; }
        public Single LENGTH { get; set; }
        public string DBHIN { get; set; }
        public string CROWNIN { get; set; }
        public int LIVE_CROWN_RATIO { get; set; }
        public string CROWNCLASSCODE { get; set; }
        public int CROWN_POSITION { get; set; }
        public int CROWNDAMAGECODE { get; set; }
        public int DEFOLIATING_INSECT { get; set; }
        public int FOLIAR_DISEASE { get; set; }
        public string STEMQUALITYCODE { get; set; }
        public int BARKRETENTIONCODE { get; set; }
        public int WOODCONDITIONCODE { get; set; }
        public int DECAYCLASS { get; set; }
        public int MORTALITYCAUSECODE { get; set; }
        public string BROKENTOP { get; set; }
   //     public double AZIMUTH { get; set; }
   //     public double DISTANCE { get; set; }
        public Single DBH1 { get; set; }
        public Single DBH2 { get; set; }
        public Single DIRECTTOTALHEIGHT { get; set; }
        public Single OCULARTOTALHEIGHT { get; set; }
        public Single HEIGHTTODEADTIP { get; set; }
        public Single DIRECTHEIGHTTOCONTLIVECROWN { get; set; }
        public Single OCULARHEIGHTTOCONTLIVECROWN { get; set; }
        public Single DIRECTOFFSETDISTANCE { get; set; }
        public int DEGREEOFLEAN { get; set; }
        public Single HEIGHTTOCORE { get; set; }
        public string CORESTATUSCODE { get; set; }
        public Single SOUNDWOODLENGTH { get; set; }
        public int FIELDAGE { get; set; }
        public int OFFICERINGCOUNT { get; set; }
        public int MISSINGRINGS { get; set; }
        public string COMMENTS { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }

    }
    public class TREELIST : TREE
    {
        public string VSNPLOTTYPECODE { get; set; }
        public double AZIMUTH { get; set; }
        public double DISTANCE { get; set; }
        public bool IsPlotTypeB
        {
            get
            {
                //                if (VSNPLOTTYPECODE == "AB" || VSNPLOTTYPECODE == "ABC" || VSNPLOTTYPECODE == "B")
                try
                {
                    if (VSNPLOTTYPECODE.Contains("B") || VSNPLOTTYPECODE.Contains("C"))
                    {
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set { }
        }
        public bool IsPlotTypeC
        {
            get
            {
                try
                {
                    if (VSNPLOTTYPECODE.Contains("C"))
                    {
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set { }
        }
        public bool LiveTree
        {
            get
            {
                try
                {
                    if (TREESTATUSCODE.Contains("L") || TREESTATUSCODE.Contains("V"))
                    {
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            set { }
        }
        public string SpeciesName
        {
            get
            {
                try
                {
                    Utilities.Utils util = new Utilities.Utils();
                    if (!util.UseAlphaSpecies) 
                    {
                        return SPECIESCODE.ToString();
                    }
                    else { return Services.PickerService.GetValue(Services.PickerService.SpeciesMaster().ToList(), SPECIESCODE); }
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

        }
        public Xamarin.Forms.Color StemMapColor
        {
            get
            {
                try
                {

                    if (DISTANCE == 0)
                    {
                        //   return Xamarin.Forms.Color.Red;
                        return Xamarin.Forms.Color.FromHex("#FFE52E15");

                    }
                    else
                    {
                        //  return Xamarin.Forms.Color.Accent;
                        return Xamarin.Forms.Color.FromHex("#FF333333");
                    }
                }
                catch (Exception ex)
                {
                    return Xamarin.Forms.Color.Accent;
                }
            }

        }
        public Xamarin.Forms.Color AgeColor
        {
            get
            {
                try
                {

                    if (CORESTATUSCODE == null || CORESTATUSCODE == "")
                    {
                        //   return Xamarin.Forms.Color.Red;
                        return Xamarin.Forms.Color.FromHex("#FF333333");

                    }
                    else
                    {
                        //  return Xamarin.Forms.Color.Accent;
                        return Xamarin.Forms.Color.FromHex("#FF88C800");
                    }
                }
                catch (Exception ex)
                {
                    return Xamarin.Forms.Color.Accent;
                }
            }

        }
        public Xamarin.Forms.Color DBHColor
        {
            get
            {
                try
                {
                    if (DBH == 0)
                    {
                        //   return Xamarin.Forms.Color.Red;
                        return Xamarin.Forms.Color.FromHex("#FFE52E15");
                    }
                    else
                    {
                        //  return Xamarin.Forms.Color.Accent;
                        return Xamarin.Forms.Color.FromHex("#FF333333");
                    }
                }
                catch (Exception ex)
                {
                    return Xamarin.Forms.Color.Accent;
                }
            }

        }
    }
    public class SMALLTREELIST : SMALLTREE
    {
        public string SpeciesName
        {
            get
            {
                try
                {
                    Utilities.Utils util = new Utilities.Utils();
                    if (!util.UseAlphaSpecies)
                    {
                        return SPECIESCODE.ToString();
                    }
                    else { return Services.PickerService.GetValue(Services.PickerService.SmallSpeciesMaster().ToList(), SPECIESCODE); }
                }
                catch (Exception ex)
                {
                    return "";
                }
            }

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
        public double CROWNWIDTH1 { get; set; }
        public double CROWNWIDTH2 { get; set; }
        public int CROWNOFFSETAZIMUTH { get; set; }
        public double CROWNOFFSETDISTANCE { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }
    }
    [Table("ECOSITE")]
    public class ECOSITE
    {
        [PrimaryKey]
        public string ECOSITEID { get; set; }
        public string PLOTID { get; set; }
        public int HUMUSFORMCODE { get; set; }
        public int DRAINAGECLASSCODE { get; set; }
        public string STRATIFIED { get; set; }
        public string EFFECTIVE_PORE_PATTERN { get; set; }
        public string ELC_SUBSTRATE_TYPE { get; set; }
        public Nullable<int> DEPTHTODISTINCTMOTTLES { get; set; }
        public Nullable<int> DEPTHTOPROMINENTMOTTLES { get; set; }
        public Nullable<int> DEPTHTOGLEY { get; set; }
        public Nullable<int> DEPTHTOBEDROCK { get; set; }
        public Nullable<int> DEPTHTOCARBONATES { get; set; }
        public string MOISTURE_REGIME_DEPTH_CLASS { get; set; }
        public string MOISTUREREGIMECODE { get; set; }
        public string MODEOFDEPOSITIONCODE1 { get; set; }
        public string MODEOFDEPOSITIONCODE2 { get; set; }
        public int FUNCTIONALROOTINGDEPTH { get; set; }
        public int MAXIMUMROOTINGDEPTH { get; set; }
        public Nullable<int> DEPTHTOROOTRESTRICTION { get; set; }
        public Nullable<int> DEPTHTOWATERTABLE { get; set; }
        public Nullable<int> DEPTHTOIMPASSABLECOARSEFRAGMENTS { get; set; }
        public string PROBLEMATICSITE { get; set; }
        public Nullable<int> DEPTHTOSEEPAGE { get; set; }
        public string SOIL_PIT_PHOTO { get; set; }
        public string PRI_ECO { get; set; }
        public int PRI_ECO_PCT { get; set; }
        public string SEC_ECO { get; set; }
        public int SEC_ECO_PCT { get; set; }
        public int PITAZIMUTH { get; set; }
        public Single PITDISTANCE { get; set; }
        public string SUBSTRATENOTE { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }
        public int POREPATTERNCODE { get; set; }
        public string MINERALTEXTURECODE { get; set; }
        public string DECOMPOSITIONCODE { get; set; }
      
        public DateTime SUBSTRATEDATE { get; set; }
        public int MODEOFDEPOSITIONRANK1 { get; set; }
        public int MODEOFDEPOSITIONRANK2 { get; set; }

    }
    [Table("SOIL")]
    public class SOIL
    {
        [PrimaryKey]
        public string SOILID { get; set; }
        public string PLOTID { get; set; }
        public int HORIZONNUMBER { get; set; }
        public float DEPTHFROM { get; set; }
        public float DEPTHTO { get; set; }
        public string HORIZON { get; set; }
        public string DECOMPOSITIONCODE { get; set; }
        public string MINERALTEXTURECODE { get; set; }
        public string POREPATTERNCODE { get; set; }
        public string STRUCTURE { get; set; }
        public string MATRIXCOLOUR { get; set; }
        public string MOTTLECOLOUR { get; set; }
        public int PERCENTGRAVEL { get; set; }
        public int PERCENTCOBBLE { get; set; }
        public int PERCENTSTONE { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }
        public string GLEYCOLOUR { get; set; }
    }
    [Table("SMALLTREE")]
    public class SMALLTREE
    {
        [PrimaryKey]
        public string SMALLTREEID { get; set; }
        public string PLOTID { get; set; }
        public int SPECIESCODE { get; set; }
        public int HT_CLASS1_COUNT { get; set; }
        public int HT_CLASS2_COUNT { get; set; }
        public int HT_CLASS3_COUNT { get; set; }
        public int HT_CLASS4_COUNT { get; set; }
        public int HT_CLASS5_COUNT { get; set; }
        public int HT_CLASS6_COUNT { get; set; }
        public int HT_CLASS7_COUNT { get; set; }
        public int HT_CLASS8_COUNT { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }
        public int COUNT { get; set; }
        public double HEIGHT { get; set; }

    }
    [Table("VEGETATION")]
    public class VEGETATION
    {
        [PrimaryKey]
        public string VEGETATIONID { get; set; }
        public string PLOTID { get; set; }
        public string VSNSPECIESCODE { get; set; }
        public int SPECIMENNUMBER { get; set; }
        public int QUAD1 { get; set; }
        public int QUAD2 { get; set; }
        public int QUAD3 { get; set; }
        public int QUAD4 { get; set; }
        public int ELCLAYER3 { get; set; }
        public int ELCLAYER4 { get; set; }
        public int ELCLAYER5 { get; set; }
        public int ELCLAYER6 { get; set; }
        public int ELCLAYER7 { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }
        public int QUAD1_ELC4 { get; set; }
        public int QUAD2_ELC4 { get; set; }
        public int QUAD3_ELC4 { get; set; }
        public int QUAD4_ELC4 { get; set; }
        public int QUAD1_ELC5 { get; set; }
        public int QUAD2_ELC5 { get; set; }
        public int QUAD3_ELC5 { get; set; }
        public int QUAD4_ELC5 { get; set; }
        public int QUAD1_ELC6 { get; set; }
        public int QUAD2_ELC6 { get; set; }
        public int QUAD3_ELC6 { get; set; }
        public int QUAD4_ELC6 { get; set; }
        public int QUAD1_ELC7 { get; set; }
        public int QUAD2_ELC7 { get; set; }
        public int QUAD3_ELC7 { get; set; }
        public int QUAD4_ELC7 { get; set; }
    }
    [Table("VEGETATIONCENSUS")]
    public class VEGETATIONCENSUS
    {
        [PrimaryKey]
        public string VEGETATIONCENSUSID { get; set; }
        public string PLOTID { get; set; }
        public string VSNSPECIESCODE { get; set; }
        public int SPECIMENNUMBER { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }
    }
    public class DEFORMITY
    {
        [PrimaryKey]
        public string DEFORMITYID { get; set; }
        public string TREEID { get; set; }
        public int DEFORMITYTYPECODE { get; set; }
        public int CAUSE { get; set; }
        public double HEIGHTFROM { get; set; }
        public double HEIGHTTO { get; set; }
        public string QUADRANTCODE { get; set; }
        public int EXTENT { get; set; }
        public int DEGREELEANARCH { get; set; }
        public int AZIMUTH { get; set; }
        public double DEFORMITYLENGTH { get; set; }
        public double DEFORMITYWIDTH { get; set; }
        public int SCUFF { get; set; }
        public int SCRAPE { get; set; }
        public int GOUGE { get; set; }
        public string OLD_FEEDING_CAVITY { get; set; }
        public string NEW_FEEDING_CAVITY { get; set; }
        public string OLD_NEST_CAVITY { get; set; }
        public string NEW_NEST_CAVITY { get; set; }
        public string STICK_NEST { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }

    }
    public class DWD
    {
        [PrimaryKey]
        public string DWDID { get; set; }
        public string PLOTID { get; set; }
        public int LINENUMBER { get; set; }
        public int DWDNUM { get; set; }
        public int SPECIESCODE { get; set; }
        public double DIAMETER { get; set; }
        public int DECOMPOSITIONCLASS { get; set; }
        public string DOWNWOODYDEBRISORIGINCODE { get; set; }
        public int TILTANGLE { get; set; }
        public double DOWNWOODYDEBRISLENGTH { get; set; }
        public double SMALLDIAMETER { get; set; }
        public double LARGEDIAMETER { get; set; }
        public string MOSS { get; set; }
        public string BURNED { get; set; }
        public string HOLLOW { get; set; }
        public string IS_ACCUM { get; set; }
        public double ACCUMULATIONLENGTH { get; set; }
        public double ACCUMULATIONDEPTH { get; set; }
        public int PERCENTCONIFER { get; set; }
        public int PERCENTHARDWOOD { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public string IsDeleted { get; set; }

    }

}

