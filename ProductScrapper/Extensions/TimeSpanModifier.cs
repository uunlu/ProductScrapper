using HtmlTags;
using HtmlTags.Conventions;
using HtmlTags.Conventions.Elements;
using System;

namespace ProductScrapper.Extensions
{
    /// <summary>
    /// Handles duration time picker HTML components via conventions. 
    /// </summary>
    public class TimeSpanModifier : IElementModifier
    {
        public bool Matches(ElementRequest token)
        {
            return token.Accessor.PropertyType.IsEquivalentTo(typeof(TimeSpan))
                || token.Accessor.PropertyType.IsEquivalentTo(typeof(TimeSpan?));
        }

        public void Modify(ElementRequest request)
        {
            string value = request.Value<TimeSpan?>().HasValue
                ? request.Value<TimeSpan>().ToString(@"hh\:mm\:ss")
                : string.Empty;

            request.CurrentTag
                .Data("type", "timespan")
                .Value(value);

            if (request.Accessor.PropertyType == typeof(TimeSpan))
            {
                request.WrapWith(new HtmlTag("div").AppendHtml(HtmlConstants.ICON_TIMEPICKER));
            }
            else // if property is nullable
            {
                request.WrapWith(new HtmlTag("div")
                    .AppendHtml(HtmlConstants.ICON_TIMEPICKER)
                    .AppendHtml(HtmlConstants.ICON_DATEPICKER_CLEAR));
            }
        }
    }
}