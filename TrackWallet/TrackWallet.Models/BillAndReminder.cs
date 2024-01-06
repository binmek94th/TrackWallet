using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class BillAndReminder
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public double amount { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("UserSelectedCategory")] 
    public int USCategoryId { get; set; }
    public UserSelectedCategory UserSelectedCategory { get; set; }

}