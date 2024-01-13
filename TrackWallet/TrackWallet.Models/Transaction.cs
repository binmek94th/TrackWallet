using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    public string Direction { get; set; }
    public double Amount { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Wallet")]
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }
    
    [ForeignKey("BillAndReminder")]
    public int? BillAndReminderId { get; set; }
    public BillAndReminder BillAndReminder { get; set; }
    
    [ForeignKey("LoanAndDebt")]
    public int? LoanAndDebtId { get; set; }
    public LoanAndDebt LoanAndDebt { get; set; }
}