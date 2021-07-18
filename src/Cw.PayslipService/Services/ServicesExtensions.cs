using Cw.PayslipService.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Cw.PayslipService.Services
{
	public static class ServicesExtensions
	{
		public static void ConfigureServices(this IServiceCollection services)
		{
			services.AddSingleton<IPayslipRepository, PayslipRepository>();
			services.AddTransient<IAuthenticationManager, AuthenticationManager>();
		}

		public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtSettings = configuration.GetSection("jwtSettings");

			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(jwt =>
			{
				jwt.SaveToken = true;

				jwt.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("secret").Value)),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

			services.AddAuthorization(config =>
			{
				config.AddPolicy("SubmitEmployee", builder => builder.RequireClaim(ClaimTypes.Role, "Admin"));
			});
		}
	}
}