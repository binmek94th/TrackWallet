using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class Notification
{
    [Key]
    public int Id { get; set; }

    public string Message { get; set; }
    public Boolean IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    
    [ForeignKey("Event")]
    public int? EventId { get; set; }
    public Event Event { get; set; }
}