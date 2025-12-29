using Microsoft.EntityFrameworkCore;

namespace P.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
        {
        }
        public DbSet<Student> StudentsInformation { get; set; }
	}
}
