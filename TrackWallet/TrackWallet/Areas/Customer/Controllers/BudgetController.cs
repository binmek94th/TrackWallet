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
            _unitOfWork.Budget.GetAll(includeProperties:"UserSelectedCategory").ToList();

        List<Models.Budget> selectedBudget = new List<Models.Budget>();

        foreach (var elements in objbudgetList)
        {
            if (userId == elements.UserId)
            {
                selectedBudget.Add(elements);
            }
        }
        BudgetIndexVM budgetIndexVm = new()
        {
            Budgets =  selectedBudget,
            Category = _unitOfWork.Category.GetAll().ToList()
        };
        
        return View(budgetIndexVm);
    }


    public IActionResult Create()
    {
        IEnumerable<Models.Category> categoriesFiltered = _unitOfWork.Category.GetAll().Where(item => item.CategoryType == "Expense");
        List<Models.UserSelectedCategory> userSelectedCategories = new List<Models.UserSelectedCategory>();;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        foreach (var ele in categoriesFiltered)
        {
            var b = _unitOfWork.UserSelectedCategory.Get(item => item.CategoryId == ele.Id && item.IsActive);
            if (b != null)
            {
                if (b.UserId == userId)
                {
                    userSelectedCategories.Add(b);
                }
            }

        }
        IEnumerable<SelectListItem> Category = userSelectedCategories.Select(u => new SelectListItem
        {
            Text =u.Category.Name,
            Value = u.Id.ToString()
        });

        BudgetVM budget = new()
        {
            CategoryList = Category,
            Budget = new Models.Budget()
        };
        return View(budget);
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
        IEnumerable<Models.Category> categoriesFiltered = _unitOfWork.Category.GetAll().Where(item => item.CategoryType == "Expense");
        List<Models.UserSelectedCategory> userSelectedCategories = new List<Models.UserSelectedCategory>();;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        foreach (var ele in categoriesFiltered)
        {
            var b = _unitOfWork.UserSelectedCategory.Get(item => item.CategoryId == ele.Id && item.IsActive);
            if (b != null)
            {
                if (b.UserId == userId)
                {
                    userSelectedCategories.Add(b);
                }
            }

        }
        IEnumerable<SelectListItem> Category = userSelectedCategories.Select(u => new SelectListItem
        {
            Text =u.Category.Name,
            Value = u.Id.ToString()
        });
        BudgetVM budget = new()
        {
            CategoryList = Category,
            Budget = new Models.Budget()
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
    
    
}


    