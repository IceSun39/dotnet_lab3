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

        public ProgrammerLevel Level
        {
            get { return level; }
            set { level = value; }
        }

        public Programmer(string title, int experience, double hour_rate, ProgrammerLevel level) : base(title, experience, hour_rate)
        {
            this.level = level;
        }

        public override double calculateMonthSalary(int hoursWorked)
        {
            if (hoursWorked < 0)
            {
                throw new ArgumentException("Hours worked cannot be negative.");
            }
            double salary = HourRate * hoursWorked;
            return salary;
        }

        public override List<string> getProjectList()
        {
            return projects;
        }

        public double calculateBonus(int overtimeHours)
        {
            if (overtimeHours < 0)
            {
                throw new ArgumentException("Overtime hours cannot be negative.");
            }
            return HourRate * overtimeHours * 0.5;
        }
    }
}
