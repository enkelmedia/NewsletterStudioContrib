using Microsoft.AspNetCore.Mvc;
using NewsletterStudio.Core.Public;
using NewsletterStudio.Core.Public.Models;

namespace Demo.Web.Features.AddRecipient;

public class AddRecipientController : Controller
{
    private readonly INewsletterStudioService _newsletterStudioService;

    public AddRecipientController(INewsletterStudioService newsletterStudioService)
    {
        _newsletterStudioService = newsletterStudioService;
    }

    [HttpGet("examples/add")]
    public IActionResult Add()
    {
        var mailingList = _newsletterStudioService.GetMailingListsForAllWorkspaces().First().Lists.First();

        var addRecipientRequest = AddRecipientRequest.Create("john.doe@foobar.com")
            .ForWorkspace(mailingList.WorkspaceKey) // Optional workspaceKey, only used with multiple workspaces
            .WithFirstname("John")
            .WithLastname("Doe")
            .WithSource("Website")
            .WithCustomField("city", "London")
            .SubscribeTo(mailingList.UniqueKey)
            .Build();

        var result = _newsletterStudioService.AddRecipient(addRecipientRequest);

        if (result.Success)
        {
            return Content("Subscribed");
        }
        else
        {
            return Content($"Error: " + result.Message);
        }
    }
}
