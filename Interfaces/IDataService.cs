using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppWpfStudents.Models;

namespace TestAppWpfStudents.Interfaces
{
    public interface IDataService
    {
        void Initialize();
        void AddStudent(string name);
        List<Student> GetStudents();
        void AddSubject(string name);
        List<Subject> GetSubjects();
        void AddGrade(int studentId, int subjectId, int value);
        List<Grade> GetGradesForStudent(int studentId);
    }
}
