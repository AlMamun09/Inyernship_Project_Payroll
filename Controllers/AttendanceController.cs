using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Repositories.Interfaces;
using PayrollProject.ViewModel.AttendanceViewModels;

namespace PayrollProject.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly ApplicationDbContext _context;

        public AttendanceController(IAttendanceRepository attendanceRepository, ApplicationDbContext context)
        {
            _attendanceRepository = attendanceRepository;
            _context = context;
        }

        // GET: /Attendance/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Attendance/GetAttendancesJson
        [HttpGet]
        public async Task<IActionResult> GetAttendancesJson()
        {
            try
            {
                var attendances = await _attendanceRepository.GetAllAttendancesAsync();
                var employees = await _context.Employees.ToDictionaryAsync(e => e.EmployeeId, e => e.FullName);

                var attendanceVMs = attendances.Select(a => new AttendanceVM
                {
                    AttendanceId = a.AttendanceId,
                    EmployeeId = a.EmployeeId,
                    EmployeeName = a.EmployeeId != Guid.Empty && employees.TryGetValue(a.EmployeeId, out var name)
                        ? name
                        : "N/A",
                    AttendanceDate = a.AttendanceDate,
                    InTime = a.InTime,
                    OutTime = a.OutTime,
                    Status = a.Status,
                    WorkingHours = a.WorkingHours
                }).ToList();

                return Json(new { data = attendanceVMs });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching attendances: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // GET: /Attendance/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateEmployeeSelectList();
            ViewBag.Title = "Create Attendance";
            ViewBag.FormAction = Url.Action("Create");
            return View("Create", new AttendanceVM());
        }

        // POST: /Attendance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] AttendanceVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var entity = MapToEntity(model);
            entity.AttendanceId = Guid.NewGuid();

            await _attendanceRepository.AddAttendanceAsync(entity);
            return Json(new { success = true, message = "Attendance record added successfully!" });
        }

        // GET: /Attendance/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(id);
            if (attendance == null)
                return NotFound();

            await PopulateEmployeeSelectList();
            ViewBag.Title = "Edit Attendance";
            ViewBag.FormAction = Url.Action("Edit");
            var vm = MapToVM(attendance);
            return View("Create", vm);
        }

        // POST: /Attendance/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] AttendanceVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var existing = await _attendanceRepository.GetAttendanceByIdAsync(model.AttendanceId);
            if (existing == null)
                return NotFound(new { success = false, message = "Attendance record not found." });

            existing.EmployeeId = model.EmployeeId;
            existing.AttendanceDate = model.AttendanceDate;
            existing.InTime = model.InTime;
            existing.OutTime = model.OutTime;
            existing.Status = model.Status;
            existing.WorkingHours = model.WorkingHours;

            await _attendanceRepository.UpdateAttendanceAsync(existing);
            return Json(new { success = true, message = "Attendance record updated successfully!" });
        }

        // GET: /Attendance/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var attendance = await _attendanceRepository.GetAttendanceByIdAsync(id);
            if (attendance == null)
                return NotFound();

            var employees = await _context.Employees.ToDictionaryAsync(e => e.EmployeeId, e => e.FullName);
            var vm = MapToVM(attendance);
            vm.EmployeeName = employees.TryGetValue(vm.EmployeeId, out var name) ? name : "N/A";
            return View(vm);
        }

        // POST: /Attendance/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _attendanceRepository.DeleteAttendanceAsync(id);
                return Json(new { success = true, message = "Attendance record deleted successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the attendance record." });
            }
        }

        private async Task PopulateEmployeeSelectList()
        {
            var employees = await _context.Employees
                .Where(e => e.Status == "Active")
                .OrderBy(e => e.FullName)
                .ToListAsync();
            ViewBag.Employees = employees;
        }

        private static Attendance MapToEntity(AttendanceVM vm)
        {
            return new Attendance
            {
                AttendanceId = vm.AttendanceId,
                EmployeeId = vm.EmployeeId,
                AttendanceDate = vm.AttendanceDate,
                InTime = vm.InTime,
                OutTime = vm.OutTime,
                Status = vm.Status,
                WorkingHours = vm.WorkingHours
            };
        }

        private static AttendanceVM MapToVM(Attendance a)
        {
            return new AttendanceVM
            {
                AttendanceId = a.AttendanceId,
                EmployeeId = a.EmployeeId,
                AttendanceDate = a.AttendanceDate,
                InTime = a.InTime,
                OutTime = a.OutTime,
                Status = a.Status,
                WorkingHours = a.WorkingHours
            };
        }
    }
}