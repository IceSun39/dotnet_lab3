using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_lab3.Classes
{
    public abstract class Employee
    {
        private string title;
        private int experience;
        private double hour_rate;
        protected List<string> projects = new();
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public int Experience
        {
            get { return experience; }
            set { 
                if(value <= 0)
                {
                    throw new ArgumentException("Experience must be greater than 0.");
                }
                experience = value;
            }
        }

        public double HourRate
        {
            get { return hour_rate; }
            set { 
                if(value <= 0)
                {
                    throw new ArgumentException("Hour rate must be greater than 0.");
                }
                hour_rate = value;
            }
        }

        public Employee(string title, int experience, double hour_rate)
        {
            this.title = title;
            this.experience = experience;
            this.hour_rate = hour_rate;
        }

        public abstract double calculateMonthSalary(int hoursWorked);
        public abstract List<string> getProjectList();
    }
}
