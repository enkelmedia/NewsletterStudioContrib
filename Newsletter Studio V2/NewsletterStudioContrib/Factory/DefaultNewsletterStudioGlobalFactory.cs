using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsletterStudio.Core.Interfaces;
using NewsletterStudio.Core.Interfaces.Data;
using NewsletterStudio.Infrastucture.Compression;
using NewsletterStudio.Infrastucture.Data;

namespace NewsletterStudio.Infrastucture
{
    /// <summary>
    /// The default implementation of the Newsletter Studio Global Factory that handles the creation of different objects in the package.
    /// This can be overridden with a custom implementation.
    /// </summary>
    public class DefaultNewsletterStudioGlobalFactory : INewsletterStudioGlobalFactory
    {
        public IMailingListRepository MailingListRepository
        {
            get { return new MailingListRepository(); }
        }

        public ISubscriberRepository SubscriberRepository 
        {
            get { return new SubscriberRepository();}
        }

        public INewsletterRepository NewsletterRepository
        {
            get { return new NewsletterRepository(); }
        }

        public IEmailTrackingItemRepository EmailTrackingItemRepository
        {
            get { return new EmailTrackingItemRepository(); }
        }

        public ITrackingItemRepository TrackingItemRepository {
            get { return new TrackingItemRepository(); }
        }

        public ISkinRepository SkinRepository {
            get { return new SkinRepository(); }
        }

        public IAnalyticsDetailsRepository AnalyticsDetailsRepository 
        {
            get { return new PPAnalyticsDetailsRepository(); }
        }

        public IAnalyticsRepository AnalyticsRepository 
        {
            get { return new PPAnalyticsRepository();}
        }

        public IJsonSerializer JsonSerializer {
            get { return new JavaScriptJsonSerializer();}
        }

        public ICompressor Compressor {
            get
            {
                // Should be SimpleAESCompressor in the future.
                return new LegacySimpleAESProxyCompressor();
            }
        }
    }
}