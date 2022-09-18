namespace project_api.Models
{
	public class TeachersGraph
	{
		public int SubjectId { get; set; }
		public string? SubjectName { get; set; }
		public ICollection<TeachersGradeGraph>? TeachersGradeGraph { get; set; }
	}

	public class TeachersGradeGraph
	{
		public string? TeacherFullName { get; set; }
		public double? AverageGrade { get; set; } = 0;
	}
}
