using NewsletterStudio.Infrastucture;
using NewsletterStudio.Services.RenderTasks;

namespace NewsletterStudioContrib.RenderTasks
{
    /// <summary>
    /// Shows how to add a custom header before sendout
    /// </summary>
    /// <seealso cref="NewsletterStudio.Services.RenderTasks.RenderTask" />
    public class AddCustomHeaderTask : RenderTask
    {

        private string GetOriginalUnsubscribeUrl()
        {
            return RenderTask.ProtocolAndHost.ToString() + "app_plugins/newsletterstudio/pages/tracking/unsubscribe.aspx?e=[compress_email]&sb=[compress_subscriptionId]&nid=[compress_newsletterId]";
        }

        public override void ProcessPreRender(RenderResult renderResult, RenderTaskParameters parameters)
        {

        }


        public override void ProcessUniqueItem(RenderResult renderResult, RenderTaskUniqueItemParameters parameters)
        {
            //compress information to create final URL
            var e = GlobalFactory.Current.Compressor.Compress(parameters.EmailTrackingItem.Email.ToString());
            var nid = GlobalFactory.Current.Compressor.Compress(parameters.Newsletter.Id.ToString());
            var sid = GlobalFactory.Current.Compressor.Compress(parameters.Newsletter.SubscriptionAlias.ToString());

            //get the url and replace the information
            string listUnsubscribeUrl = GetOriginalUnsubscribeUrl();
            listUnsubscribeUrl = listUnsubscribeUrl.Replace("[compress_email]", e);
            listUnsubscribeUrl = listUnsubscribeUrl.Replace("[compress_subscriptionId]", sid);
            listUnsubscribeUrl = listUnsubscribeUrl.Replace("[compress_newsletterId]", nid);

            parameters.MailMessage.Headers.Add("List-Unsubscribe", listUnsubscribeUrl);
        }

    }
}
