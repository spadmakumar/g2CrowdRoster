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
		private readonly HttpClient _httpClient;
		private readonly string _url;
		private readonly MemoryCaching _cache = MemoryCaching.Instance;
		public G2CrowdDataService()
		{
			_httpClient = new HttpClient();
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


		private G2RosterModel MockData() {
			return new G2RosterModel
			{
				G2CrowdRoster = new List<G2CrowdPersonalData>
				{
					new G2CrowdPersonalData {
						Name = "Michael Wheeler",
						Title="CTO / Co-founder",
						Image_Url="https://d2eyrv63e6x6lp.cloudfront.net/wp-content/uploads/2015/02/20141833/Mike-Wheeler.jpg",
						Bio="Mike has been programming since his dad bought him an Apple II when he was 12, and he is as passionate about the craft of programming as he is about building and supporting a quality business. He came to G2Crowd from TapJoy, where he helped to launch a mobile webproduct to an audience of more than 3 million users. Mike also spent four years on the BigMachines team, harnessing the latest web standards to design, build, and launch innovative web applications. When he isn’t applying technological know-how to business problems, Mike can usually be found dancing lindy hop at one of Chicago’s many jazz clubs, or training at a Capoeira academy."
					},
					new G2CrowdPersonalData {
						Name = "Hamed Asghari",
						Title="Senior Software Engineer",
						Image_Url="https://d2eyrv63e6x6lp.cloudfront.net/wp-content/uploads/2015/07/20200159/Asghari2940-high-res.jpg",
						Bio="Hamed has been working as a back-end developer since 2011. His passions are writing optimized, modularized code and learning elegant designpatterns. He spends most of his time working in Ruby, Javascript and Go. When he’s not coding, Hamed can be found watching or playing soccer, organizing his tracksuit collection, and de-seeding pomegranates."
					}
				}
			};

		}
	}
}