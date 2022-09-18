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

		public DbSet<User> User => Set<User>();
		public DbSet<UserRole> UserRole => Set<UserRole>();
		public DbSet<Faculty> Faculty => Set<Faculty>();
		public DbSet<FacultyNum> FacultyNum => Set<FacultyNum>();
		public DbSet<Course> Course => Set<Course>();
		public DbSet<CourseN> CourseN => Set<CourseN>();
		public DbSet<Schedule> Schedule => Set<Schedule>();
		public DbSet<SchedulesSubjects> SchedulesSubjects => Set<SchedulesSubjects>();
		public DbSet<Subject> Subject => Set<Subject>();
		public DbSet<Student> Student => Set<Student>();
		public DbSet<Gender> Gender => Set<Gender>();
		public DbSet<StudentsSubjects> StudentsSubjects => Set<StudentsSubjects>();
		public DbSet<Grade> Grade => Set<Grade>();
		public DbSet<Teacher> Teacher => Set<Teacher>();
		public DbSet<TeachersSubjects> TeachersSubjects => Set<TeachersSubjects>();
		public DbSet<Address> Address => Set<Address>();
		public DbSet<City> City => Set<City>();

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Teacher>()
				.HasIndex(s => s.Email)
				.IsUnique();

			#region StudentsSubjects
			modelBuilder
				.Entity<StudentsSubjects>()
				.HasKey(table => new
				{
					table.StudentId,
					table.SubjectId
				});

			modelBuilder
				.Entity<StudentsSubjects>()
				.HasOne(s => s.Student)
				.WithMany(s => s.StudentsSubjects)
				.HasForeignKey(s => s.StudentId);

			modelBuilder
				.Entity<StudentsSubjects>()
				.HasOne(s => s.Subject)
				.WithMany(s => s.StudentsSubjects)
				.HasForeignKey(s => s.SubjectId);
			#endregion
			#region SchedulesSubjects
			modelBuilder
			.Entity<SchedulesSubjects>()
			.HasKey(table => new
			{
				table.ScheduleId,
				table.SubjectId
			});

			modelBuilder
				.Entity<SchedulesSubjects>()
				.HasOne(s => s.Schedule)
				.WithMany(s => s.SchedulesSubjects)
				.HasForeignKey(s => s.ScheduleId);

			modelBuilder
				.Entity<SchedulesSubjects>()
				.HasOne(s => s.Subject)
				.WithMany(s => s.SchedulesSubjects)
				.HasForeignKey(s => s.SubjectId);
			#endregion
			#region TeachersSubjects
			modelBuilder
			.Entity<TeachersSubjects>()
			.HasKey(table => new
			{
				table.TeacherId,
				table.SubjectId
			});

			modelBuilder
				.Entity<TeachersSubjects>()
				.HasOne(s => s.Teacher)
				.WithMany(s => s.TeachersSubjects)
				.HasForeignKey(s => s.TeacherId);

			modelBuilder
				.Entity<TeachersSubjects>()
				.HasOne(s => s.Subject)
				.WithMany(s => s.TeachersSubjects)
				.HasForeignKey(s => s.SubjectId);
			#endregion

			modelBuilder
				.Entity<FacultyNum>()
				.HasOne(s => s.Course)
				.WithOne();
		}
	}
}
