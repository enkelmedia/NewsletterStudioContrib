using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using NewsletterStudio;
using NewsletterStudio.Bll.Providers;
using umbraco.BusinessLogic;
using umbraco.DataLayer;

namespace NewsletterStudioContrib.SubscriptionProviders
{
    
    public class TeaCommerceSubscriptionProvider : SubscriptionProviderBase
    {
        public override string UniqName { get { return "TeaCommerceSubscriptionProvider"; } }

        public override string DisplayName { get { return "TeaCommerce"; } }

        public override List<Receiver> GetSubscribersForSendOut(string listItemValue, SendOutParams parameters)
        {

            var listAll = new List<TeaReceiver>();
            IRecordsReader dr;

            dr = Application.SqlHelper.ExecuteReader("select Id, Firstname, Lastname, Email, DateFinalized FROM TeaCommerce_Order WHERE Email is not null AND DateFinalized != ''");

            while (dr.Read())
            {

                var dataProviderKey = dr.Get<int>("Id").ToString();
                var email = dr.Get<string>("Email");
                var fullname = string.Format("{0} {1}", dr.Get<string>("Firstname"), dr.Get<string>("Lastname"));
                var orderDate = dr.Get<DateTime>("DateFinalized");

                // Not in the list?
                if (Common.IsValidEmail(email) && listAll.Count(x => x.Email == email) == 0)
                {
                    var newReciver = new TeaReceiver()
                    {
                        DataProviderKey = dataProviderKey,
                        Email = email,
                        Fullname = fullname,
                        OrderDate = orderDate
                    };

                    listAll.Add(newReciver);
                }
            }
            dr.Close();


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
                                        new ListItem() { Text = "Customer no order in 3 months", Value = "1" } 
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
            return true;
        }
    }

    public class TeaReceiver : Receiver
    {
        public DateTime OrderDate;
    }

}
