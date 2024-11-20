using NewsletterStudio.Core.Editor.Controls;
using NewsletterStudio.Core.Editor.Models;
using NewsletterStudio.Core.Models;
using NewsletterStudio.Core.Rendering;

namespace Demo.Web.Extensions.EmailEditorControl;

//NOTE! Formatting here is made to fit the snippets on the website to avoid horizontal scrolling

public class HelloEmailControlType 
    : EmailControlTypeBase<HelloEmailControlData, HelloEmailControlViewModel>
{

    public override string Alias => HelloControlConstants.Alias;
    public override string IconSvg { get; }
    public override string Icon => "icon-alarm-clock";

    public override HelloEmailControlViewModel DoBuildViewModel(
        HelloEmailControlData model, 
        EmailControlRenderingData renderingData
        )
    {
        // This method will be called once for each Campaign, this can be used
        // to perform work that is not unique to the recipient. In our case the 
        // content of the textarea will always be the same so we can safely perform
        // all our work here.

        var vm = new HelloEmailControlViewModel();
        vm.Text = model.Text;
        return vm;
    }

    public override HelloEmailControlViewModel DoUpdateUniqueViewModel(
        HelloEmailControlViewModel model, 
        IRecipientDataModel recipient
        )
    {
        // This method is called once for every recipient, the "model" object 
        // will be a clone of the result from the DoBuildViewModel-method. 
        // Here we could update specific properties based on the recipient.
        // In our case we don't need to do this.

        if (recipient is SubscriberRecipientDataModel subscriberRecipient)
        {
            // A Campaign in sending
            // Do something with SubscriberRecipientDataModel 
        }

        if (recipient is TransactionalRecipientDataModel transactionalRecipient)
        {
            // A Transactional Email is sending
            // Do something with TransactionalRecipientDataModel 
        }

        return model;
    }

    public override HelloEmailControlData BuildEmptyInstance()
    {
        // This method is called to create an empty instance of the model, 
        // used to set default values when the control is dragged from the 
        // toolbox into the email.

        return new HelloEmailControlData()
        {
            Text = "Default data..."
        };
    }

    public override void DoPrepareForEdit(
        HelloEmailControlData control
        )
    {
        // This method is called when the editor is loaded, here we can pass
        // meta-data to the control the meta data is not saved but only used
        // for the front end editing

        control.Meta.Add("addedBy","customCSharpCode");

        base.DoPrepareForEdit(control);
    }

    public override bool ShouldAppearInToolbox(
        EmailType emailType
        )
    {
        // This could be used to limit the usage of a control type to only be available
        // to campaigns or transactional emails.

        return true;
    }
}