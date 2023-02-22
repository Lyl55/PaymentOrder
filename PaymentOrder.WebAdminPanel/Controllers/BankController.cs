using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentOrder.WebAdminPanel.Models;
using PaymentOrder.WebCore.Models;
using PaymentOrder.WebCore.Services.Contracts;
using System;
using System.Linq;

namespace PaymentOrder.WebAdminPanel.Controllers
{
    [Authorize(Roles ="A,SA")]
    public class BankController : Controller
    {
        private readonly IBankService bankService;
        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var bankModels = bankService.GetAll();

            BankViewModel viewModel = new BankViewModel()
            {
                Banks = bankModels
            };

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Save(int id)
        {
            if (id == 0)
                return PartialView();

            var bankModel = bankService.Get(id);

            return PartialView(bankModel);
        }

        [HttpPost]
        public IActionResult Save(BankModel model)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                    var errorMessage = errors.Aggregate((message, value) =>
                    {
                        if (message.Length == 0)
                            return value;

                        return message + ", " + value;
                    });

                    TempData["Message"] = errorMessage;
                    return RedirectToAction("Index");
                }

                bankService.Save(model);

                TempData["Message"] = "Operation successfully";
            }
            catch (Exception exc)
            {
                // log exception here

                TempData["Message"] = "Operation unsuccessfully";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(BankViewModel viewModel)
        {
            var deletedId = viewModel.Deleted.Id;

            bankService.Delete(deletedId);

            TempData["Message"] = "Operation successfully";

            return RedirectToAction("Index");
        }
    }
}
