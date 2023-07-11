using Microsoft.AspNetCore.Mvc;
using ECommerce_New.Models;
using ECommerce_New.ModelClasses;

namespace ECommerce_New.Controllers
{
    [LogInOnly]
    public class RolesController : Controller
    {
        private readonly EcommerceNewContext _context;

        public RolesController(EcommerceNewContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RoleModel model)
        {
            var role = new Role();
            role.Name = model.Name;

            _context.Roles.Add(role);
            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = role.Id });
        }

        public IActionResult Index()
        {
            var li = _context.Roles.ToList();

            var modelLi = new List<RoleModel>();

            foreach (var obj in li)
            {
                var model = new RoleModel();

                model.Id = obj.Id;
                model.Name = obj.Name;

                modelLi.Add(model);
            }

            return View(modelLi);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var role = _context.Roles.Where(x => x.Id == id).FirstOrDefault();
            
            var model = new RoleModel();
            model.Id = role.Id;
            model.Name = role.Name;

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(RoleModel model)
        {
            var role = _context.Roles.Where(x => x.Id == model.Id).FirstOrDefault();

            role.Name = model.Name;

            _context.SaveChanges();

            return View();
        }

        public IActionResult Delete(int Id)
        {
            var role = _context.Roles.Where(x => x.Id == Id).FirstOrDefault();

            _context.Roles.Remove(role);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
