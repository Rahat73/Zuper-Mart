using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Zuper_Mart.Models;

namespace Zuper_Mart.Controllers
{
    public class HomeController : Controller
    {
        ZuperMartEntities entities = new ZuperMartEntities();


        
        public ActionResult Index()
        {
            Purser();
            List<Product> products = entities.Products.Where(temp => temp.Exclusivity.Equals(0)).OrderByDescending(temp=> temp.ProductID).ToList();
            return View(products);

        }

        public ActionResult Purser()
        {
            if(TempData["customer"] == null)
            {
                return View();
            }
            else
            {
                Customer customerInfo = TempData["customer"] as Customer;
                int customerID = customerInfo.CustomerID;
                Customer customers = entities.Customers.Where(temp => temp.CustomerID == customerID).FirstOrDefault();
                ViewBag.username = customers.UserName;
                Session["customerID"] = customerID;
                return View();
            }
        }

        public ActionResult About()
        {
            AboutU aboutU = entities.AboutUs.FirstOrDefault();
            return View(aboutU);
        }

        public ActionResult Products()
        {
            List<Product> products = entities.Products.Where(temp => temp.Exclusivity.Equals(0)).ToList();
            return View(products);
        }

        
        [HttpPost]
        public ActionResult Products(String PName)
        {
            List<Product> searchResult = entities.Products.Where(temp => temp.PName.Contains(PName) && temp.Exclusivity.Equals(0)).ToList();
            return View(searchResult);
        }
        public ActionResult Contact()
        {
            if (Session["customerID"] != null)
            {
                int customerID = (int)Session["customerID"];
                Customer customer = entities.Customers.Where(temp => temp.CustomerID == customerID).FirstOrDefault();
                ViewBag.customerInfo = customer.UserName;
                ViewBag.customerEmail = customer.Email;
                ViewBag.customerPhone = customer.Phone;

                AboutU aboutU = entities.AboutUs.FirstOrDefault();

                return View(aboutU);
            }
            else
            {
                AboutU aboutU = entities.AboutUs.FirstOrDefault();

                return View(aboutU);
            }

            
        }

        [HttpPost]
        public ActionResult Contact(String name, String email, String phone, String message)
        {
            Message msg = new Message();
            msg.FullName = name;
            msg.Email = email;
            msg.PhoneNo = phone;
            msg.Message1 = message;
            if(Session["customerID"] != null)
            {
                msg.CustomerID = (int)Session["customerID"];
            }
            entities.Messages.Add(msg);
            entities.SaveChanges();
            AboutU aboutU = entities.AboutUs.FirstOrDefault();
            ViewBag.msgSubmit = "Message sent Successfully";
            return View(aboutU);

        }

        [Authorize]
        public ActionResult ExclusiveProducts()
        {
            List<Product> xproducts = entities.Products.Where(temp => temp.Exclusivity.Equals(1)).ToList();
            return View(xproducts);
        }

       
    
    
    
    
    }
}