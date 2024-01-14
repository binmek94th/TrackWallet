using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrackWallet.Models.ViewModel;

public class TransactionVM
{
    public Transaction Transaction { get; set; }
    
    [ValidateNever] 
    public IEnumerable<SelectListItem> CategoryList { get; set; }

    public IEnumerable<SelectListItem> WalletList { get; set; }
    public IEnumerable<SelectListItem> recurringList { get; set; }
    public IEnumerable<SelectListItem> loanList { get; set; }
}