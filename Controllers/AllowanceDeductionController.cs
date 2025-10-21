using Microsoft.AspNetCore.Mvc;
using PayrollProject.Data;
using PayrollProject.DataModels;
using PayrollProject.ViewModel.AllowanceDeductionViewModels;

namespace PayrollProject.Controllers
{
    public class AllowanceDeductionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AllowanceDeductionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(AllowanceDeductionVM viewModel)
        {
            var dataModel = new AllowanceDeduction();
            dataModel.ADType = viewModel.ADType;
            dataModel.ADName = viewModel.ADName;
            dataModel.CalculationType = viewModel.CalculationType;
            dataModel.Percentage = viewModel.Percentage?? 0m;
            dataModel.FixedAmount = viewModel.FixedAmount?? 0m;
            dataModel.EffectiveFrom = viewModel.EffectiveFrom;
            _context.AllowanceDeductions.Add(dataModel);
            _context.SaveChanges();
            return View(viewModel);
        }
    }
}
