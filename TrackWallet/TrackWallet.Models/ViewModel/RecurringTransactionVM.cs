using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrackWallet.Models.ViewModel;

public class RecurringTransactionVM
{
    public RecurringTransaction RecurringTransaction { get; set; }
    public IEnumerable<SelectListItem> Category { get; set; }
    public IEnumerable<SelectListItem> Wallet { get; set; }
}