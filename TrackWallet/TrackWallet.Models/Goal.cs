using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class Goal
{
    [Key]
    public int GoalId { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Wallet")]
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; }
    
    [ForeignKey("Event")]
    public int? EventId { get; set; }
    public Event Event { get; set; }

    public string GoalName { get; set; }
    public String ContributionSchecdule { get; set; }
    public double TargetAmount { get; set; }
    public double CurrentAmount { get; set; }
    public DateTime Deadline { get; set; }
    public Boolean IsCompleted { get; set; }
    
}