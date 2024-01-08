using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrackWallet.Models;

public class Category 
{
    [ValidateNever]
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Category Name can't be empty")]
    [DisplayName("Category Name")]
    [MaxLength(30,ErrorMessage = "Category Name can't be more than 30 characters")]
    public string  Name { get; set; }
    [Required(ErrorMessage = "Select category type")]
    [DisplayName("Category Type")]
    public string CategoryType { get; set; }
    [Required(ErrorMessage = "Upload an image")]
    [DisplayName ("Image")]
    [ValidateNever]
    public string? ImageUrl { get; set; }
    
}