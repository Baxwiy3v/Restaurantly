using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurantly_MVC.Areas.Admin.ViewModels;
using Restaurantly_MVC.DAL;
using Restaurantly_MVC.Models;
using Restaurantly_MVC.Utitilies.Extentions;

namespace Restaurantly_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return View(products);
        }

        public IActionResult Create()

        {
            return View();

        }
        [HttpPost]

        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (!ModelState.IsValid) return View(productVM);

            bool result = await _context.Products.AnyAsync(p => p.Name.Trim().ToLower() == productVM.Name.Trim().ToLower());

            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda name movcuddur");
                return View(productVM);
            }

            if (!productVM.Photo.ValidatorType())
            {
                ModelState.AddModelError("Photo", "Seklin tipi uygun deyil");

                return View(productVM);
            }

            if (!productVM.Photo.ValidatorSize(5 * 1024))
            {
                ModelState.AddModelError("Photo", "Seklin olcusu uygun deyl");

                return View(productVM);
            }

            string image = await productVM.Photo.CreateFile(_env.WebRootPath, "assets", "img", "menu");

            Product product = new Product
            {


                Name = productVM.Name,
                Description = productVM.Description,
                ImageUrl = image,
                Price = productVM.Price


            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");


        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Product existed = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (existed == null) return NotFound();

            UpdateProductVM productVM = new UpdateProductVM
            {


                Name = existed.Name,
                Description = existed.Description,
                ImageUrl = existed.ImageUrl,
                Price = existed.Price,


            };
            return View(productVM);

        }

        [HttpPost]

        public async Task<IActionResult> Update(int id, UpdateProductVM productVM)
        {
            if (!ModelState.IsValid) return View(productVM);


            if (id <= 0) return BadRequest();

            Product existed = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (existed == null) return NotFound();


            bool result = await _context.Products.AnyAsync(p => p.Name.Trim().ToLower() == productVM.Name.Trim().ToLower() && p.Id != id);

            if (result)
            {
                ModelState.AddModelError("Name", "Bu adda name movcuddur");
                return View(productVM);
            }

            if (productVM.Photo is not null)
            {
                if (!productVM.Photo.ValidatorType())
                {
                    ModelState.AddModelError("Photo", "Seklin tipi uygun deyil");

                    return View(productVM);
                }

                if (!productVM.Photo.ValidatorSize(5 * 1024))
                {
                    ModelState.AddModelError("Photo", "Seklin olcusu uygun deyl");

                    return View(productVM);
                }

                string newimage = await productVM.Photo.CreateFile(_env.WebRootPath, "assets", "img", "menu");

                existed.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "img", "menu");
                existed.ImageUrl = newimage;
            }

            existed.Name = productVM.Name;
            existed.Description = productVM.Description;
            existed.Price = productVM.Price;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }


        public async Task<IActionResult> Delete(int id)
        {


            if (id <= 0) return BadRequest();

            Product existed = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existed == null) return NotFound();

            _context.Products.Remove(existed);

            existed.ImageUrl.DeleteFile(_env.WebRootPath, "assets", "img", "menu");

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
