using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class RecurringTransaction
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name can't be empty")]
    public string Name { get; set; }
    [Required (ErrorMessage = "Select a date")]
    public DateTime nextDate { get; set; }

    public string RecurringType { get; set; }
    [Required]
    public double Amount { get; set; }
    [Required]
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
    
    [ForeignKey("Event")]
    public int? EventId { get; set; }
    public Event Event { get; set; }
}