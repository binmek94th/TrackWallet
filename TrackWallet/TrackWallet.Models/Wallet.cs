using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TrackWallet.Models;

public class Wallet 
{
    public int WalletId { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public string Name { get; set; }
    public string WalletType { get; set; }
    public string Currency { get; set; }
    public double Balance { get; set; }

}