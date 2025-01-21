using System.ComponentModel.DataAnnotations;

namespace AuthService.DTO;

public record UserRegisterDto([Required]string UserName, [Required]string Email, [Required]string Password);
