using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using G2CrowdRoster.Models;
using System.Collections.Generic;
using G2CrowdRoster.Services;
using System.Runtime.Caching;
using NSubstitute;
using G2CrowdRoster.HttpClientUtil;
using System.Net.Http;
using System.Net;
using System.Configuration;
using System.Net.Http.Formatting;

namespace G2Crowd.Tests
{
	[TestClass]
	public class G2CrowdDataServiceTests
	{
		[TestMethod]
		public void GetEmployee_Should_Return_Cached_Data_And_New_Additions()
		{
			var _cacheInstance = MemoryCaching.Instance;
			BasicHttpClient _httpClient = new BasicHttpClient(CreateFakeResponseHandler());
			_cacheInstance._cacheObject.Set("g2Roster", Cache_MockData(), new CacheItemPolicy());
			var _g2CrowdDataService = new G2CrowdDataService(_cacheInstance,_httpClient);
			var response = _g2CrowdDataService.GetEmployeeData();
			Assert.AreEqual(response.G2CrowdRoster.Count,3);

		}

		[TestMethod]
		public void GetEmployee_Should_Return_Cached_Data_With_Right_NumberOfVotes()
		{
			var _cacheInstance = MemoryCaching.Instance;
			BasicHttpClient _httpClient = new BasicHttpClient(CreateFakeResponseHandler());
			_cacheInstance._cacheObject.Set("g2Roster", Cache_MockData(), new CacheItemPolicy());
			var _g2CrowdDataService = new G2CrowdDataService(_cacheInstance, _httpClient);
			var response = _g2CrowdDataService.GetEmployeeData();
			Assert.AreEqual(response.G2CrowdRoster[0].Number_Of_Votes, 5);
			Assert.AreEqual(response.G2CrowdRoster[1].Number_Of_Votes, 4);
		}

		[TestMethod]
		public void GetEmployee_Should_Return_Cached_Data_With_Updated_Votes()
		{
			var _cacheInstance = MemoryCaching.Instance;
			BasicHttpClient _httpClient = new BasicHttpClient(CreateFakeResponseHandler());
			_cacheInstance._cacheObject.Set("g2Roster", Cache_MockData(), new CacheItemPolicy());
			var _g2CrowdDataService = new G2CrowdDataService(_cacheInstance, _httpClient);
			var personalData = new G2CrowdPersonalData {
				Name = "Michael Wheeler",
				Title = "CTO / Co-founder",
				Image_Url = "https://d2eyrv63e6x6lp.cloudfront.net/wp-content/uploads/2015/02/20141833/Mike-Wheeler.jpg",
				Bio = "Mike has been programming since his dad bought him an Apple II when he was 12, and he is as passionate about the craft of programming as he is about building and supporting a quality business. He came to G2Crowd from TapJoy, where he helped to launch a mobile webproduct to an audience of more than 3 million users. Mike also spent four years on the BigMachines team, harnessing the latest web standards to design, build, and launch innovative web applications. When he isn’t applying technological know-how to business problems, Mike can usually be found dancing lindy hop at one of Chicago’s many jazz clubs, or training at a Capoeira academy.",
				Number_Of_Votes = 5
			};
			_g2CrowdDataService.UpdateVotingInfo(personalData, Update_Info_Data());
			var result = (G2RosterModel)_cacheInstance._cacheObject.Get("g2Roster");
			Assert.AreEqual(result.G2CrowdRoster[0].Number_Of_Votes, 6);
		}

		private HttpMessageHandler CreateFakeResponseHandler()
		{
			var response = new HttpResponseMessage(HttpStatusCode.Accepted);
			List<G2CrowdPersonalData> mock_Data = new List<G2CrowdPersonalData>() {new G2CrowdPersonalData {
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
					},
						new G2CrowdPersonalData {
						Name = "LoreleiPang",
						Title="Junior Software Engineer",
						Image_Url="https://d2eyrv63e6x6lp.cloudfront.net/wp-content/uploads/2015/11/24111646/Dan-P.jpg",
						Bio="Lorelei studied Computer Science at the University of Illinois in Urbana-Champaign, where her passion for dynamic problem-solving led her to seek a startup environment, and her love of awesome things led her to G2 Crowd. When not in front of a computer, she can be found playing card and board games, making music, or moving towards a computer to do those same things electronically."
					}};
			response.Content = new ObjectContent<List<G2CrowdPersonalData>>(mock_Data, new JsonMediaTypeFormatter(), "application/json");
			var fakeResponseHandler = new FakeResponseHandler();
			fakeResponseHandler.AddFakeResponse(new Uri("https://api.myjson.com/bins/16roa3"), response);
			return fakeResponseHandler;
		}

		private G2RosterModel Cache_MockData()
		{
			return new G2RosterModel
			{
				G2CrowdRoster = Test_Data()
			};

		}

		private G2RosterModel Update_Info_Data()
		{
			return new G2RosterModel
			{
				G2CrowdRoster = Test_Data()
			};

		}

		private List<G2CrowdPersonalData> Test_Data() {
			return new List<G2CrowdPersonalData>
				{
					new G2CrowdPersonalData {
						Name = "Michael Wheeler",
						Title="CTO / Co-founder",
						Image_Url="https://d2eyrv63e6x6lp.cloudfront.net/wp-content/uploads/2015/02/20141833/Mike-Wheeler.jpg",
						Bio="Mike has been programming since his dad bought him an Apple II when he was 12, and he is as passionate about the craft of programming as he is about building and supporting a quality business. He came to G2Crowd from TapJoy, where he helped to launch a mobile webproduct to an audience of more than 3 million users. Mike also spent four years on the BigMachines team, harnessing the latest web standards to design, build, and launch innovative web applications. When he isn’t applying technological know-how to business problems, Mike can usually be found dancing lindy hop at one of Chicago’s many jazz clubs, or training at a Capoeira academy.",
						Number_Of_Votes =5
					},
					new G2CrowdPersonalData {
						Name = "Hamed Asghari",
						Title="Senior Software Engineer",
						Image_Url="https://d2eyrv63e6x6lp.cloudfront.net/wp-content/uploads/2015/07/20200159/Asghari2940-high-res.jpg",
						Bio="Hamed has been working as a back-end developer since 2011. His passions are writing optimized, modularized code and learning elegant designpatterns. He spends most of his time working in Ruby, Javascript and Go. When he’s not coding, Hamed can be found watching or playing soccer, organizing his tracksuit collection, and de-seeding pomegranates.",
						Number_Of_Votes =4
					}
				};
		}
	}
}
