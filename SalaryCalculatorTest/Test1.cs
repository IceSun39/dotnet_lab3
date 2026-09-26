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
            // Arrange
            var designer = new Designer("Designer", 5, 50, "UI/UX");
            var programmer = new Programmer("Programmer", 3, 60, ProgrammerLevel.Junior);
            var employeeHours = new Dictionary<Employee, int>
            {
                { designer, 160 },
                { programmer, 160 }
            };
            var salaryCalculator = new dotnet_lab3.Classes.SalaryCalculator();
            // Act
            double totalSalary = salaryCalculator.totalSalaryForEmployees(employeeHours);
            // Assert
            double expectedSalary = (designer.HourRate * 160) + (programmer.HourRate * 160);
            Assert.AreEqual(expectedSalary, totalSalary);
        }
    }
}
