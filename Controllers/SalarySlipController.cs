using Microsoft.AspNetCore.Mvc;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel.EmployeeViewModels;
using PayrollProject.ViewModel.SalarySlipViewModels;

namespace PayrollProject.Controllers
{
    public class SalarySlipController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SalarySlipController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(SalarySlipVM viewModel)
        {
            var dataModel = new SalarySlip();
            dataModel.PayrollId = viewModel.PayrollId;
            dataModel.EmployeeId = viewModel.EmployeeId;
            dataModel.Month = viewModel.Month;
            dataModel.Year = viewModel.Year;
            dataModel.GrossEarnings = viewModel.GrossEarnings;
            dataModel.TotalDeductions = viewModel.TotalDeductions;
            dataModel.NetPay = viewModel.NetPay;
            dataModel.GeneratedDate = viewModel.GeneratedDate;
            _context.SalarySlips.Add(dataModel);
            _context.SaveChanges();
            return View(viewModel);
        }
    }
}
