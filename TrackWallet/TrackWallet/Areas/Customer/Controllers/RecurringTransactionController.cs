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
public class RecurringTransaction : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;


    public RecurringTransaction(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Models.RecurringTransaction> objbudgetList =
            _unitOfWork.RecurringTransaction.GetAll(includeProperties:"UserSelectedCategory,Wallet").ToList();

        List<Models.RecurringTransaction> selectedRecurringTransactions= new List<Models.RecurringTransaction>();
        List<DateTime> next = new List<DateTime>();
        int day;
        int month;
        int year;
        
        foreach (var elements in objbudgetList)
        {
            if (userId == elements.UserId)
            {
                // day = elements.nextDate.Day;
                // month = elements.nextDate.Month + 1;
                // year = elements.nextDate.Year;
                // DateTime nextObj = new DateTime(year, month, day);
                // elements.nextDate = nextObj;
                selectedRecurringTransactions.Add(elements);
            }
        }
        RecurringTransactionIndexVM recurringTransactionIndexVm = new()
        {
            RecurringTransactions =  selectedRecurringTransactions,
            Category = _unitOfWork.Category.GetAll().ToList(),
            
        };
        return View(recurringTransactionIndexVm);
    }


    public IActionResult Create()
    {
        IEnumerable<Models.Category> categoriesFiltered = _unitOfWork.Category.GetAll().Where(item => item.CategoryType == "Expense");
        List<Models.UserSelectedCategory> userSelectedCategories = new List<Models.UserSelectedCategory>();;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        List<Wallet> wallets = new List<Wallet>();

        IEnumerable<Wallet> walletFiltered = _unitOfWork.Wallet.GetAll().Where(item => item.UserId == userId);
        

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

        IEnumerable<SelectListItem> Wallets = walletFiltered.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });

        RecurringTransactionVM recurringTransaction = new()
        {
            Category = Category,
            RecurringTransaction = new Models.RecurringTransaction(),
            Wallet = Wallets
        };
        return View(recurringTransaction);
    }


    [HttpPost]
    public IActionResult CreatePost(RecurringTransactionVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        obj.RecurringTransaction.UserId = userId;
        _unitOfWork.RecurringTransaction.Add(obj.RecurringTransaction);
        _unitOfWork.Save();
 
        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.RecurringTransaction recurringTransactionFromDb = _unitOfWork.RecurringTransaction.Get(u=> u.Id == id);
        if (recurringTransactionFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<Models.Category> categoriesFiltered = _unitOfWork.Category.GetAll().Where(item => item.CategoryType == "Expense");
        List<Models.UserSelectedCategory> userSelectedCategories = new List<Models.UserSelectedCategory>();;
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        List<Wallet> wallets = new List<Wallet>();

        IEnumerable<Wallet> walletFiltered = _unitOfWork.Wallet.GetAll().Where(item => item.UserId == userId);
        

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

        IEnumerable<SelectListItem> Wallets = walletFiltered.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });

        RecurringTransactionVM recurringTransaction = new()
        {
            Category = Category,
            RecurringTransaction = recurringTransactionFromDb,
            Wallet = Wallets
        };
        return View(recurringTransaction);

    }

    [HttpPost]
    public IActionResult Edit(RecurringTransactionVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        obj.RecurringTransaction.UserId = userId;
        _unitOfWork.RecurringTransaction.Update(obj.RecurringTransaction);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.RecurringTransaction recurringTransactionFromDb = _unitOfWork.RecurringTransaction.Get(u=> u.Id == id);
        if (recurringTransactionFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.UserSelectedCategory.GetAll(includeProperties: "Category").Select(u => new SelectListItem
        {
            Text = u.Category.Name,
            Value = u.Id .ToString()
        });
        RecurringTransactionVM recurringTransaction = new()
        {
            Category = CategoryList,
            RecurringTransaction = recurringTransactionFromDb
        };
        return View(recurringTransaction);
    }

    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        var recurringTransactionFromDb = _unitOfWork.RecurringTransaction.Get(u => u.Id == id);

        if (recurringTransactionFromDb == null)
        {
            RedirectToAction("Index", "Budget");
        }

        _unitOfWork.RecurringTransaction.Remove(recurringTransactionFromDb);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    
    
}


    