using Cw.PayslipService.Models;

namespace Cw.PayslipService.Services
{
	public interface IAuthenticationManager
	{
		bool ValidateCredentails(AuthCredentials credentials);

		string CreateToken();
	}
}