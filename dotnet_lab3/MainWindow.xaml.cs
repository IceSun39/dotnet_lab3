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
            if (specializationLabel == null || levelComboBox == null || typeSpecialistComboBox == null || specializationTextBox == null)
                return;

            int specialist = typeSpecialistComboBox.SelectedIndex;
            if (specialist == 0) // Дизайнер
            {
                specializationLabel.Visibility = Visibility.Visible;
                specializationLabel.Content = "Спеціалізація:";
                specializationTextBox.Visibility = Visibility.Visible;
                levelComboBox.Visibility = Visibility.Collapsed;
            }
            else if (specialist == 1) // Програміст
            {
                specializationLabel.Visibility = Visibility.Visible;
                specializationLabel.Content = "Рівень:";
                specializationTextBox.Visibility = Visibility.Collapsed;
                levelComboBox.Visibility = Visibility.Visible;
            }
            else if (specialist == 2) // Проджект Менеджер
            {
                // У менеджера немає специфічних полів, тому ховаємо їх
                specializationLabel.Visibility = Visibility.Hidden;
                specializationTextBox.Visibility = Visibility.Collapsed;
                levelComboBox.Visibility = Visibility.Collapsed;
            }
        }

        private void typeSpecialistComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetInitialUIState();
        }

        private void updateEmployeeLabel(Employee employee)
        {
            // Беремо відпрацьовані години з нашого словника
            int workedHours = employees[employee];

            if (employee is Designer designer)
            {
                string specialization = $"Спеціалізація: {designer.Specialization}";
                allEmployeesLabel.Text += $"Позиція: {employee.Title} (Дизайнер), {specialization}, Досвід: {employee.Experience} років, Ставка: {employee.HourRate} грн/год, ЗП: {employee.calculateMonthSalary(workedHours)} грн\n";
            }
            else if (employee is Programmer programmer)
            {
                string level = programmer.Level == ProgrammerLevel.Junior ? "Джун" : "Сеньйор";
                allEmployeesLabel.Text += $"Позиція: {employee.Title} (Програміст), Рівень: {level}, Досвід: {employee.Experience} років, Ставка: {employee.HourRate} грн/год, ЗП: {employee.calculateMonthSalary(workedHours)} грн\n";
            }
            else if (employee is ProjectManager pm)
            {
                allEmployeesLabel.Text += $"Позиція: {employee.Title} (PM), Досвід: {employee.Experience} років, Ставка: {employee.HourRate} грн/год, ЗП: {employee.calculateMonthSalary(workedHours)} грн\n";
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
            if (roleId == -1)
            {
                MessageBox.Show("Будь ласка, оберіть тип спеціаліста.");
                return;
            }

            string title = titleTextBox.Text;
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Назва посади не може бути порожньою.");
                return;
            }

            int experience;
            if (!int.TryParse(experienceTextBox.Text, out experience) || experience < 0)
            {
                MessageBox.Show("Досвід повинен бути додатнім числом або нулем.");
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

            if (roleId == 0) // Дизайнер
            {
                string specialization = specializationTextBox.Text;
                if (string.IsNullOrWhiteSpace(specialization))
                {
                    MessageBox.Show("Спеціалізація не може бути порожньою.");
                    return;
                }
                employee = new Designer(title, experience, hourRate, specialization);
            }
            else if (roleId == 1) // Програміст
            {
                string level = (levelComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (string.IsNullOrWhiteSpace(level))
                {
                    MessageBox.Show("Рівень не може бути порожнім.");
                    return;
                }
                ProgrammerLevel levelEnum = level == "Джун" ? ProgrammerLevel.Junior : ProgrammerLevel.Senior;
                employee = new Programmer(title, experience, hourRate, levelEnum);
            }
            else // Проджект Менеджер (roleId == 2)
            {
                employee = new ProjectManager(title, experience, hourRate);
            }

            // Додаємо в загальний словник
            employees.Add(employee, hoursWorked);

            // Оновлюємо інтерфейс
            updateEmployeeLabel(employee);
            updateTotalSalary(employees);

            if (employee is ProjectManager)
            {
                pmComboBox.Items.Add(employee);
            }
            else
            {
                employeeComboBox.Items.Add(employee);
            }
        }

        private void AddToTeamButton_Click(object sender, RoutedEventArgs e)
        {
            // Перевіряємо, чи обрані об'єкти в обох списках
            if (pmComboBox.SelectedItem is ProjectManager pm && employeeComboBox.SelectedItem is Employee emp)
            {
                // Перевіряємо, чи немає цього працівника вже в команді
                if (!pm.Team.Contains(emp))
                {
                    pm.Team.Add(emp);
                    MessageBox.Show($"Працівника '{emp.Title}' успішно додано до команди менеджера '{pm.Title}'.\n" +
                                    $"Тепер у команді {pm.Team.Count} людей.");
                }
                else
                {
                    MessageBox.Show("Цей працівник вже знаходиться в команді цього PM-а.");
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть Проджект Менеджера та працівника зі списків.");
            }
        }

        private void ShowTeamButton_Click(object sender, RoutedEventArgs e)
        {
            // Перевіряємо, чи обрано PM-а у випадаючому списку
            if (pmComboBox.SelectedItem is ProjectManager pm)
            {
                // Якщо команда порожня
                if (pm.Team == null || pm.Team.Count == 0)
                {
                    MessageBox.Show($"Команда менеджера '{pm.Title}' наразі порожня.", "Інформація про команду");
                    return;
                }

                // Формуємо список учасників
                string teamList = $"Команда менеджера '{pm.Title}':\n\n";

                foreach (Employee emp in pm.Team)
                {
                    // Додаємо тип спеціаліста для більшої інформативності
                    string role = emp is Designer ? "Дизайнер" : "Програміст";
                    teamList += $"- {emp.Title} ({role}), Досвід: {emp.Experience} років\n";
                }

                // Виводимо список на екран
                MessageBox.Show(teamList, "Склад команди");
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть Проджект Менеджера зі списку, щоб переглянути його команду.");
            }
        }
    }


}