using Bit.Butil;
using Project1.Shared.Dtos.PushNotification;

namespace Project1.Client.Web.Services;

public partial class WebPushNotificationService : PushNotificationServiceBase
{
    [AutoInject] private Notification notification = default!;
    [AutoInject] private readonly IJSRuntime jSRuntime = default!;
    [AutoInject] private readonly ClientWebSettings clientWebSettings = default!;

    public override async Task<PushNotificationSubscriptionDto> GetSubscription(CancellationToken cancellationToken)
    {
        return await jSRuntime.GetPushNotificationSubscription(clientWebSettings.AdsPushVapid!.PublicKey);
    }

    public override async Task<bool> IsPushNotificationSupported(CancellationToken cancellationToken) =>string.IsNullOrEmpty(clientWebSettings.AdsPushVapid?.PublicKey) is false && await notification.IsNotificationAvailable();
}
