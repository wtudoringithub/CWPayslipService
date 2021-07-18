using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Cw.Platform.Jwt
{
	public class JwtProvider : IJwtProvider
	{
		private readonly TokenValidationParameters _parameters;
		private readonly Action<SecurityTokenDescriptor> _options;
		private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

		public JwtProvider(TokenValidationParameters parameters, Action<SecurityTokenDescriptor> options)
		{
			_parameters = parameters;
			_options = options;
		}

		// Generate token method
	}
}