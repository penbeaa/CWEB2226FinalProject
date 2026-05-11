using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClassStore.Domain.Entities;

namespace ClassStore.WebUI.Infrastructure.Binders
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

            public object BindModel(ControllerContext controllContext, ModelBindingContext bindingContext)
            {
                Cart cart = null;
                if (controllContext.HttpContext.Session != null)
                {
                    cart = controllContext.HttpContext.Session[sessionKey] as Cart;
                }
                if (cart == null)
                {
                    cart = new Cart();
                    if (controllContext.HttpContext.Session != null)
                    {
                        controllContext.HttpContext.Session[sessionKey] = cart;
                    }
                }
                return cart;
            }
        }
    }