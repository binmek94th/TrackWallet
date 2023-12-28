using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class UserSelectedCategory
{
    [Key]
    public int Id { get; set; }
    public bool IsActive { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
}