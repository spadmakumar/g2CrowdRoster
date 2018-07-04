using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;

namespace G2CrowdRoster.HttpClientUtil
{
	public class BasicHttpClient : IHttpClient
	{
		private readonly HttpClient _internalClient;

		public BasicHttpClient(HttpMessageHandler messageHandler = null)
		{
			if (messageHandler == null)
			{
				_internalClient = new HttpClient();
			}
			else
			{
				_internalClient = new HttpClient(messageHandler);
			}
		}

		public void Dispose()
		{
			_internalClient.Dispose();
		}

		public async Task<HttpResponseMessage> GetAsync(Uri requestUri)
		{
			var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

			return await _internalClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
		}
	}
}