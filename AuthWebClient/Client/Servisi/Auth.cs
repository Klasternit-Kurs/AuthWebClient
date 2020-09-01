using AuthWebClient.Shared;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

namespace AuthWebClient.Client.Servisi
{
	public class Auth : IAuth
	{
		HttpClient hc;
		AuthenticationStateProvider asp;
		ILocalStorageService lss;

		public Auth(HttpClient h, AuthenticationStateProvider a, ILocalStorageService s)
		{
			hc = h;
			asp = a;
			lss = s;
		}

		public async Task<LoginRez> Logovanje(Login l)
		{
			var lJson = System.Text.Json.JsonSerializer.Serialize(l);
			var odg = await hc.PostAsync("log", new StringContent(lJson, Encoding.UTF8, "application/json"));

			var rez = System.Text.Json.JsonSerializer.Deserialize<LoginRez>
				(await odg.Content.ReadAsStringAsync(), new JsonSerializerOptions {PropertyNameCaseInsensitive = true });

			if (!odg.IsSuccessStatusCode)
				return rez;

			await lss.SetItemAsync("AToken", rez.Token);
			//TODO Nadji paket((ApiAuthenticationStateProvider)asp)

			hc.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", rez.Token);

			return rez;
		}

		public async Task<RegistracijaRezultat> Registracija(Registracija r)
		{
			var rez = await hc.PostJsonAsync<RegistracijaRezultat>("reg", r);
			return rez;
		}
	}
}
