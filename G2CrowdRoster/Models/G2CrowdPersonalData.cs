using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace G2CrowdRoster.Models
{
	public class G2CrowdPersonalData
	{
		public string Name { get; set; }
		public string Image_Url { get; set; }
		public string Title { get; set; }
		public string Bio { get; set; }
		public int Number_Of_Votes { get; set; }
	}
}