using Microsoft.AspNetCore.Mvc;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel.EmployeeViewModels;
using PayrollProject.ViewModel.PayrollViewModels;

namespace PayrollProject.Controllers
{
    public class PayrollController : Controller
    {
        private readonly ApplicationDbContext _context;
        public PayrollController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(PayrollVM viewModel)
        {
            var dataModel = new Payroll();
            dataModel.EmployeeId = viewModel.EmployeeId;
            dataModel.PayPeriodStart = viewModel.PayPeriodStart;
            dataModel.PayPeriodEnd = viewModel.PayPeriodEnd;
            dataModel.BasicSalary = viewModel.BasicSalary;
            dataModel.TotalAllowances = viewModel.TotalAllowances;
            dataModel.TotalDeductions = viewModel.TotalDeductions;
            dataModel.NetSalary = viewModel.NetSalary;
            dataModel.PaymentStatus = viewModel.PaymentStatus;
            dataModel.PaymentDate = viewModel.PaymentDate;
            _context.Payrolls.Add(dataModel);
            _context.SaveChanges();
            return View(viewModel);
        }
    }
}
