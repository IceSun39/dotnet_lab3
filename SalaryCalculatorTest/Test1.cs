using Microsoft.VisualStudio.TestTools.UnitTesting;
using dotnet_lab3.Classes;

namespace SalaryCalculatorTest
{
    [TestClass]
    public sealed class SalaryCalculatorTest
    {
        [TestMethod]
        public void calculateSucces()
        {

            var designer = new Designer("Designer", 5, 50, "UI/UX");
            var programmer = new Programmer("Programmer", 3, 60, ProgrammerLevel.Junior);
            var employeeHours = new Dictionary<Employee, int>
            {
                { designer, 160 },
                { programmer, 160 }
            };
            var salaryCalculator = new SalaryCalculator();

            double totalSalary = salaryCalculator.totalSalaryForEmployees(employeeHours);

            double expectedSalary = (designer.HourRate * 160) + (programmer.HourRate * 160);
            Assert.AreEqual(expectedSalary, totalSalary);
        }
        [TestMethod]
        public void calculateEmptyDictionary()
        {

            var employeeHours = new Dictionary<Employee, int>();
            var salaryCalculator = new SalaryCalculator();

            double totalSalary = salaryCalculator.totalSalaryForEmployees(employeeHours);

            Assert.AreEqual(0, totalSalary);
        }
        [TestMethod]
        public void calculateNegativeHours()
        {
            var designer = new Designer("Designer", 5, 50, "UI/UX");
            var employeeHours = new Dictionary<Employee, int>
            {
                { designer, -160 }
            };
            var salaryCalculator = new SalaryCalculator();
            Assert.ThrowsException<ArgumentException>(() => salaryCalculator.totalSalaryForEmployees(employeeHours));
        }
    }
}
