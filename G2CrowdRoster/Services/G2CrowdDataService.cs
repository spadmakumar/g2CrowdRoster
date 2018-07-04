using G2CrowdRoster.HttpClientUtil;
using G2CrowdRoster.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Web;

namespace G2CrowdRoster.Services
{
	public class G2CrowdDataService
	{
		private readonly IHttpClient _httpClient;
		private readonly string _url;
		private readonly MemoryCaching _cache;
		public G2CrowdDataService(MemoryCaching cache, IHttpClient httpClient)
		{
			_httpClient = httpClient;
			_cache = cache;
			_url = "https://api.myjson.com/bins/16roa3";
		}
		public G2RosterModel GetEmployeeData()
		{
			try {
				G2RosterModel parsedResponse = null;
				G2RosterModel cacheResult = null;
				if (_cache._cacheObject.Get("g2Roster") != null)
				{
					cacheResult = (G2RosterModel)_cache._cacheObject.Get("g2Roster");
				}
				var result = _httpClient.GetAsync(new Uri(_url)).Result;
				result.EnsureSuccessStatusCode();
				var response = result.Content.ReadAsStringAsync().Result;
				parsedResponse = new G2RosterModel { G2CrowdRoster = JsonConvert.DeserializeObject<List<G2CrowdPersonalData>>(response) };
				if (cacheResult != null)
				{
					var NotInCacheList = parsedResponse.G2CrowdRoster.Where(x => !cacheResult.G2CrowdRoster.Any(y => y.Name == x.Name)).ToList();
					cacheResult.G2CrowdRoster.AddRange(NotInCacheList);
				}
				else
				{
					cacheResult = parsedResponse;
				}
				_cache._cacheObject.Set("g2Roster", cacheResult, new CacheItemPolicy());
				return cacheResult;
			}
			catch (Exception e) {
				return null;
			}
		}

		public void UpdateVotingInfo(G2CrowdPersonalData personalData,G2RosterModel g2Roster)
		{
			foreach (var data in g2Roster.G2CrowdRoster) {
				if (data.Name == personalData.Name) {
					data.Number_Of_Votes++;
					data.Voted = true;
				}
			}
			_cache._cacheObject.Set("g2Roster", g2Roster,new CacheItemPolicy());
		}
	}
}