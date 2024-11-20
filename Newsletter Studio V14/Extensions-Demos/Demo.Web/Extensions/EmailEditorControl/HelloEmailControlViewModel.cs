using NewsletterStudio.Core.Rendering.ViewModels;

namespace Demo.Web.Extensions.EmailEditorControl;

public class HelloEmailControlViewModel : EmailControlViewModelBase
{
    public string Text { get; set; } = "";
}