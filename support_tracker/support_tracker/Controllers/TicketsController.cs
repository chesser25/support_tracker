using support_tracker.Models;
using System.Web.Mvc;
using support_tracker.AbstractRepos;

namespace support_tracker.Controllers
{
    public class TicketsController : Controller
    {
        private IGenericRepository<Department> departmentsRepository;

        public TicketsController(IGenericRepository<Department> departmentsRepository)
        {
            this.departmentsRepository = departmentsRepository;
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Departments = new SelectList(this.departmentsRepository.GetAll(), "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ticket ticket)
        {
            return View();
        }
    }
}