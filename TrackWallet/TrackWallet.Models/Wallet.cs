using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TrackWallet.Models;

public class Wallet 
{
    [Key]
    public int WalletId { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    [Required]
    public string Name { get; set; }
    [DisplayName("Wallet Type")]
    public string WalletType { get; set; }
    [Required]
    public string Currency { get; set; }
    [Required]
    public double Balance { get; set; }

    [Required] 
    public Boolean IsActive { get; set; }
    
    public ICollection<Goal> Goals { get; set; }
    public ICollection<RecurringTransaction> RecurringTransactions { get; set; }
    public ICollection<SharedWallet> SharedWallets { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<LoanAndDebt> LoanAndDebts { get; set; }


    

}