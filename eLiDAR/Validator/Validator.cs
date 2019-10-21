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
}
