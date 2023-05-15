﻿using System;
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
        private readonly IOperationClaimService _operationClaim;

        public OperationClaimsController(IOperationClaimService operationClaim)
        {
            _operationClaim = operationClaim;
        }

        [HttpPost("Add")]
        public IActionResult Add(OperationClaim operationClaim)
        {
            _operationClaim.Add(operationClaim);
            return Ok();
        }
    }
}

