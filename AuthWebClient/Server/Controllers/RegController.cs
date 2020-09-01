using AuthWebClient.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthWebClient.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RegController : ControllerBase
	{
		UserManager<IdentityUser> um;

		public RegController(UserManager<IdentityUser> u)
		{
			um = u;
		}

		[HttpPost]
		public async Task<IActionResult> Registracija([FromBody]Registracija r)
		{
			var kor = new IdentityUser { UserName = r.Mejl, Email = r.Mejl };

			var rez = await um.CreateAsync(kor, r.Sifra);

			return rez.Succeeded ?
				Ok(new RegistracijaRezultat { Uspesno = true }) : 
			    Ok(new RegistracijaRezultat { Uspesno = false });
		}
	}
}
