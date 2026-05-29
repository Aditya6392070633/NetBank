using System.ComponentModel.DataAnnotations;

namespace NetBank.Models;

public class Beneficiary
{
    public int BeneficiaryId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    [Required] public string AccountNumber { get; set; } = string.Empty;
    [Required] public string NickName { get; set; } = string.Empty;
}
