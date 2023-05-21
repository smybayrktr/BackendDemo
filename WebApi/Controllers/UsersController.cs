using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using Core.Utilities.Results;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

       
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
           var result = _userService.GetList();
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("GetById")]
        public IActionResult GetById(int userId)
        {
            var result = _userService.GetById(userId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPut("Update")]
        public IActionResult Update(User user)
        {
            var result = _userService.Update(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(User user)
        {
            var result = _userService.Delete(user);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var result = _userService.ChangePassword(userChangePasswordDto);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

    }
}

