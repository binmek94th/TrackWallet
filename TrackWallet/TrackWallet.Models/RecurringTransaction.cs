using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class RecurringTransaction
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public double Amount { get; set; }
    public string Type { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Wallet")]
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }
    
    [ForeignKey("UserSelectedCategory")]
    public int USCategoryId { get; set; }
    public UserSelectedCategory UserSelectedCategory { get; set; }
}