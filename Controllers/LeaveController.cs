using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.Repositories.Interfaces;
using PayrollProject.ViewModel;

namespace PayrollProject.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILeaveRepository _leaveRepository;
        private readonly ApplicationDbContext _context;

        public LeaveController(ILeaveRepository leaveRepository, ApplicationDbContext context)
        {
            _leaveRepository = leaveRepository;
            _context = context;
        }

        // GET: /Leave/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Leave/GetLeavesJson
        [HttpGet]
        public async Task<IActionResult> GetLeavesJson()
        {
            try
            {
                var leaves = await _leaveRepository.GetAllLeavesAsync();
                var employees = await _context.Employees.ToDictionaryAsync(e => e.EmployeeId, e => e.FullName);

                var leaveVMs = leaves.Select(l => new LeaveVM
                {
                    LeaveId = l.LeaveId,
                    EmployeeId = l.EmployeeId,
                    EmployeeName = employees.TryGetValue(l.EmployeeId, out var name) ? name : "N/A",
                    LeaveType = l.LeaveType,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    TotalDays = l.TotalDays,
                    LeaveStatus = l.LeaveStatus,
                    Remarks = l.Remarks
                }).ToList();

                return Json(new { data = leaveVMs });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching leaves: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request." });
            }
        }

        // GET: /Leave/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await PopulateEmployeeSelectList();
            ViewBag.Title = "Create Leave";
            ViewBag.FormAction = Url.Action("Create");
            return View("Create", new LeaveVM());
        }

        // POST: /Leave/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] LeaveVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var entity = MapToEntity(model);
            entity.LeaveId = Guid.NewGuid();

            await _leaveRepository.AddLeaveAsync(entity);
            return Json(new { success = true, message = "Leave record added successfully!" });
        }

        // GET: /Leave/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var leave = await _leaveRepository.GetLeaveByIdAsync(id);
            if (leave == null)
                return NotFound();

            await PopulateEmployeeSelectList();
            ViewBag.Title = "Edit Leave";
            ViewBag.FormAction = Url.Action("Edit");
            var vm = MapToVM(leave);
            return View("Create", vm);
        }

        // POST: /Leave/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] LeaveVM model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { success = false, message = string.Join("\n", errors) });
            }

            var existing = await _leaveRepository.GetLeaveByIdAsync(model.LeaveId);
            if (existing == null)
                return NotFound(new { success = false, message = "Leave record not found." });

            existing.EmployeeId = model.EmployeeId;
            existing.LeaveType = model.LeaveType;
            existing.StartDate = model.StartDate;
            existing.EndDate = model.EndDate;
            existing.TotalDays = model.TotalDays;
            existing.LeaveStatus = model.LeaveStatus;
            existing.Remarks = model.Remarks;

            await _leaveRepository.UpdateLeaveAsync(existing);
            return Json(new { success = true, message = "Leave record updated successfully!" });
        }

        // GET: /Leave/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var leave = await _leaveRepository.GetLeaveByIdAsync(id);
            if (leave == null)
                return NotFound();

            var employees = await _context.Employees.ToDictionaryAsync(e => e.EmployeeId, e => e.FullName);
            var vm = MapToVM(leave);
            vm.EmployeeName = employees.TryGetValue(vm.EmployeeId, out var name) ? name : "N/A";
            return View(vm);
        }

        // POST: /Leave/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _leaveRepository.DeleteLeaveAsync(id);
                return Json(new { success = true, message = "Leave record deleted successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { success = false, message = "An error occurred while deleting the leave record." });
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

        private static Leave MapToEntity(LeaveVM vm)
        {
            return new Leave
            {
                LeaveId = vm.LeaveId,
                EmployeeId = vm.EmployeeId,
                LeaveType = vm.LeaveType,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                TotalDays = vm.TotalDays,
                LeaveStatus = vm.LeaveStatus,
                Remarks = vm.Remarks
            };
        }

        private static LeaveVM MapToVM(Leave l)
        {
            return new LeaveVM
            {
                LeaveId = l.LeaveId,
                EmployeeId = l.EmployeeId,
                LeaveType = l.LeaveType,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                TotalDays = l.TotalDays,
                LeaveStatus = l.LeaveStatus,
                Remarks = l.Remarks
            };
        }
    }
}