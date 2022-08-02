using Microsoft.EntityFrameworkCore;
using project_api.Database.Entities.Access;
using project_api.Database.Entities.Location;
using project_api.Database.Entities.People;
using project_api.Database.Entities.University;

namespace project_api.Database.Contexts
{
	public class DatabaseContext : DbContext
	{
		public DatabaseContext(DbContextOptions options) : base(options)
		{
			// Default constructor
			//ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
			//ChangeTracker.DeleteOrphansTiming = CascadeTiming.OnSaveChanges;
		}

		public DbSet<Account> Account => Set<Account>();
		public DbSet<UserRole> UserRole => Set<UserRole>();
		public DbSet<Faculty> Faculty => Set<Faculty>();
		public DbSet<FacultyNum> FacultyNum => Set<FacultyNum>();
		public DbSet<Course> Course => Set<Course>();
		public DbSet<CourseN> CourseN => Set<CourseN>();
		public DbSet<Subject> Subject => Set<Subject>();
		public DbSet<Student> Student => Set<Student>();
		public DbSet<StudentsSubjects> StudentsSubjects => Set<StudentsSubjects>();
		public DbSet<Grade> Grade => Set<Grade>();
		public DbSet<Teacher> Teacher => Set<Teacher>();
		public DbSet<Address> Address => Set<Address>();
		public DbSet<City> City => Set<City>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			/*modelBuilder
				.Entity<TeachersDepartments>()
				.HasKey(table => new
				{
					table.DepartmentsId,
					table.TeachersId
				});

			modelBuilder
				.Entity<TeachersDepartments>()
				.HasOne(s => s.Teacher)
				.WithMany(s => s.TeachersDepartments)
				.HasForeignKey(s => s.TeachersId);
			//.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<TeachersDepartments>()
				.HasOne(s => s.Department)
				.WithMany(s => s.TeachersDepartments)
				.HasForeignKey(s => s.DepartmentsId);
			//.OnDelete(DeleteBehavior.Cascade);*/

			/*	/////////////////////////////////////////////////

				modelBuilder.Entity<Teacher>()
					.HasOne(s => s.Faculty)
					.WithMany()
					.OnDelete(DeleteBehavior.NoAction);

				modelBuilder.Entity<Student>()
				   .HasOne(s => s.Faculty)
				   .WithMany()
				   .OnDelete(DeleteBehavior.NoAction);

				modelBuilder.Entity<Student>()
				   .HasOne(s => s.Course)
				   .WithMany()
				   .OnDelete(DeleteBehavior.NoAction);

				/////////////////////////////////////////////////*/

			modelBuilder
				.Entity<StudentsSubjects>()
				.HasKey(table => new
				{
					table.StudentsId,
					table.SubjectsId
				});

			modelBuilder
				.Entity<StudentsSubjects>()
				.HasOne(s => s.Student)
				.WithMany(s => s.StudentsSubjects)
				.HasForeignKey(s => s.StudentsId);
			//.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<StudentsSubjects>()
				.HasOne(s => s.Subject)
				.WithMany(s => s.StudentsSubjects)
				.HasForeignKey(s => s.SubjectsId);
			//.OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<FacultyNum>()
				.HasOne(s => s.Course)
				.WithOne();
		}
	}
}
