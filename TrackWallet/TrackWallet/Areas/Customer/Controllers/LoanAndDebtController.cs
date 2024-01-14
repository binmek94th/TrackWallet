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
public class LoanAndDebt : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;


    public LoanAndDebt(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Models.LoanAndDebt> objList =
            _unitOfWork.LoanAndDebt.GetAll().Where(item=>item.UserId == userID).ToList();
        
        
        return View(objList);
    }


    public IActionResult Create()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        IEnumerable<Wallet> wallets = _unitOfWork.Wallet.GetAll().Where(item => item.UserId == userId);

        IEnumerable<SelectListItem> WalletList = wallets.Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.WalletId.ToString()
        });
        LoanAndDebtVM loanAndDebt = new LoanAndDebtVM
        {
            LoanAndDebt = new Models.LoanAndDebt(),
            WalletList = WalletList
        };
        return View(loanAndDebt);
    }


    [HttpPost]
    public IActionResult CreatePost(LoanAndDebtVM obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var wallet = _unitOfWork.Wallet.Get(u => u.WalletId == obj.LoanAndDebt.WalletId);

        if (obj.LoanAndDebt.Type == "Loan")
        {
            if (wallet.Balance > obj.LoanAndDebt.Amount)
            {
                wallet.Balance -= obj.LoanAndDebt.Amount;
            }
        }
        else if (obj.LoanAndDebt.Type == "Debt")
        {
            wallet.Balance += obj.LoanAndDebt.Amount;
        }
        _unitOfWork.Wallet.Update(wallet);
        obj.LoanAndDebt.UserId = userId;
        _unitOfWork.LoanAndDebt.Add(obj.LoanAndDebt);
        _unitOfWork.Save();

        return RedirectToAction("Index");
    }
    
      public IActionResult Edit(int? id)
      {
          if (id == null || id == 0)
          {
              return NotFound();
          }
          var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

          Models.LoanAndDebt loanAdnDebtFromDb = _unitOfWork.LoanAndDebt.Get(u=> u.Id == id) ;
          if (loanAdnDebtFromDb == null)
          {
              return NotFound();
          }
          return View(loanAdnDebtFromDb);
      }

      [HttpPost]
      public IActionResult Edit(Models.LoanAndDebt obj)
      {
          var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
          obj.UserId = userId;
          _unitOfWork.LoanAndDebt.Update(obj);
          _unitOfWork.Save();
          return RedirectToAction("Index");
      }
      public IActionResult Delete(int? id)
      {
          if (id == null || id == 0)
          {
              return NotFound();
          }
          var userID = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

          Models.LoanAndDebt loanAdnDebtFromDb = _unitOfWork.LoanAndDebt.Get(u=> u.Id == id) ;
          if (loanAdnDebtFromDb == null)
          {
              return NotFound();
          }
          return View(loanAdnDebtFromDb);
      }

      [HttpPost]
      public IActionResult DeletePost(int? id)
      {
          var loanAndDebt = _unitOfWork.LoanAndDebt.Get(u => u.Id == id);

          if (loanAndDebt == null)
          {
              RedirectToAction("Index", "LoanAndDebt");
          }

          _unitOfWork.LoanAndDebt.Remove(loanAndDebt);
          _unitOfWork.Save();

          return RedirectToAction("Index");
      }
      
    
}


    