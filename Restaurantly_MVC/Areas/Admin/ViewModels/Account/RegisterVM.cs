using System.ComponentModel.DataAnnotations;

namespace Restaurantly_MVC.Areas.Admin.ViewModels;

public class RegisterVM
{
    [Required]
    [MinLength(3)]
    [MaxLength(75)]
    public string Name { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(75)]
    public string Surname { get; set; }

    [Required]
    [MinLength(3)]
    [MaxLength(75)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(255)]
    [DataType(DataType.EmailAddress)]



    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]

    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirimPassword { get; set; }


}
