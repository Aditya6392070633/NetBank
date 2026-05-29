using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetBank.Models;

public class Account
{
    public int AccountId { get; set; }
    [Required, StringLength(20)] public string AccountNumber { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")] public decimal Balance { get; set; }
    [Required] public string AccountType { get; set; } = "Savings";
    public bool IsActive { get; set; } = true;
    public int UserId { get; set; }
    public User? User { get; set; }
}
