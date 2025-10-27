using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Repositories.Interfaces;
using PayrollProject.ViewModel;

namespace PayrollProject.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly ApplicationDbContext _context;

        public ShiftController(IShiftRepository shiftRepository, ApplicationDbContext context)
        {
            _shiftRepository = shiftRepository;
            _context = context;
        }

        // GET: /Shift/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Shift/GetShiftsJson
        [HttpGet]
        public async Task<IActionResult> GetShiftsJson()
        {
            try
            {
                var shifts = await _shiftRepository.GetAllShiftsAsync();
                var shiftVMs = shifts.Select(s => new ShiftVM
                {
                    ShiftId = s.ShiftId,
                    ShiftName = s.ShiftName,
                    StartTime = s.StartTime,
                    EndTime = s.EndTime,
                    IsActive = s.IsActive
                }).ToList();

                return Json(new { data = shiftVMs });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching shifts: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // GET: /Shift/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Title = "Create Shift";
            ViewBag.FormAction = Url.Action("Create");
            return View("Create", new ShiftVM());
        }

        // POST: /Shift/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ShiftVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var entity = MapToEntity(model);
            entity.ShiftId = Guid.NewGuid();

            await _shiftRepository.AddShiftAsync(entity);
            return Json(new { success = true, message = "Shift added successfully!" });
        }

        // GET: /Shift/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var shift = await _shiftRepository.GetShiftByIdAsync(id);
            if (shift == null)
                return NotFound();

            ViewBag.Title = "Edit Shift";
            ViewBag.FormAction = Url.Action("Edit");
            var vm = MapToVM(shift);
            return View("Create", vm); // reuse Create view for Edit
        }

        // POST: /Shift/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] ShiftVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var existing = await _shiftRepository.GetShiftByIdAsync(model.ShiftId);
            if (existing == null)
                return NotFound(new { success = false, message = "Shift not found." });

            existing.ShiftName = model.ShiftName;
            existing.StartTime = model.StartTime;
            existing.EndTime = model.EndTime;
            existing.IsActive = model.IsActive;

            await _shiftRepository.UpdateShiftAsync(existing);
            return Json(new { success = true, message = "Shift updated successfully!" });
        }

        // GET: /Shift/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var shift = await _shiftRepository.GetShiftByIdAsync(id);
            if (shift == null)
                return NotFound();

            var vm = MapToVM(shift);
            return View(vm);
        }

        // POST: /Shift/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _shiftRepository.DeleteShiftAsync(id);
                return Json(new { success = true, message = "Shift deleted successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the shift." });
            }
        }

        private static Shift MapToEntity(ShiftVM vm)
        {
            return new Shift
            {
                ShiftId = vm.ShiftId,
                ShiftName = vm.ShiftName,
                StartTime = vm.StartTime,
                EndTime = vm.EndTime,
                IsActive = vm.IsActive
            };
        }

        private static ShiftVM MapToVM(Shift s)
        {
            return new ShiftVM
            {
                ShiftId = s.ShiftId,
                ShiftName = s.ShiftName,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                IsActive = s.IsActive
            };
        }
    }
}