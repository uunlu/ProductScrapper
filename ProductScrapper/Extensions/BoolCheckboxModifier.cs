using HtmlTags;
using HtmlTags.Conventions;
using HtmlTags.Conventions.Elements;

namespace ProductScrapper.Extensions
{
    public class BoolCheckboxModifier : IElementModifier
    {
        public bool Matches(ElementRequest token) => token.Accessor.PropertyType == typeof(bool) || token.Accessor.PropertyType == typeof(bool?);

        public void Modify(ElementRequest request)
        {
            request.CurrentTag.Id(request.ElementId).Value("True");
            request.CurrentTag
                    .Append(new HtmlTag("label")
                            .Attr("for", request.ElementId)
                            .Text(request.ElementId)
                           );
            request.WrapWith(new HtmlTag("div").AddClass("checkbox checkbox-inline"));

        }
    }
}