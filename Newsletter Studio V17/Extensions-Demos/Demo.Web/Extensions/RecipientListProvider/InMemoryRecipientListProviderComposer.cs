using NewsletterStudio.Core.Composing;

namespace Demo.Web.Extensions.RecipientListProvider;

public class InMemoryRecipientListProviderComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.NewsletterStudio()
            .RecipientListProviders
            .Append<InMemoryRecipientListProvider>();
    }
}