using ClassStore.Domain.Abstract;
using ClassStore.Domain.Entities;
using ClassStore.WebUI.Models;
using Ninject.Planning.Targets;
using System.Linq;
using System.Web.Mvc;

namespace ClassStore.WebUI.Controllers
{
    public class ClassController : Controller
    {
        private IClassRepository myrepository;

        public ClassController(IClassRepository classrepository)
        {
            this.myrepository = classrepository;
        }

        public int PageSize = 4;

        public ViewResult List(string Category, int page = 1)
        {
            ClassesListViewModel model = new ClassesListViewModel
            {
                Classes = myrepository.Classes
                    .Where(c => Category == null || c.Category == Category)
                    .OrderBy(c => c.ClassID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = Category == null
                        ? myrepository.Classes.Count()
                        : myrepository.Classes.Count(e => e.Category == Category)
                },

                CurrentCategory = Category
            };

            return View(model);
        }

        public FileContentResult GetImage(int classId)
        {
            Class clas = myrepository.Classes.FirstOrDefault(c => c.ClassID == classId);



            if (clas != null)
            {
                return File(clas.ImageData, clas.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }
}