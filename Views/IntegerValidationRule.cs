using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace TestAppWpfStudents.Views
{
    public class IntegerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!int.TryParse(value.ToString(), out _))
            {
                return new ValidationResult(false, "Введите целое число.");
            }
            return ValidationResult.ValidResult;
        }
    }
}
