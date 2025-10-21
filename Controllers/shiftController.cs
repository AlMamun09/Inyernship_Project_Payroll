using Microsoft.AspNetCore.Mvc;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel.ShiftViewModels;

namespace PayrollProject.Controllers
{
    public class shiftController : Controller
    {
        private readonly ApplicationDbContext _context;
        public shiftController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index (ShiftVM viewModel)
        {
            var dataModel = new Shift();
            dataModel.ShiftName = viewModel.ShiftName;
            dataModel.StartTime = viewModel.StartTime;
            dataModel.EndTime = viewModel.EndTime;
            dataModel.IsActive = viewModel.IsActive;
            _context.Shifts.Add(dataModel);
            _context.SaveChanges();
            return View(viewModel);
        }
    }
}
