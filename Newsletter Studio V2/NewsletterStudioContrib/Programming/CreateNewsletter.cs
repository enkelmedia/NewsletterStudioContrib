using NewsletterStudio.Bll;
using NewsletterStudio.Core.Model;
using NewsletterStudio.Infrastucture;
using System;
using System.Linq;

/// <summary>
/// Example of programming with NS against v 2
/// </summary>
namespace NewsletterStudioContrib.Programming
{
    class CreateNewsletter
    {
        /// <summary>
        /// Copies a NS template to a new one. Equivalent to UI "Copy to new"
        /// </summary>
        /// <param name="newsletterId">The newsletter identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private Newsletter CopyToNew(int newsletterId, string name)
        {
            var _container = GlobalFactory.Current;

            Newsletter byId = _container.NewsletterRepository.GetById(newsletterId);
            Newsletter newsletter = new Newsletter();
            newsletter.Name = name;
            newsletter.MessageBody = byId.MessageBody;
            newsletter.EmailSubject = byId.EmailSubject;
            newsletter.SkinAlias = byId.SkinAlias;
            newsletter.EmailFrom = ConfigManager.NewsletterStudio.SenderDefaultsForced ? ConfigManager.NewsletterStudio.SenderDefaultEmail : byId.EmailFrom;
            newsletter.SenderName = ConfigManager.NewsletterStudio.SenderDefaultsForced ? ConfigManager.NewsletterStudio.SenderDefaultName : byId.SenderName;
            newsletter.Status = 0;
            newsletter.Initialized = false;
            newsletter.CreatedByUserId = byId.CreatedByUserId;
            return newsletter;
        }

        /// <summary>
        /// Prepares the ns from template.
        /// </summary>
        /// <exception cref="Exception">NS Template with name 'Couponing 2017 hasn't been found'</exception>
        private void PrepareNSFromTemplate()
        {
            var _container = GlobalFactory.Current;

            var templateNs = _container.NewsletterRepository.GetBy("Couponing 2017").FirstOrDefault();


            if (templateNs == null)
            {
                throw new Exception("NS Template with name 'Couponing 2017 hasn't been found'");
            }
            Newsletter ns = this.CopyToNew(templateNs.Id, templateNs.Name);

            ns.SubscriptionAlias = "SubscriptionProvider_1";

            //ns.Status = DateTime.Now;
            var sendDate = DateTime.Now;
            sendDate = sendDate.AddHours(2);   //delay on purpose
            ns.ScheduledSendDate = sendDate;
            ns.CreatedDate = sendDate; // Setting the date to now so it will be send right away

            // Save to database
            _container.NewsletterRepository.Save(ns);
        }


        /// <summary>
        /// Prepares the ns from scratch.
        /// </summary>
        private void PrepareNSFromScratch()
        {
            var _container = GlobalFactory.Current;

            Newsletter ns = new Newsletter();
            ns.Name = "Couponing 2017";
            ns.MessageBody = "put your content here";
            ns.EmailSubject = "A email for you";
            ns.SkinAlias = "MySkin";   //Valid skin alias
            ns.EmailFrom = ConfigManager.NewsletterStudio.SenderDefaultsForced ? ConfigManager.NewsletterStudio.SenderDefaultEmail : "from@domain.com";
            ns.SenderName = ConfigManager.NewsletterStudio.SenderDefaultsForced ? ConfigManager.NewsletterStudio.SenderDefaultName : "Your sender name";
            ns.Status = 0;
            ns.Initialized = false;
            //ns.CreatedByUserId = 0;
            ns.SubscriptionAlias = "SubscriptionProvider_1";

            //ns.Status = DateTime.Now;
            var sendDate = DateTime.Now;
            sendDate = sendDate.AddHours(2);   //delay on purpose
            ns.ScheduledSendDate = sendDate;
            ns.CreatedDate = sendDate; // Setting the date to now so it will be send right away

            // Save to database
            _container.NewsletterRepository.Save(ns);
        }
    }
}
