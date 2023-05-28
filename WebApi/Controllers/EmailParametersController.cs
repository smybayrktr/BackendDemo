using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailParametersController : ControllerBase
    {
        private readonly IEmailParameterService _emailParameterService;

        public EmailParametersController(IEmailParameterService emailParameterService)
        {
            _emailParameterService = emailParameterService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add(EmailParameter emailParameter)
        {
            var result = await _emailParameterService.Add(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Update(EmailParameter emailParameter)
        {
            var result = await _emailParameterService.Update(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Delete(EmailParameter emailParameter)
        {
            var result = await _emailParameterService.Delete(emailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetList()
        {
            var result = await _emailParameterService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _emailParameterService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}

