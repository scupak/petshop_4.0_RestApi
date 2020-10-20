using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Petshop.core.ApplicationServices;
using Petshop.core.ApplicationServices.impl;
using Petshop.core.DomainServices;
using Petshop.Core.Entity;

namespace PetShop_RestAPI.Controllers
{
    [Route("/token")]
    [ApiController]
    public class TokenController : Controller
    {
        private IUserService _service;
        private IAuthenticationHelper authenticationHelper;

        public TokenController(IUserService service, IAuthenticationHelper authService)
        {
            _service = service;
            authenticationHelper = authService;
        }


        [HttpPost]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = _service.GetAll().List.FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!authenticationHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }

    }
}
