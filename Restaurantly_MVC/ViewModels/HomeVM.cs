using Microsoft.EntityFrameworkCore;
using Restaurantly_MVC.Models;

namespace Restaurantly_MVC.ViewModels
{
    public class HomeVM
    {
        public List<Product> Products { get; set; }
    }
}
