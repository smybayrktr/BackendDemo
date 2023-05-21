using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class OperationClaimsController : Controller
    {
        private readonly IOperationClaimService _operationClaimService;

        public OperationClaimsController(IOperationClaimService operationClaimService)
        {
            _operationClaimService = operationClaimService;
        }

        [HttpPost("Add")]
        public IActionResult Add(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Add(operationClaim);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public IActionResult Update(OperationClaim operationClaim)
        {
           var result = _operationClaimService.Update(operationClaim);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(OperationClaim operationClaim)
        {
            var result = _operationClaimService.Delete(operationClaim);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _operationClaimService.GetAll();
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("Get")]
        public IActionResult Get(int Id)
        {
            var result = _operationClaimService.GetById(Id);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}

