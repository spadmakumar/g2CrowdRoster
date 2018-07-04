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
		static readonly MemoryCaching _instance = new MemoryCaching();

		//Singleton
		public static MemoryCaching Instance
		{
			get
			{
				return _instance;
			}
		}
	}
}