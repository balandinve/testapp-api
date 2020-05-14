using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using testapp_api.Models;
using testapp_api.Services;

namespace testapp_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IMapper _mapper;
        private UserService _userService;

        public AccountController( IMapper mapper, UserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post(StoreUserRegisterView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            try
            {
                await _userService.AddAsync(model);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest("Has errors.");
            }
        }

        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token([FromBody]StoreUserRegisterView model)
        {
            try
            {
                var token = await _userService.GetTokenAsync(model.UserName, model.Password);
                if (token == null)
                {
                    return Ok(model);
                    //return BadRequest(new { errorText = "Invalid username or password." });
                }
                return Ok(new { access_token = token });
            }
            catch(Exception ex)
            {
                return BadRequest("Login failed");
            }
        }

    }
}