using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class LoanAndDebt
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Type { get; set; }
    [Required]
    [Range(1,10000000, ErrorMessage = "Must be Greater Than 0")]
    public double Amount { get; set; }
    [Required]
    public string PaymentOption { get; set; }
    public string? BorrowerName { get; set; }
    public DateTime StartingDate { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Event")]
    public int? EventId { get; set; }

    public Event Event { get; set; }

    [ForeignKey("Wallet")] 
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }

    public ICollection<Transaction> Transactions { get; set; }
}