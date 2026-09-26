using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_lab3.Classes
{

    public enum ProgrammerLevel
    {
        Junior,
        Senior
    }

    public class Programmer : Employee
    {
        ProgrammerLevel level;

        public Programmer(string title, int experience, double hour_rate, ProgrammerLevel level) : base(title, experience, hour_rate)
        {
            this.level = level;
        }

        public override double calculateMonthSalary(int hoursWorked)
        {
            double salary = HourRate * hoursWorked;
            return salary;
        }

        public override List<string> getProjectList()
        {
            return projects;
        }

        public double calculateBonus(int overtimeHours)
        {
            return HourRate * overtimeHours * 0.5;
        }
    }
}
