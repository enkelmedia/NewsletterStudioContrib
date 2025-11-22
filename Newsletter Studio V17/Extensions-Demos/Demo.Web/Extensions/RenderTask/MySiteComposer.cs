using NewsletterStudio.Core.Composing;
using Umbraco.Cms.Core.Composing;

namespace Demo.Web.Extensions.RenderTask;

public class MySiteComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.NewsletterStudio().RenderTasks.Append<AddAttachmentRenderTask>();
    }
}