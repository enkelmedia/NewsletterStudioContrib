using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using NewsletterStudio.Core.Interfaces;
using Umbraco.Core.Logging;

namespace NewsletterStudioContrib.Factory
{
    public class SuperJsonEncoder : IJsonSerializer
    {
        public T Deserialize<T>(string obj)
        {
            LogHelper.Debug<SuperJsonEncoder>("NewsletterStudioContrib: Let's Deserialize" + obj);
            
            return new JavaScriptSerializer().Deserialize<T>(obj);
        }

        public string Serialize(object obj)
        {
            LogHelper.Debug<SuperJsonEncoder>("NewsletterStudioContrib: Let's Serialize" + obj);

            return new JavaScriptSerializer().Serialize(obj);
        }
    }
}
