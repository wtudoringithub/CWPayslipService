using Cw.PayslipService.Models;

namespace Cw.PayslipService.Services
{
	public class PayslipCalculator
	{
		private readonly Employee _employee;
		private const double ThreShold = 150000;
		private const double LowerTaxRate = 0.3;
		private const double HigherTaxRate = 0.4;
		private const int Months = 12;

		public PayslipCalculator(Employee employee)
		{
			this._employee = employee;
		}

		public Payslip Calculate()
		{
			Payslip result = new Payslip();
			result.Employee = _employee;
			result.IncomeGross = _employee.Salary / Months;
			result.IncomeTax = _employee.Salary >= ThreShold ? result.IncomeGross * HigherTaxRate : result.IncomeGross * LowerTaxRate;
			result.IncomeNet = result.IncomeGross - result.IncomeTax;
			return result;
		}
	}
}