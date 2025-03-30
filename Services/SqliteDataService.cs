using System;
using System.Collections.Generic;
using TestAppWpfStudents.Models;
using Microsoft.Data.Sqlite;
using System.IO;
using TestAppWpfStudents.Interfaces;

namespace TestAppWpfStudents.Services
{
    public class SqliteDataService : IDataService
    {
        private readonly string dbPath;

        public SqliteDataService()
        {
            string exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dbPath = Path.Combine(exeDirectory, "school.db");
        }

        public void Initialize()
        {

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Students (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Subjects (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL
                );
                CREATE TABLE IF NOT EXISTS Grades (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    StudentID INTEGER,
                    SubjectID INTEGER,
                    Value INTEGER,
                    FOREIGN KEY (StudentID) REFERENCES Students(ID),
                    FOREIGN KEY (SubjectID) REFERENCES Subjects(ID)
                );";
                command.ExecuteNonQuery();
            }
        }

        public void AddStudent(string name)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Students (Name) VALUES (@name)";
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
        }

        public List<Student> GetStudents()
        {
            var students = new List<Student>();
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID, Name FROM Students";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student { ID = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }
            return students;
        }

        public void AddSubject(string name)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Subjects (Name) VALUES (@name)";
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
            }
        }

        public List<Subject> GetSubjects()
        {
            var subjects = new List<Subject>();
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID, Name FROM Subjects";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjects.Add(new Subject { ID = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }
            return subjects;
        }

        public void AddGrade(int studentId, int subjectId, int value)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Grades (StudentID, SubjectID, Value) VALUES (@studentId, @subjectId, @value)";
                command.Parameters.AddWithValue("@studentId", studentId);
                command.Parameters.AddWithValue("@subjectId", subjectId);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
            }
        }

        public List<Grade> GetGradesForStudent(int studentId)
        {
            var grades = new List<Grade>();
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT ID, SubjectID, Value FROM Grades WHERE StudentID = @studentId";
                command.Parameters.AddWithValue("@studentId", studentId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        grades.Add(new Grade { ID = reader.GetInt32(0), StudentID = studentId, SubjectID = reader.GetInt32(1), Value = reader.GetInt32(2) });
                    }
                }
            }
            return grades;
        }
    }
}
