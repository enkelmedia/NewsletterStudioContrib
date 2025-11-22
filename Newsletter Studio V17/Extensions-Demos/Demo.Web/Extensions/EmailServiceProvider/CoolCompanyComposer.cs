using NewsletterStudio.Core.Composing;
using Umbraco.Cms.Core.Composing;

namespace Demo.Web.Extensions.EmailServiceProvider;

public class CoolCompanyComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.NewsletterStudio().EmailServiceProviders.Append<CoolEmailCompanyEmailServiceProvider>();
    }
}