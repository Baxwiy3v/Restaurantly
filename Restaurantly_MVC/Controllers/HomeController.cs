using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurantly_MVC.DAL;
using Restaurantly_MVC.Models;
using Restaurantly_MVC.ViewModels;

namespace Restaurantly_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
           _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.ToListAsync();



            HomeVM homeVM = new HomeVM{
            
            
                Products = products
            
            };

            return View(homeVM);
        }
    }
}
