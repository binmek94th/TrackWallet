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
    public IActionResult CreatePost(BudgetVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        obj.Budget.UserId = userId;
        _unitOfWork.Budget.Add(obj.Budget);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    /*
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Budget BudgetFromDb = _unitOfWork.Budget.Get(u=> u.Id == id);
        if (BudgetFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.UserSelectedCategory.GetAll(includeProperties: "Category").Select(u => new SelectListItem
        {
            Text = u.Category.Name,
            Value = u.Id .ToString()
        });
        BudgetVM budget = new()
        {
            CategoryList = CategoryList,
            Budget = BudgetFromDb
        };
        return View(budget);
    }

    [HttpPost]
    public IActionResult Edit(BudgetVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        obj.Budget.UserId = userId;
        _unitOfWork.Budget.Update(obj.Budget);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Budget BudgetFromDb = _unitOfWork.Budget.Get(u=> u.Id == id);
        if (BudgetFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.UserSelectedCategory.GetAll(includeProperties: "Category").Select(u => new SelectListItem
        {
            Text = u.Category.Name,
            Value = u.Id .ToString()
        });
        BudgetVM budget = new()
        {
            CategoryList = CategoryList,
            Budget = BudgetFromDb
        };
        return View(budget);
    }

    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        var BudgetToDeleted = _unitOfWork.Budget.Get(u => u.Id == id);

        if (BudgetToDeleted == null)
        {
            RedirectToAction("Index", "Budget");
        }

        _unitOfWork.Budget.Remove(BudgetToDeleted);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    */
    
}


    