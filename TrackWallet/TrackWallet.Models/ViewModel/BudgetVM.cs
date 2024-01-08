using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrackWallet.Models.ViewModel;

public class BudgetVM
{
    public Budget Budget { get; set; }
    
    [ValidateNever] 
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}