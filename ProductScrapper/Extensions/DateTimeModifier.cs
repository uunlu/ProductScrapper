using HtmlTags;
using HtmlTags.Conventions;
using HtmlTags.Conventions.Elements;
using HtmlTags.Reflection;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProductScrapper.Extensions
{
    /// <summary>
    /// Handles Date, DateTime and Time picker HTML components via conventions. 
    /// Use attributes like [DataType.Date] for date-only picker and [DataType.Time] for time-only picker. By default it is full datetime picker.
    /// </summary>
    public class DateTimeModifier : IElementModifier
    {
        public bool Matches(ElementRequest token)
        {
            return token.Accessor.PropertyType.IsEquivalentTo(typeof(DateTime))
                || token.Accessor.PropertyType.IsEquivalentTo(typeof(DateTime?));
        }

        public void Modify(ElementRequest request)
        {
            var attr = request.Accessor.GetAttribute<DataTypeAttribute>();
            string data_type = "";
            string value = "";

            if (attr == null || attr.DataType == DataType.DateTime)
            {
                data_type = "datetime";
                value = request.Value<DateTime?>().HasValue
                    ? request.Value<DateTime>().ToShortDateString() + " " + request.Value<DateTime>().ToLongTimeString()
                    : string.Empty;
            }
            else if (attr.DataType == DataType.Time)
            {
                data_type = "time";
                value = request.Value<DateTime?>().HasValue ? request.Value<DateTime>().ToLongTimeString() : string.Empty;
            }
            else if (attr.DataType == DataType.Date)
            {
                data_type = "date";
                value = request.Value<DateTime?>().HasValue ? request.Value<DateTime>().ToShortDateString() : string.Empty;
            }
            else if (attr.CustomDataType.ToLowerInvariant() == "Month".ToLowerInvariant())
            {
                data_type = "month";
                value = request.Value<DateTime?>().HasValue ? request.Value<DateTime>().ToString("MM/yyyy") : string.Empty;
            }
            else
                throw new Exception("Not supported attribute type for this DateTimeModifier.");

            request.CurrentTag
                .Data("type", data_type)
                .Value(value);

            if (request.Accessor.PropertyType == typeof(DateTime))
            {
                if (attr != null && attr.DataType == DataType.Time)
                    request.WrapWith(new HtmlTag("div").AppendHtml(HtmlConstants.ICON_TIMEPICKER));
                else
                    request.WrapWith(new HtmlTag("div").AppendHtml(HtmlConstants.ICON_DATEPICKER));
            }
            else // if property is nullable
            {
                if (attr != null && attr.DataType == DataType.Time)
                {
                    request.WrapWith(new HtmlTag("div")
                        .AppendHtml(HtmlConstants.ICON_TIMEPICKER)
                        .AppendHtml(HtmlConstants.ICON_DATEPICKER_CLEAR));
                }
                else
                {
                    request.WrapWith(new HtmlTag("div")
                        .AppendHtml(HtmlConstants.ICON_DATEPICKER)
                        .AppendHtml(HtmlConstants.ICON_DATEPICKER_CLEAR));
                }
            }
        }
    }
}