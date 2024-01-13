using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrackWallet.Models;

public class Event
{
    [Key] 
    public int Id { get; set; }
    
    [ForeignKey("ApplicationUser")]
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public string EventName { get; set; }
    public string EventType { get; set; }
    public DateTime datetime { get; set; }
    
    public ICollection<SharedWallet> SharedWallets { get; set; }
    public ICollection<BillAndReminder> BillAndReminders { get; set; }
    public ICollection<RecurringTransaction> RecurringTransactions { get; set; }
    public ICollection<Budget> Budgets { get; set; }
    public ICollection<LoanAndDebt> LoanAndDebts { get; set; }
    public ICollection<Notification> Notifications { get; set; }
}