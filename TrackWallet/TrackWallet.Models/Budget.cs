using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class Budget
{
    [Key]
    public int Id { get; set; }
    [Required (ErrorMessage = "Name can't be empty")]
    public string Name { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("UserSelectedCategory")] 
    public int? USCategoryId { get; set; }
    public UserSelectedCategory UserSelectedCategory { get; set; }
    
    [ForeignKey("Event")]
    public int? EventId { get; set; }
    public Event Event { get; set; }
    
    [Required]
    public string? BudgetType { get; set; }
    [Required]
    public double Amount { get; set; }
    public double Used { get; set; }
    public Boolean IsActive { get; set; }

    public ICollection<Occasion> Occasions { get; set; }
}