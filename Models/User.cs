using System.ComponentModel.DataAnnotations;

namespace NetBank.Models;

public class User
{
    public int UserId { get; set; }
    [Required, StringLength(100)] public string Name { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(150)] public string Email { get; set; } = string.Empty;
    [Required] public string PasswordHash { get; set; } = string.Empty;
    [Required] public string Role { get; set; } = "User"; // User/Admin
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    public ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();
}
