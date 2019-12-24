using FluentValidation;
using eLiDAR.Models;

namespace eLiDAR.Validator
{
    public class ProjectValidator : AbstractValidator<PROJECT>  
    {  
        public ProjectValidator()  
        {  
            RuleFor(c => c.NAME).Must(n=>ValidateStringEmpty(n)).WithMessage("Project name should not be empty.");
            RuleFor(c => c.DESCRIPTION).Must(a => ValidateStringEmpty(a)).WithMessage("Project description should not be empty.");
            RuleFor(c => c.PROJECT_DATE).Must(d => ValidateStringEmpty(d.ToString())).WithMessage("Project date should not be empty.");
        }  

        bool ValidateStringEmpty(string stringValue){
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
    }
    public class PlotValidator : AbstractValidator<PLOT>
    {
        public PlotValidator()
        {
            RuleFor(c => c.PLOTNUM).Must(n => ValidateStringEmpty(n)).WithMessage("Plot number should not be empty.");
            RuleFor(c => c.PLOT_TYPE).Must(a => ValidateStringEmpty(a)).WithMessage("Plot type should not be empty.");
            RuleFor(c => c.PLOT_DATE).Must(d => ValidateStringEmpty(d.ToString())).WithMessage("Plot date should not be empty.");
        }

        bool ValidateStringEmpty(string stringValue)
        {
            if (!string.IsNullOrEmpty(stringValue))
                return true;
            return false;
        }
    }
    public class TreeValidator : AbstractValidator<TREE>
    {
        public TreeValidator()
        {
            RuleFor(c => c.TREENUM).Must(n => ValidateIntEmpty(n)).WithMessage("Tree number should not be empty.");
            RuleFor(c => c.SPECIES).Must(a => ValidateIntEmpty(a)).WithMessage("Species should not be empty.");
            //RuleFor(c => c.TAG_TYPE).Must(d => ValidateStringEmpty(d.ToString())).WithMessage("Tree Tag Type should not be empty.");
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
    }
    public class StemMapValidator : AbstractValidator<STEMMAP>
    {
        public StemMapValidator()
        {
            RuleFor(c => c.AZIMUTH).Must(n => ValidateIntEmpty(n)).WithMessage("Azimuth should not be empty.");
            RuleFor(c => c.DISTANCE).Must(a => ValidateDblEmpty(a)).WithMessage("Distance should not be empty.");
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
           // RuleFor(c => c.PRI_ECO).Must(n => ValidateStringEmpty(n)).WithMessage("Primary ecosite should not be empty.");
            RuleFor(c => c.MOISTURE_REGIME).Must(a => ValidateIntEmpty(a)).WithMessage("Moisture Regime should not be empty.");
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
            // RuleFor(c => c.PRI_ECO).Must(n => ValidateStringEmpty(n)).WithMessage("Primary ecosite should not be empty.");
        //    RuleFor(c => c.FROM).Must(a => ValidateIntEmpty(a)).WithMessage("From should not be empty.");
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
            // RuleFor(c => c.PRI_ECO).Must(n => ValidateStringEmpty(n)).WithMessage("Primary ecosite should not be empty.");
            RuleFor(c => c.SPECIES).Must(a => ValidateIntEmpty(a)).WithMessage("Species should not be empty.");
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
