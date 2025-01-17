using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging;
using OrchardCore.Commerce.Extensions;

namespace OrchardCore.Commerce.Models;

public class StripeApiSettings
{
    public string PublishableKey { get; set; }
    public string SecretKey { get; set; }
    public string WebhookSigningSecret { get; set; }

    public string DecryptWebhookSigningSecret(IDataProtectionProvider dataProtectionProvider, ILogger logger) =>
        WebhookSigningSecret.DecryptStripeApiKey(dataProtectionProvider, logger);

    public string DecryptSecretKey(IDataProtectionProvider dataProtectionProvider, ILogger logger) =>
        SecretKey.DecryptStripeApiKey(dataProtectionProvider, logger);
}
