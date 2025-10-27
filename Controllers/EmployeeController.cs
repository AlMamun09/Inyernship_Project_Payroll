using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Repositories.Interfaces;
using PayrollProject.ViewModel;

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

        // GET: /Employee/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Employee/GetEmployeesJson
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
                    EmployeeCode = e.EmployeeCode,
                    FullName = e.FullName,
                    Designation = e.Designation,
                    Department = e.Department,
                    JoiningDate = e.JoiningDate,
                    Status = e.Status,
                    ShiftId = e.ShiftId,
                    ShiftName = e.ShiftId.HasValue && shifts.TryGetValue(e.ShiftId.Value, out var shiftName)
                ? shiftName
                : "N/A",
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

        // GET: /Employee/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateShiftSelectList();
            ViewBag.Title = "Create Employee";
            ViewBag.FormAction = Url.Action("Create");
            return View("Create", new EmployeeVM());
        }

        // POST: /Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EmployeeVM model)
        {
            if (model.DateOfBirth.AddYears(18) > model.JoiningDate)
            {
                ModelState.AddModelError(nameof(model.JoiningDate),
                    "Employee must be 18 or older at the time of joining.");
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var entity = MapToEntity(model);
            entity.EmployeeId = Guid.NewGuid();

            var lastNumericId = await _employeeRepository.GetMaxEmployeeNumericIdAsync();
            var newNumericId = lastNumericId + 1;
            entity.EmployeeNumericId = newNumericId;
            entity.EmployeeCode = $"EMP {newNumericId.ToString("D3")}";

            await _employeeRepository.AddEmployeeAsync(entity);
            return Json(new { success = true, message = "Employee added successfully!" });
        }

        // GET: /Employee/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            await PopulateShiftSelectList();
            ViewBag.Title = "Edit Employee";
            ViewBag.FormAction = Url.Action("Edit");
            var vm = MapToVM(employee);
            return View("Create", vm); // reuse Create view for Edit
        }

        // POST: /Employee/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var existing = await _employeeRepository.GetEmployeeByIdAsync(model.EmployeeId);
            if (existing == null)
                return NotFound(new { success = false, message = "Employee not found." });

            existing.FullName = model.FullName;
            existing.Gender = model.Gender;
            existing.DateOfBirth = model.DateOfBirth;
            existing.Designation = model.Designation;
            existing.Department = model.Department;
            existing.JoiningDate = model.JoiningDate;
            existing.BasicSalary = model.BasicSalary;
            existing.EmploymentType = model.EmploymentType;
            existing.ShiftId = model.ShiftId;
            existing.Status = model.Status;

            await _employeeRepository.UpdateEmployeeAsync(existing);
            return Json(new { success = true, message = "Employee updated successfully!" });
        }

        // GET: /Employee/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee == null)
                return NotFound();

            var shifts = await _context.Shifts.ToDictionaryAsync(s => s.ShiftId, s => s.ShiftName);
            var vm = MapToVM(employee);
            if (vm.ShiftId.HasValue && shifts.TryGetValue(vm.ShiftId.Value, out var name))
            {
                vm.ShiftName = name;
            }
            return View(vm);
        }

        // POST: /Employee/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _employeeRepository.DeleteEmployeeAsync(id);
                return Json(new { success = true, message = "Employee deleted successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the employee." });
            }
        }

        private async Task PopulateShiftSelectList()
        {
            var shifts = await _context.Shifts.AsNoTracking().OrderBy(s => s.ShiftName).ToListAsync();
            ViewBag.Shifts = shifts;
        }

        private static Employee MapToEntity(EmployeeVM vm)
        {
            return new Employee
            {
                EmployeeId = vm.EmployeeId,
                EmployeeCode = vm.EmployeeCode,
                FullName = vm.FullName,
                Gender = vm.Gender,
                DateOfBirth = vm.DateOfBirth,
                Designation = vm.Designation,
                Department = vm.Department,
                JoiningDate = vm.JoiningDate,
                BasicSalary = vm.BasicSalary,
                PaymentSystem = vm.PaymentSystem,
                AccountHolderName = vm.AccountHolderName,
                BankAndBranchName = vm.BankAndBranchName,
                BankAccountNumber = vm.BankAccountNumber,
                MobileNumber = vm.MobileNumber,
                EmploymentType = vm.EmploymentType,
                ShiftId = vm.ShiftId,
                Status = vm.Status
            };
        }

        private static EmployeeVM MapToVM(Employee e)
        {
            return new EmployeeVM
            {
                EmployeeId = e.EmployeeId,
                EmployeeCode = e.EmployeeCode,
                FullName = e.FullName,
                Gender = e.Gender,
                DateOfBirth = e.DateOfBirth,
                Designation = e.Designation,
                Department = e.Department,
                JoiningDate = e.JoiningDate,
                BasicSalary = e.BasicSalary,
                PaymentSystem = e.PaymentSystem,
                AccountHolderName = e.AccountHolderName,
                BankAndBranchName = e.BankAndBranchName,
                BankAccountNumber = e.BankAccountNumber,
                MobileNumber = e.MobileNumber,
                EmploymentType = e.EmploymentType,
                ShiftId = e.ShiftId,
                Status = e.Status
            };
        }
    }
}
