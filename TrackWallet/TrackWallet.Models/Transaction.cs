using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    public string Direction { get; set; }
    public double Amount { get; set; }
    public string? Description { get; set; }
    public double AmountEdit { get; set; }
    public DateTime date { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Wallet")]
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }
    
    [ForeignKey("UserSelectedCategory")]
    public int UserSelectedCategoryId { get; set; }
    public UserSelectedCategory UserSelectedCategory { get; set; }
    

    [ForeignKey("LoanAndDebt")]
    public int? LoanAndDebtId { get; set; }
    public LoanAndDebt LoanAndDebt { get; set; }
}