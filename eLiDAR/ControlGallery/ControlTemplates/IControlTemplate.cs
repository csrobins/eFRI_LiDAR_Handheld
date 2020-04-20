using Xamarin.Forms;

namespace eLiDAR.ControlGallery.ControlTemplates
{
    public interface IControlTemplate
    {
        View TargetControl { get; set; }
    }
}