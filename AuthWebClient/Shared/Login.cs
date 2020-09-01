using System;
using System.Collections.Generic;
using System.Text;

namespace AuthWebClient.Shared
{
	public class Login
	{
		public string Mejl { get; set; }
		public string Sifra { get; set; }

		public bool Pamti { get; set; }
	}

	public class LoginRez
	{
		public bool Uspesno { get; set; }
		public string Token { get; set; }
	}
}
