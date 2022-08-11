using System.Net.Http.Headers;

namespace project_mvc.ApiClient
{
	public class Client
	{
		public static readonly string _routeCities = "api/Cities";
		public static readonly string _routeCourseN = "api/CourseN";
		public static readonly string _routeCourses = "api/Courses";
		public static readonly string _routeSchedules = "api/Schedules";
		public static readonly string _routeFaculties = "api/Faculties";
		public static readonly string _routeSubjects = "api/Subjects";
		public static readonly string _routeTeachers = "api/Teachers";
		public static readonly string _routeStudents = "api/Students";

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
