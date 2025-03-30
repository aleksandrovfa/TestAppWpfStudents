using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TestAppWpfStudents.Helpers;
using TestAppWpfStudents.Interfaces;

namespace TestAppWpfStudents.ViewModels
{

    public class AddStudentViewModel : ObservableObject
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

        public string Title => "Добавить студента";
        public string LabelText => "Имя студента:";

        public ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(Save, CanSave));
        public AddStudentViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        private void Save()
        {
            if (CanSave())
            {
                _dataService.AddStudent(Name);
                IsCompleted = true;
            }
        }

        private bool CanSave() => !string.IsNullOrWhiteSpace(Name);

      
    }
}
