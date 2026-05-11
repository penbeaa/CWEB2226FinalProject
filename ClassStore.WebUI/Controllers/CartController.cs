using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassStore.Domain.Abstract;
using ClassStore.Domain.Entities;
using ClassStore.WebUI.Models;

namespace ClassStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        private IClassRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IClassRepository repo, IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }


        public RedirectToRouteResult AddToCart(Cart cart, int classId, string returnUrl)
        {
            Class cls = repository.Classes.FirstOrDefault
                                    (c => c.ClassID == classId);
            if (cls != null)
            {
                cart.AddItem(cls, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int classId, string returnUrl)
        {
            Class cls = repository.Classes.FirstOrDefault
                                       (c => c.ClassID == classId);
            if (cls != null)
            {
                cart.RemoveLine(cls);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Hey Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed", shippingDetails);
            }
            else
            {
                return View(shippingDetails);
            }
                
        }
         
    }
}