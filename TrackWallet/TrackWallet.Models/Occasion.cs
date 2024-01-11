using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class Occasion
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Description { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Budget")]
    public int? BudgetId { get; set; }
    public Budget Budget { get; set; }
    
    public ICollection<Budget> Budgets { get; set; }
}