using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using NewsletterStudio.Umbraco.WebAPI;
using Umbraco.Web.Editors;
using Umbraco.Web.Mvc;
using Umbraco.Web.WebApi;

namespace NewsletterStudioContrib.Web.App_Plugins.FormsChecklistPrevalue
{
    [CamelCaseController]
    [IsBackOffice()]
    [PluginController("NewsletterStudioContrib")]
    public class MailingListsController : UmbracoAuthorizedJsonController
    {
        [HttpGet]
        public IEnumerable<NewsletterStudio.Core.Model.MailingList> GetMailingLists()
        {
            return NewsletterStudio.Infrastucture.GlobalFactory.Current.MailingListRepository.GetAll();
        }
    }
    
}