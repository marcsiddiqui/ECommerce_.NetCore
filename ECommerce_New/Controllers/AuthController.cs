using Microsoft.AspNetCore.Mvc;
using ECommerce_New.Models;
using ECommerce_New.ModelClasses;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ECommerce_New.LogicLayer;

namespace ECommerce_New.Controllers
{
    public class AuthController : Controller
    {
        private readonly EcommerceNewContext _context;

        public AuthController(EcommerceNewContext context)
        {
            _context = context;
        }

        // Create
        public IActionResult SignUp()
        {
            return View();
        }

        // Create
        [HttpPost]
        public IActionResult SignUp(CustomerModel model)
        {
            var customer = new Customer();
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.Username = model.Username;
            customer.Password = model.Password;
            customer.Cnic = model.Cnic;
            customer.PhoneNumber = model.PhoneNumber;

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = customer.Id });
        }

        // Edit
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Edit
        [HttpPost]
        public IActionResult Login(CustomerModel model)
        {
            var customer = _context.Customers.Where(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefault();

            if (customer != null)
            {
                CookieOptions co = new CookieOptions();
                co.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append("AuthenticatedCustomer", customer.Id.ToString());

                SmallData.CustomerName = customer.FirstName + " " + customer.LastName;
                SmallData.CustomerIcon = customer.ImagePath;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("AuthenticatedCustomer");

            SmallData.CustomerName = "";

            return RedirectToAction("Login");
        }
    }
}
