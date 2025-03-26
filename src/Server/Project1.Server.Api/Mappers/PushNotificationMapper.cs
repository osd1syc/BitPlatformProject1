using Project1.Server.Api.Models.PushNotification;
using Project1.Shared.Dtos.PushNotification;
using Riok.Mapperly.Abstractions;

namespace Project1.Server.Api.Mappers;

/// <summary>
/// More info at Server/Mappers/README.md
/// </summary>
[Mapper]
public static partial class PushNotificationMapper
{
    public static partial void Patch(this PushNotificationSubscriptionDto source, PushNotificationSubscription destination);
}
