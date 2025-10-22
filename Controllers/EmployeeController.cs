using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Models;
using PayrollProject.Repositories.Interfaces;
using PayrollProject.ViewModel.EmployeeViewModels;

namespace PayrollProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ApplicationDbContext _context;
        public EmployeeController(IEmployeeRepository employeeRepository, ApplicationDbContext context)
        {
            _employeeRepository = employeeRepository;
            _context = context;
        }

        //GET: /Employee/Index
        public IActionResult Index()
        {
            return View();
        }

        //GET: /Employee/GetEmployeesJson
        [HttpGet]
        public async Task<IActionResult> GetEmployeesJson()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                var shifts = await _context.Shifts.ToDictionaryAsync(s => s.ShiftId, s => s.ShiftName);
                var employeeVMs = employees.Select(e => new EmployeeVM
                {
                    EmployeeId = e.EmployeeId,
                    FullName = e.FullName,
                    Designation = e.Designation,
                    Department = e.Department,
                    JoiningDate = e.JoiningDate,
                    Status = e.Status,
                    ShiftId = e.ShiftId,
                    ShiftName = e.ShiftId.HasValue && shifts.TryGetValue(e.ShiftId.Value, out var shiftName)
                                ? shiftName : "N/A",
                    BasicSalary = e.BasicSalary
                }).ToList();

                return Json(new { data = employeeVMs });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching employees: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        //GET: /Employee/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateShiftsViewBag();
            var model = new EmployeeVM();
            return PartialView("_EmployeeFormPartial", model);

        }

        //POST: /Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeVM viewModel)
        {
            ModelState.Remove(nameof(EmployeeVM.ShiftName));
            if (ModelState.IsValid)
            {
                var employee = new Employee
                {
                    FullName = viewModel.FullName,
                    Gender = viewModel.Gender,
                    DateOfBirth = viewModel.DateOfBirth,
                    Designation = viewModel.Designation,
                    Department = viewModel.Department,
                    JoiningDate = viewModel.JoiningDate,
                    BasicSalary = viewModel.BasicSalary,
                    EmploymentType = viewModel.EmploymentType,
                    BankAccountNumber = viewModel.BankAccountNumber,
                    ShiftId = viewModel.ShiftId,
                    Status = viewModel.Status ?? PayrollEnums.EmploymentStatus.Active
                };

                await _employeeRepository.AddEmployeeAsync(employee);
                return Json(new { success = true, message = "Employee created successfully." });
            }

            await PopulateShiftsViewBag();
            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                );
            return Json(new { success = false, errors });
        }

        // Helper method to populate shifts in ViewBag
        private async Task PopulateShiftsViewBag(object? selectedShift = null)
        {
            ViewBag.Shifts = await _context.Shifts.Where(s => s.IsActive).OrderBy(s => s.ShiftName).Select(s => new SelectListItem
            {
                Value = s.ShiftId.ToString(),
                Text = s.ShiftName,
            }).ToListAsync();
        }
    }
}
