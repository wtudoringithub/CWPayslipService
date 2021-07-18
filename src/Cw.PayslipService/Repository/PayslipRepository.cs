using Cw.PayslipService.Models;
using Cw.PayslipService.Services;
using System.Collections.Generic;

namespace Cw.PayslipService.Repository
{
	public class PayslipRepository : IPayslipRepository
	{
		private Dictionary<int, Employee> _employees = new Dictionary<int, Employee>();

		public int Count => _employees.Count;

		public void AddEmployee(Employee employee)
		{
			if (!_employees.ContainsKey(employee.PayrollNumber))
			{
				_employees.Add(employee.PayrollNumber, employee);
			}
			else
			{
				_employees[employee.PayrollNumber] = employee;
			}
		}

		public Payslip GetPayslip(int payrollNumber)
		{
			if (!_employees.ContainsKey(payrollNumber))
			{
				return null;
			}
			return new PayslipCalculator(_employees[payrollNumber]).Calculate();
		}
	}
}