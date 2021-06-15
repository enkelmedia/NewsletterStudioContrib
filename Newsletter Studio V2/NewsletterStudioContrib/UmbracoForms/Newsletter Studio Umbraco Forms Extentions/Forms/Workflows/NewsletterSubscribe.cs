using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Forms.Core;
using Umbraco.Forms.Core.Attributes;
using Umbraco.Forms.Core.Enums;

namespace NewsletterStudioContrib.Web.Forms.Workflows
{
    public class NewsletterSubscribe : WorkflowType
    {

        [Setting("E-Mail Field Name", prevalues = "", 
            description = "The name of the field containing the e-mail address (i.e. 'E-Mail')", 
            view = "textstring")]
        public string EmailFieldName { get; set; }

        [Setting("Name field", prevalues = "", 
            description = "The name of the field containing the name (i.e. 'Full Name')", 
            view = "textstring")]
        public string NameFieldName { get; set; }

        [Setting("Lists field", 
            prevalues = "", 
            description = "The name of the field containing the selected maling lists (i.e. 'mailing lists')", 
            view = "textstring")]
        public string ListsFieldName { get; set; }

        public NewsletterSubscribe()
        {
            // - GUID, name et al setup
            this.Id = new Guid("b8e4262e-34ee-46c6-a047-a04095e83846");
            this.Name = "Subscribe to Newsletter Studio mailing list";
            this.Description = "Adds an e-mail to a mailing list within Newsletter Studio";
            this.Icon = "icon-inbox";
        }

        public override WorkflowExecutionStatus Execute(Record record, RecordEventArgs e)
        {
            try
            {
                // - get the email value and subscribe
                string email = record.GetRecordField(EmailFieldName).ValuesAsString();
                string name = record.GetRecordField(NameFieldName).ValuesAsString();

                // Validation the email before trying to parse the selected mailing-lists
                if (NewsletterStudio.Api.IsValidEmail(email))
                {

                    var listField = record.GetRecordField(ListsFieldName);

                    if (!string.IsNullOrEmpty(ListsFieldName) && listField != null && listField.HasValue())
                    {
                        List<object> lists = record.GetRecordField(ListsFieldName).Values;

                        foreach (string list in lists)
                        {
                            // getting the mailing list to figure out the ID
                            var mlist = NewsletterStudio.Infrastucture.GlobalFactory.Current
                                .MailingListRepository.GetAll().First(x => x.Name == list);

                            NewsletterStudio.Api.Subscribe(email, name, mlist.Id);
                        }

                    }

                }

                // - all good
                return WorkflowExecutionStatus.Completed;
            }
            catch (Exception ex)
            {
                // - that didn't work out: log it and bubble up failed execution
                Umbraco.Core.Logging.LogHelper.WarnWithException(this.GetType(), string.Format("Unable to subscribe e-mail (EmailFieldName: '{1}'): {0}", ex.Message, EmailFieldName), ex);
                return WorkflowExecutionStatus.Failed;
            }
        }

        public override List<Exception> ValidateSettings()
        {
            return new List<Exception>();
        }
    }

}