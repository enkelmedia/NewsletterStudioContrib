using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewsletterStudio.Core.Interfaces.Data;
using NewsletterStudio.Core.Model;
using NewsletterStudio.Infrastucture;
using NewsletterStudio.Services.Mail;

namespace NewsletterStudioContrib.BounceManagement
{
    /// <summary>
    /// This helper demonstrates how to add new bounces to Newsletter Studio using code. 
    /// This might be a API that will be merged into the core in the future if we see this demand.
    /// </summary>
    public class NewsletterBounceHelper
    {
        private ISubscriberRepository _subscriberRepository = GlobalFactory.Current.SubscriberRepository;
        private IEmailTrackingItemRepository _emailTrackingItemRepository = GlobalFactory.Current.EmailTrackingItemRepository;

        /// <summary>
        /// Adds a new bounce to the Newsletter Studio data and sets the subscriber as bounced. 
        /// </summary>
        /// <param name="email">The e-mail (yada@yada.com) that has bounced</param>
        /// <param name="message">Error message that will be displayed for the user in the UI</param>
        public void AddNewHardBounce(string email, string message = "BounceHandler: Hard Bounce")
        {   
            AddNewHardBounce(new List<string>() {email}, message);
        }

        public void AddNewHardBounce(List<string> emails, string message = "BounceHandler: Hard Bounce")
        {
            // Setting this e-mail as a hard bounce by updateing the statics and set it to "EmailTrackingStatus.Error", the "message"-param will be displayed in the UI.
            _emailTrackingItemRepository.SetStatus(emails, message, (int)EmailTrackingStatus.Error);

            // Sets the status of the subscribers in the mailinglists
            _subscriberRepository.SetAsBounced(emails);

        }
        
    }
}
