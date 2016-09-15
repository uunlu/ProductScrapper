using HtmlTags;
using HtmlTags.Conventions;
using HtmlTags.Conventions.Elements;
using HtmlTags.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductScrapper.Extensions
{
    public class DisplayDateTimeModifier : IElementModifier
    {
        public bool Matches(ElementRequest token) => token.Accessor.PropertyType == typeof(DateTime) || token.Accessor.PropertyType == typeof(DateTime?);

        public void Modify(ElementRequest request)
        {
            var attr = request.Accessor.GetAttribute<DataTypeAttribute>();
            string text = "";

            if (attr == null || attr.DataType == DataType.DateTime)
            {
                text = request.Value<DateTime?>().HasValue
                    ? request.Value<DateTime>().ToShortDateString() + " " + request.Value<DateTime>().ToLongTimeString()
                    : string.Empty;
            }
            else if (attr.DataType == DataType.Time)
            {
                text = request.Value<DateTime?>().HasValue ? request.Value<DateTime>().ToLongTimeString() : string.Empty;
            }
            else if (attr.DataType == DataType.Date)
            {
                text = request.Value<DateTime?>().HasValue ? request.Value<DateTime>().ToShortDateString() : string.Empty;
            }
            else if (attr.CustomDataType.ToLowerInvariant() == "Month".ToLowerInvariant())
            {
                text = request.Value<DateTime?>().HasValue ? request.Value<DateTime>().ToString("MM/yyyy") : string.Empty;
            }
            else
                throw new Exception("Not supported attribute type for this DateTimeModifier.");

            request.CurrentTag.Text(text);

            if (request.Accessor.PropertyType == typeof(DateTime))
            {
                if (attr != null && attr.DataType == DataType.Time)
                    request.WrapWith(new HtmlTag("div").AppendHtml(HtmlConstants.ICON_TIMEPICKER));
                else
                    request.WrapWith(new HtmlTag("div").AppendHtml(HtmlConstants.ICON_DATEPICKER));
            }
        }
    }

}