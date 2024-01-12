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
public class Occasion : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;


    public Occasion(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Models.Occasion> objOccasionList =
            _unitOfWork.Occasion.GetAll().Where(item=> item.UserId == userId).ToList();

        List<Models.Budget> objBudgetList = _unitOfWork.Budget.GetAll().ToList();

        OcassionIndexVM obj = new OcassionIndexVM
        {
            Occasions = objOccasionList,
            Budgets = objBudgetList
        };
        
        return View(obj);
    }


    public IActionResult Create()
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        IEnumerable<Models.Budget> budgets = _unitOfWork.Budget.GetAll().Where( u => u.UserId == userID);
        
        IEnumerable<SelectListItem> budget = budgets.Select(u => new SelectListItem
        {
            Text =u.Name,
            Value = u.Id.ToString()
        });

        OccasionVM occasion = new()
        {
            BudgetList = budget,
            Occasion = new Models.Occasion()
        };
        return View(occasion);
    }


    [HttpPost]
    public IActionResult CreatePost(OccasionVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (obj.Occasion.BudgetId == null)
        {
            obj.Budget.UserId = userId;
            obj.Budget.Name = obj.Occasion.Name;
            obj.Budget.IsActive = true;
            obj.Budget.BudgetType = "Occasional";
            _unitOfWork.Budget.Add(obj.Budget);
            _unitOfWork.Save();
            obj.Occasion.BudgetId = obj.Budget.Id;
        }
        obj.Occasion.UserId = userId;
        _unitOfWork.Occasion.Add(obj.Occasion);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Occasion occasionFromDb = _unitOfWork.Occasion.Get(u=> u.Id == id);
        if (occasionFromDb == null)
        {
            return NotFound();
        }
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        IEnumerable<Models.Budget> budgets = _unitOfWork.Budget.GetAll().Where( u => u.UserId == userID);
        IEnumerable<SelectListItem> budget = budgets.Select(u => new SelectListItem
        {
            Text =u.Name,
            Value = u.Id.ToString()
        });

        OccasionVM occasion = new()
        {
            BudgetList = budget,
            Occasion = occasionFromDb
        };
        return View(occasion);
    }

    [HttpPost]
    public IActionResult Edit(OccasionVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        obj.Occasion.UserId = userId;
        if (obj.Occasion.BudgetId == null)
        {
            obj.Budget.UserId = userId;
            obj.Budget.Name = obj.Occasion.Name;
            obj.Budget.IsActive = true;
            obj.Budget.BudgetType = "Occasional";
            _unitOfWork.Budget.Update(obj.Budget);
            _unitOfWork.Save();
            obj.Occasion.BudgetId = obj.Budget.Id;
        }
        obj.Occasion.UserId = userId;
        _unitOfWork.Occasion.Update(obj.Occasion);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Occasion occasionFromDb = _unitOfWork.Occasion.Get(u=> u.Id == id);
        if (occasionFromDb == null)
        {
            return NotFound();
        }
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        IEnumerable<Models.Budget> budgets = _unitOfWork.Budget.GetAll().Where( u => u.UserId == userID);
        IEnumerable<SelectListItem> budget = budgets.Select(u => new SelectListItem
        {
            Text =u.Name,
            Value = u.Id.ToString()
        });

        OccasionVM occasion = new()
        {
            BudgetList = budget,
            Occasion = occasionFromDb
        };
        return View(occasion);
    }

    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        var occasionToBeDeleted = _unitOfWork.Occasion.Get(u => u.Id == id);

        if (occasionToBeDeleted == null)
        {
            RedirectToAction("Index", "Occasion");
        }

        _unitOfWork.Occasion.Remove(occasionToBeDeleted);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    
}


    