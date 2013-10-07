using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using NewsletterStudio.Core.Interfaces;
using umbraco.BusinessLogic;

namespace NewsletterStudioContrib.Factory
{
    public class SuperJsonEncoder : IJsonSerializer
    {
        public T Deserialize<T>(string obj)
        {
            Log.Add(LogTypes.Debug, 0, "NewsletterStudioContrib: Let's Deserialize" + obj);

            return new JavaScriptSerializer().Deserialize<T>(obj);
        }

        public string Serialize(object obj)
        {
            Log.Add(LogTypes.Debug, 0, "NewsletterStudioContrib: Let's Serialize" + obj.ToString());

            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
