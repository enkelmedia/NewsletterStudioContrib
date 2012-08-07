using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web.UI.WebControls;
using NewsletterStudio;
using NewsletterStudio.Bll.Providers;
using umbraco.BusinessLogic;
using umbraco.DataLayer;

namespace NewsletterStudioContrib.SubscriptionProviders
{
    // This is a example of how you could implement a subscription provider
    // for Newsletter Studio for Umbraco. 
    // BE AWARE! This implementation may not be the best way to fetch customers from
    // uCommerce if you want to improve the implementation, please fork and create a request.
    
    public class uCommerceSubscriptionProvider : SubscriptionProviderBase
    {
        public override string UniqName { get { return "uCommerceSubscriptionProvider"; } }

        public override string DisplayName { get { return "uCommerce"; } }

        public override List<Receiver> GetSubscribersForSendOut(string listItemValue, SendOutParams parameters)
        {

            var list = new List<Receiver>();
            IRecordsReader dr;
            dr = Application.SqlHelper.ExecuteReader("Select CustomerId, EmailAddress, Firstname, Lastname from uCommerce_Customer");

            while (dr.Read())
            {

                var dataProviderKey = dr.Get<int>("CustomerId").ToString();
                var email = dr.Get<string>("EmailAddress");
                var fullname = string.Format("{0} {1}", dr.Get<string>("Firstname"), dr.Get<string>("Lastname"));

                if (Common.IsValidEmail(email) && list.Count(x => x.Email == email) == 0)
                    list.Add(new Receiver()
                    {
                        DataProviderKey = dataProviderKey,
                        Email = email,
                        Fullname = fullname
                    });
            }
            dr.Close();

            return list;
        }

        public override IEnumerable<ListItem> GetListItems()
        {
            return new List<ListItem> { new ListItem() { Text = "Customers", Value = "0" } };
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
            return true;
        }
    }
    
}
