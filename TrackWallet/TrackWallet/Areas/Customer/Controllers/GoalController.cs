using System.Collections;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TrackWallet.DataAccess.Repository.IRepository;
using TrackWallet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using TrackWallet.DataAccess.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrackWallet.Models.ViewModel;
using TrackWallet.Utility;

namespace TrackWallet.Areas.Admin.Controllers;

[Area("Customer")]
[Authorize(Roles = SD.Role_Customer)]
public class Goal : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;


    public Goal(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Models.Goal> objbudgetList =
            _unitOfWork.Goal.GetAll(includeProperties:"Wallet").ToList();

        List<Models.Goal> selectedGoals = new List<Models.Goal>();

        foreach (var elements in objbudgetList)
        {
            if (userId == elements.UserId)
            {
                selectedGoals.Add(elements);
            }
        }
        
        return View(selectedGoals);
    }


    public IActionResult Create()
    {
        IEnumerable<SelectListItem> WalletList = _unitOfWork.Wallet.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });
        GoalVM goal = new()
        {
            Goal = new Models.Goal(),
            Wallet = WalletList
        };
        return View(goal);
    }


    [HttpPost]
    public IActionResult CreatePost(GoalVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        obj.Goal.UserId = userId;
        _unitOfWork.Goal.Add(obj.Goal);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Goal goalFromDb = _unitOfWork.Goal.Get(u=> u.GoalId == id);
        if (goalFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<SelectListItem> WalletList = _unitOfWork.Wallet.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });
        GoalVM goal = new()
        {
            Goal = goalFromDb,
            Wallet = WalletList
        };
        return View(goal);
    }

    [HttpPost]
    public IActionResult Edit(GoalVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        obj.Goal.UserId = userId;
        _unitOfWork.Goal.Update(obj.Goal);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Goal goalFromDb = _unitOfWork.Goal.Get(u=> u.GoalId == id);
        if (goalFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<SelectListItem> walletList = _unitOfWork.Wallet.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });
        GoalVM goal = new()
        {
            Wallet = walletList,
            Goal = goalFromDb
        };
        return View(goal);
    }

    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        Models.Goal goalFromDb = _unitOfWork.Goal.Get(u=> u.GoalId == id);

        if (goalFromDb == null)
        {
            RedirectToAction("Index", "Goal");
        }

        _unitOfWork.Goal.Remove(goalFromDb);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    
    
}


    