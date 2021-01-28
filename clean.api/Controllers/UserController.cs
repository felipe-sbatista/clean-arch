using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using clean.api.Controllers.Base;
using clean.application.Interfaces;
using clean.application.Models;
using Microsoft.AspNetCore.Mvc;

namespace clean.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseHandlerController
    {
        public UserController()
        {
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<UserDto>>> Get([FromServices] IUserAppService appService)
        {
            if (!ModelState.IsValid)
            {
                HandleModelStateErrors();
                return ResponseApi();
            }
            return await ResponseApiAsync(async () =>
            {
                return await appService.ListAllAsync();
            });

        }
    }
}
