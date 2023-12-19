using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrackWallet.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Category Name can't be empty")]
    [DisplayName("Category Name")]
    [MaxLength(30,ErrorMessage = "Category Name can't be more than 30 characters")]
    public string  Name { get; set; }
    [Required(ErrorMessage = "Category Type can't be empty")]
    [DisplayName("Category Type")]
    public string CategoryType { get; set; }
    [ValidateNever]
    public string ImageUrl { get; set; }
}