﻿using System.ComponentModel.DataAnnotations;

namespace Cw.PayslipService.Models
{
	public class AuthCredentials
	{
		[Required]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }
	}
}