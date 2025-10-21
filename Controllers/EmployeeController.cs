using Microsoft.AspNetCore.Mvc;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel;

namespace PayrollProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index (EmployeeVM viewModel)
        {
            var dataModel = new Employee();
            dataModel.FullName = viewModel.FullName;
            dataModel.Gender = viewModel.Gender;
            dataModel.DateOfBirth = viewModel.DateOfBirth;
            dataModel.Designation = viewModel.Designation;
            dataModel.Department = viewModel.Department;
            dataModel.JoiningDate = viewModel.JoiningDate;
            dataModel.BasicSalary = viewModel.BasicSalary;
            dataModel.EmploymentType = viewModel.EmploymentType;
            dataModel.BankAccountNumber = viewModel.BankAccountNumber;
            dataModel.ShiftId = viewModel.ShiftId;
            dataModel.Status = viewModel.Status;
            _context.Employees.Add(dataModel);
            _context.SaveChanges();
            return View(viewModel);
        }
    }
}
