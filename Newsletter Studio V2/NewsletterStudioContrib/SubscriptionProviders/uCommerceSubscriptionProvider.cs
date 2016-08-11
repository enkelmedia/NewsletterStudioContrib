using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using NewsletterStudio;
using NewsletterStudio.Bll.Providers;
using NewsletterStudio.Bll.Providers.Model;
using Umbraco.Core;


namespace NewsletterStudioContrib.SubscriptionProviders
{
    // This is a example of how you could implement a subscription provider
    // for Newsletter Studio for Umbraco. 
    // BE AWARE! This implementation may not be the best way to fetch customers from
    // uCommerce if you want to improve the implementation, please fork and create a request.
    
    public class uCommerceSubscriptionProvider : SubscriptionProviderBase
    {
        
        public override string UniqueName { get { return "uCommerceSubscriptionProvider"; } }
        
        public override string DisplayName { get { return "uCommerce"; } }

        public override List<Receiver> GetSubscribersForSendOut(string listItemValue, SendOutParams parameters)
        {

            var list = new List<Receiver>();
            var db = ApplicationContext.Current.DatabaseContext.Database;
            var uCommerceCustomers = db.Fetch<UCommerceCustomer>("SELECT CustomerId, EmailAddress, Firstname, Lastname FROM uCommerce_Customer");

            foreach (var customer in uCommerceCustomers)
            {
                if (Common.IsValidEmail(customer.EmailAddress) && !list.Any(x => x.Email == customer.EmailAddress))
                    list.Add(new Receiver()
                    {
                        DataProviderKey = customer.CustomerId.ToString(),
                        Email = customer.EmailAddress,
                        Fullname = string.Format("{0} {1}", customer.Firstname, customer.Lastname)
                    });
            }
            
            return list;
        }

        public override IEnumerable<ListItem> GetListItems()
        {
            return new List<ListItem> { new ListItem() { Text = "All customers", Value = "0" } };
        }

        public override bool CanEditSubsciber
        {
            get { return false; }
        }

        public override string GetEditUrl(string subscriptionId, string email)
        {
            return "";
        }

        public override bool Unsubscribe(string email)
        {
            // Just returns true as this is not implemented. Could be stored as a property on a Umbraco member or in a custom table.
            return true;
        }

        public override bool Unsubscribe(string email, string listItemId)
        {
            // Just returns true as this is not implemented. Could be stored as a property on a Umbraco member or in a custom table.
            return true;
        }
    }

    public class UCommerceCustomer
    {
        public int CustomerId { get; set; }
        public string EmailAddress { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }

}
