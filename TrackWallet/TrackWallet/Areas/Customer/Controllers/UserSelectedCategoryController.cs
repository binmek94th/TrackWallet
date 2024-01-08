using System.Collections;
using System.Net.Mime;
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
public class UserSelectedCategory : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;


    public UserSelectedCategory(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        List<Models.UserSelectedCategory> objCategoryList =
            _unitOfWork.UserSelectedCategory.GetAll(includeProperties: "Category").ToList();

        List<Models.UserSelectedCategory> selectedCategories = new List<Models.UserSelectedCategory>();

        foreach (var elements in objCategoryList)
        {
            if (userId == elements.UserId)
            {
                selectedCategories.Add(elements);
            }
        }

        return View(selectedCategories);
    }


    public IActionResult Create()
    {
        IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
        {
            Text = u.Name,
            Value = u.Id.ToString(),
        });

        ViewData["CategoryList"] = CategoryList;
        UserSelectedCategoriesVM userSelectedCategoriesVm = new()
        {
            CategoryList = CategoryList,
            UserSelectedCategory = new Models.UserSelectedCategory()
        };
        return View(userSelectedCategoriesVm);
    }
    


    [HttpPost]
    public IActionResult Create(UserSelectedCategoriesVM obj )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var existingRecord = _unitOfWork.UserSelectedCategory.Get(
            filter: u => u.UserId == userId && u.CategoryId == obj.UserSelectedCategory.CategoryId
        );

        if (existingRecord != null)
        {
            // A record with the same CategoryId and UserId already exists
            // You can handle this situation, e.g., show an error message or redirect to another action
            TempData["ErrorMessage"] = "A record with the same category already exists.";
            return RedirectToAction("Create");
        }
        
        obj.UserSelectedCategory.Amount = 0;
        obj.UserSelectedCategory.UserId = userId;
        obj.UserSelectedCategory.IsActive = true;
        _unitOfWork.UserSelectedCategory.Add(obj.UserSelectedCategory);
        _unitOfWork.Save();

       return RedirectToAction("Index");
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Models.UserSelectedCategory categoryFromDb = _unitOfWork.UserSelectedCategory.Get(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Edit(Models.UserSelectedCategory obj)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        obj.UserId = userId;
        _unitOfWork.UserSelectedCategory.Update(obj);
        _unitOfWork.Save();
        return RedirectToAction("Index");
    }
}


    