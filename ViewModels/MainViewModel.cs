using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using TestAppWpfStudents.Helpers;
using TestAppWpfStudents.Interfaces;
using TestAppWpfStudents.Models;
using TestAppWpfStudents.Views;

namespace TestAppWpfStudents.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        private Subject _selectedSubject;
        private StudentViewModel _selectedStudent;

        public ObservableCollection<StudentViewModel> Students { get; } = new ObservableCollection<StudentViewModel>();
        public ObservableCollection<Subject> Subjects { get; } = new ObservableCollection<Subject>();
        public Subject SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                _selectedSubject = value;
                OnPropertyChanged();
                LoadStudents();
            }
        }
        public StudentViewModel SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ICommand _addStudentCommand;
        public ICommand AddStudentCommand => _addStudentCommand ?? (_addStudentCommand = new RelayCommand(AddStudent));

        private ICommand _addSubjectCommand;
        public ICommand AddSubjectCommand => _addSubjectCommand ?? (_addSubjectCommand = new RelayCommand(AddSubject));

        private ICommand _addGradeCommand;
        public ICommand AddGradeCommand => _addGradeCommand ?? (_addGradeCommand = new RelayCommand(AddGrade));

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
            _dataService.Initialize();
            LoadSubjects();
            LoadStudents();
        }

        private void LoadSubjects()
        {
            var existingIds = Subjects.Select(s => s.ID).ToList();
            _dataService
                .GetSubjects().Where(s => !existingIds.Contains(s.ID))
                .ToList()
                .ForEach(s => Subjects.Add(s));
        }

        private void LoadStudents()
        {
            Students.Clear();
            if (SelectedSubject == null)
                return;
            var students = _dataService.GetStudents();
            foreach (var student in students)
            {
                var grades = _dataService.GetGradesForStudent(student.ID)
                    .Where(x => x.SubjectID == SelectedSubject.ID)
                    .Select(x => x.Value)
                    .ToList();
                Students.Add(new StudentViewModel(student, grades));
            }
        }

        private void AddStudent()
        {
            var vm = new AddStudentViewModel(_dataService);
            var window = new AddEntityWindow(vm);
            window.ShowDialog();
            if (vm.IsCompleted)
            {
                LoadStudents();
            }
        }

        private void AddSubject()
        {
            var vm = new AddSubjectViewModel(_dataService);
            var window = new AddEntityWindow(vm);
            window.ShowDialog();
            if (vm.IsCompleted)
            {
                LoadSubjects();
            }
        }

        private void AddGrade()
        {
            var vm = new AddGradeViewModel(_dataService, SelectedStudent?.ID, SelectedSubject?.ID);
            var window = new AddGradeWindow(vm);
            window.ShowDialog();
            if (vm.IsCompleted)
            {
                LoadStudents();
            }
        }
    }
}
