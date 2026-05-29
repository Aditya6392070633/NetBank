using System.ComponentModel.DataAnnotations.Schema;

namespace NetBank.Models;

public class Transaction
{
    public int TransactionId { get; set; }
    public string FromAccount { get; set; } = string.Empty;
    public string ToAccount { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")] public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public string Type { get; set; } = "Transfer";
    public string Remarks { get; set; } = string.Empty;
}
