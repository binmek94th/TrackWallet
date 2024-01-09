using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrackWallet.Models.ViewModel;

public class GoalVM
{
    public Goal Goal { get; set; }
    public IEnumerable<SelectListItem> Wallet { get; set; }
}