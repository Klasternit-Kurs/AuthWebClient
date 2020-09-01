using AuthWebClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebClient.Client.Servisi
{
	public interface IAuth
	{
		Task<RegistracijaRezultat> Registracija(Registracija r);
		Task<LoginRez> Logovanje(Login l);
	}
}
