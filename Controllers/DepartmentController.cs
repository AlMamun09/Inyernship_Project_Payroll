using Microsoft.AspNetCore.Mvc;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Repositories;
using PayrollProject.Repositories.Interfaces;
using PayrollProject.ViewModel;

namespace PayrollProject.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ApplicationDbContext _context;
        public DepartmentController (IDepartmentRepository departmentRepository, ApplicationDbContext context)
        {
            _departmentRepository = departmentRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartmentsJson()
        {
            try
            {
                var departments = await _departmentRepository.GetAllDepartmentsAsync();
                var departmentVMs = departments.Select(d => new DepartmentVM
                {
                    DepartmentId = d.DepartmentId,
                    DepartmentName = d.DepartmentName,
                    IsActive = d.IsActive
                }).ToList();
                return Json(new { data = departmentVMs });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching shifts: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Create Department";
            ViewBag.FormAction = Url.Action("Create");
            return View("Create", new DepartmentVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] DepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(d => d.Errors).Select(e=>e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }
            var entity = MapToEntity(departmentVM);
            entity.DepartmentId = Guid.NewGuid();
            await _departmentRepository.AddDepartmentAsync(entity);
            return Json(new { success = true, message = "Department added successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit (Guid id)
        {
            var department = await _departmentRepository.GetDepartmentsByIdAsync(id);
            if (department == null)
                return NotFound();

            ViewBag.Title = "Edit Department";
            ViewBag.FormAction = Url.Action("Edit");
            var vm = MapToVM(department);
            return View("Create", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] DepartmentVM departmentVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(d => d.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var existing = await _departmentRepository.GetDepartmentsByIdAsync(departmentVM.DepartmentId);
            if (existing == null)
            {
                return NotFound();
            }

            existing.DepartmentName = departmentVM.DepartmentName;
            existing.IsActive = departmentVM.IsActive;

            await _departmentRepository.UpdateDepartmentAsync(existing);
            return Json(new { success = true, message = "Department updated successfully" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (Guid id)
        {
            try
            {
                await _departmentRepository.DeleteDepartmentAsync(id);
                return Json(new { success = true, message = "Department deleted successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the department." });
            }
        }

        private static Department MapToEntity (DepartmentVM departmentVM)
        {
            return new Department
            {
                DepartmentId = departmentVM.DepartmentId,
                DepartmentName = departmentVM.DepartmentName,
                IsActive = departmentVM.IsActive
            };
        }

        private static DepartmentVM MapToVM(Department department)
        {
            return new DepartmentVM
            {
                DepartmentId = department.DepartmentId,
                DepartmentName = department.DepartmentName,
                IsActive = department.IsActive,
            };
        }
    }
}
