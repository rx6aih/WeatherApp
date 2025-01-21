using System.ComponentModel.DataAnnotations;

namespace AuthService.DTO;

public record UserLoginDto([Required] string Email, [Required] string Password);
