using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_lab3.Classes
{
    public class ProjectManager: Employee
    {
        private List<Employee> team= new();

        public List<Employee> Team
        {
            get { return team; }
            set { team = value; }
        }

        public ProjectManager(string title, int experience, double hour_rate): base(title, experience, hour_rate)
        {
            team = new List<Employee>();
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

    }
}
