using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentOrder.WebCore.Models;
using PaymentOrder.WebCore.Services.Contracts;
using System;

namespace PaymentOrder.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankController : ControllerBase
    {
        private readonly IBankService bankService;
        public BankController(IBankService bankService)
        {
            this.bankService = bankService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var banks = bankService.GetAll();

                return Ok(banks);
            }
            catch
            {
                return BadRequest("Unknown error occured");
            }
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var bank= bankService.Get(id);

                if (bank == null)
                    return BadRequest("No Bank found with given id");
                
                return Ok(bank);
            }
            catch
            {
                return BadRequest("Unknown error occured");
            }
        }

        [HttpPost]
        public IActionResult Post(BankModel bankModel)
        {
            try
            {
                bankService.Save(bankModel);

                return Ok("Success!");
            }
            catch
            {
                return BadRequest("Failed to add");
            }
        }

        [HttpPut("{id:int}")]
        public  IActionResult UpdateEmployee(int id, BankModel bankModel)
        {
            try
            {
                if (id != bankModel.Id)
                    return BadRequest("BankModel ID mismatch");

                var bankToUpdate = bankService.Get(id);

                if (bankToUpdate == null)
                    return NotFound($"Employee with Id = {id} not found");
                
                bankService.Save(bankModel);
                
                return Ok("Successfully Updated");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var bank = bankService.Get(id);
                
                if(bank == null)
                    return BadRequest("No such a bank found to delete");
                
                bankService.Delete(id);
                
                return Ok("Successfully deleted");
            }
            catch
            {
                return BadRequest("Failed to delete");
            }
        }
    }
}
