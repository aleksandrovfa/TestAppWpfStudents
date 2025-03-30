using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestAppWpfStudents.Helpers;
using TestAppWpfStudents.Interfaces;
using TestAppWpfStudents.Models;

namespace TestAppWpfStudents.ViewModels
{
    public class AddGradeViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        private Student _selectedStudent;
        private Subject _selectedSubject;
        private string _grade;
        private bool _isCompleted;

        public ObservableCollection<Student> Students { get; }
        public ObservableCollection<Subject> Subjects { get; }
        public Student SelectedStudent
        {
            get => _selectedStudent;
            set { _selectedStudent = value; OnPropertyChanged(); }
        }
        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set { _selectedSubject = value; OnPropertyChanged(); }
        }
        public string Grade
        {
            get => _grade;
            set { _grade = value; OnPropertyChanged(); }
        }
        public bool IsCompleted
        {
            get => _isCompleted;
            set { _isCompleted = value; OnPropertyChanged(); }
        }

        public ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(Save, CanSave));

        public AddGradeViewModel(IDataService dataService, int? studentId = null, int? subjectId = null)
        {
            _dataService = dataService;
            Students = new ObservableCollection<Student>(_dataService.GetStudents());
            Subjects = new ObservableCollection<Subject>(_dataService.GetSubjects());
            if (studentId != null)
                SelectedStudent = Students.FirstOrDefault(x => x.ID == studentId);
            if (subjectId != null)
                SelectedSubject = Subjects.FirstOrDefault(x => x.ID == subjectId);
        }

        private void Save()
        {
            if (int.TryParse(Grade, out int value))
            {
                _dataService.AddGrade(SelectedStudent.ID, SelectedSubject.ID, value);
                IsCompleted = true;
            }
        }

        private bool CanSave() => SelectedStudent != null && SelectedSubject != null && int.TryParse(Grade, out _);
      
    }
}
