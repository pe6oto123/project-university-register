namespace project_api.Models.Analysis.Derived
{
	public class StudentsGraph : FacultyGraph
	{
		public int? TotalStudents { get; set; }
		public ICollection<StudentsGenderGraph>? StudentsGenderGraph { get; set; }
	}

	public class StudentsGenderGraph
	{
		public int? StudentsNum { get; set; }
		public string? StudentGender { get; set; }
		public string? Color { get; set; }
		public ICollection<StudentsGradeGraph>? StudentsGradeGraph { get; set; }
	}

	public class StudentsGradeGraph
	{
		public string? GradeName { get; set; }
		public double? GradeNum { get; set; }

	}
}
