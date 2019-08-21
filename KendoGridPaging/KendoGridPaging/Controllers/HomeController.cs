using System.Linq;
using System.Web.Mvc;
using KendoGridPaging.KendoGridUtilities;
using KendoGridPaging.Models;

namespace KendoGridPaging.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadStudents(int page, int pageSize, int take, bool? activeOnly)
        {
            var sorterCollection = KendoGridSorterCollection.BuildCollection(Request);
            var filterCollection = KendoGridFilterCollection.BuildCollection(Request);

            var students = (!(activeOnly ?? false))
                               ? StudentRepository.Students
                               : StudentRepository.Students.Where(s => s.Active);
            var filteredStudents = students.MultipleFilter(filterCollection.Filters);
            var sortedStudents = filteredStudents.MultipleSort(sorterCollection.Sorters).ToList();
            var count = sortedStudents.Count();
            var data = (from v in sortedStudents.Skip((page - 1) * pageSize)
                            .Take(pageSize) select v).ToList();

            var jsonData = new { total = count, data };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAStudent(int id)
        {
            var student = StudentRepository.Students.Single(s => s.Id == id);
            return PartialView("Student", student);
        }
    }
}
