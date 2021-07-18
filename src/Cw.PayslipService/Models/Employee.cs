using System.ComponentModel.DataAnnotations;

namespace Cw.PayslipService.Models
{
	public class Employee
	{
		[Required]
		public int PayrollNumber { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		[Range(1, double.MaxValue, ErrorMessage = "Annual Salary cannot be 0.")]
		public double Salary { get; set; }
	}
}