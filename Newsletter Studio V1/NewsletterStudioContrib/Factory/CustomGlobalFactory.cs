using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsletterStudio.Core.Interfaces;
using NewsletterStudio.Core.Interfaces.Data;
using NewsletterStudio.Infrastucture;
using NewsletterStudio.Infrastucture.Compression;
using NewsletterStudio.Infrastucture.Data;

namespace NewsletterStudioContrib.Factory
{
    public class CustomGlobalFactory : INewsletterStudioGlobalFactory
    {
        public IMailingListRepository MailingListRepository
        {
            get { return new DemoMailingListRepository(); }
        }

        public ISubscriberRepository SubscriberRepository
        {
            get { return new SubscriberRepository(); }
        }

        public INewsletterRepository NewsletterRepository
        {
            get { return new NewsletterRepository(); }
        }

        public IEmailTrackingItemRepository EmailTrackingItemRepository
        {
            get { return new EmailTrackingItemRepository(); }
        }

        public ITrackingItemRepository TrackingItemRepository
        {
            get { return new TrackingItemRepository(); }
        }

        public ISkinRepository SkinRepository
        {
            get { return new SkinRepository(); }
        }

        public IAnalyticsDetailsRepository AnalyticsDetailsRepository
        {
            get { return new PPAnalyticsDetailsRepository(); }
        }

        public IAnalyticsRepository AnalyticsRepository
        {
            get { return new PPAnalyticsRepository(); }
        }

        public IJsonSerializer JsonSerializer
        {
            get
            {
                // This has been changed from the default encode JavaScriptJsonSerializer() into our new super encoder;
                return new SuperJsonEncoder();
            }
        }

        public ICompressor Compressor
        {
            get { return new LegacySimpleAESProxyCompressor(); }
        }
    }
}
