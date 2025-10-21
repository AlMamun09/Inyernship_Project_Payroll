using Microsoft.AspNetCore.Mvc;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel.AttendanceViewModels;
using PayrollProject.ViewModel.LeaveViewModels;

namespace PayrollProject.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LeaveController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(LeaveVM viewModel)
        {
            var dataModel = new Leave();
            dataModel.EmployeeId = viewModel.EmployeeId;
            dataModel.LeaveType = viewModel.LeaveType;
            dataModel.StartDate = viewModel.StartDate;
            dataModel.EndDate = viewModel.EndDate;
            dataModel.TotalDays = viewModel.TotalDays;
            dataModel.LeaveStatus = viewModel.LeaveStatus;
            dataModel.Remarks = viewModel.Remarks;
            _context.Leaves.Add(dataModel);
            _context.SaveChanges();
            return View(viewModel);
        }
    }
}
