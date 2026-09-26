using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_lab3.Classes
{
    public class SalaryCalculator
    {
        public double totalSalaryForEmployees(Dictionary<Employee, int> employeeHours)
        {
            double totalSalary = 0;
            foreach (var entry in employeeHours)
            {
                Employee employee = entry.Key;
                int hoursWorked = entry.Value;
                totalSalary += employee.calculateMonthSalary(hoursWorked);
            }
            return totalSalary;
        }
    }
}
