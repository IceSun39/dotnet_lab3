using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet_lab3.Classes;

namespace ProgrammerTest
{
    [TestClass]
    public sealed class ProgrammerTest
    {
        [TestMethod]
        public void createProgrammerSuccess()
        {
          
            string title = "Software Engineer";
            int experience = 5;
            double hourRate = 50.0;
            ProgrammerLevel level = ProgrammerLevel.Senior;
   
            Programmer programmer = new Programmer(title, experience, hourRate, level);
            
            Assert.AreEqual(title, programmer.Title);
            Assert.AreEqual(experience, programmer.Experience);
            Assert.AreEqual(hourRate, programmer.HourRate);
        }
        [TestMethod]
        public void calculateMonthSalarySuccess()
        {
            string title = "Software Engineer";
            int experience = 5;
            double hourRate = 50.0;
            ProgrammerLevel level = ProgrammerLevel.Senior;

            Programmer programmer = new Programmer(title, experience, hourRate, level);
            int hoursWorked = 160; 

            double expectedSalary = hourRate * hoursWorked;
            double actualSalary = programmer.calculateMonthSalary(hoursWorked);
            Assert.AreEqual(expectedSalary, actualSalary);
        }
        [TestMethod]
        public void calculateBonusSuccess()
        {
            string title = "Software Engineer";
            int experience = 5;
            double hourRate = 50.0;
            ProgrammerLevel level = ProgrammerLevel.Senior;

            Programmer programmer = new Programmer(title, experience, hourRate, level);
            int overtimeHours = 20;
            
            double expectedBonus = hourRate * overtimeHours * 0.5;
            double actualBonus = programmer.calculateBonus(overtimeHours);
            Assert.AreEqual(expectedBonus, actualBonus);
        }
        [TestMethod]
        public void getProjectListSuccess()
        {
            string title = "Software Engineer";
            int experience = 5;
            double hourRate = 50.0;
            ProgrammerLevel level = ProgrammerLevel.Senior;
            Programmer programmer = new Programmer(title, experience, hourRate, level);
            programmer.getProjectList().Add("Project A");
            programmer.getProjectList().Add("Project B");
            List<string> expectedProjects = new List<string> { "Project A", "Project B" };
            List<string> actualProjects = programmer.getProjectList();
            CollectionAssert.AreEqual(expectedProjects, actualProjects);
        }
        [TestMethod]
        public void createProgrammerWithInvalidExperience()
        {
            string title = "Software Engineer";
            int experience = -1; 
            double hourRate = 50.0;
            ProgrammerLevel level = ProgrammerLevel.Senior;
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Programmer programmer = new Programmer(title, experience, hourRate, level);
            });
        }
        [TestMethod]
        public void createProgrammerWithInvalidHourRate()
        {
            string title = "Software Engineer";
            int experience = 5;
            double hourRate = -10.0; 
            ProgrammerLevel level = ProgrammerLevel.Senior;
            Assert.ThrowsException<ArgumentException>(() =>
            {
                Programmer programmer = new Programmer(title, experience, hourRate, level);
            });
        }
        [TestMethod]
        public void setInvalidExperience()
        {
            string title = "Software Engineer";
            int experience = 5;
            double hourRate = 50.0;
            ProgrammerLevel level = ProgrammerLevel.Senior;
            Programmer programmer = new Programmer(title, experience, hourRate, level);
            Assert.ThrowsException<ArgumentException>(() =>
            {
                programmer.Experience = -1; 
            });
        }
        [TestMethod]
        public void setInvalidHourRate()
        {
            string title = "Software Engineer";
            int experience = 5;
            double hourRate = 50.0;
            ProgrammerLevel level = ProgrammerLevel.Senior;
            Programmer programmer = new Programmer(title, experience, hourRate, level);
            Assert.ThrowsException<ArgumentException>(() =>
            {
                programmer.HourRate = -10.0; 
            });
        }
    }
}
