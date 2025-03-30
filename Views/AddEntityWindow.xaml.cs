using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TestAppWpfStudents.Helpers;

namespace TestAppWpfStudents.Views
{
    /// <summary>
    /// Логика взаимодействия для AddEntityWindow.xaml
    /// </summary>
    public partial class AddEntityWindow : Window
    {
        public AddEntityWindow(ObservableObject vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
