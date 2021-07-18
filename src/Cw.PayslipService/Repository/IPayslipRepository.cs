using Cw.PayslipService.Models;

namespace Cw.PayslipService.Repository
{
	public interface IPayslipRepository
	{
		void AddEmployee(Employee employee);

		Payslip GetPayslip(int payrollNumber);

		int Count { get; }
	}
}