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
public class Budget : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;


    public Budget(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Models.Budget> objbudgetList =
            _unitOfWork.Budget.GetAll(includeProperties:"Category").ToList();

        List<Models.Budget> selectedBudget = new List<Models.Budget>();

        foreach (var elements in objbudgetList)
        {
            if (userId == elements.UserId)
            {
                selectedBudget.Add(elements);
            }
        }

        return View(selectedBudget);
    }


    public IActionResult Create()
    {
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        ViewData["UserSelected"] = CategoryList;
        return View();
    }


    [HttpPost]
    public IActionResult Create(Models.Budget obj )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        
        obj.UserId = userId;
        
        _unitOfWork.Budget.Add(obj);
        _unitOfWork.Save();

       return RedirectToAction("Index");
    }
    
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
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        ViewData["UserSelected"] = CategoryList;
        return View(BudgetFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Models.Budget obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        obj.UserId = userId;
        _unitOfWork.Budget.Update(obj);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
   /* public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Budget categoryFromDb = _unitOfWork.Budget.Get(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString()
        });
        ViewData["UserSelected"] = CategoryList;
        return View(categoryFromDb);
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        Models.Budget obj = _unitOfWork.Budget.Get(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }


        _unitOfWork.Budget.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
*/
    [HttpPost]
    public void Delete(int? id)
    {
        var BudgetToDeleted = _unitOfWork.Budget.Get(u => u.Id == id);

        if (BudgetToDeleted == null)
        {
            RedirectToAction("Index", "Budget");
        }
        _unitOfWork.Budget.Remove(BudgetToDeleted);
        _unitOfWork.Save();

        RedirectToAction("Index", "Budget");
    }
    
}


    