using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrackWallet.Models.ViewModel;

public class OccasionVM
{
    public Occasion Occasion { get; set; }
    public Budget Budget { get; set; }
    
    [ValidateNever] 
    public IEnumerable<SelectListItem> BudgetList { get; set; }
}