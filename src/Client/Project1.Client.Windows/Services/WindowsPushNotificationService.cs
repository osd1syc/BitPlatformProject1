using Project1.Shared.Dtos.PushNotification;

namespace Project1.Client.Windows.Services;

public partial class WindowsPushNotificationService : PushNotificationServiceBase
{
    public override Task<PushNotificationSubscriptionDto> GetSubscription(CancellationToken cancellationToken) =>
        throw new NotImplementedException();
}
