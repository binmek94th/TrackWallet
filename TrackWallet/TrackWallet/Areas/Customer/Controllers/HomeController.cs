using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TrackWallet.DataAccess.Data;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;
using TrackWallet.Models.ViewModel;
using TrackWallet.Utility;
using Transaction = TrackWallet.Areas.Admin.Controllers.Transaction;

namespace TrackWallet.Controllers;

[Area("Customer")]
[Authorize(Roles = SD.Role_Customer)]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;


    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Wallet> objWalletList = _unitOfWork.Wallet.GetAll().Where(u=>u.UserId == userId).ToList();
        List<Models.Transaction> objTransaction = _unitOfWork.Transaction.GetAll().Where(u => u.UserId == userId).ToList();

        HomeVM obj = new HomeVM
        {
            Wallet = objWalletList,
            Transaction = objTransaction
        };
        
        return View(obj);
    }
    
    public IActionResult Create()
    {
        WalletVM walletVm = new WalletVM();
        return View(walletVm);
    }

    [HttpPost]
    public IActionResult Create(WalletVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (obj.Wallet.Name == null)
        {
            return View(obj);
        }

        ModelState.Remove("Id");
        obj.Wallet.UserId = userId;

        _unitOfWork.Wallet.Add(obj.Wallet);
        _unitOfWork.Save();
        TempData["success"] = "Wallet created successfully";
            
        if (obj.Email != null)
        {
            var allUsers = _userManager.Users.Where(u=>u.Email == obj.Email).ToList();
            foreach (var user in allUsers)
            {
                SharedWallet Swallet = new SharedWallet();
                Swallet.UserId = user.Id;
                Swallet.WalletId = obj.Wallet.WalletId;
                _unitOfWork.SharedWallet.Add(Swallet);
                _unitOfWork.Save();
            }
        }
        
        return RedirectToAction("Index", "Home");
    }
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Wallet categoryFromDb = _unitOfWork.Wallet.Get(u => u.WalletId == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }

        return View(categoryFromDb);
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

