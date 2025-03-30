using System.Collections.Generic;
using System.Linq;
using TestAppWpfStudents.Models;

namespace TestAppWpfStudents.ViewModels
{
    public class StudentViewModel
    {
        public int ID { get; }
        public string Name { get; }
        public string Grades { get; }
        public double AverageGrade { get; }
        public StudentViewModel(Student student, List<int> grades)
        {
            ID = student.ID;
            Name = student.Name;
            Grades = grades.Any() ? string.Join(", ", grades) : "Нет оценок";
            AverageGrade = grades.Any() ? grades.Average() : 0;
        }
    }
}
