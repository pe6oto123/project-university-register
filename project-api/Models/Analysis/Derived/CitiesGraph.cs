namespace project_api.Models.Analysis.Derived
{
    public class CitiesGraph : FacultyGraph
    {
        public ICollection<CitiesGradeGraph>? CitiesGradeGraph { get; set; }
    }

    public class CitiesGradeGraph
    {
        public string? CityName { get; set; }
        public double? AverageGrade { get; set; }
    }
}
