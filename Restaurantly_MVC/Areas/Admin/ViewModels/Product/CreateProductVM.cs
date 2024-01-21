using System.ComponentModel.DataAnnotations;

namespace Restaurantly_MVC.Areas.Admin.ViewModels;

public class CreateProductVM
{
    [Required]
    public int Price { get; set; }
    [Required]
    [MaxLength(75)]
    public string Name { get; set; }
    [Required]

    public string Description { get; set; }
    [Required]
    public IFormFile Photo { get; set; }
}
