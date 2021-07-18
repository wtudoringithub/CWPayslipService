using Cw.PayslipService.Models;
using Cw.PayslipService.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cw.PayslipService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PayslipController : ControllerBase
	{
		private readonly IPayslipRepository _repository;

		public PayslipController(IPayslipRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("{payrollNumber}")]
		[Authorize]
		public IActionResult Get(int payrollNumber)
		{
			Payslip result = _repository.GetPayslip(payrollNumber);
			if (result == null)
			{
				return BadRequest();
			}
			return Ok(result);
		}

		[HttpPost()]
		[Authorize(Policy = "SubmitEmployee")]
		public IActionResult Post([FromBody] Employee employee)
		{
			_repository.AddEmployee(employee);
			return Ok();
		}
	}
}