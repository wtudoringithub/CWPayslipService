using Cw.PayslipService.Models;
using Cw.PayslipService.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw.PayslipService.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthenticationController : Controller
	{
		private readonly IAuthenticationManager _authenticationManager;

		public AuthenticationController(IAuthenticationManager authenticationManager)
		{
			this._authenticationManager = authenticationManager;
		}

		[HttpPost("login")]
		public IActionResult Login([FromBody] AuthCredentials authCredentials)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			return !_authenticationManager.ValidateCredentails(authCredentials)
			? Unauthorized()
			: Ok(new { token = _authenticationManager.CreateToken() });
		}
	}
}