using Cw.PayslipService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cw.PayslipService.Services
{
	public class AuthenticationManager : IAuthenticationManager
	{
		private string _user;
		private readonly IConfiguration _configuration;

		public AuthenticationManager(IConfiguration configuration)
		{
			this._configuration = configuration;
		}

		public string CreateToken()
		{
			var jwtSettings = _configuration.GetSection("jwtSettings");

			var claims = GetClaims();

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("secret").Value));

			var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(

				issuer: jwtSettings.GetSection("validIssuer").Value,
				audience: jwtSettings.GetSection("validAudience").Value,
				claims: claims,
				expires: DateTime.Now.AddMinutes(60),
				signingCredentials: signingCredentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private List<Claim> GetClaims()
		{
			var claims = new List<Claim> { new Claim(ClaimTypes.Name, _user) };

			switch (_user)
			{
				case "jimmy":
					{
						claims.Add(new Claim(ClaimTypes.Role, "Standard"));
						break;
					}
				case "admin":
					{
						claims.Add(new Claim(ClaimTypes.Role, "Admin"));
						break;
					}
			}
			return claims;
		}

		public bool ValidateCredentails(AuthCredentials credentials)
		{
			_user = credentials.Username.ToLower();

			switch (_user)
			{
				case "jimmy":
					{
						return (credentials.Password.ToLower() == "1234");
					}
				case "admin":
					{
						return (credentials.Password.ToLower() == "5678");
					}
				default: return false;
			}
		}
	}
}