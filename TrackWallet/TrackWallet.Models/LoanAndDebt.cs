using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class LoanAndDebt
{
    [Key]
    public int Id { get; set; }
    public string Type { get; set; }
    public string Amount { get; set; }
    public string PaymentOption { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}