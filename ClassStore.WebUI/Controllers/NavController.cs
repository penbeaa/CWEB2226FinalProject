using ClassStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IClassRepository repository;

        public NavController(IClassRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;

            IEnumerable<String> categories = repository.Classes.Select(x => x.Category)
                                                                .Distinct()
                                                                .OrderBy(x => x);
            return PartialView(categories);
        }
    }
}