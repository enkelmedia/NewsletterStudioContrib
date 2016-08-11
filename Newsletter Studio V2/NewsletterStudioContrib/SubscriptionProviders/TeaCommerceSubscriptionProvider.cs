using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewsletterStudio;
using NewsletterStudio.Bll.Providers;
using NewsletterStudio.Bll.Providers.Model;
using Umbraco.Core;


namespace NewsletterStudioContrib.SubscriptionProviders
{
    
    public class TeaCommerceSubscriptionProvider : SubscriptionProviderBase
    {
        public override string UniqueName { get { return "TeaCommerceSubscriptionProvider"; } }
        
        public override string DisplayName { get { return "TeaCommerce"; } }

        public override List<Receiver> GetSubscribersForSendOut(string listItemValue, SendOutParams parameters)
        {

            var listAll = new List<TeaReceiver>();

            var db = ApplicationContext.Current.DatabaseContext.Database;

            var listOfCustomers = db.Fetch<TeaCustomer>("select Id, Firstname, Lastname, Email, DateFinalized FROM TeaCommerce_Order WHERE Email is not null AND DateFinalized != ''");

            foreach (var customer in listOfCustomers)
            {
             
                // Not in the list?
                if (Common.IsValidEmail(customer.Email) && listAll.Any(x => x.Email == customer.Email))
                {
                    var newReciver = new TeaReceiver()
                    {
                        DataProviderKey = customer.Id.ToString(),
                        Email = customer.Email,
                        Fullname = string.Format("{0} {1}", customer.Firstname, customer.Lastname),
                        OrderDate = customer.DateFinalized
                    };

                    listAll.Add(newReciver);
                }
            }
            
            // Filter list if only no order in last 3 months
            if (listItemValue == "1")
            {
                var delList = listAll.Where(x => x.OrderDate < DateTime.Now.AddMonths(-3));
                foreach (var item in delList)
                {
                    listAll.Remove(item);
                }
            }

            return (from item in listAll select new Receiver() {DataProviderKey = item.DataProviderKey, Email = item.Email, Fullname = item.Fullname}).ToList();

        }

        public override IEnumerable<ListItem> GetListItems()
        {
            return new List<ListItem> { 
                                        new ListItem() { Text = "All customers", Value = "0" }, 
                                        new ListItem() { Text = "Customer with no order last 3 months", Value = "1" } 
                                      };
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

    public class TeaReceiver : Receiver
    {
        public DateTime OrderDate;
    }

    public class TeaCustomer
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime DateFinalized { get; set; }
    }


}
