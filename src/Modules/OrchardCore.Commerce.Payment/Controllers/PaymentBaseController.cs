using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrchardCore.Commerce.Payment.Constants;
using OrchardCore.Commerce.Payment.ViewModels;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Mvc.Core.Utilities;
using System;
using System.Threading.Tasks;

namespace OrchardCore.Commerce.Payment.Controllers;
public abstract class PaymentBaseController : Controller
{
    private readonly INotifier _notifier;
    private readonly ILogger _logger;
    protected PaymentBaseController(INotifier notifier, ILogger logger)
    {
        _notifier = notifier;
        _logger = logger;
    }

    public async Task<IActionResult> ProduceActionResultAsync(PaymentOperationStatusViewModel paidStatusViewModel)
    {
        if (paidStatusViewModel.ShowMessage != null)
        {
            switch (paidStatusViewModel.Status)
            {
                case PaymentOperationStatus.Succeeded:
                    await _notifier.SuccessAsync(paidStatusViewModel.ShowMessage);
                    break;
                case PaymentOperationStatus.Failed:
                    await LogAndNotifyFailedAsync(paidStatusViewModel);
                    break;
                case PaymentOperationStatus.NotFound:
                    await LogAndNotifyWarningAsync(paidStatusViewModel);
                    break;
                case PaymentOperationStatus.NotThingToDo:
                case PaymentOperationStatus.WaitingForStripe:
                case PaymentOperationStatus.WaitingForPayment:
                    await LogAndNotifyInformationAsync(paidStatusViewModel);
                    break;
                default:
                    await LogAndNotifyFailedAsync(paidStatusViewModel);
                    break;
            }
        }

        return paidStatusViewModel.Status switch
        {
            PaymentOperationStatus.Succeeded => RedirectToActionWithParams<PaymentController>(
                nameof(PaymentController.Success),
                FeatureIds.Area,
                orderId: paidStatusViewModel.Content?.ContentItemId),

            PaymentOperationStatus.Failed => RedirectToActionWithParams<PaymentController>(
                nameof(PaymentController.Index),
                FeatureIds.Payment),

            PaymentOperationStatus.NotFound => NotFound(),

            PaymentOperationStatus.NotThingToDo => this.RedirectToContentDisplay(paidStatusViewModel.Content),

            PaymentOperationStatus.WaitingForStripe => RedirectToActionWithNames(
                "PaymentConfirmationMiddleware",
                "OrchardCore.Commerce.Payment.Stripe",
                "Stripe"),

            PaymentOperationStatus.WaitingForPayment => RedirectToActionWithParams<PaymentController>(
                nameof(PaymentController.Wait),
                FeatureIds.Payment,
                paidStatusViewModel.Url),

            _ => throw new ArgumentOutOfRangeException(paidStatusViewModel.ToString()),
        };
    }

    private RedirectToActionResult RedirectToActionWithParams<TController>(
        string actionName,
        string area,
        string? returnUrl = null,
        string? orderId = null)
        where TController : Controller
    {
        string localReturnUrl = string.Empty;
        if (!string.IsNullOrEmpty(returnUrl))
        {
            localReturnUrl = string.IsNullOrEmpty(returnUrl)
                    ? HttpContext.Request.GetDisplayUrl()
                    : returnUrl;
        }

        object? routeValues = new { area, returnUrl = localReturnUrl };

        if (!string.IsNullOrEmpty(orderId))
        {
            routeValues = new { area, orderId, returnUrl = localReturnUrl };
        }

        return RedirectToAction(
            actionName,
            typeof(TController).ControllerName(),
            routeValues
        );
    }

    private RedirectToActionResult RedirectToActionWithNames(
        string actionName,
        string area,
        string controllerName,
        string? returnUrl = null,
        string? orderId = null)
    {
        string localReturnUrl = string.Empty;
        if (!string.IsNullOrEmpty(returnUrl))
        {
            localReturnUrl = string.IsNullOrEmpty(returnUrl)
                    ? HttpContext.Request.GetDisplayUrl()
                    : returnUrl;
        }

        object? routeValues = new { area, returnUrl = localReturnUrl };

        if (!string.IsNullOrEmpty(orderId))
        {
            routeValues = new { area, orderId, returnUrl = localReturnUrl };
        }

        return RedirectToAction(
            actionName,
            controllerName,
            routeValues
        );
    }

    private async Task LogAndNotifyFailedAsync(PaymentOperationStatusViewModel paidStatusViewModel)
    {
        await _notifier.ErrorAsync(paidStatusViewModel.ShowMessage);
#pragma warning disable CA2254
        _logger.LogCritical(paidStatusViewModel.HideMessage);
#pragma warning restore CA2254
    }

    private async Task LogAndNotifyWarningAsync(PaymentOperationStatusViewModel paidStatusViewModel)
    {
        await _notifier.WarningAsync(paidStatusViewModel.ShowMessage);
#pragma warning disable CA2254
        _logger.LogWarning(paidStatusViewModel.HideMessage);
#pragma warning restore CA2254
    }

    private async Task LogAndNotifyInformationAsync(PaymentOperationStatusViewModel paidStatusViewModel)
    {
        await _notifier.InformationAsync(paidStatusViewModel.ShowMessage);
#pragma warning disable CA2254
        _logger.LogInformation(paidStatusViewModel.HideMessage);
#pragma warning restore CA2254
    }
}