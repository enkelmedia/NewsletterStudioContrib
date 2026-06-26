using NewsletterStudio.Core.Composing;
using Umbraco.Cms.Core.Composing;

namespace Demo.Web.Extensions.EmailEditorControl;

public class HelloEmailControlComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.NewsletterStudio().EmailControlTypes.Append<HelloEmailControlType>();
    }
}