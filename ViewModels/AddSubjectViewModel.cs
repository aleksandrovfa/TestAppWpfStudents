using System.Windows.Input;
using TestAppWpfStudents.Helpers;
using TestAppWpfStudents.Interfaces;

namespace TestAppWpfStudents.ViewModels
{
    public class AddSubjectViewModel : ObservableObject
    {
        private readonly IDataService _dataService;
        private string _name;
        private bool _isCompleted;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set { _isCompleted = value; OnPropertyChanged(); }
        }

        public string Title => "Добавить предмет";
        public string LabelText => "Название предмета:";

        public ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(Save, CanSave));

        public AddSubjectViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        private void Save()
        {
            if (CanSave())
            {
                _dataService.AddSubject(Name);
                IsCompleted = true;
            }
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(Name);
    }
}
