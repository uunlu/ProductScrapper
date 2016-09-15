using HtmlTags.Conventions;
using ProductScrapper.Extensions;
using StructureMap;

namespace ProductScrapper.DependencyResolution
{
    public class HtmlTagsRegistry : Registry
    {
        public HtmlTagsRegistry()
        {
            var htmlConventionLibrary = new HtmlConventionLibrary();
            var conv = new CustomHtmlConventions();
            conv.Apply(htmlConventionLibrary);

            For<HtmlConventionLibrary>().Use(htmlConventionLibrary);

            //For<HttpRequestBase>().Use(c => c.GetInstance<HttpRequestWrapper>());
            //For<HttpContextBase>().Use(c => c.GetInstance<HttpContextWrapper>());
            //For<HttpRequest>().Use(() => HttpContext.Current.Request);
            //For<HttpContext>().Use(() => HttpContext.Current);
            //For<ITypeResolverStrategy>().Use<TypeResolver.DefaultStrategy>();

            //  For<IElementNamingConvention>().Use<DotNotationElementNamingConvention>();
            //  //For(typeof(ITagGenerator<>)).Use(typeof(TagGenerator<>));
            //  For(typeof(IElementGenerator<>)).Use(ctx =>
            //  {
            //      var x = ElementGenerator<TModel

            //});
        }
    }
}