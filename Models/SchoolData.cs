using System.Collections.Generic;

namespace TestAppWpfStudents.Models
{
    public class SchoolData
    {
        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
