using NewsletterStudio.Core.Rendering;
using NewsletterStudio.Core.Rendering.Tasks;
using NewsletterStudio.Core.Sending;

namespace Demo.Web.Extensions.RenderTask;

public class AddAttachmentRenderTask : IRenderTask
{
    public void Process(RenderTaskProcessingResult result, RenderingUniqueContext context)
    {
        context.EmailMessage.Attachments.Add(GetAttachmentFor(context.Recipient));
    }

    private EmailAttachment GetAttachmentFor(IRecipientDataModel recipientDataModel)
    {
        using var stream = File.OpenRead("c:\\temp\\invoice.pdf");

        return new EmailAttachment("Invoice123.pdf", stream);
    }
}