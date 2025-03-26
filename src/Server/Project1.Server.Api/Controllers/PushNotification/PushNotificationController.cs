using Project1.Server.Api.Services;
using Project1.Shared.Dtos.PushNotification;
using Project1.Shared.Controllers.PushNotification;

namespace Project1.Server.Api.Controllers.PushNotification;

[Route("api/[controller]/[action]")]
[ApiController, AllowAnonymous]
public partial class PushNotificationController : AppControllerBase, IPushNotificationController
{
    [AutoInject] PushNotificationService pushNotificationService = default!;

    [HttpPost]
    public async Task Subscribe([Required] PushNotificationSubscriptionDto subscription, CancellationToken cancellationToken)
    {
        await pushNotificationService.Subscribe(subscription, cancellationToken);
    }

    [HttpPost("{deviceId}")]
    public async Task Unsubscribe([Required] string deviceId, CancellationToken cancellationToken)
    {
        await pushNotificationService.Unsubscribe(deviceId, cancellationToken);
    }
}
