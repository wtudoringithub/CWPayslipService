using Cw.PayslipService.Models;
using Cw.PayslipService.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Cw.PayslipService.Tests.Unit
{
	public class PayslipCalculatorTests
	{
		[Theory]
		[InlineData(120000, 10000, 3000, 7000)]
		[InlineData(150000, 12500, 5000, 7500)]
		[InlineData(240000, 20000, 8000, 12000)]
		public void CalculateRangeOfSalaries_ShouldProduceCorrectPayslip(double salary, double expectedGross, double expectedTax, double expectedNet)
		{
			Employee employee = new Employee() { FirstName = "John", LastName = "Cross", PayrollNumber = 1, Salary = salary };
			PayslipCalculator calculator = new PayslipCalculator(employee);
			Payslip result = calculator.Calculate();
			result.IncomeGross.Should().Be(expectedGross, "Incorrect Income Gross");
			result.IncomeTax.Should().Be(expectedTax, "Incorrect Income Tax");
			result.IncomeNet.Should().Be(expectedNet, "Incorrect Income Net");
		}
	}
}