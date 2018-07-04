using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace G2CrowdRoster.HttpClientUtil
{
	public interface IHttpClient : IDisposable
	{
		Task<HttpResponseMessage> GetAsync(Uri requestUri);
	}
}
