using System.ComponentModel.DataAnnotations;

namespace Restaurantly_MVC.Areas.Admin.ViewModels;

public class LoginVM
{

    [Required]
    [MinLength(3)]
    [MaxLength(255)]
 
    public string UserOrEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]

    public string Password { get; set; }


    public bool RememberMe { get; set; }
}
