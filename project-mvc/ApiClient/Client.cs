using System.Net.Http.Headers;

namespace project_mvc.ApiClient
{
	public class Client
	{
		public static HttpClient GetClient()
		{
			HttpClient _httpClient = new()
			{
				BaseAddress = new Uri("https://localhost:7011")
			};
			_httpClient.DefaultRequestHeaders.Accept.Clear();
			_httpClient.DefaultRequestHeaders.Accept
				.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			return _httpClient;
		}
	}
}
