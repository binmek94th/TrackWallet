using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;
using TrackWallet.Utility;

namespace TrackWallet.Controllers;

[Area("Customer")]
[Authorize(Roles = SD.Role_Customer)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Wallet> objWalletList = _unitOfWork.Wallet.GetAll().ToList();
        List<Wallet> UserWalletList = new List<Wallet>();
        foreach (var VARIABLE in objWalletList)
        {
            if (VARIABLE.UserId == userId)
            {
                UserWalletList.Add(VARIABLE);
            }
        }
        return View(UserWalletList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

