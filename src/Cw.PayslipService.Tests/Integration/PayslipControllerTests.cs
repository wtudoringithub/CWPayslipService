using Cw.PayslipService.Controllers;
using Cw.PayslipService.Models;
using Cw.PayslipService.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Cw.PayslipService.Tests.Integration
{
	public class PayslipControllerTests
	{
		[Fact]
		public async Task SubmitNewEmployee_ShouldReturnOKResult()
		{
			IPayslipRepository repository = new PayslipRepository();
			PayslipController controller = new PayslipController(repository);

			var result = controller.Post(new Employee()
			{
				PayrollNumber = 1,
				FirstName = "Johnny",
				LastName = "Cash",
				Salary = 120000
			});

			result.Should().BeAssignableTo<StatusCodeResult>();
			(result as StatusCodeResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
			repository.Count.Should().Be(1);
		}

		[Fact]
		public async Task GetInvalidEmployee_ShouldReturnBadRequest()
		{
			IPayslipRepository repository = new PayslipRepository();
			PayslipController controller = new PayslipController(repository);

			var result = controller.Get(1);

			result.Should().BeAssignableTo<StatusCodeResult>();
			(result as StatusCodeResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task GetPayslipForValidEmployee_ShouldReturnCorrectPayslip()
		{
			IPayslipRepository repository = new PayslipRepository();
			repository.AddEmployee(new Employee()
			{
				PayrollNumber = 1,
				FirstName = "Johnny",
				LastName = "Cash",
				Salary = 120000
			});

			PayslipController controller = new PayslipController(repository);
			var result = controller.Get(1);

			result.Should().BeAssignableTo<ObjectResult>();
			(result as ObjectResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
			(result as ObjectResult).Value.Should().NotBeNull();
			(result as ObjectResult).Value.Should().BeAssignableTo<Payslip>();
			Payslip payslip = (result as ObjectResult).Value as Payslip;
			payslip.IncomeGross.Should().Be(10000, "Incorrect Income Gross");
			payslip.IncomeTax.Should().Be(3000, "Incorrect Income Tax");
			payslip.IncomeNet.Should().Be(7000, "Incorrect Income Net");
		}

		[Fact]
		public async Task SubmitMultipleEmployees_RepositoryShouldHaveCorrectNumber()
		{
			IPayslipRepository repository = new PayslipRepository();
			PayslipController controller = new PayslipController(repository);
			controller.Post(new Employee()
			{
				PayrollNumber = 1,
				FirstName = "Johnny",
				LastName = "Cash",
				Salary = 120000
			});
			controller.Post(new Employee()
			{
				PayrollNumber = 2,
				FirstName = "Paul",
				LastName = "Giamatti",
				Salary = 220000
			});
			controller.Post(new Employee()
			{
				PayrollNumber = 3,
				FirstName = "Maria",
				LastName = "Casanova",
				Salary = 60000
			});

			repository.Count.Should().Be(3);
		}

		[Fact]
		public async Task SubmitTwiceSamePayrollNumberDifferentDetails_ShouldUpdateEmployeeInRepository()
		{
			IPayslipRepository repository = new PayslipRepository();
			PayslipController controller = new PayslipController(repository);
			controller.Post(new Employee()
			{
				PayrollNumber = 1,
				FirstName = "Johnny",
				LastName = "Cash",
				Salary = 120000
			});
			controller.Post(new Employee()
			{
				PayrollNumber = 1,
				FirstName = "Gino",
				LastName = "Mattarella",
				Salary = 100000
			});

			repository.Count.Should().Be(1);

			var payslip = repository.GetPayslip(1);

			payslip.Should().NotBeNull();
			payslip.Employee.FirstName.Should().Be("Gino");
			payslip.Employee.LastName.Should().Be("Mattarella");
			payslip.Employee.Salary.Should().Be(100000);
		}
	}
}