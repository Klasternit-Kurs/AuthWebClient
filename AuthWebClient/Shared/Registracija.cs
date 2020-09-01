using System;
using System.Collections.Generic;
using System.Text;

namespace AuthWebClient.Shared
{
	public class Registracija
	{
		public string Mejl { get; set; }

		//Ovo validacijom proveravamo je li isto 
		public string Sifra { get; set; }
		public string OveraSifre { get; set; }
	}

	public class RegistracijaRezultat
	{
		public bool Uspesno { get; set; }
	}
}
