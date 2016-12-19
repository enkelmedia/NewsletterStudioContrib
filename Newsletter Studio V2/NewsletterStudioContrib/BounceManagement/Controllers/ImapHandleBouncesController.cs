/// <summary>
/// This
/// </summary>
namespace NewsletterStudioContrib.BounceManagement.Controllers
{
    using Connector;
    using Models;
    using NewsletterStudio.Bll;
    using NewsletterStudio.Bll.Configuration;
    using NewsletterStudio.Core.Interfaces.Data;
    using NewsletterStudio.Services.Mail;
    using System.Collections.Generic;
    using System.Web.Http;
    using Umbraco.Core.Logging;
    using Umbraco.Web.WebApi;

    namespace NewsletterStudioContrib.BounceManagement.Controllers
    {

        //[RoutePrefix("umbraco/api/ImapHandleBounces")]
        public class ImapHandleBouncesController : UmbracoApiController
        {

            private ISubscriberRepository _subscriberRepository;
            private IEmailTrackingItemRepository _emailTrackingItemRepository;

            [HttpGet]
            [Route("handle")]
            public IHttpActionResult Handle()
            {
                IHttpActionResult result = Ok();
                try
                {
                    if (ConfigManager.NewsletterStudio.BounceManagement.Enabled)
                    {
                        StartHandleBounces();
                    }
                }
                catch (System.Exception exc)
                {
                    LogHelper.Error<ImapHandleBouncesController>("Error when handling bounced email ", exc);
                    result = BadRequest();
                }

                return result;
            }

            /// <summary>
            /// Starts the handle bounces.
            /// The interface currently offers only POP3 but you can currently set any value as long as they match with the required configuration.
            /// </summary>
            public void StartHandleBounces()
            {
                ConfigurationBounceManagement bounceManagement = ConfigManager.NewsletterStudio.BounceManagement;
                int port = bounceManagement.Port != 0 ? bounceManagement.Port : 143;
                ImapBounceConnector bounceMailConnector = new ImapBounceConnector(bounceManagement.Host, port);
                bounceMailConnector.User = bounceManagement.Username;
                bounceMailConnector.Password = bounceManagement.Password;
                bounceMailConnector.Connect();

                bounceMailConnector.PullList();

                ProcessBouncedEmail(bounceMailConnector.Messages());

                bounceMailConnector.CommitAndQuit();
            }

            private void ProcessBouncedEmail(List<IBounceMessage> messages)
            {

                NewsletterBounceHelper newsletterBounceHelper = new NewsletterBounceHelper();
                //uppercast
                foreach (ImapBouncedEmail item in messages)
                {
                    //take actions here
                    if (!string.IsNullOrWhiteSpace(item.ReceiverEmail))
                    {
                        LogHelper.Debug<ImapHandleBouncesController>("Bounced email for " + item.ReceiverEmail);


                        //parse can have extracted wrong information
                        if (item.BounceType == BounceType.HardBounce)
                        {
                            //update status for all mailing list
                            newsletterBounceHelper.AddNewHardBounce(item.ReceiverEmail, item.RawBody);
                        }
                    }
                }
            }
        }
    }
}
