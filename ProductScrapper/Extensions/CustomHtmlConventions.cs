using HtmlTags;
using HtmlTags.Conventions;
using ProductScrapper.AppServices.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ProductScrapper.Extensions
{
    public class CustomHtmlConventions : DefaultHtmlConventions
    {
        public CustomHtmlConventions()
        {
            Editors.Always.AddClass("form-control");
            Editors.IfPropertyIs<bool>().ModifyWith(m => m.CurrentTag.Attr("type", "checkbox").RemoveClass("form-control"));
            Editors.IfPropertyIs<bool?>().ModifyWith(m => m.CurrentTag.Attr("type", "checkbox").RemoveClass("form-control"));
            Editors.IfPropertyIs<int>().ModifyWith(m => m.CurrentTag.Data("type", "integer"));
            Editors.IfPropertyIs<int?>().ModifyWith(m => m.CurrentTag.Data("type", "integer"));
            Editors.IfPropertyIs<long>().ModifyWith(m => m.CurrentTag.Data("type", "integer"));
            Editors.IfPropertyIs<long?>().ModifyWith(m => m.CurrentTag.Data("type", "integer"));
            Editors.IfPropertyIs<decimal>().ModifyWith(m => m.CurrentTag.Data("type", "decimal"));
            Editors.IfPropertyIs<decimal?>().ModifyWith(m => m.CurrentTag.Data("type", "decimal"));
            Editors.IfPropertyIs<float>().ModifyWith(m => m.CurrentTag.Data("type", "decimal"));
            Editors.IfPropertyIs<float?>().ModifyWith(m => m.CurrentTag.Data("type", "decimal"));
            Editors.Modifier<EnumDropDownModifier>();
            Editors.Modifier<DateTimeModifier>();
            Editors.Modifier<TimeSpanModifier>();
            Editors.Modifier<BoolCheckboxModifier>();
            Editors.Modifier<DatePeriodModifier>();
            // TODO
            //Editors.HasAttributeValue<DataTypeAttribute>(x=>x.DataType == DataType.MultilineText).
            //Editors.HasAttributeValue<DataTypeAttribute>(x => x.DataType == DataType.Password).Attr("type", "password");
            //Editors.HasAttributeValue<DataTypeAttribute>(x => x.DataType == DataType.EmailAddress).ModifyWith(m => m.CurrentTag.Data("type", "email"));
            //Editors.HasAttributeValue<DataTypeAttribute>(x => x.DataType == DataType.Url).ModifyWith(m => m.CurrentTag.Data("type", "url"));
            Editors.ModifyForAttribute<DisplayAttribute>((t, a) =>
            {
                if (string.IsNullOrEmpty(a.Prompt))
                    return;
                var description = new HtmlTag("div").AddClass("input-description").Text(a.Description);
                t.Attr("placeholder", a.Prompt);
                t.UseClosingTag().Append(description);
            });
            Editors.ModifyForAttribute<TooltipAttribute>((t, a) => t.Attr("title", a.Description));

            Labels.Always.AddClass("control-label");
            Labels.Modifier<FluentValidationForLabelsModifier>();
            Labels.ModifyForAttribute<DisplayAttribute>((t, a) => t.Text(a.Name));
            Labels.ModifyForAttribute<TooltipAttribute>((t, a) => t.Attr("title", a.Description));

            // TODO: Displays for dates, times, datetimes, etc.
            //Displays.IfPro    // Place modifier for DateTime Time OR Date
            Displays.Modifier<DisplayDateTimeModifier>();
        }
    }
}