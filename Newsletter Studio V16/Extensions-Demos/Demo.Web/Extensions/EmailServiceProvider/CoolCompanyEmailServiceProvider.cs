using NewsletterStudio.Core.Models.System;
using NewsletterStudio.Core.Notifications;
using NewsletterStudio.Core.Sending;
using NewsletterStudio.Core.Sending.Providers;
using Umbraco.Cms.Core.Events;

namespace Demo.Web.Extensions.EmailServiceProvider;

public class CoolEmailCompanyEmailServiceProvider : IEmailServiceProvider
{
    private readonly IEventAggregator _eventAggregator;

    public string Alias => "coolEmail";

    public string DisplayName => "Cool Email";

    public Dictionary<string, object> Settings { get; set; }

    public CoolEmailCompanyEmailServiceProvider(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public SendOutConfiguration GetSendOutConfiguration()
    {
        return new SendOutConfiguration()
        {
            MaxItemsPerBatch = 10,
            SendBatchSize = 10
        };
    }

    public ErrorCollection ValidateSettings(Dictionary<string, object> settings)
    {
        var errors = new ErrorCollection();

        var apiKeyValue = settings["cc_apiKey"]?.ToString();

        if (string.IsNullOrEmpty(settings["cc_apiKey"]?.ToString()))
        {
            errors.Add(new ValidationError("cc_apiKey","API key is required"));
        }
        if (settings["cc_apiKey"]?.ToString() == "lorem")
        {
            errors.Add(new ValidationError("cc_apiKey", "API cannot be lorem"));
        }

        return errors;
    }

    public async Task SendAsync(List<SendEmailJob> batch)
    {
        foreach (var job in batch)
        {
            var fakeDtoForEmailService = new
            {
                sendTo = job.Message.To,
                body = job.Message.HtmlBody
            };

            // Fires the EmailSendingNotification to allow package consumers to make adjustments to
            // the model before calling upstream services.
            // This is optional for internal implementation but recommended if you plan to share
            await _eventAggregator.PublishAsync(new EmailSendingNotification(fakeDtoForEmailService)).ConfigureAwait(false);

            // Then send the email
            //_coolCompanyEmailApi.Send(fakeDtoForEmailService)

            var success = true;

            if (success)
            {
                job.Successful = true;
                job.ExternalId = "---id-if-provided-from-service";
            }
            else
            {
                job.ErrorMessage = "--error message---";
            }


        }

    }

    public async Task<CommandResult> SendAsync(EmailMessage message)
    {
        var fakeBulk = new List<SendEmailJob>();
        fakeBulk.Add(new SendEmailJob() { Message = message });

        await SendAsync(fakeBulk);

        var res = fakeBulk.First();

        if (res.Successful)
            return CommandResult.Successful();

        return CommandResult.Error(new ValidationError("", res.ErrorMessage));

    }

}