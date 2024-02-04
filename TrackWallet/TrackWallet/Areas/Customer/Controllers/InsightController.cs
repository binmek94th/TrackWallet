using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackWallet.Controllers;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;
using TrackWallet.Models.ViewModel;
using TrackWallet.Utility;

namespace TrackWallet.Areas.Customer.Controllers;


public class DailySummary
{
    public DateTime Date { get; set; }
    public int TotalAmount { get; set; }
}

[Area("Customer")]
[Authorize(Roles = SD.Role_Customer)]
public class InsightController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;


    public InsightController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var objTransaction = _unitOfWork.Transaction.GetAll().Where(u => u.UserId == userId).ToList();
        List<DailySummary> dailySummaries = new List<DailySummary>();

        foreach (var transaction in objTransaction)
        {
            // Check if a summary for this date already exists
            var existingSummary = dailySummaries.FirstOrDefault(summary =>
                summary.Date.ToShortDateString() == transaction.date.ToShortDateString());

            if (existingSummary != null)
            {
                // Add to the existing summary
                existingSummary.TotalAmount += (int)transaction.Amount;
            }
            else
            {
                // Create a new summary for this date
                var newSummary = new DailySummary
                {
                    Date = transaction.date,
                    TotalAmount = (int)transaction.Amount
                };
                dailySummaries.Add(newSummary);
            }
        }

        return View(dailySummaries);
    }
}