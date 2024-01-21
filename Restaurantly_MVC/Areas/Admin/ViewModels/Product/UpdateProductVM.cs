using System.ComponentModel.DataAnnotations;

namespace Restaurantly_MVC.Areas.Admin.ViewModels;

public class UpdateProductVM
{

    [Required]
    public int Price { get; set; }
    [Required]
    [MaxLength(75)]
    public string Name { get; set; }
    [Required]

    public string Description { get; set; }

    public IFormFile? Photo { get; set; }
    public string? ImageUrl { get; set; }
}
