using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet_lab3.Classes;

namespace DesignerTest
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void createDesignerSuccess()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            string specialization = "UI/UX Design";

            Designer designer = new Designer(title, experience, hourRate, specialization);

            Assert.AreEqual(title, designer.Title);
            Assert.AreEqual(experience, designer.Experience);
            Assert.AreEqual(hourRate, designer.HourRate);
        }
        [TestMethod]
        public void createDesignerFailure()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            string specialization = "UI/UX Design";
            Assert.ThrowsException<ArgumentException>(() => new Designer(title, -1, hourRate, specialization));
            Assert.ThrowsException<ArgumentException>(() => new Designer(title, experience, -1, specialization));
            Assert.ThrowsException<ArgumentException>(() => new Designer(title, experience, hourRate, ""));
        }
        [TestMethod]
        public void calculateMonthSalarySuccess()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            string specialization = "UI/UX Design";
            Designer designer = new Designer(title, experience, hourRate, specialization);
            int hoursWorked = 160; 
            double expectedSalary = hourRate * hoursWorked;
            double actualSalary = designer.calculateMonthSalary(hoursWorked);
            Assert.AreEqual(expectedSalary, actualSalary);
        }
        [TestMethod]
        public void getProjectListSuccess()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            string specialization = "UI/UX Design";
            Designer designer = new Designer(title, experience, hourRate, specialization);
            designer.getProjectList().Add("Project A");
            designer.getProjectList().Add("Project B");
            List<string> expectedProjects = new List<string> { "Project A", "Project B" };
            List<string> actualProjects = designer.getProjectList();
            CollectionAssert.AreEqual(expectedProjects, actualProjects);
        }
        [TestMethod]
        public void calculateProjectCostSuccess()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            string specialization = "UI/UX Design";
            Designer designer = new Designer(title, experience, hourRate, specialization);
            int hours = 100; 
            double expectedCost = hourRate * hours;
            double actualCost = designer.calculateProjectCost(hours);
            Assert.AreEqual(expectedCost, actualCost);
        }
        [TestMethod]
        public void createDesignerWithInvalidSpecialization()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            Assert.ThrowsException<ArgumentException>(() => new Designer(title, experience, hourRate, ""));
        }
        [TestMethod]
        public void setInvalidSpecialization()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            string specialization = "UI/UX Design";
            Designer designer = new Designer(title, experience, hourRate, specialization);
            Assert.ThrowsException<ArgumentException>(() => designer.Specialization = "");
        }
        [TestMethod]
        public void createDesignerWithInvalidExperience()
        {
            string title = "UI/UX Designer";
            int experience = -1; 
            double hourRate = 40.0;
            string specialization = "UI/UX Design";
            Assert.ThrowsException<ArgumentException>(() => new Designer(title, experience, hourRate, specialization));
        }
        [TestMethod]
        public void setInvalidExperience()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            string specialization = "UI/UX Design";
            Designer designer = new Designer(title, experience, hourRate, specialization);
            Assert.ThrowsException<ArgumentException>(() => designer.Experience = -1);
        }
        [TestMethod]
        public void createDesignerWithInvalidHourRate()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = -1.0; 
            string specialization = "UI/UX Design";
            Assert.ThrowsException<ArgumentException>(() => new Designer(title, experience, hourRate, specialization));
        }
        [TestMethod]
        public void setInvalidHourRate()
        {
            string title = "UI/UX Designer";
            int experience = 3;
            double hourRate = 40.0;
            string specialization = "UI/UX Design";
            Designer designer = new Designer(title, experience, hourRate, specialization);
            Assert.ThrowsException<ArgumentException>(() => designer.HourRate = -1.0);
        }
    }
}
