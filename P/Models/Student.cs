using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P.Models
{
    public class Student
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Column("student_name", TypeName = "varchar(100)")]
		[Required]
		public string Name { get; set; }
        [Column("StudentGender", TypeName = "varchar(10)")]
		[Required]
		public string Gender { get; set; }
		[Required]
		public int Age { get; set; }
		[Required]
		public string City { get; set; }


	}
}
