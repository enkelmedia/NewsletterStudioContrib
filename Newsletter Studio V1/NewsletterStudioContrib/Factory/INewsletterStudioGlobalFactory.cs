using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsletterStudio.Core.Interfaces.Data;

namespace NewsletterStudio.Core.Interfaces
{
    /// <summary>
    /// An class of this type is resonsible for resolving dependencies for the Newsletter Studio Package.
    /// </summary>
    public interface INewsletterStudioGlobalFactory
    {
        IMailingListRepository MailingListRepository { get; }

        ISubscriberRepository SubscriberRepository { get; }

        INewsletterRepository NewsletterRepository { get; }

        IEmailTrackingItemRepository EmailTrackingItemRepository { get; }

        ITrackingItemRepository TrackingItemRepository { get; }

        ISkinRepository SkinRepository { get; }

        IAnalyticsDetailsRepository AnalyticsDetailsRepository { get; }

        IAnalyticsRepository AnalyticsRepository { get; }

        IJsonSerializer JsonSerializer { get; }

        ICompressor Compressor { get; }

    }
}
