using AuthWebClient.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace AuthWebClient.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class LogController : ControllerBase
	{
		IConfiguration conf;
		SignInManager<IdentityUser> sim;

		public LogController(IConfiguration c, SignInManager<IdentityUser> s)
		{
			conf = c;
			sim = s;
		}

		public async Task<IActionResult> SajnIn([FromBody]Login l)
		{
			var rez = await sim.PasswordSignInAsync(l.Mejl, l.Sifra, false, false);

			if (!rez.Succeeded)
				return BadRequest(new LoginRez { Uspesno = false });

			var klejm = new[] { new Claim(ClaimTypes.Name, l.Mejl) };
			var kljuc = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWTKey"]));
			var kred = new SigningCredentials(kljuc, SecurityAlgorithms.HmacSha256);
			var isticanje = DateTime.Now.AddDays(int.Parse(conf["JWTTrajanjeDana"]));

			var token = new JwtSecurityToken(
				conf["JWTIssuer"],
				conf["JWTAudience"],
				klejm,
				expires: isticanje,
				signingCredentials: kred
				);

			return Ok(new LoginRez { Uspesno = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
		}
	}
}
