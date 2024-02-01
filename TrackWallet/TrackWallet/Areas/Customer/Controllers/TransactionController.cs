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
public class Transaction : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;


    public Transaction(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Models.Transaction> objTransactionList =
            _unitOfWork.Transaction.GetAll(includeProperties:"Wallet").Where(u=>u.UserId == userId).ToList();
        
        
        return View(objTransactionList);
    }


    public IActionResult Create()
    {
        IEnumerable<Models.Category> IcategoriesFiltered = _unitOfWork.Category.GetAll().Where(u=> u.CategoryType == "Income");
        IEnumerable<Models.Category> EcategoriesFiltered = _unitOfWork.Category.GetAll().Where(u=> u.CategoryType == "Expense");
        List<Models.UserSelectedCategory> IuserSelectedCategories = new List<Models.UserSelectedCategory>();
        List<Models.UserSelectedCategory> EuserSelectedCategories = new List<Models.UserSelectedCategory>();

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        foreach (var ele in IcategoriesFiltered)
        {
            var b = _unitOfWork.UserSelectedCategory.Get(item => item.CategoryId == ele.Id && item.IsActive);
            if (b != null)
            {
                if (b.UserId == userId)
                {
                    IuserSelectedCategories.Add(b);
                }
            }

        }
        foreach (var ele in EcategoriesFiltered)
        {
            var b = _unitOfWork.UserSelectedCategory.Get(item => item.CategoryId == ele.Id && item.IsActive);
            if (b != null)
            {
                if (b.UserId == userId)
                {
                    EuserSelectedCategories.Add(b);
                }
            }

        }

        IEnumerable<Wallet> wallets = _unitOfWork.Wallet.GetAll().Where(item => item.UserId == userId);
        IEnumerable<Models.LoanAndDebt> Debts = _unitOfWork.LoanAndDebt.GetAll().Where(item => item.UserId == userId && item.Type == "Debt");
        IEnumerable<Models.LoanAndDebt> Loans = _unitOfWork.LoanAndDebt.GetAll().Where(item => item.UserId == userId && item.Type == "Loan");
        IEnumerable<Models.RecurringTransaction> recurringTransactions = _unitOfWork.RecurringTransaction.GetAll().Where(item => item.UserId == userId);
        IEnumerable<SelectListItem> ECategory = EuserSelectedCategories.Select(u => new SelectListItem
        {
            Text =u.Category.Name,
            Value = u.Id.ToString()
        });
        IEnumerable<SelectListItem> ICategory = IuserSelectedCategories.Select(u => new SelectListItem
        {
            Text =u.Category.Name,
            Value = u.Id.ToString()
        });
        IEnumerable<SelectListItem> WalletList = wallets.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> Debt = Debts.Select(u => new SelectListItem
        {
            Text = u.BorrowerName,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> Loan = Loans.Select(u => new SelectListItem
        {
            Text = u.BorrowerName,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> recurringList = recurringTransactions.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });

        TransactionVM transaction = new TransactionVM
        {
            Transaction = new Models.Transaction(),
            WalletList = WalletList,
            recurringList = recurringList,
            DebtList = Debt,
            loanList = Loan,
            IncomeCatergoryList = ICategory,
            ExpenseCategoryList = ECategory
        };
        
        return View(transaction);
    }


    [HttpPost]
    public IActionResult CreatePost(TransactionVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var wallet = _unitOfWork.Wallet.Get(u => u.WalletId == obj.Transaction.WalletId);
        if (obj.Transaction.Direction == "Expense")
        {
            if (wallet.Balance < obj.Transaction.Amount)
            {
                TempData["ErrorMessage"] = "No sufficient balance to fund this transaction";
                return RedirectToAction("Create");
            }
            wallet.Balance -= obj.Transaction.Amount;
        }
        else if (obj.Transaction.Direction == "Income")
        {
            wallet.Balance += obj.Transaction.Amount;
            if (obj.Transaction.LoanAndDebtId != null)
            {
                var loanDebt = _unitOfWork.LoanAndDebt.Get(u => u.Id == obj.Transaction.LoanAndDebtId);
                loanDebt.PaidAmount += obj.Transaction.Amount;
                _unitOfWork.LoanAndDebt.Update(loanDebt);
            }
        }
        
        _unitOfWork.Wallet.Update(wallet);
        obj.Transaction.UserId = userId;
        _unitOfWork.Transaction.Add(obj.Transaction);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Transaction transactionFromDb = _unitOfWork.Transaction.Get(u=> u.Id == id);
        transactionFromDb.AmountEdit = transactionFromDb.Amount;
        if (transactionFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<Models.Category> IcategoriesFiltered = _unitOfWork.Category.GetAll().Where(u=> u.CategoryType == "Income");
        IEnumerable<Models.Category> EcategoriesFiltered = _unitOfWork.Category.GetAll().Where(u=> u.CategoryType == "Expense");
        List<Models.UserSelectedCategory> IuserSelectedCategories = new List<Models.UserSelectedCategory>();
        List<Models.UserSelectedCategory> EuserSelectedCategories = new List<Models.UserSelectedCategory>();;

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        foreach (var ele in IcategoriesFiltered)
        {
            var b = _unitOfWork.UserSelectedCategory.Get(item => item.CategoryId == ele.Id && item.IsActive);
            if (b != null)
            {
                if (b.UserId == userId)
                {
                    IuserSelectedCategories.Add(b);
                }
            }

        }
        foreach (var ele in EcategoriesFiltered)
        {
            var b = _unitOfWork.UserSelectedCategory.Get(item => item.CategoryId == ele.Id && item.IsActive);
            if (b != null)
            {
                if (b.UserId == userId)
                {
                    EuserSelectedCategories.Add(b);
                }
            }

        }

        IEnumerable<Wallet> wallets = _unitOfWork.Wallet.GetAll().Where(item => item.UserId == userId);
        IEnumerable<Models.LoanAndDebt> Debts = _unitOfWork.LoanAndDebt.GetAll().Where(item => item.UserId == userId && item.Type == "Debt");
        IEnumerable<Models.LoanAndDebt> Loans = _unitOfWork.LoanAndDebt.GetAll().Where(item => item.UserId == userId && item.Type == "Loan");
        IEnumerable<Models.RecurringTransaction> recurringTransactions = _unitOfWork.RecurringTransaction.GetAll().Where(item => item.UserId == userId);
        IEnumerable<SelectListItem> ECategory = EuserSelectedCategories.Select(u => new SelectListItem
        {
            Text =u.Category.Name,
            Value = u.Id.ToString()
        });
        IEnumerable<SelectListItem> ICategory = IuserSelectedCategories.Select(u => new SelectListItem
        {
            Text =u.Category.Name,
            Value = u.Id.ToString()
        });
        IEnumerable<SelectListItem> WalletList = wallets.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> Debt = Debts.Select(u => new SelectListItem
        {
            Text = u.BorrowerName,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> Loan = Loans.Select(u => new SelectListItem
        {
            Text = u.BorrowerName,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> recurringList = recurringTransactions.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });

        TransactionVM transaction = new TransactionVM
        {
            Transaction = transactionFromDb,
            WalletList = WalletList,
            recurringList = recurringList,
            DebtList = Debt,
            loanList = Loan,
            IncomeCatergoryList = ICategory,
            ExpenseCategoryList = ECategory
        };
    
        return View(transaction);
    }

    [HttpPost]
    public IActionResult Edit(TransactionVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        obj.Transaction.UserId = userId;
        var wallet = _unitOfWork.Wallet.Get(u => u.WalletId == obj.Transaction.WalletId);
        if (obj.Transaction.Amount > obj.Transaction.AmountEdit)
        {
            var change = obj.Transaction.Amount - obj.Transaction.AmountEdit;
            obj.Transaction.Amount += change;
            wallet.Balance += change;
        }
        else if (obj.Transaction.Amount < obj.Transaction.AmountEdit)
        {
            var change = obj.Transaction.AmountEdit - obj.Transaction.Amount;
            if (change <= 0)
            {
                RedirectToAction("Edit");
            }
            obj.Transaction.Amount -= change;
            wallet.Balance -= change;
        }
        _unitOfWork.Transaction.Update(obj.Transaction);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.Transaction transactionFromDb = _unitOfWork.Transaction.Get(u=> u.Id == id);
        transactionFromDb.AmountEdit = transactionFromDb.Amount;
        if (transactionFromDb == null)
        {
            return NotFound();
        }
        IEnumerable<Models.Category> IcategoriesFiltered = _unitOfWork.Category.GetAll().Where(u=> u.CategoryType == "Income");
        IEnumerable<Models.Category> EcategoriesFiltered = _unitOfWork.Category.GetAll().Where(u=> u.CategoryType == "Expense");
        List<Models.UserSelectedCategory> IuserSelectedCategories = new List<Models.UserSelectedCategory>();
        List<Models.UserSelectedCategory> EuserSelectedCategories = new List<Models.UserSelectedCategory>();;

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


        foreach (var ele in IcategoriesFiltered)
        {
            var b = _unitOfWork.UserSelectedCategory.Get(item => item.CategoryId == ele.Id && item.IsActive);
            if (b != null)
            {
                if (b.UserId == userId)
                {
                    IuserSelectedCategories.Add(b);
                }
            }

        }
        foreach (var ele in EcategoriesFiltered)
        {
            var b = _unitOfWork.UserSelectedCategory.Get(item => item.CategoryId == ele.Id && item.IsActive);
            if (b != null)
            {
                if (b.UserId == userId)
                {
                    EuserSelectedCategories.Add(b);
                }
            }

        }

        IEnumerable<Wallet> wallets = _unitOfWork.Wallet.GetAll().Where(item => item.UserId == userId);
        IEnumerable<Models.LoanAndDebt> Debts = _unitOfWork.LoanAndDebt.GetAll().Where(item => item.UserId == userId && item.Type == "Debt");
        IEnumerable<Models.LoanAndDebt> Loans = _unitOfWork.LoanAndDebt.GetAll().Where(item => item.UserId == userId && item.Type == "Loan");
        IEnumerable<Models.RecurringTransaction> recurringTransactions = _unitOfWork.RecurringTransaction.GetAll().Where(item => item.UserId == userId);
        IEnumerable<SelectListItem> ECategory = EuserSelectedCategories.Select(u => new SelectListItem
        {
            Text =u.Category.Name,
            Value = u.Id.ToString()
        });
        IEnumerable<SelectListItem> ICategory = IuserSelectedCategories.Select(u => new SelectListItem
        {
            Text =u.Category.Name,
            Value = u.Id.ToString()
        });
        IEnumerable<SelectListItem> WalletList = wallets.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> Debt = Debts.Select(u => new SelectListItem
        {
            Text = u.BorrowerName,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> Loan = Loans.Select(u => new SelectListItem
        {
            Text = u.BorrowerName,
            Value = u.WalletId.ToString()
        });
        IEnumerable<SelectListItem> recurringList = recurringTransactions.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });

        TransactionVM transaction = new TransactionVM
        {
            Transaction = transactionFromDb,
            WalletList = WalletList,
            recurringList = recurringList,
            DebtList = Debt,
            loanList = Loan,
            IncomeCatergoryList = ICategory,
            ExpenseCategoryList = ECategory
        };
    
        return View(transaction);
    }
    

    [HttpPost]
    public IActionResult DeletePost(int? id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var obj = _unitOfWork.Transaction.Get(u => u.Id == id);
        var wallet = _unitOfWork.Wallet.Get(u => u.WalletId == obj.WalletId);
        if (obj.Direction == "Income")
        {
            wallet.Balance -= obj.Amount;
        }
        else if (obj.Direction == "Expense")
        {
            wallet.Balance += obj.Amount;
        }
        _unitOfWork.Wallet.Update(wallet);
        _unitOfWork.Transaction.Remove(obj);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
    
    
}


    