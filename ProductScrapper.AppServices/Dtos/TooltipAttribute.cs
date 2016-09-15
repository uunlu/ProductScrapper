using System.ComponentModel;


namespace ProductScrapper.AppServices.Dtos
{
    public class TooltipAttribute : DescriptionAttribute
    {
        public TooltipAttribute()
            : base("")
        {

        }

        public TooltipAttribute(string description)
            : base(description)
        {

        }
    }
}
