using System;
using System.Collections.Generic;
using System.Linq;
using TestAppWpfStudents.Models;
using System.IO;
using TestAppWpfStudents.Interfaces;
using System.Xml.Serialization;

namespace TestAppWpfStudents.Services
{
    public class XmlDataService : IDataService
    {
        private readonly string xmlPath;
        private SchoolData data;

        public XmlDataService()
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            xmlPath = Path.Combine(exeDirectory, "school.xml");
        }

        public void Initialize()
        {
            if (File.Exists(xmlPath))
            {
                LoadData();
            }
            else
            {
                data = new SchoolData
                {
                    Students = new List<Student>(),
                    Subjects = new List<Subject>(),
                    Grades = new List<Grade>()
                };
                SaveData();
            }
        }

        private void SaveData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SchoolData));
            using (var writer = new StreamWriter(xmlPath))
            {
                serializer.Serialize(writer, data);
            }
        }

        private void LoadData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SchoolData));
            using (var reader = new StreamReader(xmlPath))
            {
                data = (SchoolData)serializer.Deserialize(reader);
            }
        }

        public void AddStudent(string name)
        {
            int newId = data.Students.Any() ? data.Students.Max(s => s.ID) + 1 : 1;
            data.Students.Add(new Student { ID = newId, Name = name });
            SaveData();
        }

        public List<Student> GetStudents() => data.Students;

        public void AddSubject(string name)
        {
            int newId = data.Subjects.Any() ? data.Subjects.Max(s => s.ID) + 1 : 1;
            data.Subjects.Add(new Subject { ID = newId, Name = name });
            SaveData();
        }

        public List<Subject> GetSubjects() => data.Subjects;

        public void AddGrade(int studentId, int subjectId, int value)
        {
            int newId = data.Grades.Any() ? data.Grades.Max(g => g.ID) + 1 : 1;
            data.Grades.Add(new Grade { ID = newId, StudentID = studentId, SubjectID = subjectId, Value = value });
            SaveData();
        }

        public List<Grade> GetGradesForStudent(int studentId) =>
            data.Grades.Where(g => g.StudentID == studentId).ToList();
    }
}
