using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrackWallet.Models.ViewModel;

public class UserSelectedCategoriesVM
{
    public UserSelectedCategory UserSelectedCategory { get; set; }
    
    [ValidateNever] 
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}