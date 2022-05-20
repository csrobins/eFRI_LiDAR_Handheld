using FluentValidation;
using eLiDAR.Models;
using eLiDAR.Helpers;
using System;
using FluentValidation.Validators;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace eLiDAR.Validator
{
    public class ParseValidater
    {
        private string _msg;
        private int _count;
        public (int, string) Parse(ValidationResult validationResults)
        {
            _count = 0;
            foreach (var err in validationResults.Errors)
            {
                _msg = _msg + err.ErrorMessage + "\n";
                _count = _count + 1;
            }
            return (_count, _msg);
        }
    }

    public class FullValidater
    {
        private PLOT _plot;
        private string _msg;
        private DatabaseHelper _databasehelper = new DatabaseHelper() ;
        public  FullValidater(PLOT plot)
        {
            _plot = plot;
   //         _databasehelper = new DatabaseHelper(); 
        }
            
        public bool ValidAll()
        {
            bool isvalid = false;
            try
            {
             
                isvalid = ValidPlot();
                if (!ValidTree()) { isvalid = false; }
                if (!ValidSmallTree()) { isvalid = false; }
                if (!ValidSoil()) { isvalid = false; }
                if (!ValidEcosite()) { isvalid = false; }
                if (!ValidPhoto()) { isvalid = false; }
                if (_plot.VSNPLOTTYPECODE.Contains("C") )
                {
                    if (!ValidDWD()) { isvalid = false; }
                    if (!ValidVeg()) { isvalid = false; }
                    if (!ValidVegCensus()) { isvalid = false; }
                }

                return isvalid;
            }
            catch(Exception ex)
            {
                return false;
            }
            
        }
        public bool ValidPlot()
        {
            // This will run the partial and full plot validaters
            PlotValidator _validator = new PlotValidator(true) ;
         //   PLOT _plot = _databasehelper.GetPlotData(_plotid);
            msg = "Checked plot " + _plot.VSNPLOTNAME;
         //   _validator.CascadeMode = CascadeMode.Continue; 
            ValidationResult validationResults = _validator.Validate(_plot);
            if (validationResults.IsValid)
            { return  true; }
            else
            { 

                foreach (var err in validationResults.Errors)
                {
                    AddMsg(err.ErrorMessage, 1); 
                }
                return false;
            }
        }
        public bool ValidTree()
        {
            bool isvalid = true;
            List<TREE> _treelist = _databasehelper.GetFilteredTreeData(_plot.PLOTID);  
            TreeValidator _validator = new TreeValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _treelist.Count.ToString() + " trees in plot " + _plot.VSNPLOTNAME;
            foreach (var _tree in _treelist)
            {
                msg = "Checked tree " + _tree.TREENUMBER.ToString();
                ValidationResult validationResults = _validator.Validate(_tree);
                if (!validationResults.IsValid)
                {
               //     msg = validationResults.Errors[0].ErrorMessage;
                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }

                    isvalid = false;
                }
                msg = "Checked age for tree " + _tree.TREENUMBER.ToString();
                TreeAgeValidator _agevalidator = new TreeAgeValidator(true);
                ValidationResult validationageResults = _agevalidator.Validate(_tree);
                if (!validationageResults.IsValid)
                {
                    //     msg = validationResults.Errors[0].ErrorMessage;
                    foreach (var err in validationageResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }

                    isvalid = false;
                }

                if ( _plot.VSNPLOTTYPECODE.Contains("C"))
                {
                    if (!ValidDeformity(_tree)) { isvalid = false; }
                }
                if (_plot.VSNPLOTTYPECODE.Contains("B") || _plot.VSNPLOTTYPECODE.Contains("C"))
                {
                    List<STEMMAP> _stemlist = _databasehelper.GetFilteredStemmapData(_tree.TREEID);
                    if (_stemlist.Count > 0)
                    {
                        // check if count == 0
                        if (!ValidStemMap(_tree)) { isvalid = false; }
                        if (_tree.TREESTATUSCODE == "X" || _tree.TREESTATUSCODE == "V") 
                                {
                                 msg = " no stem maps requrired for X, C status trees in tree " + _tree.TREENUMBER.ToString();
                                isvalid = false; 
                                }
                    }
                    else
                    {
                        msg = "0 stem maps in tree " + _tree.TREENUMBER.ToString();
                        isvalid = false;
                    }

                }
            }
            return isvalid;
        }
        public bool ValidDWD()
        {
            bool isvalid = true;
            List<DWD> _list = _databasehelper.GetFilteredDWDData(_plot.PLOTID);
            DWDValidator _validator = new DWDValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " dwd in plot " + _plot.VSNPLOTNAME;
            foreach (var _itm in _list)
            {
                msg = "Checked " + _itm.DWDNUM.ToString();
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {

                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }

                    isvalid = false;
                }
            }
            return isvalid;
        }
        public bool ValidSmallTree()
        {
            bool isvalid = true;
            List<SMALLTREE> _list = _databasehelper.GetFilteredSmallTreeData(_plot.PLOTID);
            SmallTreeValidator _validator = new SmallTreeValidator(true);
    //        _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " small trees in plot " + _plot.VSNPLOTNAME;
            foreach (var _itm in _list)
            {
                msg = "Checked " + _itm.SPECIESCODE.ToString();
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {

                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }

                    isvalid = false;
                }
            }
            return isvalid;
        }
        public bool ValidSoil()
        {
            bool isvalid = true;
            List<SOIL> _list = _databasehelper.GetFilteredSoilData(_plot.PLOTID);
            SoilValidator _validator = new SoilValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " soil layers in plot " + _plot.VSNPLOTNAME;
            foreach (var _itm in _list)
            {
                msg = "Checked " + _itm.HORIZONNUMBER.ToString();
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {

                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }

                    isvalid = false;
                }
            }
            return isvalid;
        }
        public bool ValidEcosite()
        {
            bool isvalid = true;
            List<ECOSITE> _list = _databasehelper.GetFilteredEcositeData(_plot.PLOTID);
            EcositeValidator _validator = new EcositeValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " substrate/ecosites in plot " + _plot.VSNPLOTNAME;
            foreach (var _itm in _list)
            {
                msg = "Checked " + _itm.PRI_ECO;
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {
                    //msg = validationResults.Errors[0].ErrorMessage;
                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }

                    isvalid = false;
                }
            }
            return isvalid;
        }
        public bool ValidPhoto()
        {
            bool isvalid = true;
            List<PHOTO> _list = _databasehelper.GetFilteredPhotoData(_plot.PLOTID);
            PhotoValidator _validator = new PhotoValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " photos in plot " + _plot.VSNPLOTNAME;
            foreach (var _itm in _list)
            {
                msg = "Checked " + _itm.PHOTOTYPE ;
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {
//                    msg = validationResults.Errors[0].ErrorMessage;
                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }
                    isvalid = false;
                }
            }
            return isvalid;
        }
        public bool ValidVeg()
        {
            bool isvalid = true;
            List<VEGETATION> _list = _databasehelper.GetFilteredVegetationData(_plot.PLOTID);
            VegetationValidator _validator = new VegetationValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " veg species in plot " + _plot.VSNPLOTNAME;
            foreach (var _itm in _list)
            {
                msg = "Checked " + _itm.VSNSPECIESCODE.ToString();
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {
                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }

//                    msg = validationResults.Errors[0].ErrorMessage;
                    isvalid = false;
                }
            }
            return isvalid;
        }
        public bool ValidVegCensus()
        {
            bool isvalid = true;
            List<VEGETATIONCENSUS> _list = _databasehelper.GetFilteredVegetationCensusData(_plot.PLOTID);
            VegetationCensusValidator _validator = new VegetationCensusValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " veg census species in plot " + _plot.VSNPLOTNAME;
            foreach (var _itm in _list)
            {
                msg = "Checked " + _itm.VSNSPECIESCODE.ToString();
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {
                    //msg = validationResults.Errors[0].ErrorMessage;
                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 1);
                    }

                    isvalid = false;
                }
            }
            return isvalid;
        }

        public bool ValidDeformity(TREE _tree)
        {
            bool isvalid = true;
            List<DEFORMITY> _list = _databasehelper.GetFilteredDeformityData(_tree.TREEID);
            DeformityValidator _validator = new DeformityValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " deformity(ies) in tree " + _tree.TREENUMBER.ToString();
            foreach (var _itm in _list)
            {
                msg = "Checked deformity " + _itm.DEFORMITYTYPECODE.ToString();
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {
//                    msg = validationResults.Errors[0].ErrorMessage;
                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 2);
                    }

                    isvalid = false;
                }
            }
            return isvalid;
        }

        public bool ValidStemMap(TREE _tree)
        {
            bool isvalid = true;
            List<STEMMAP> _list = _databasehelper.GetFilteredStemmapData(_tree.TREEID);
            StemMapValidator _validator = new StemMapValidator(true);
            _validator.CascadeMode = CascadeMode.Continue;
            msg = _list.Count.ToString() + " stem map(s) in tree " + _tree.TREENUMBER.ToString();
            foreach (var _itm in _list)
            {
                ValidationResult validationResults = _validator.Validate(_itm);
                if (!validationResults.IsValid)
                {
                    //  msg = validationResults.Errors[0].ErrorMessage;
                    foreach (var err in validationResults.Errors)
                    {
                        AddMsg(err.ErrorMessage, 2);
                    }

                    isvalid = false;
                }
            }
            return isvalid;
        }

        public string msg
        {
            get
            {
                return _msg;
            }
            set
            {
                _msg = _msg + value + Environment.NewLine;
            }
        }
        private void AddMsg(string newmsg, int indent = 0)
        {
            string tmpmsg = "";
            for (int i = 0; i < indent; i++)
            {
                tmpmsg = tmpmsg + "\t";
            }
            msg = tmpmsg + newmsg;
        }
    }
    public class ProjectValidator : AbstractValidator<PROJECT>  
    {  
        public ProjectValidator()  
        {  
            RuleFor(c => c.NAME).NotEmpty().WithMessage("Project name should not be empty.");
            RuleFor(c => c.DESCRIPTION).NotEmpty().WithMessage("Project description should not be empty.");
            RuleFor(c => c.PROJECT_DATE).NotEmpty().WithMessage("Project date should not be empty.");
        }  

    }
    public class PersonValidator : AbstractValidator<PERSON>
    {
        public PersonValidator()
        {
            RuleFor(c => c.FIRSTNAME).NotEmpty().WithMessage("First name should not be empty.");
            RuleFor(c => c.LASTNAME).NotEmpty().WithMessage("Last name should not be empty.");
        }
    }
    public class PhotoValidator : AbstractValidator<PHOTO>
    {
        public PhotoValidator(bool DoFullValidation = false)
        {
            RuleFor(c => c.PHOTOTYPE).NotEmpty().WithMessage("Photo Type cannot be empty");
            if (DoFullValidation)
            {
                RuleFor(c => c).Must(c => c.AZIMUTH <= 360 && c.AZIMUTH >= 0).WithMessage("Azimuth must be between 0 and 360");
                RuleFor(c => c).Must(c => c.DISTANCE <= 15 && c.DISTANCE >= 0).WithMessage("Distance must be between 0 and 15m");

            }
        }
    }
    public class PlotValidator : AbstractValidator<PLOT>
    {
        public PlotValidator(bool DoFullvalidation = false)
        {
            RuleFor(c => c).Must(c => IsUniquePlotNum(c)).WithMessage("Plot number must be unique within the project.");
            RuleFor(c => c.VSNPLOTNAME).Must(c => c.Length == 9).WithMessage("Plot length must be 9 digits.");
            RuleFor(c => c.VSNPLOTNAME).NotEmpty().WithMessage("Plot number should not be empty.");
            RuleFor(c => c.VSNPLOTTYPECODE).NotEmpty().WithMessage("Plot type should not be empty.");
            RuleFor(c => c.PLOTOVERVIEWDATE).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("Plot date should > 01-01-2020.");

            if (DoFullvalidation )
            {
                RuleFor(c => c.DECLINATION).Must(c => c >= -20 && c <= 20).WithMessage("Declination must be between -20 and 20.");
                RuleFor(c => c.UTMZONE).Must(c => c >= 15 && c <= 18).WithMessage("UTM Zone be between 15 and 18.");
                RuleFor(c => c.FIELD_CREW1).NotEmpty().WithMessage("You must have at least one crew member in Field Crew 1 field in the Plot screen");
                RuleFor(c => c.STANDINFODATE).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("Stand Information date should > 01-01-2020.");
                RuleFor(c => c.STANDINFOPERSON).NotEmpty().WithMessage("You must have at Stand information Person in the Stand Info screen");
                RuleFor(c => c.AGEPERSON).NotEmpty().WithMessage("You must have an Age Person in the Plot Crew Screen");
                RuleFor(c => c.AGEDATE).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("You must have a valid Age Date in the Plot Crew screen");

             //   RuleFor(c => c.SITERANK).Must(c => c >= 1 && c <= 2).WithMessage("Site Rank must be 1 or 2.");
                RuleFor(c => c.PERCENTAFFECTED).Must(c => c >= 20 && c <= 100).When(c => c.DISTURBANCECODE1 != 0).WithMessage("Percent affected must be between 20 and 100 when Disturbance is present.");
                RuleFor(c => c.PERCENTMORTALITY).Must(c => c >= 0 && c <= 100).When(c => c.DISTURBANCECODE1 != 0).WithMessage("Percent mortality must be between 0 and 100 when Disturbance is present.");
                RuleFor(c => c.DISTANCETARGETMOVED).Must(c => c <= 50).WithMessage("Distance target moved must be less than 50m");
                RuleFor(c => c.EASTING).Must(c => c >= 250000 && c <= 750000).WithMessage("Easting should be between 250000 and 750000 m");
                RuleFor(c => c.GROWTHPLOTNUMBER).Must(c => c > 0).When(c => c.VSNPLOTTYPECODE == "RME").WithMessage("Growth plot number should be populated when Plot type = RME");
                RuleFor(c => c.EXISTINGPLOTNAME).Must(c => c != null).When(c => c.VSNPLOTTYPECODE == "RME").WithMessage("Existing plot name should be populated when Plot type = RME");
                RuleFor(c => c.EXISTINGPLOTTYPECODE).Must(c => c != null).When(c => c.VSNPLOTTYPECODE == "RME").WithMessage("Existing plot type code should be populated when Plot type = RME");
                RuleFor(c => c.NONSTANDARDTYPECODE).Must(c => c == 6).When(c => c.VSNPLOTTYPECODE == "RME").WithMessage("Non Standard Type Code should be 6 when Plot type = RME");
                RuleFor(c => c.NORTHING).Must(c => c >= 4620000 && c <= 7000000).WithMessage("Northing should be between 4620000 and 7000000 m");
                RuleFor(c => c.CANOPYSTRUCTURECODE1).NotEmpty().WithMessage("Canopy Structure 1 should be filled out in the Stand Info Screen");
                RuleFor(c => c.MAINCANOPYORIGINCODE1).NotEmpty().WithMessage("Main Canopy Origin 1 should be filled out in the Stand Info Screen");
                RuleFor(c => c.MATURITYCLASSCODE1).NotEmpty().WithMessage("Maturity Class Code 1 should be filled out in the Stand Info Screen");

                When(c => c.VSNPLOTTYPECODE == "C", () =>
                {
                    RuleFor(c => c.DOWNWOODYDEBRISPERSON ).NotEmpty().WithMessage("You must have a DWD Person in the Plot Crew Screen");
                    RuleFor(c => c.DOWNWOODYDEBRISDATE).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("You must have a valid DWD Date in the Plot Crew screen");
                    RuleFor(c => c.SMALLTREEPERSON ).NotEmpty().WithMessage("You must have a Small Tree Person in the Plot Crew Screen");
                    RuleFor(c => c.SMALLTREESHRUBDATE).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("You must have a valid Small Tree Date in the Plot Crew screen");
                    RuleFor(c => c.STEMMAPPINGPERSON ).NotEmpty().WithMessage("You must have a DWD Person in the Stem Mapping Person in the Plot Screen");
                    RuleFor(c => c.STEMMAPPINGDATE ).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("You must have a valid Stem Mapping Date in the Plot Crew screen");
                    RuleFor(c => c.UNDERSTORYVEGETATIONPERSON ).NotEmpty().WithMessage("You must have a Understory Veg Person in the Plot Crew Screen");
                    RuleFor(c => c.UNDERSTORYVEGETATIONDATE ).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("You must have a valid Understory Veg Date in the Plot Crew screen");
                    RuleFor(c => c.UNDERSTORYCENSUSPERSON ).NotEmpty().WithMessage("You must have a Understory Census Person in the Plot Crew Screen");
                    RuleFor(c => c.UNDERSTORYCENSUSDATE).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("You must have a valid Understory Census Date in the Plot Crew screen");


                });
                When(c => (c.VSNPLOTTYPECODE == "B"), () =>
                {
                    RuleFor(c => c.SMALLTREEPERSON).NotEmpty().WithMessage("You must have a Small Tree Person in the Plot Crew Screen");
                    RuleFor(c => c.SMALLTREESHRUBDATE).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("You must have a valid Small Tree Date in the Plot Crew screen");
                    RuleFor(c => c.STEMMAPPINGPERSON).NotEmpty().WithMessage("You must have a DWD Person in the Stem Mapping Person in the Plot Screen");
                    RuleFor(c => c.STEMMAPPINGDATE).GreaterThanOrEqualTo(DateTime.Parse("1/1/2020")).WithMessage("You must have a valid Stem Mapping Date in the Plot Crew screen");
                });

            }
        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool IsUniquePlotNum(PLOT _plot)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsPlotNumUnique(_plot);
        }
    }
    
    public class TreeValidator : AbstractValidator<TREE>
    {
        public TreeValidator(bool DoFullvalidation = false)
        {
            RuleFor(c => c).Must(c => IsUniqueTreeNum(c)).WithMessage("Tree number must be unique within the plot.");
            RuleFor(c => c.TREENUMBER).NotEmpty().WithMessage("Tree number should not be empty.");
          //  RuleFor(c => c.TREENUMBER).Length(1, 5).WithMessage("Tree numbers must be integers");
            RuleFor(c => c.TREENUMBER).LessThan(2000).WithMessage("Tree numbers must be < 2000");
            RuleFor(c => c.TREENUMBER).GreaterThan(0).WithMessage("Tree numbers must be > 0");
            RuleFor(c => c.SPECIESCODE).NotEmpty().WithMessage("Species should not be empty.");
            
            if (DoFullvalidation)
            {

            RuleFor(c => c.TREEORIGINCODE).NotEmpty().WithMessage("Tree origin should not be empty.");
            RuleFor(c => c.DBH).NotEmpty().WithMessage("Tree DBH should not be empty and be between 0 and 200.");
            RuleFor(c => c.DBH).GreaterThanOrEqualTo((float)7.10).WithMessage("Tree DBH should be >= 7.10cm.");
            RuleFor(c => c.DBH).LessThanOrEqualTo((float)150.00).WithMessage("Tree DBH should be <= 150.00cm");
            RuleFor(c => (decimal)c.DBH).ScalePrecision(2, 6).WithMessage("DBH can have up to 2 decimals");
            RuleFor(c => c).Must(c => c.SPECIESCODE  == 0).WithMessage("Species code should be empty when tree status = C, X.").When(c => c.TREESTATUSCODE.Contains("C") || c.TREESTATUSCODE.Contains("X"));
            RuleFor(c => c).Must(c => c.TREEORIGINCODE == null).WithMessage("Tree Origin Code should be null when tree status = C, X.").When(c => c.TREESTATUSCODE.Contains("C") || c.TREESTATUSCODE.Contains("X"));
            RuleFor(c => c).Must(c => c.HEIGHTTODBH == 0).WithMessage("Ht to DBH should be 0 when tree status = C, X.").When(c => c.TREESTATUSCODE.Contains("C") || c.TREESTATUSCODE.Contains("X"));
            // Live  Trees and dead trees
            When(c => (c.TREESTATUSCODE.Contains("L") || c.TREESTATUSCODE.Contains("V") || c.TREESTATUSCODE.Contains("D")) && c.VSNSTATUSCODE != "i", () =>
                {
                    RuleFor(c => c).Must(c => c.SPECIESCODE > 0).WithMessage("Species Code should not be null when tree status = L,V,D,DV.");
                    RuleFor(c => c).Must(c => c.TREEORIGINCODE != null).WithMessage("Tree origin code should not be null when tree status = L,V,D,DV.");
                    RuleFor(c => c).Must(c => c.HEIGHTTODBH > 0 && c.HEIGHTTODBH < 3).WithMessage("Ht to DBH should be > 0 and < 3 when tree status = L,V,D,DV.");
                    RuleFor(c => c).Must(c => c.BARKRETENTIONCODE > 0).WithMessage("Bark retention should not be empty when tree status = L,V,D,DV.");
                    RuleFor(c => c).Must(c => c.WOODCONDITIONCODE > 0).WithMessage("Wood condition should not be empty when tree status = L,V,D,DV.");
                });
                // Live  Trees
            When(c => (c.TREESTATUSCODE.Contains("L") || c.TREESTATUSCODE == "V") && c.VSNSTATUSCODE != "i", () =>
                {
                    RuleFor(c => c).Must(c => c.CROWNCLASSCODE != null).WithMessage("Crown Class Code should not be null when tree status = L,V.");
                    RuleFor(c => c).Must(c => c.STEMQUALITYCODE != null).WithMessage("Stem Quality should not be null when tree status = L,V.");
                    RuleFor(c => c).Must(c => c.DIRECTHEIGHTTOCONTLIVECROWN > 0 || c.OCULARHEIGHTTOCONTLIVECROWN > 0).WithMessage("Ht to LC must not be 0 when tree status = L,V.");
                    RuleFor(c => c).Must(c => c.DIRECTTOTALHEIGHT > c.DIRECTHEIGHTTOCONTLIVECROWN).When(c => c.DIRECTHEIGHTTOCONTLIVECROWN != 999).WithMessage("Ht must be > ht to live crown when tree status = L,V");
                    RuleFor(c => c.DIRECTTOTALHEIGHT).Must(c => c >= 2 && c <= 50).WithMessage("Ht must be between 0 and 50m when tree status = L,V");
                    RuleFor(c => c.DIRECTHEIGHTTOCONTLIVECROWN).Must(c => c > 0 && c <= 50).WithMessage("Ht to live crown must be between 0 and 50m when tree status = L,V");
                    RuleFor(c => c).Must(c => c.FIELDAGE >= 1 && c.FIELDAGE <= 500).When(c => c.CORESTATUSCODE != null).WithMessage("Field age shouldbe between 1 and 500");
                    RuleFor(c => c).Must(c => c.HEIGHTTOCORE > 0 && c.HEIGHTTOCORE <= 2.5).When(c => c.CORESTATUSCODE != null).WithMessage("Ht to core should be between 0 and 2.5m");
                    RuleFor(c => c).Must(c => c.OFFICERINGCOUNT <= 500).When(c => c.CORESTATUSCODE != null).WithMessage("Office ring count should be less than 500");
                    RuleFor(c => c).Must(c => c.MISSINGRINGS <= 20).When(c => c.CORESTATUSCODE != null).WithMessage("Missing ring count should be less than 20");
                    RuleFor(c => c).Must(c => c.SOUNDWOODLENGTH <= 50).When(c => c.CORESTATUSCODE != null).WithMessage("Sound wood length should be less than 50cm");
                    RuleFor(c => c).Must(c => c.HEIGHTTODEADTIP > c.DIRECTTOTALHEIGHT).When(c => c.HEIGHTTODEADTIP > 0 && c.HEIGHTTODEADTIP != 999).WithMessage("Ht to dead tip must be > than ht");
                    RuleFor(c => c).Must(c => c.DIRECTTOTALHEIGHT == 0).When(c => c.OCULARTOTALHEIGHT > 0).WithMessage("Direct total height must be zero when using Ocular Height");
                    RuleFor(c => c).Must(c => c.OCULARTOTALHEIGHT == 0).When(c => c.DIRECTTOTALHEIGHT  > 0).WithMessage("Ocular height must be zero when using Direct Height");

                });
                // Dead  Trees
                When(c => c.TREESTATUSCODE.Contains("D") && c.VSNSTATUSCODE != "i", () =>
                {
                    RuleFor(c => c).Must(c => c.DECAYCLASS > 0).WithMessage("Decay Class should not be 0  when tree status = D,DV.");
                    RuleFor(c => c).Must(c => c.MORTALITYCAUSECODE > 0).WithMessage("Mortality cause should not be 0 when tree status = D,DV.");
                    RuleFor(c => c).Must(c => c.HEIGHTTOCORE == 0).WithMessage("Ht to Core must be 0 when tree status = D,DV.");
                    RuleFor(c => c).Must(c => c.DIRECTHEIGHTTOCONTLIVECROWN == 999).WithMessage("Ht to LC must be 999 when tree status = D,DV.");
                    RuleFor(c => c).Must(c => c.OCULARHEIGHTTOCONTLIVECROWN == 999).WithMessage("Ht to LC must be 999 when tree status = D,DV.");
                    RuleFor(c => c).Must(c => c.HEIGHTTOCORE == 0).WithMessage("Ht to Core must be 0 when tree status = D,DV.");
                    RuleFor(c => c).Must(c => c.OCULARTOTALHEIGHT > 0).WithMessage("Ocular Ht must not be 0 when tree status = D,DV");
                    RuleFor(c => c.CORESTATUSCODE).Must(c => c == null).WithMessage("No ages are done for tree when tree status = D,DV");
                    RuleFor(c => c.FIELDAGE).Must(c => c == 0).WithMessage("No ages are done for tree when tree status = D,DV");

                });

                RuleFor(c => c).Must(c => c.DIRECTOFFSETDISTANCE > 0).When(c => c.DEGREEOFLEAN > 0).WithMessage("Offset dist must be > 0 when degree of lean > 0").Unless(c => c.VSNSTATUSCODE == "i");
                RuleFor(c => c).Must(c => c.DEGREEOFLEAN > 0).When(c => c.DIRECTOFFSETDISTANCE > 0).WithMessage("Degree of Lean must be > 0 when offset distance > 0").Unless(c => c.VSNSTATUSCODE == "i");
                RuleFor(c => c.DEGREEOFLEAN).Must(c => c <= 90).WithMessage("Degree of lean must be <= 90").Unless(c => c.VSNSTATUSCODE == "i");
                // Offset check
                When(c => c.DIRECTTOTALHEIGHT > 0, () =>
               {
                   RuleFor(c => c).Must(c => c.DIRECTOFFSETDISTANCE <= c.DIRECTTOTALHEIGHT).WithMessage("Direct Offset Dist must be <= height").Unless(c => c.VSNSTATUSCODE == "i");
               });
                When(c => c.OCULARTOTALHEIGHT > 0, () =>
                {

                    RuleFor(c => c).Must(c => c.DIRECTOFFSETDISTANCE <= c.OCULARTOTALHEIGHT).WithMessage("Direct Offset Dist must be <= height").Unless(c => c.VSNSTATUSCODE == "i");
                });

            }

        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool IsUniqueTreeNum(TREE _tree)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsTreeNumUnique(_tree); 
        }

    }

    public class TreeAgeValidator : AbstractValidator<TREE>
    {
        public TreeAgeValidator(bool DoFullValidation = false)
        {
            if (DoFullValidation)
            {
                // Organic horizons
            When(c => c.CORESTATUSCODE == "C" || c.CORESTATUSCODE == "M" || c.CORESTATUSCODE == "S", () =>
            {
                RuleFor(c => c).Must(c => c.FIELDAGE > 0 && c.FIELDAGE < 500).WithMessage("Field age should be between 1 and 500");
                RuleFor(c => c).Must(c => c.HEIGHTTOCORE >=0 && c.HEIGHTTOCORE < 2.5).WithMessage("Height to core should be < 2.5m");
                RuleFor(c => c).Must(c => c.MISSINGRINGS >= 0 && c.MISSINGRINGS <= 10).WithMessage("Missing rings should be between 0 and 10");
                RuleFor(c => c).Must(c => c.OFFICERINGCOUNT > 0 && c.OFFICERINGCOUNT < 500).WithMessage("Office ring count should be between 1 and 500");
          //      RuleFor(c => c).Must(c => c.SOUNDWOODLENGTH  > 0 && c.SOUNDWOODLENGTH < 50).WithMessage("Sound Wood length be between 0 and 50");
                RuleFor(c => c).Must(c => c.TREESTATUSCODE == "L" || c.TREESTATUSCODE == "V").WithMessage("Only L, V trees should have ages");
//            ([CoreStatusCode] = 'C' AND[MissingRings] IS NOT NULL AND[MissingRings]>= (0) AND[MissingRings] <= (4) OR[CoreStatusCode] = 'M' AND[MissingRings] IS NOT NULL AND[MissingRings]>= (5) AND[MissingRings] <= (10) OR([CoreStatusCode] = 'R' OR[CoreStatusCode] = 'S') AND[MissingRings] IS NULL)

            });
            When(c => c.CORESTATUSCODE == "R", () =>
                {
                    RuleFor(c => c).Must(c => c.FIELDAGE > 0 && c.FIELDAGE < 500).WithMessage("Field age should be between 1 and 500");
                    RuleFor(c => c).Must(c => c.HEIGHTTOCORE >= 0 && c.HEIGHTTOCORE < 2.5).WithMessage("Height to core should be < 2.5m");
                    RuleFor(c => c).Must(c => c.MISSINGRINGS >= 0 && c.MISSINGRINGS <= 10).WithMessage("Missing rings should be between 0 and 10");
                    RuleFor(c => c).Must(c => c.OFFICERINGCOUNT > 0 && c.OFFICERINGCOUNT < 500).WithMessage("Office ring count should be between 1 and 500");
                    RuleFor(c => c).Must(c => c.SOUNDWOODLENGTH > 0 && c.SOUNDWOODLENGTH < 50).WithMessage("Sound Wood length be between 0 and 50");
                    RuleFor(c => c).Must(c => c.TREESTATUSCODE == "L" || c.TREESTATUSCODE == "V").WithMessage("Only L, V trees should have ages");
                    //            ([CoreStatusCode] = 'C' AND[MissingRings] IS NOT NULL AND[MissingRings]>= (0) AND[MissingRings] <= (4) OR[CoreStatusCode] = 'M' AND[MissingRings] IS NOT NULL AND[MissingRings]>= (5) AND[MissingRings] <= (10) OR([CoreStatusCode] = 'R' OR[CoreStatusCode] = 'S') AND[MissingRings] IS NULL)

                });
            }

        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool IsUniqueTreeNum(TREE _tree)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsTreeNumUnique(_tree);
        }

    }
    public class StemMapValidator : AbstractValidator<STEMMAP>
    {
        public StemMapValidator(bool DofullValidation = false)
        {
            RuleFor(c => c.AZIMUTH).NotEmpty().WithMessage("Azimuth should not be empty.");
            RuleFor(c => c.DISTANCE).NotEmpty().WithMessage("Distance should not be empty.");
            RuleFor(c => c).Must(c => c.AZIMUTH <=360).WithMessage("Azimuth should not be empty and less than 360.");
            RuleFor(c => c).Must(c => c.DISTANCE <= 20).WithMessage("Distance should not be empty and less than 20m.");
            if (DofullValidation)
            {
//                RuleFor(c => c.CROWNWIDTH1).NotEmpty().WithMessage("Crown Width 1 should not be empty.");
//                RuleFor(c => c.CROWNWIDTH2).NotEmpty().WithMessage("crown Width 2 should not be empty.");
                RuleFor(c => c).Must(c => c.CROWNWIDTH1 >= 0 && c.CROWNWIDTH1 <= 30).WithMessage("Crown width 1 should be less than 30m.");
                RuleFor(c => c).Must(c => c.CROWNWIDTH2 >= 0 &&  c.CROWNWIDTH2 <= 30).WithMessage("Crown width 2 should be less than 30m.");
                RuleFor(c => c).Must(c => c.CROWNOFFSETAZIMUTH <= 360).WithMessage("Crown Offset Azimuth should be less than 360.");
                RuleFor(c => c).Must(c => c.CROWNOFFSETDISTANCE <= 50).WithMessage("Crown offset distance should be and less than 50m.");
                RuleFor(c => c).Must(c => c.CROWNOFFSETDISTANCE > 0).When(c => c.CROWNOFFSETAZIMUTH >0).WithMessage("Crown offset distance should be populated when crown offset azimuth >0");
                RuleFor(c => c).Must(c => c.CROWNOFFSETAZIMUTH > 0).When(c => c.CROWNOFFSETDISTANCE > 0).WithMessage("Crown offset azimuth should be populated when crown offset distance >0");
            }
        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
    }
 
    public class EcositeValidator : AbstractValidator<ECOSITE>
    {
        public EcositeValidator(bool DoFullValidation = false)
        {
          //  RuleFor(c => c.PRI_ECO).NotEmpty().When(c => c.PRI_ECO !=null).WithMessage("Primary ecosite should not be empty.");
            RuleFor(c => c).Must(c => IsValidEcosite(c.PRI_ECO)).When(c => c.PRI_ECO != null).WithMessage("Invalid ecosite for PRI_ECO.");
            RuleFor(c => c).Must(c => IsValidEcosite(c.SEC_ECO)).When(c => c.SEC_ECO != null).WithMessage("Invalid ecosite for SEC_ECO.");
            RuleFor(c => c.MOISTUREREGIMECODE).NotEmpty().WithMessage("Moisture Regime should not be empty.");
            if (DoFullValidation )
            {
                //  RuleFor(c => c.PRI_ECO).NotEmpty().When(c => c.PRI_ECO !=null).WithMessage("Primary ecosite should not be empty.");
                RuleFor(c => c).Must(c => IsValidEcosite(c.PRI_ECO)).WithMessage("Invalid ecosite for PRI_ECO.");
                RuleFor(c => c).Must(c => IsValidEcosite(c.SEC_ECO)).When(c => c.SEC_ECO != null).WithMessage("Invalid ecosite for SEC_ECO.");
                RuleFor(c => c.MOISTUREREGIMECODE).NotEmpty().WithMessage("Moisture Regime should not be empty.");
                RuleFor(c => c).Must(c => c.PRI_ECO_PCT + c.SEC_ECO_PCT == 100).WithMessage("Ecosites must sum to 100.");
                RuleFor(c => c).Must(c => c.PRI_ECO_PCT >= 20 && +c.PRI_ECO_PCT <= 100).WithMessage("Primary ecosite must be between 20 and 100.");
                RuleFor(c => c).Must(c => c.SEC_ECO_PCT <= 100).WithMessage("Secondary ecosite must be less than 100.");
                RuleFor(c => c).Must(c => c.DEPTHTODISTINCTMOTTLES <= c.DEPTHTOIMPASSABLECOARSEFRAGMENTS).When(c => c.DEPTHTOIMPASSABLECOARSEFRAGMENTS > 0).WithMessage("Depth to mottles must be < depth to impassable coarse frags.");
                RuleFor(c => c).Must(c => c.DEPTHTOPROMINENTMOTTLES <= c.DEPTHTOIMPASSABLECOARSEFRAGMENTS).When(c => c.DEPTHTOIMPASSABLECOARSEFRAGMENTS > 0).WithMessage("Depth to mottles must be < depth to impassable coarse frags.");
                RuleFor(c => c).Must(c => c.DEPTHTOGLEY <= c.DEPTHTOIMPASSABLECOARSEFRAGMENTS).When(c => c.DEPTHTOIMPASSABLECOARSEFRAGMENTS > 0).WithMessage("Depth to gley must be < depth to impassable coarse frags.");
                RuleFor(c => c).Must(c => c.DEPTHTOCARBONATES <= c.DEPTHTOIMPASSABLECOARSEFRAGMENTS).When(c => c.DEPTHTOIMPASSABLECOARSEFRAGMENTS > 0).WithMessage("Depth to carbonates must be < depth to impassable coarse frags.");
                RuleFor(c => c).Must(c => c.DEPTHTOSEEPAGE  <= c.DEPTHTOIMPASSABLECOARSEFRAGMENTS).When(c => c.DEPTHTOIMPASSABLECOARSEFRAGMENTS > 0).WithMessage("Depth to seepage must be < depth to impassable coarse frags.");
                RuleFor(c => c).Must(c => c.DEPTHTOWATERTABLE <= c.DEPTHTOIMPASSABLECOARSEFRAGMENTS).When(c => c.DEPTHTOIMPASSABLECOARSEFRAGMENTS > 0).WithMessage("Depth to water table must be < depth to impassable coarse frags.");
                RuleFor(c => c).Must(c => c.FUNCTIONALROOTINGDEPTH <= c.DEPTHTOIMPASSABLECOARSEFRAGMENTS).When(c => c.DEPTHTOIMPASSABLECOARSEFRAGMENTS > 0).WithMessage("Functional rooting must be < depth to impassable coarse frags.");
                RuleFor(c => c).Must(c => c.MAXIMUMROOTINGDEPTH <= c.DEPTHTOIMPASSABLECOARSEFRAGMENTS).When(c => c.DEPTHTOIMPASSABLECOARSEFRAGMENTS > 0).WithMessage("Max rooting depth must be < depth to impassable coarse frags.");
                RuleFor(c => c).Must(c => c.DEPTHTODISTINCTMOTTLES <= c.DEPTHTOBEDROCK).When(c => c.DEPTHTOBEDROCK > 0).WithMessage("Depth to mottles must be < depth to bedrock.");
                RuleFor(c => c).Must(c => c.DEPTHTOPROMINENTMOTTLES <= c.DEPTHTOBEDROCK).When(c => c.DEPTHTOBEDROCK > 0).WithMessage("Depth to mottles must be < depth to bedrock.");
                RuleFor(c => c).Must(c => c.DEPTHTOGLEY <= c.DEPTHTOBEDROCK).When(c => c.DEPTHTOBEDROCK > 0).WithMessage("Depth to gley must be < depth to bedrock.");
                RuleFor(c => c).Must(c => c.DEPTHTOCARBONATES <= c.DEPTHTOBEDROCK).When(c => c.DEPTHTOBEDROCK > 0).WithMessage("Depth to carbonates must be < depth to bedrock.");
                RuleFor(c => c).Must(c => c.DEPTHTOSEEPAGE <= c.DEPTHTOBEDROCK).When(c => c.DEPTHTOBEDROCK > 0).WithMessage("Depth to seepage must be < depth to bedrock.");
                RuleFor(c => c).Must(c => c.DEPTHTOWATERTABLE <= c.DEPTHTOBEDROCK).When(c => c.DEPTHTOBEDROCK > 0).WithMessage("Depth to water table must be < depth to bedrock.");
            //    RuleFor(c => c).Must(c => c.FUNCTIONALROOTINGDEPTH <= c.DEPTHTOBEDROCK).When(c => c.DEPTHTOBEDROCK > 0).WithMessage("Functional rooting must be < depth to bedrock.");
             //   RuleFor(c => c).Must(c => c.MAXIMUMROOTINGDEPTH <= c.DEPTHTOBEDROCK).When(c => c.DEPTHTOBEDROCK > 0).WithMessage("Max rooting depth must be < depth to bedrock.");
                RuleFor(c => c).Must(c => c.PITAZIMUTH >= 1 && c.PITAZIMUTH <= 360).WithMessage("Pit azimuth should be between 1 and 360.");
                RuleFor(c => c).Must(c => c.PITDISTANCE >= 0 && c.PITDISTANCE  <= 20).WithMessage("Pit distance should be <= 20m.");

                // RuleFor(c => c).Must(c => c..SEC_ECO)).When(c => c.SEC_ECO != null).WithMessage("Invalid ecosite for SEC_ECO.");
            }

        }
        bool IsValidEcosite(string strcheck)
        {
            List<string> ListCheck = new List<string>();
            using (var s = typeof(EcositeValidator).Assembly.GetManifestResourceStream("eLiDAR.Data.Ecosites.txt"))
            {
                ListCheck = new System.IO.StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
                if (ListCheck.Contains(strcheck)) { return true; }
            }
            return false;
        }
        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
    }
    public class SoilValidator : AbstractValidator<SOIL>
    {
        public SoilValidator(bool DoFullValidation = false)
        {
            RuleFor(c => c).Must(c => IsUniqueSoilNum(c)).WithMessage("Soil layer number must be unique within the plot.");
            RuleFor(c => c).Must(c => IsValidHorizon(c.HORIZON)).When(c => c.HORIZON != null).WithMessage("Invalid soil horizon.");
            RuleFor(c => c).Must(c => IsValidColour(c.MATRIXCOLOUR)).When(c => c.MATRIXCOLOUR != null).WithMessage("Invalid soil matrix colour.");
            RuleFor(c => c).Must(c => IsValidStructure(c.STRUCTURE)).When(c => c.STRUCTURE != null).WithMessage("Invalid soil structure.");
            RuleFor(c => c).Must(c => IsValidColour(c.GLEYCOLOUR)).When(c => c.GLEYCOLOUR != null).WithMessage("Invalid soil gley colour.");
            RuleFor(c => c).Must(c => IsValidColour(c.MOTTLECOLOUR)).When(c => c.MOTTLECOLOUR != null).WithMessage("Invalid soil mottle colour.");
            RuleFor(c => c.HORIZONNUMBER).NotEmpty().WithMessage("Soil Layer number is required.");
            RuleFor(c => c.DEPTHTO).NotEqual(c => c.DEPTHFROM).WithMessage("To and From must not be the same value.");
            RuleFor(c => c.DEPTHTO).LessThan(250).WithMessage("To must be be less than 250cm.");
            RuleFor(c => c.DEPTHFROM).LessThan(250).WithMessage("From must be be less than 250cm.");

            if (DoFullValidation)
            {

                When(c => c.DEPTHTO > c.DEPTHFROM, () =>
                {
                    RuleFor(c => c.DEPTHTO).LessThan(c => c.DEPTHFROM).WithMessage("This soil layer is below the soil 0 cm mark.  Confirm that is the case.");
                });

                When(c => c.DEPTHTO < c.DEPTHFROM, () =>
                {
                    RuleFor(c => c.DEPTHTO).LessThan(c => c.DEPTHFROM).WithMessage("This soil layer is above the soil 0 cm mark.  Confirm that is the case.");
                });

                // Organic horizons
                When(c => c.HORIZON == "Of" || c.HORIZON == "Of1" || c.HORIZON == "Of2" || c.HORIZON == "Of3" || c.HORIZON == "Of4" || c.HORIZON == "Om" || c.HORIZON == "Om1" || c.HORIZON == "Om2" || c.HORIZON == "Oh" || c.HORIZON == "Oh1" || c.HORIZON == "Oh2", () =>
                 {
                     RuleFor(c => c).Must(c => c.DECOMPOSITIONCODE != null).WithMessage("A decomposition code is required for organic horizons");
                     RuleFor(c => c).Must(c => c.MATRIXCOLOUR == null).WithMessage("No matrix colour is used for organic horizons");
                     RuleFor(c => c).Must(c => c.MOTTLECOLOUR == null).WithMessage("No mottle colour is used for organic horizons");
                     RuleFor(c => c).Must(c => c.GLEYCOLOUR == null).WithMessage("No gley colour is used for organic horizons");
                     RuleFor(c => c).Must(c => c.PERCENTGRAVEL == 0).WithMessage("% gravel must be 0 for organic horizons");
                     RuleFor(c => c).Must(c => c.PERCENTCOBBLE == 0).WithMessage("% cobble must be 0 for organic horizons");
                     RuleFor(c => c).Must(c => c.PERCENTSTONE == 0).WithMessage("% stone must be 0 for organic horizons");
                     RuleFor(c => c).Must(c => c.STRUCTURE == null).WithMessage("Structure must be empty for organic horizons");
                     RuleFor(c => c).Must(c => c.MINERALTEXTURECODE == null).WithMessage("Texture must be empty for organic horizons");
                     RuleFor(c => c).Must(c => c.POREPATTERNCODE == null).WithMessage("Pore pattern must be empty for organic horizons");
                 });
                // Mineral horizons
                When(c => c.HORIZON != "LFH" && c.HORIZON != "LF" && c.HORIZON != "L" && c.HORIZON != "F" && c.HORIZON != "H" && c.HORIZON != "Hi" && c.HORIZON != "Of" && c.HORIZON != "Of1" && c.HORIZON != "Of2" && c.HORIZON != "Of3" && c.HORIZON != "Of4" && c.HORIZON != "Om" && c.HORIZON != "Om1" && c.HORIZON != "Om2" && c.HORIZON != "Oh" && c.HORIZON != "Oh1" && c.HORIZON != "Oh2", () =>
                {
                    RuleFor(c => c).Must(c => c.DECOMPOSITIONCODE == null).WithMessage("A decoposition code is not required for mineral horizons");
                    RuleFor(c => c).Must(c => c.MATRIXCOLOUR != null).WithMessage("Matrix colour is used for mineral horizons");
                // RuleFor(c => c).Must(c => c.MOTTLECOLOUR != null).WithMessage("Mottle colour is used for mineral horizons");
                // RuleFor(c => c).Must(c => c.GLEYCOLOUR != null).WithMessage("No gley colour is used for mineral horizons");
                RuleFor(c => c).Must(c => c.PERCENTGRAVEL <= 100).WithMessage("% gravel must be <100 for mineral horizons");
                    RuleFor(c => c).Must(c => c.PERCENTCOBBLE <= 100).WithMessage("% cobble must be <100 for mineral horizons");
                    RuleFor(c => c).Must(c => c.PERCENTSTONE <= 100).WithMessage("% stone must be <100 for mineral horizons");
                    RuleFor(c => c).Must(c => c.STRUCTURE != null).WithMessage("structure must not be empty for mineral horizons");
                    RuleFor(c => c).Must(c => c.MINERALTEXTURECODE != null).WithMessage("texture must not be empty for mineral horizons");
                    RuleFor(c => c).Must(c => c.POREPATTERNCODE != null).WithMessage("Pore pattern must not be empty for mineral horizons");
                });
            }
        }
        bool IsValidHorizon(string strcheck)
        {
            List<string> ListCheck = new List<string>();
            using (var s = typeof(SoilValidator).Assembly.GetManifestResourceStream("eLiDAR.Data.Horizons.txt"))
            {
                ListCheck = new System.IO.StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
                if (ListCheck.Contains(strcheck)) { return true; }
            }
            return false;
        }
        bool IsValidColour(string strcheck)
        {
            List<string> ListCheck = new List<string>();
            using (var s = typeof(SoilValidator).Assembly.GetManifestResourceStream("eLiDAR.Data.colours.txt"))
            {
                ListCheck = new System.IO.StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
                if (ListCheck.Contains(strcheck)) { return true; }
            }
            return false;
        }
        bool IsValidStructure(string strcheck)
        {
            List<string> ListCheck = new List<string>();
            using (var s = typeof(SoilValidator).Assembly.GetManifestResourceStream("eLiDAR.Data.Structure.txt"))
            {
                ListCheck = new System.IO.StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
                if (ListCheck.Contains(strcheck)) { return true; }
            }
            return false;
        }

        bool IsUniqueSoilNum(SOIL _soil)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsSoilNumUnique(_soil);
        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
    }
    public class SmallTreeValidator : AbstractValidator<SMALLTREE>
    {
        public SmallTreeValidator(bool DoFullValidation = false)
        {
            if(DoFullValidation)
            {
                RuleFor(c => c).Must(c => IsUnique(c)).WithMessage("Shrub species must be unique within the plot.");
                RuleFor(c => c).Must(c => c.HT_CLASS1_COUNT + c.HT_CLASS2_COUNT + c.HT_CLASS3_COUNT + c.HT_CLASS4_COUNT + c.HT_CLASS5_COUNT + c.HT_CLASS6_COUNT + c.HT_CLASS7_COUNT + c.HT_CLASS8_COUNT + c.COUNT > 0).WithMessage("Shrub Count must be > 0.");
            }
            RuleFor(c => c.SPECIESCODE).NotEmpty().WithMessage("Species should not be empty.");
        }
        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool IsUnique(SMALLTREE _table)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsSmallTreeUnique(_table);
        }
    }

    public class SmallTreeTallyValidator : AbstractValidator<SMALLTREETALLY>
    {
        public SmallTreeTallyValidator(bool DoFullValidation = false)
        {
            RuleFor(c => c.SPECIESCODE).NotEmpty().WithMessage("Species should not be empty.");
            //RuleFor(c => c).Must(c => IsUnique(c)).WithMessage("Small tree species must be unique within the plot.");
            RuleFor(c => c.HEIGHT).Must(c => c > 1.3).WithMessage("Height must be greater than 1.3");
            // dbh must be between 2.5 and 7
            RuleFor(c => c.DBH).Must(c => c > 2.5).WithMessage("Dbh must be greater than 2.5");
            RuleFor(c => c.DBH).Must(c => c < 7).WithMessage("Dbh must be less than 7");
            RuleFor(c => c.COUNT).Must(c => c > 0).WithMessage("Count must be greater than 0");

        }
        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool IsUnique(SMALLTREETALLY _table)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsSmallTreeTallyUnique(_table);
        }
    }


    public class VegetationValidator : AbstractValidator<VEGETATION>
    {
        public VegetationValidator(bool DoFullValidation = false)
        { 
            RuleFor(c => c.VSNSPECIESCODE).NotEmpty().WithMessage("Species should not be empty.");
        //    RuleFor(c => c).Must(c => IsUnique(c)).WithMessage("Vegetation species must be unique within the plot.");
            RuleFor(c => c).Must(c => IsValidPlant(c.VSNSPECIESCODE)).WithMessage("Invalid plant");
            RuleFor(c => c).Must(c => IsSpecimenUnique(c)).WithMessage("Vegetation specimen number must be unique within the plot.");

            RuleFor(c => c).Must(c => c.ELCLAYER3 >= 0 && c.ELCLAYER3 <= 99).WithMessage("% cover for ELC layer 3 must be <= 99%");
            RuleFor(c => c).Must(c => c.ELCLAYER4 >= 0 && c.ELCLAYER4 <= 99).WithMessage("% cover for ELC layer 4 must be <= 99%");
            RuleFor(c => c).Must(c => c.ELCLAYER5 >= 0 && c.ELCLAYER5 <= 99).WithMessage("% cover for ELC layer 5 must be <= 99%");
            RuleFor(c => c).Must(c => c.ELCLAYER6 >= 0 && c.ELCLAYER6 <= 99).WithMessage("% cover for ELC layer 6 must be <= 99%");
            RuleFor(c => c).Must(c => c.ELCLAYER7 >= 0 && c.ELCLAYER7 <= 99).WithMessage("% cover for ELC layer 7 must be <= 99%");

            RuleFor(c => (decimal)c.ELCLAYER3).ScalePrecision(1, 4).WithMessage("ELClayer3 can have up to 1 decimals");
            RuleFor(c => (decimal)c.ELCLAYER4).ScalePrecision(1, 4).WithMessage("ELClayer4 can have up to 1 decimals");
            RuleFor(c => (decimal)c.ELCLAYER5).ScalePrecision(1, 4).WithMessage("ELClayer5 can have up to 1 decimals");
            RuleFor(c => (decimal)c.ELCLAYER6).ScalePrecision(1, 4).WithMessage("ELClayer6 can have up to 1 decimals");
            RuleFor(c => (decimal)c.ELCLAYER7).ScalePrecision(1, 4).WithMessage("ELClayer7 can have up to 1 decimals");

            if (DoFullValidation)
            {
                RuleFor(c => c).Must(c => c.ELCLAYER3 + c.ELCLAYER4 + c.ELCLAYER5 + c.ELCLAYER6 + c.ELCLAYER7 > 0).WithMessage("No cover has been selected for this plant");
            }

        }
        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool IsValidPlant(string plant)
        {
            List<string> ListPlant = new List<string>();
            using (var s = typeof(VegetationValidator).Assembly.GetManifestResourceStream("eLiDAR.Data.Genus.txt"))
            {
                ListPlant = new System.IO.StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
                if (ListPlant.Contains(plant)) { return true; }
            }
            return false;
        }
        bool IsUnique(VEGETATION _table)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsVegUnique(_table);
        }
        bool IsSpecimenUnique(VEGETATION _table)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsSpecimenUnique(_table);
        }
    }
    public class VegetationCensusValidator : AbstractValidator<VEGETATIONCENSUS>
    {
        public VegetationCensusValidator(bool DoFullValidation = false )
        {
            RuleFor(c => c.VSNSPECIESCODE).NotEmpty().WithMessage("Species should not be empty.");
            RuleFor(c => c).Must(c => IsValidPlant(c.VSNSPECIESCODE)).WithMessage("Invalid plant");
      //      RuleFor(c => c).Must(c => IsUnique(c)).WithMessage("Vegetation species must be unique within the plot.");
            RuleFor(c => c).Must(c => IsSpecimenUnique(c)).WithMessage("Vegetation specimen number must be unique within the plot.");
        }
        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool IsValidPlant(string plant)
        {
            List<string> ListPlant = new List<string>();
            using (var s = typeof(VegetationCensusValidator).Assembly.GetManifestResourceStream("eLiDAR.Data.Genus.txt"))
            {
                ListPlant = new System.IO.StreamReader(s).ReadToEnd().Split('\n').Select(t => t.Trim()).ToList();
                if (ListPlant.Contains(plant)) { return true; }
            }
            return false;
        }
        bool IsUnique(VEGETATIONCENSUS _table)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsVegUnique(_table);
        }
        bool IsSpecimenUnique(VEGETATIONCENSUS _table)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsSpecimenUnique(_table);
        }
    }
    public class DeformityValidator : AbstractValidator<DEFORMITY>
    {
        public DeformityValidator(bool DoFullValidation = false)
        {
          //  RuleFor(c => c.HEIGHTFROM).NotEmpty().WithMessage("Ht From should not be empty.");
          //  RuleFor(c => c.HEIGHTTO).NotEmpty().WithMessage("Ht To should not be empty.");
            RuleFor(c => c).Must(c => c.HEIGHTFROM < c.HEIGHTTO).When(c => c.HEIGHTTO > 0).WithMessage("Ht To should > Ht from.");
            RuleFor(c => c).Must(c => c.DEFORMITYLENGTH <=10).WithMessage("Deformity length should be < 10m.");
            RuleFor(c => c).Must(c => c.DEFORMITYWIDTH <= 2).WithMessage("Deformity width should be < 2m.");
            RuleFor(c => c).Must(c => c.SCRAPE <=100).WithMessage("Scrape should be <= 100");
            RuleFor(c => c).Must(c => c.GOUGE <= 100).WithMessage("Gouge should be <= 100");
            RuleFor(c => c).Must(c => c.SCUFF <= 100).WithMessage("Scuff should be <= 100");
            RuleFor(c => c).Must(c => c.EXTENT  <= 100).WithMessage("Extent should be <= 100");
            RuleFor(c => c).Must(c => c.DEGREELEANARCH  <= 90).WithMessage("Degree Lean/Arch should be <= 90");
            RuleFor(c => c).Must(c => c.AZIMUTH >= 0 && c.AZIMUTH <= 360).WithMessage("Azimuth should be between 0 and 360");
        
            if (DoFullValidation)
            {
                // for specific deformites
                When(c => c.DEFORMITYTYPECODE == 0 || c.DEFORMITYTYPECODE == 7 || c.DEFORMITYTYPECODE == 24 || c.DEFORMITYTYPECODE == 25 || c.DEFORMITYTYPECODE == 26, () =>
                {
                    RuleFor(c => c).Must(c => c.HEIGHTFROM == 0).WithMessage("Ht from should be empty for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.HEIGHTTO == 0).WithMessage("Ht to should be empty for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.EXTENT == 0).WithMessage("Extent should be empty for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.QUADRANTCODE == null).WithMessage("Quadrant code should be empty for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.DEGREELEANARCH == 0).WithMessage("Degree lean arch should be empty for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.DEFORMITYLENGTH == 0).WithMessage("Deformity length should be empty for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.DEFORMITYWIDTH == 0).WithMessage("Deformity width should be empty for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.SCRAPE == 0).WithMessage("Scrape should be 0 for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.GOUGE == 0).WithMessage("Gouge should be 0 for deformities 0,7,24,25,26.");
                    RuleFor(c => c).Must(c => c.SCUFF == 0).WithMessage("Scuff should be 0 for deformities 0,7,24,25,26.");
                });
                // for specific deformites
                When(c => c.DEFORMITYTYPECODE == 1 || c.DEFORMITYTYPECODE == 2 || c.DEFORMITYTYPECODE == 6 || c.DEFORMITYTYPECODE == 14 || c.DEFORMITYTYPECODE == 17, () =>
                {
                    RuleFor(c => c).Must(c => c.HEIGHTFROM >= 0).WithMessage("Ht from should not be empty for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.HEIGHTTO == 0).WithMessage("Ht to should be empty for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.EXTENT == 0).WithMessage("Extent should be empty for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.QUADRANTCODE == null).WithMessage("Quadrant code should be empty for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.DEGREELEANARCH == 0).WithMessage("Degree lean arch should be empty for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.DEFORMITYLENGTH == 0).WithMessage("Deformity length should be empty for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.DEFORMITYWIDTH == 0).WithMessage("Deformity width should be empty for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.SCRAPE == 0).WithMessage("Scrape should be 0 for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.GOUGE == 0).WithMessage("Gouge should be 0 for deformities 1,2,6,14,17.");
                    RuleFor(c => c).Must(c => c.SCUFF == 0).WithMessage("Scuff should be 0 for deformities 1,2,6,14,17.");
                });
                When(c => c.DEFORMITYTYPECODE == 4 || c.DEFORMITYTYPECODE == 21, () =>
                {
                    RuleFor(c => c).Must(c => c.HEIGHTFROM == 0).WithMessage("Ht from should be empty for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.HEIGHTTO == 0).WithMessage("Ht to should be empty for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.EXTENT == 0).WithMessage("Extent should be empty for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.QUADRANTCODE == null).WithMessage("Quadrant code should be empty for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.DEGREELEANARCH > 0).WithMessage("Degree lean arch should not be empty for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.DEFORMITYLENGTH == 0).WithMessage("Deformity length should be empty for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.DEFORMITYWIDTH == 0).WithMessage("Deformity width should be empty for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.SCRAPE == 0).WithMessage("Scrape should be 0 for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.GOUGE == 0).WithMessage("Gouge should be 0 for deformities  4,21.");
                    RuleFor(c => c).Must(c => c.SCUFF == 0).WithMessage("Scuff should be 0 for deformities  4,21.");
                });
                When(c => c.DEFORMITYTYPECODE == 5 || c.DEFORMITYTYPECODE == 13 ||  c.DEFORMITYTYPECODE == 19 || c.DEFORMITYTYPECODE == 22 ||  c.DEFORMITYTYPECODE == 23, () =>
                {
                    RuleFor(c => c).Must(c => c.HEIGHTFROM >= 0).WithMessage("Ht from should not be empty for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.HEIGHTTO >= c.HEIGHTFROM).WithMessage("Ht to should not be empty and should be > Ht from for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.EXTENT == 0).WithMessage("Extent should be empty for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.QUADRANTCODE == null).WithMessage("Quadrant code should be empty for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.DEGREELEANARCH ==  0).WithMessage("Degree lean arch should not be empty for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.DEFORMITYLENGTH == 0).WithMessage("Deformity length should be empty for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.DEFORMITYWIDTH == 0).WithMessage("Deformity width should be empty for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.SCRAPE == 0).WithMessage("Scrape should be 0 for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.GOUGE == 0).WithMessage("Gouge should be 0 for deformities 5,13,19,22,23.");
                    RuleFor(c => c).Must(c => c.SCUFF == 0).WithMessage("Scuff should be 0 for deformities 5,13,19,22,23.");
                });
                When(c => c.DEFORMITYTYPECODE == 8, () =>
                {
                    RuleFor(c => c).Must(c => c.HEIGHTFROM == 0).WithMessage("Ht from should  be empty for deformity 8.");
                    RuleFor(c => c).Must(c => c.HEIGHTTO == 0).WithMessage("Ht to should be empty for deformity 8.");
                    RuleFor(c => c).Must(c => c.EXTENT > 0).WithMessage("Extent should not be empty for deformity 8.");
                    RuleFor(c => c).Must(c => c.QUADRANTCODE == null).WithMessage("Quadrant code should be empty for deformity 8.");
                    RuleFor(c => c).Must(c => c.DEGREELEANARCH == 0).WithMessage("Degree lean arch should not be empty for deformity 8.");
                    RuleFor(c => c).Must(c => c.DEFORMITYLENGTH == 0).WithMessage("Deformity length should be empty for deformity 8.");
                    RuleFor(c => c).Must(c => c.DEFORMITYWIDTH == 0).WithMessage("Deformity width should be empty for deformity 8.");
                    RuleFor(c => c).Must(c => c.SCRAPE == 0).WithMessage("Scrape should be 0 for deformity 8.");
                    RuleFor(c => c).Must(c => c.GOUGE == 0).WithMessage("Gouge should be 0 for deformity 8.");
                    RuleFor(c => c).Must(c => c.SCUFF == 0).WithMessage("Scuff should be 0 for deformity 8.");
                });
                When(c => c.DEFORMITYTYPECODE == 10, () =>
                {
                    RuleFor(c => c).Must(c => c.HEIGHTFROM >= 0).WithMessage("Ht from should not  be empty for deformity 10.");
                    RuleFor(c => c).Must(c => c.HEIGHTTO >= c.HEIGHTFROM).WithMessage("Ht to should not be empty for deformity 10.");
                    RuleFor(c => c).Must(c => c.EXTENT == 0).WithMessage("Extent should be empty for deformity 10.");
                    RuleFor(c => c).Must(c => c.QUADRANTCODE == null).WithMessage("Quadrant code should be empty for deformity 10.");
                    RuleFor(c => c).Must(c => c.DEGREELEANARCH == 0).WithMessage("Degree lean arch should not be empty for deformity 10.");
                    RuleFor(c => c).Must(c => c.DEFORMITYLENGTH > 0).WithMessage("Deformity length should not be empty for deformity 10.");
                    RuleFor(c => c).Must(c => c.DEFORMITYWIDTH > 0).WithMessage("Deformity width should not be empty for deformity 10.");
                    RuleFor(c => c).Must(c => c.SCRAPE > 0).WithMessage("Scrape should not be 0 for deformity 10.");
                    RuleFor(c => c).Must(c => c.GOUGE > 0).WithMessage("Gouge should not be 0 for deformity 10.");
                    RuleFor(c => c).Must(c => c.SCUFF > 0).WithMessage("Scuff should not be 0 for deformity 10.");
                    RuleFor(c => c).Must(c => c.AZIMUTH  > 0).WithMessage("Azimuth should not be 0 for deformity 10.");

                });
            }

        }
        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
    }
    public class DWDValidator : AbstractValidator<DWD>
    {
        public DWDValidator(bool DoFullValidation = false)
        {
            
            RuleFor(c => c.LINENUMBER).NotEmpty().WithMessage("Line Number must be populated");
            if (DoFullValidation)
            {
   //             RuleFor(c => c).Must(c => IsUniqueDWDNum(c)).WithMessage("DWD number must be unique within the line.");
                RuleFor(c => c.DECOMPOSITIONCLASS).NotEmpty().WithMessage("Decomp Class should not be empty.");
               
                //for accumualtions
                When(c => c.IS_ACCUM == "Y", () =>
                {
                    RuleFor(c => c).Must(c => c.ACCUMULATIONDEPTH >= 0.01 && c.ACCUMULATIONDEPTH <= 9.99).WithMessage("DWD accumulation depth should be between 0.01 and 9.99");
                    RuleFor(c => c).Must(c => c.ACCUMULATIONLENGTH >= 0.01 && c.ACCUMULATIONLENGTH <= 50).WithMessage("DWD accumulation length should be between 0.01 and 50");
                    RuleFor(c => c).Must(c => c.PERCENTCONIFER + c.PERCENTHARDWOOD == 100).WithMessage("DWD accumulation conifer + hardwood must = 100%");
                    RuleFor(c => c.DWDNUM).NotEmpty().WithMessage("DWD Number must be populated");
                });
                // for regular DWD
                When(c => c.IS_ACCUM != "Y", () =>
                {
                    RuleFor(c => c).Must(c => c.DOWNWOODYDEBRISLENGTH >= 0.01 && c.DOWNWOODYDEBRISLENGTH <= 40).WithMessage("DWD length should be between 0.01 and 40");
                    RuleFor(c => c).Must(c => c.LARGEDIAMETER >= 7.5 && c.LARGEDIAMETER <= 60).WithMessage("DWD large diam should be between 7.5 and 60cm");
                    RuleFor(c => c).Must(c => c.SMALLDIAMETER >= 7.5 && c.SMALLDIAMETER <= 60).WithMessage("DWD snmall diam should be between 7.5 and 60cm");
                    RuleFor(c => c).Must(c => c.DIAMETER >= 7.5 && c.DIAMETER <= 60).WithMessage("DWD diam should be between 7.5 and 60cm");
                    RuleFor(c => c).Must(c => c.TILTANGLE >= 0 && c.TILTANGLE <= 90).WithMessage("DWD tilt angle should be between 0 and 90");
                });
            }
        }
        bool IsUniqueDWDNum(DWD _dwd)
        {
            DatabaseHelper _db = new DatabaseHelper();
            return _db.IsDWDNumUnique(_dwd);
        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
        bool ValidateIntEmpty(int value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
        bool ValidateDblEmpty(double value)
        {
            if (!(value == 0))
                return true;
            return false;
        }
    }
}
