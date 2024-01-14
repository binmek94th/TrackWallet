using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrackWallet.Models.ViewModel;

public class LoanAndDebtVM
{
    public LoanAndDebt LoanAndDebt { get; set; }
    public IEnumerable<SelectListItem> WalletList { get; set; }
}