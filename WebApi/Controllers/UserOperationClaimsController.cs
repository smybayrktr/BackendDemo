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
    public class UserOperationClaimsController : Controller
    {
        private readonly IUserOperationClaimService _userOperationClaimService;

        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            _userOperationClaimService = userOperationClaimService;
        }

        
        [HttpPost("Add")]
        public IActionResult Add(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Add(userOperationClaim);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("Update")]
        public IActionResult Update(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Update(userOperationClaim);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimService.Delete(userOperationClaim);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _userOperationClaimService.GetAll();
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("Get")]
        public IActionResult Get(int Id)
        {
            var result = _userOperationClaimService.GetById(Id);
            if (result.Success)
            {
                return Ok();
            }
            return BadRequest();
        }

    }
}

