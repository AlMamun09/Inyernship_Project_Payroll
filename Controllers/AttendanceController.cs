using Microsoft.AspNetCore.Mvc;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel.AttendanceViewModels;
using PayrollProject.ViewModel.EmployeeViewModels;

namespace PayrollProject.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AttendanceController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(AttendanceVM viewModel)
        {
            var dataModel = new Attendance();
            dataModel.EmployeeId = viewModel.EmployeeId;
            dataModel.AttendanceDate = viewModel.AttendanceDate;
            dataModel.InTime = viewModel.InTime;
            dataModel.OutTime = viewModel.OutTime;
            dataModel.Status = viewModel.Status;
            dataModel.WorkingHours = viewModel.WorkingHours;
            _context.Attendances.Add(dataModel);
            _context.SaveChanges();
            return View(viewModel);
        }
    }
}
