using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Controllers
{
    public class CategoryController : Controller
    {

        // Dependency Injection
        private BookStoreDbContext _dbContext;

        public CategoryController(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {

            IEnumerable<Category> objCategoryList = _dbContext.Categories;

            return View(objCategoryList);
        }




        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {

            if (obj.Name == obj.NumberOfProducts.ToString())
            {
                ModelState.AddModelError("CustomError", "The NumberOfProducts cannot match Name");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                return View();
            }
            return View(obj);
        }



        [HttpPost]
        public IActionResult CreateOrEditSPPost(Category category)
        {
            _dbContext.Database.ExecuteSqlRaw   
                ($"CategoryInsert_SP {category.Name}, {category.NumberOfProducts}");

            return RedirectToAction("Index");
        }



        public IActionResult Edit(int? id)
        {
            if (id==null | id==0)
            {
                NotFound();
            }

            var obj = _dbContext.Categories.SingleOrDefault(c => c.Id == id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult EditPOST(Category obj)
        {
            if (obj.Name == obj.NumberOfProducts.ToString())
            {
                ModelState.AddModelError("CustomError", "The Number of Products cannot match Name");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null | id == 0)
            {
                NotFound();
            }

            var obj = _dbContext.Categories.Find(id);

            return View(obj);
        }

        [HttpPost]
        public IActionResult DeletePOST(int? id)
        {
                
            var obj = _dbContext.Categories.SingleOrDefault(p=>p.Id==id);

            if (obj == null)
            {
                return NotFound();
            }


            _dbContext.Categories.Remove(obj);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }   


        public IActionResult DeleteSP(int id)
        {
            _dbContext.Database.ExecuteSqlRaw($"CategoryDelete_SP {id}");

            return RedirectToAction("Index");
        }


    }
}
