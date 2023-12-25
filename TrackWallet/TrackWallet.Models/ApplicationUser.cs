using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TrackWallet.Models;

public class ApplicationUser : IdentityUser
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string DefaultCurrency { get; set; }

    public DateTime CreatedAt { get; set; }
}