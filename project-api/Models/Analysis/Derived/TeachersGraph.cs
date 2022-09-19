namespace project_api.Models.Analysis.Derived
{
    public class TeachersGraph : FacultyGraph
    {
        public ICollection<TeachersSubjectGraph>? TeachersSubjectGraph { get; set; }
    }

    public class TeachersSubjectGraph
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }

        public ICollection<TeachersGradeGraph>? TeachersGradeGraph { get; set; }
    }

    public class TeachersGradeGraph
    {
        public string? TeacherFullName { get; set; }
        public double? AverageGrade { get; set; }
    }
}
