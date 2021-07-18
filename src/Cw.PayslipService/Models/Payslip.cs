namespace Cw.PayslipService.Models
{
	public class Payslip
	{
		public Employee Employee { get; set; }
		public double IncomeGross { get; set; }
		public double IncomeTax { get; set; }
		public double IncomeNet { get; set; }
	}
}