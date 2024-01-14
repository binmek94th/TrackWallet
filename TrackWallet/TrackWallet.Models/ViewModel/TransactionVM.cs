using System.Collections;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrackWallet.Models.ViewModel;

public class TransactionVM
{
    public Transaction Transaction { get; set; }
    
    public IEnumerable<SelectListItem> IncomeCatergoryList { get; set; }
    public IEnumerable<SelectListItem> ExpenseCategoryList { get; set; }

    public IEnumerable<SelectListItem> WalletList { get; set; }
    public IEnumerable<SelectListItem> recurringList { get; set; }
    public IEnumerable<SelectListItem> loanList { get; set; }
    public IEnumerable<SelectListItem> DebtList { get; set; }
}