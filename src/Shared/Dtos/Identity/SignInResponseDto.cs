
namespace Project1.Shared.Dtos.Identity;

public partial class SignInResponseDto : TokenResponseDto
{
    public bool RequiresTwoFactor { get; set; }
}
