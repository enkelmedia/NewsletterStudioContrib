using System;
using NewsletterStudio.Model;
using NewsletterStudio.Services.RenderTasks;

namespace NewsletterStudioContrib.RenderTasks
{
    public class RenderTaskExample : RenderTask 
    {

        public override void ProcessPreRender(RenderResult renderResult, RenderTaskParameters parameters)
        {
            // This method will run once per send out and can be used to create over all changes that's not dependen on the uniq subscriber.
            // if your render task only works with uniq subscriber data, this method can be left empty.
            // This demo is very simple but you could use regular expressions to find/replace content, connect to databases, 
            // interact with the umbraco api, call external webserivces or what every you like inside there methods.

            // This line of code will replace any appearences of [preRender] with "Send out: From Custom Pre Render".
            renderResult.MessageBody = renderResult.MessageBody.Replace("[preRender]", "Send out: From Custom Render Task");

        }

        public override void ProcessPreview(RenderResult renderResult, RenderTaskParameters parameters)
        {
            // This method will be called when the letter is preview. Somethimes you don't want to perform exacly the same rendering in preview mode as
            // in send out. This method lets you do that. If you don't overide the base class will call ProcessPreRender. So if you want to do nothing on 
            // preview: Override this method and leave it blank.

            // This line of code will replace any appearences of [preRender] with "Preview: From Custom Pre Render".
            renderResult.MessageBody = renderResult.MessageBody.Replace("[preRender]", "Preview: From Custom Render Task");

        }


        public override void ProcessUniqItem(RenderResult renderResult, RenderTaskUniqItemParameters parameters)
        {
            // This method is called once for every subscriber that the email will be send to. It will give you the tracking item to get personal 
            // data. The only thing we know about the reciver at this time is its email and name found in emailTrackItem.

            // Ths will replace [subscriber] with both the name and email of the subscriber.
            renderResult.MessageBody = renderResult.MessageBody.Replace("[subscriber]", String.Format("{0} ({1})", parameters.EmailTrackingItem.Name, parameters.EmailTrackingItem.Email));

        }

    }
}
