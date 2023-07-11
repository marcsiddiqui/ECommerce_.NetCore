using Microsoft.AspNetCore.Mvc;
using ECommerce_New.Models;
using ECommerce_New.ModelClasses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace ECommerce_New.Controllers
{
    [LogInOnly]
    public class CustomersController : Controller
    {
        private readonly EcommerceNewContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CustomersController(EcommerceNewContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        public IActionResult Create()
        {
            var model = new CustomerModel();

            PrepareRoleDropdown(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerModel model, IFormFile photo)
        {
            var customer = new Customer();
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.Username = model.Username;
            customer.Password = model.Password;
            customer.Cnic = model.Cnic;
            customer.PhoneNumber = model.PhoneNumber;
            customer.RoleId = model.RoleId;

            _context.Customers.Add(customer);
            _context.SaveChanges();

            if (photo != null)
            {
                var fileName = "Content\\Customer\\" + customer.Id.ToString() + "_" + customer.FirstName + "_" + photo.FileName;

                UploadImage(fileName, photo);

                customer.ImagePath = fileName;
                _context.SaveChanges();
            }
            else
            {
                customer.ImagePath = "https://cdn.vectorstock.com/i/preview-1x/82/99/no-image-available-like-missing-picture-vector-43938299.jpg";
                _context.SaveChanges();
            }



            return RedirectToAction("Edit", new { id = customer.Id });
        }

        public IActionResult Index()
        {
            string cookieValueFromReq = Request.Cookies["CUSTOMER_NAME"];

            

            var li = _context.Customers.ToList();

            var roles = _context.Roles.ToList();

            var modelLi = new List<CustomerModel>();

            foreach (var obj in li)
            {
                var model = new CustomerModel();

                model.Id = obj.Id;
                model.FirstName = obj.FirstName;
                model.LastName = obj.LastName;
                model.Email = obj.Email;
                model.Username = obj.Username;
                model.PhoneNumber = obj.PhoneNumber;
                model.Password = obj.Password;
                model.Cnic = obj.Cnic;
                if (string.IsNullOrWhiteSpace(obj.ImagePath))
                {
                    model.ImagePath = "https://cdn.vectorstock.com/i/preview-1x/82/99/no-image-available-like-missing-picture-vector-43938299.jpg";
                }
                else
                {
                    model.ImagePath = obj.ImagePath;
                }

                var role = roles.Where(x => x.Id == obj.RoleId).FirstOrDefault();
                model.RoleName = role != null ? role.Name : "No Role Set";

                modelLi.Add(model);
            }

            return View(modelLi);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            

            var customer = _context.Customers.Where(x => x.Id == id).FirstOrDefault();
            if (customer == null)
            {
                return RedirectToAction("Index");
            }

            var model = new CustomerModel();
            model.Id = customer.Id;
            model.FirstName = customer.FirstName;
            model.LastName = customer.LastName;
            model.Email = customer.Email;
            model.Username = customer.Username;
            model.PhoneNumber = customer.PhoneNumber;
            model.Password = customer.Password;
            model.Cnic = customer.Cnic;
            model.RoleId = customer.RoleId;

            PrepareRoleDropdown(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CustomerModel model)
        {
            

            var customer = _context.Customers.Where(x => x.Id == model.Id).FirstOrDefault();
            if (customer == null)
            {
                return RedirectToAction("Index");
            }

            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.Username = model.Username;
            customer.Password = model.Password;
            customer.Cnic = model.Cnic;
            customer.PhoneNumber = model.PhoneNumber;
            customer.RoleId = model.RoleId;

            _context.SaveChanges();

            PrepareRoleDropdown(model);

            return View(model);
        }

        public IActionResult Delete(int Id)
        {
            

            var customer = _context.Customers.Where(x => x.Id == Id).FirstOrDefault();

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public void PrepareRoleDropdown(CustomerModel model)
        {
            var roles = _context.Roles.Where(x => x.IsActive.Value).ToList();

            model.RoleOptions.Add(new SelectListItem { Text = "Select Role", Value = "0" });

            foreach (var role in roles)
            {
                model.RoleOptions.Add(new SelectListItem { Text = role.Name, Value = role.Id.ToString(), Selected = role.Id == model.RoleId });
            }
        }

        public async Task UploadImage(string fileName, IFormFile photo)
        {
            var path = Path.Combine(_hostEnvironment.WebRootPath, fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
                stream.Close();
            }
        }
    }
}
