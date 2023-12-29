using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class Budget
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Category")] 
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    [Required]
    public string BudgetType { get; set; }
    [Required]
    public double Amount { get; set; }
}