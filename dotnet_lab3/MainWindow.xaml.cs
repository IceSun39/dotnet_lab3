using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using dotnet_lab3.Classes;

namespace dotnet_lab3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dictionary<Employee, int> employees = new Dictionary<Employee, int>();
        public MainWindow()
        {
            InitializeComponent();
            typeSpecialistComboBox.SelectedIndex = 0; // По стандарту дизайнер
            SetInitialUIState();
        }

        private void SetInitialUIState()
        {
            if (specializationLabel == null || levelComboBox == null || typeSpecialistComboBox == null)
                return;

            int specialist = typeSpecialistComboBox.SelectedIndex;
            if (specialist == 0)
            {
                specializationLabel.Content = "Спеціалізація:";
                levelComboBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                specializationLabel.Content = "Рівень:";
                levelComboBox.Visibility = Visibility.Visible;
            }
        }

        private void typeSpecialistComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetInitialUIState();
        }

        private void updateEmployeeLabel(Employee employee)
        {
            if(employee is Designer designer)
            {
                string specialization = $"Спеціалізація: {designer.Specialization}";
                allEmployeesLabel.Text += $"Позиція: {employee.Title}, Спеціалізація: {specialization}, Досвід: {employee.Experience} років, Погодинна ставка: {employee.HourRate} грн/год, Місячна зарплата: {employee.calculateMonthSalary(160)} грн\n";
            }
            else if (employee is Programmer programmer)
            {
                string level = programmer.Level == ProgrammerLevel.Junior ? "Джун" : "Сеньйор";
                allEmployeesLabel.Text += $"Позиція: {employee.Title}, Рівень: {level}, Досвід: {employee.Experience} років, Погодинна ставка: {employee.HourRate} грн/год, Місячна зарплата: {employee.calculateMonthSalary(160)} грн\n";
            }
        }

        private void updateTotalSalary(Dictionary<Employee, int> employees)
        {
            SalaryCalculator salaryCalculator = new SalaryCalculator();
            totalSalaryLabel.Content = salaryCalculator.totalSalaryForEmployees(employees).ToString("F2") + " грн";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int roleId = typeSpecialistComboBox.SelectedIndex;
            if(roleId == -1)
            {
                MessageBox.Show("Будь ласка, оберіть тип спеціаліста.");
                return;
            }

            string title = titleTextBox.Text;
            if(string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Назва не може бути порожньою.");
                return;
            }

            int experience;
            if (!int.TryParse(experienceTextBox.Text, out experience) || experience <= 0)
            {
                MessageBox.Show("Досвід повинен бути додатнім числом.");
                return;
            }

            double hourRate;
            if (!double.TryParse(hourRateTextBox.Text, out hourRate) || hourRate <= 0)
            {
                MessageBox.Show("Погодинна ставка повинна бути додатнім числом.");
                return;
            }

            int hoursWorked;
            if (!int.TryParse(hoursWorkedTextBox.Text, out hoursWorked) || hoursWorked < 0)
            {
                MessageBox.Show("Кількість відпрацьованих годин повинна бути невід'ємним числом.");
                return;
            }

            Employee employee;
            if (roleId == 0) // Дизайнер і його унікальне поле спеціалізація
            {
                string specialization = specializationTextBox.Text;
                if (string.IsNullOrWhiteSpace(specialization))
                {
                    MessageBox.Show("Спеціалізація не може бути порожньою.");
                    return;
                }
                employee = new Designer(title, experience, hourRate, specialization);
                employees.Add(employee, hoursWorked);
            }
            else // Програміст та його унікальне поле рівень(джун, сеньйор)
            {
                string level = (levelComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (string.IsNullOrWhiteSpace(level))
                {
                    MessageBox.Show("Рівень не може бути порожнім.");
                    return;
                }
                ProgrammerLevel levelEnum = level == "Джун" ? ProgrammerLevel.Junior : ProgrammerLevel.Senior;
                employee = new Programmer(title, experience, hourRate, levelEnum);
                employees.Add(employee, hoursWorked);
            }
            updateEmployeeLabel(employee);
            updateTotalSalary(employees);
        }
    }
}