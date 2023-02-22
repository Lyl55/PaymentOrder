using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentOrder.Core.Domain.Abstract;

namespace PaymentOrder.WebAdminPanel.Controllers
{
    [Authorize]
    public class BankBranchController : Controller
    {
        private readonly IUnitOfWork db;
        public BankBranchController(IUnitOfWork db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
