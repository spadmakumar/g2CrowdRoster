using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Linq;
using System.Web;

namespace G2CrowdRoster.Services
{
	public sealed class MemoryCaching
	{
		public MemoryCache _cacheObject = MemoryCache.Default;
		static MemoryCaching _instance;
		private static object objectlock = new object();

		//Singleton
		public static MemoryCaching Instance
		{
			get
			{
				lock (objectlock) //single - check lock
				{
					if (_instance == null)
					{
						_instance = new MemoryCaching();
					}

					return _instance;
				}
			}

		}
	}
}