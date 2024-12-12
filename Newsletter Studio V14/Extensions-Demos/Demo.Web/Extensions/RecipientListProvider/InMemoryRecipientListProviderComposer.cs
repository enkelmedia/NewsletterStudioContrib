using NewsletterStudio.Core.Composing;
using Umbraco.Cms.Core.Composing;

namespace Demo.Web.Extensions.RecipientListProvider;

public class InMemoryRecipientListProviderComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.NewsletterStudio()
            .RecipientListProviders
            .Append<InMemoryRecipientListProvider>();
    }
}