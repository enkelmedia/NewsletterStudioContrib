using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Attributes;

namespace NewsletterStudioContrib.Web.Forms.PreValueSource
{
    public class NewsletterStudioPrevalusSource : FieldPreValueSourceType
    {
        [Setting("Choose mailing list to be available", 
            description = "Choose the mailing lists that you want the user to be able to choose",
            view = "../../../../NewsletterStudioContrib/FormsChecklistPrevalue/mailinglists")]
        public string SelectedMalingLists { get; set; }

        public NewsletterStudioPrevalusSource()
        {
            this.Id = new Guid("ccc618b1-c5b1-4c2e-8ee1-259633e8715d");
            this.Name = "Newsletter Studio Mailing Lists";
            this.Description = "Used Newsletter Studio Mailing Lists as sources for prevalues";
        }

        public override List<PreValue> GetPreValues(Field field, Form form)
        {

            if (string.IsNullOrEmpty(SelectedMalingLists))
                return new List<PreValue>();

            var arr = SelectedMalingLists.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var lists = NewsletterStudio.Infrastucture.GlobalFactory.Current
                        .MailingListRepository.GetAll().Where(x => arr.Contains(x.Name));

            return lists.Select(x => new PreValue() { Id = x.Id, Value = x.Name }).ToList();

        }

        public override List<Exception> ValidateSettings()
        {
            if(string.IsNullOrEmpty(SelectedMalingLists))
                return new List<Exception>(){new Exception("You need to check at least one mailing list.")};

            return new List<Exception>();
        }

    }
}