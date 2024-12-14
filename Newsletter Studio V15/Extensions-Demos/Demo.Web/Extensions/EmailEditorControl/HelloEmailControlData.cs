using NewsletterStudio.Core.Editor.Models;

namespace Demo.Web.Extensions.EmailEditorControl;

public class HelloEmailControlData : EmailControl
{
    public override string ControlTypeAlias => HelloControlConstants.Alias;
    public string Text { get; set; } = "";
    public Padding Padding { get; set; } = Padding.Default;
}