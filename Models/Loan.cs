using System.ComponentModel.DataAnnotations.Schema;

namespace NetBank.Models;

public class Loan
{
    public int LoanId { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Amount { get; set; }
    [Column(TypeName = "decimal(5,2)")] public decimal InterestRate { get; set; } = 10.5m;
    public string Status { get; set; } = "Pending"; // Pending/Approved/Rejected
    public DateTime AppliedOn { get; set; } = DateTime.Now;
}
