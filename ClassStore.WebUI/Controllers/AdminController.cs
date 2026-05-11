using ClassStore.Domain.Abstract;
using ClassStore.Domain.Entities;
using Ninject.Planning.Targets;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClassStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IClassRepository repository;

        public AdminController(IClassRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Classes);
        }

        public ViewResult Edit(int classID)
        {
            Class cls = repository.Classes
                .FirstOrDefault(c => c.ClassID == classID);

            return View(cls);
        }

        [HttpPost]
        public ActionResult Edit(Class cls, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    cls.ImageMimeType = image.ContentType;
                    cls.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(cls.ImageData, 0, image.ContentLength);
                }

                repository.SaveClass(cls);

                TempData["message"] = string.Format("{0} has been saved", cls.Name);

                return RedirectToAction("Index");
            }
            else
            {
                return View(cls);
            }
        }



        public ActionResult Create()
        {
            return View("Edit", new Class());
        }

        [HttpPost]
        public ActionResult Delete(int classID)
        {
            repository.DeleteClass(classID);
            TempData["message"] = "Class was deleted";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Class cls)
        {
            if (ModelState.IsValid)
            {
                repository.SaveClass(cls);
                TempData["message"] = $"{cls.Name} has been created";
                return RedirectToAction("Index");
            }

            return View("Edit", cls);
        }
    }
}