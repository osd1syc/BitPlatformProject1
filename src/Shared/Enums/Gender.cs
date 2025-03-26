namespace Project1.Shared.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<Gender>))]
public enum Gender
{
    Other,
    Male,
    Female,
}
