using System.ComponentModel.DataAnnotations;

namespace NetBank.Models;

public class LoginVm
{
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, DataType(DataType.Password)] public string Password { get; set; } = string.Empty;
}
public class RegisterVm
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required, EmailAddress] public string Email { get; set; } = string.Empty;
    [Required, MinLength(6), DataType(DataType.Password)] public string Password { get; set; } = string.Empty;
}
public class TransferVm
{
    [Required] public string FromAccount { get; set; } = string.Empty;
    [Required] public string ToAccount { get; set; } = string.Empty;
    [Range(1, 999999)] public decimal Amount { get; set; }
    public string Remarks { get; set; } = string.Empty;
}
