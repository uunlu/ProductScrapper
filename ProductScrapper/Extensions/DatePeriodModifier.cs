using HtmlTags;
using HtmlTags.Conventions;
using HtmlTags.Conventions.Elements;
using HtmlTags.Reflection;
using ProductScrapper.AppServices.Dtos;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ProductScrapper.Extensions
{
    public class DatePeriodModifier : IElementModifier
    {
        public bool Matches(ElementRequest token)
        {
            return token.Accessor.PropertyType.IsEquivalentTo(typeof(DatePeriodDto));
        }

        public void Modify(ElementRequest request)
        {
            request.CurrentTag
                    .Data("type", "dateperiod")
                    .Data("elementname", request.ElementId)
                    .RemoveAttr("name")
                    .RemoveAttr("id");

            var value = request.Value<DatePeriodDto>() ?? new DatePeriodDto();

            var startDate = CreateHiddenInput()
                .Data("type", "dateperiod-value")
                .Data("local-isodate", value.Start?.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                .Name($"{request.ElementId}.Start")
                .Value(value.Start?.ToShortDateString());

            var endDate = CreateHiddenInput()
                .Data("type", "dateperiod-value")
                .Data("local-isodate", value.End?.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture))
                .Name($"{request.ElementId}.End")
                .Value(value.End?.ToShortDateString());

            request.CurrentTag.Value(value.Start?.ToShortDateString() + " - " + value.End?.ToShortDateString());

            var attr = request.Accessor.GetAttribute<DataTypeAttribute>();
            if (attr != null && attr.CustomDataType != null && attr.CustomDataType.ToLowerInvariant() == "week".ToLowerInvariant())
            {
                request.CurrentTag.Data("weekpicker", "true");
            }
            request.WrapWith(new HtmlTag("div")
                .AppendHtml(HtmlConstants.ICON_DATEPICKER)
                .AppendHtml(HtmlConstants.ICON_DATEPICKER_CLEAR)
                .Append(startDate)
                .Append(endDate));
        }

        private HtmlTag CreateHiddenInput()
        {
            return new HtmlTag("input").Attr("type", "hidden");
        }
    }
}