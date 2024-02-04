using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Utility;

namespace TrackWallet.Areas.Customer.Controllers;

[Area("Customer")]
[Authorize(Roles = SD.Role_Customer)]
public class NotificationController : Controller
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;


    public NotificationController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var data = _unitOfWork.Notification.GetAll().Where(u => u.UserId == userId).ToList();
        return View(data);
    }
}