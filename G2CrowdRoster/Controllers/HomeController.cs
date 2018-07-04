using G2CrowdRoster.Models;
using G2CrowdRoster.Services;
using System.Web.Mvc;

namespace G2CrowdRoster.Controllers
{
	public class HomeController : Controller
	{
		private readonly G2CrowdDataService _g2CrowdDataService;
		private readonly MemoryCaching _cache = MemoryCaching.Instance;
		public HomeController() {
			_g2CrowdDataService = new G2CrowdDataService();
		}
		public ActionResult Index()
		{
			ViewBag.Title = "G2 Crowd Team Roster";
			ViewBag.Message = "G2 Crowd Roster Page";
			var g2crowdRosterData = _g2CrowdDataService.GetEmployeeData();
			return View(g2crowdRosterData);
		}

		//[HttpPost]
		[AllowAnonymous]
		public ActionResult Vote(G2CrowdPersonalData personalData)
		{
			G2RosterModel g2Roster = null;
			Session[personalData.Name] = "Voted";
			if (_cache._cacheObject.Get("g2Roster") != null)
			{
				g2Roster = (G2RosterModel)_cache._cacheObject.Get("g2Roster");
			}
			_g2CrowdDataService.UpdateVotingInfo(personalData, g2Roster);
			return RedirectToAction("Index");
		}
	}
}