using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using NewsletterStudio.Infrastucture.Events;
using Umbraco.Core;

namespace NewsletterStudioContrib.Events
{
    public class EventsExample : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {

            NewsletterStudio.Services.SendNewsletterService.SendingUnique += SendNewsletterServiceOnSendingUnique;

        }
        private void SendNewsletterServiceOnSendingUnique(object sender, NewsletterSendingUniqueEventArgs newsletterSendingUniqueEventArgs)
        {
            newsletterSendingUniqueEventArgs.MailMessage.Headers.Add("yada","foo");
            
        }
    }
}
