using NewsletterStudio.Core.Infrastructure.MailingLists;
using NewsletterStudio.Core.Rendering;
using NewsletterStudio.Core.Rendering.MergeFields;
using NewsletterStudio.Web.Composing;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Web;

namespace Demo.Web.Features.Recipients;

public class RecipientListProviderComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.NewsletterStudio().RecipientListProviders.Append<InMemoryRecipientListProvider>();
    }
}

public class InMemoryRecipientListProvider : IRecipientListProvider
{
    private readonly IUmbracoContextFactory _umbracoContextFactory;

    public string DisplayName => "Demo Provider";

    public string DisplayNameLocalizationKey => "site/demoProvider";

    public string Prefix => "ex";

    public bool CanRedirectToEdit => false;

    public InMemoryRecipientListProvider(IUmbracoContextFactory umbracoContextFactory)
    {
        // You can inject dependencies in the constructor if needed.
        // Providers are created as Singletons, keep this in mind when
        // injecting your dependencies.
        _umbracoContextFactory = umbracoContextFactory;
    }

    /// <summary>
    /// Called when we present available lists to the users, e.g. in the last step
    /// before sending a campaign.
    /// 
    /// Should return a list of available lists that this provider exposes.
    /// Examples: Built in provider = Mailing Lists, Umbraco Members = List of groups
    /// </summary>
    /// <param name="workspaceKey"></param>
    /// <returns></returns>
    public List<RecipientList> GetLists(Guid workspaceKey)
    {
        /*
         * Note here that each item in the list has a RecipientListIdentifier
         * This identifier is used to uniquely identify the list and will be passed
         * to other methods in this class to retrieve details.
         */

        return new List<RecipientList>()
        {
            new RecipientList()
            {
                // Replace 1 with a unique identifier (int-id, guid etc) of your list.
                Identifier = new RecipientListIdentifier(Prefix, 1), 
                Name = "Demo list 1",
                Subscribers = 25
            },
            new RecipientList()
            {
                Identifier = new RecipientListIdentifier(Prefix, 2),
                Name = "Demo list 2",
                Subscribers = 21
            }
        };
    }

    /// <summary>
    /// This method is called bu Newsletter Studio to build a for a campaign based on the lists
    /// that the user selected. The package will handle duplicates if the same email is present in
    /// more than one list.
    ///
    /// Notice that the <see cref="RecipientListIdentifier"/> from the <see cref="GetLists"/>-method
    /// is passed here and will contain your unique identifier.
    ///
    /// This method should return a list of <see cref="EmailReceiver"/> that will be used to build the queue.
    /// </summary>
    /// <param name="listId"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public List<EmailReceiver> GetReceiversForList(RecipientListIdentifier listId, GetReceiversForListParams parameters)
    {
        var list = new List<EmailReceiver>();
        list.Add(new EmailReceiver(new EmailReceiverIdentifier(Prefix, 1), "foo@bar.com", "Foo Bar"));

        if (listId.Identifier == "2")
            list.Add(new EmailReceiver(new EmailReceiverIdentifier(Prefix, 2), "foo2@bar.com", "Foo Bar"));

        return list;
    }

    /// <summary>
    /// This method is called by the package during the send out. I will be called one time for each recipient to get the
    /// details based on the <see cref="EmailReceiverIdentifier"/> created in the <see cref="GetReceiversForList"/>-method.
    ///
    /// These details are used to render the email and to provide values for any <see cref="ICampaignEmailMergeFieldProvider"/>.
    /// </summary>
    /// <param name="receiverId"></param>
    /// <returns></returns>
    public RecipientProviderRecipientDataModel GetDataModel(EmailReceiverIdentifier receiverId)
    {
        var model = new RecipientProviderRecipientDataModel();
        model.Email = "foo@bar.com";
        model.ProviderRecipientId = receiverId.ToString();

        // The optional "Model"-property is used to pass a custom model that can be used inside 
        // a IMergeFieldProvider to translate properties into Merge Fields. Leave this as null
        // if you do not need to pass any detailed data.
        model.Model = new InMemoryRecipient()
        {
            BirthYear = 1975,
            City = "London",
            CompanyName = "Test Company Inc."
        };

        return model;
    }

    /// <summary>
    /// This method is used when matching transactional recipients to existing recipients in the system.
    /// Return a <see cref="EmailReceiver"/> based on the email passed in.
    /// This method can return null if you data source does not support to query by email.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="workspaceKey"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public EmailReceiver GetByEmail(string email, Guid? workspaceKey = null)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// This method is called to unsubscribe a recipient from a given list.
    /// </summary>
    /// <param name="receiverId"></param>
    /// <param name="listId"></param>
    /// <returns></returns>
    public bool Unsubscribe(EmailReceiverIdentifier receiverId, RecipientListIdentifier listId)
    {
        //TODO: Change status
        return true;
    }

    /// <summary>
    /// This method is called to unsubscribe a recipient from all lists in the provider.
    /// </summary>
    /// <param name="receiverId"></param>
    /// <returns></returns>
    public bool UnsubscribeAll(EmailReceiverIdentifier receiverId)
    {
        //TODO: Change status
        return true;
    }

    public string GetEditUrl(EmailReceiverIdentifier receiverId)
    {
        // Used when CanRedirectToEdit is true to allow for redirection to a edit-view. 
        // Ig. the Umbraco Member-view for Umbraco members.
        throw new NotImplementedException();
    }
}

/// <summary>
/// A model to recipient data about a In Memory Recipient
/// </summary>
public class InMemoryRecipient
{
    public int BirthYear { get; set; }
    public string City { get; set; }
    public string CompanyName { get; set; }
}

