using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_lab3.Classes
{
    public class Designer : Employee
    {
        private string specialization;
        
        public string Specialization
        {
            get { return specialization; }
            set { 
                if(value == null || value.Length == 0)
                {
                    throw new ArgumentException("Specialization cannot be null or empty.");
                }
                specialization = value; 
            }
        }

        public Designer(string title, int experience, double hour_rate, string specialization) : base(title, experience, hour_rate)
        {
            if(specialization == null || specialization.Length == 0)
            {
                throw new ArgumentException("Specialization cannot be null or empty.");
            }
            this.specialization = specialization;
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

        public double calculateProjectCosr(int hours)
        {
            return HourRate * hours;
        }
    }
}
