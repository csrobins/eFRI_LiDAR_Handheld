using FluentValidation;
using eLiDAR.Models;
using eLiDAR.Helpers;
using System;
using FluentValidation.Validators;

namespace eLiDAR.Validator
{
    public class ProjectValidator : AbstractValidator<PROJECT>  
    {  
        public ProjectValidator()  
        {  
            RuleFor(c => c.NAME).NotEmpty().WithMessage("Project name should not be empty.");
            RuleFor(c => c.DESCRIPTION).NotEmpty().WithMessage("Project description should not be empty.");
      //      RuleFor(c => c.PROJECT_DATE).NotEmpty().WithMessage("Project date should not be empty.");
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
    public class PlotValidator : AbstractValidator<PLOT>
    {
        public PlotValidator()
        {
            RuleFor(c => c).Must(c => IsUniquePlotNum(c)).WithMessage("Plot number must be unique within the project.");
            RuleFor(c => c.VSNPLOTNAME).NotEmpty().WithMessage("Plot number should not be empty.");
            RuleFor(c => c.VSNPLOTTYPECODE).NotEmpty().WithMessage("Plot type should not be empty.");
        //    RuleFor(c => c.PLOT_DATE).NotEmpty().WithMessage("Plot date should not be empty.");
            RuleFor(c => c.PLOTOVERVIEWDATE).GreaterThanOrEqualTo(DateTime.Parse("01-01-2020")).WithMessage("Plot date should > 01-01-2020.");
       //     RuleFor(c => c.FMU).NotEmpty().WithMessage("FMU should not be empty.");
       //     RuleFor(c => c.FOREST_DISTRICT).NotEmpty().WithMessage("Forest District should not be empty.");
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
        public TreeValidator()
        {
            RuleFor(c => c).Must(c => IsUniqueTreeNum(c)).WithMessage("Tree number must be unique within the plot.");
            RuleFor(c => c.TREENUMBER).NotEmpty().WithMessage("Tree number should not be empty.");
            RuleFor(c => c.TREENUMBER).SetValidator(new ScalePrecisionValidator(0, 5) ).WithMessage("Tree numbers must be integers");
            RuleFor(c => c.TREENUMBER).LessThan(2000).WithMessage("Tree numbers must be < 2000");
            RuleFor(c => c.TREENUMBER).GreaterThan(0).WithMessage("Tree numbers must be > 0");
            RuleFor(c => c.SPECIESCODE).NotEmpty().WithMessage("Species should not be empty.");
            RuleFor(c => c.TREEORIGINCODE).NotEmpty().WithMessage("Tree origin should not be empty.");
            RuleFor(c => c.DBH).NotEmpty().WithMessage("Tree DBH should not be empty and be between 0 and 200.");
            RuleFor(c => c.DBH).GreaterThan(7).WithMessage("Tree DBH should be > 7cm.");
            RuleFor(c => c.DBH).LessThan(200).WithMessage("Tree DBH should be < 200.");
            RuleFor(c => c.DBH).SetValidator(new ScalePrecisionValidator(2, 6)).WithMessage("DBH can have up to 2 decimals");
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
        public StemMapValidator()
        {
            RuleFor(c => c.AZIMUTH).NotEmpty().WithMessage("Azimuth should not be empty.");
            RuleFor(c => c.DISTANCE).NotEmpty().WithMessage("Distance should not be empty.");
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
        public EcositeValidator()
        {
            RuleFor(c => c.PRI_ECO).NotEmpty().WithMessage("Primary ecosite should not be empty.");
            RuleFor(c => c.MOISTUREREGIMECODE).NotEmpty().WithMessage("Moisture Regime should not be empty.");
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
        public SoilValidator()
        {
            RuleFor(c => c).Must(c => IsUniqueSoilNum(c)).WithMessage("Soil layer number must be unique within the plot.");
            RuleFor(c => c.HORIZONNUMBER).NotEmpty().WithMessage("Soil Layer is required.");
            RuleFor(c => c.DEPTHFROM).NotEmpty().WithMessage("From is required.");
            RuleFor(c => c.DEPTHTO).NotEmpty().WithMessage("To is required.");
            RuleFor(c => c.HORIZON).NotEmpty().WithMessage("Soil horizon is required.");
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
        public SmallTreeValidator()
        {
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
    }
    public class VegetationValidator : AbstractValidator<VEGETATION>
    {
        public VegetationValidator()
        {
            RuleFor(c => c.VSNSPECIESCODE).NotEmpty().WithMessage("Species should not be empty.");
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
    public class DeformityValidator : AbstractValidator<DEFORMITY>
    {
        public DeformityValidator()
        {
            RuleFor(c => c.HEIGHTFROM).NotEmpty().WithMessage("Ht From should not be empty.");
            RuleFor(c => c.HEIGHTTO).NotEmpty().WithMessage("Ht To should not be empty.");
            RuleFor(c => c.HEIGHTFROM).LessThan(c => c.HEIGHTTO).WithMessage("Ht To should > Ht from.");
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
        public DWDValidator()
        {
            RuleFor(c => c).Must(c => IsUniqueDWDNum(c)).WithMessage("DWD number must be unique within the line.");
            RuleFor(c => c.DECOMPOSITIONCLASS).NotEmpty().WithMessage("Decomp Class should not be empty.");
           
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
