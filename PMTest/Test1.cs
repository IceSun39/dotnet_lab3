using dotnet_lab3.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace ProjectManagerTest
{
    [TestClass]
    public sealed class ProjectManagerTest
    {
        [TestMethod]
        public void ProjectManager_Initialization_Success()
        {
            var pm = new ProjectManager("Lead PM", 5, 45.5);

            Assert.AreEqual("Lead PM", pm.Title);
            Assert.AreEqual(5, pm.Experience);
            Assert.AreEqual(45.5, pm.HourRate);
            Assert.IsNotNull(pm.Team);
            Assert.AreEqual(0, pm.Team.Count);
        }

        [TestMethod]
        public void CalculateMonthSalary_Success()
        {
            var pm = new ProjectManager("PM", 3, 50);

            double salary = pm.calculateMonthSalary(160);

            Assert.AreEqual(8000, salary); // 50 * 160
        }

        [TestMethod]
        public void CalculateMonthSalary_NegativeHours_ThrowsException()
        {
            var pm = new ProjectManager("PM", 3, 50);

            Assert.ThrowsException<ArgumentException>(() => pm.calculateMonthSalary(-10));
        }

        [TestMethod]
        public void TeamProperty_AddEmployee_Success()
        {
            var pm = new ProjectManager("PM", 5, 60);
            var designer = new Designer("UI/UX Designer", 2, 40, "Web");
            var programmer = new Programmer("Backend Dev", 3, 55, ProgrammerLevel.Junior);

            pm.Team.Add(designer);
            pm.Team.Add(programmer);

            Assert.AreEqual(2, pm.Team.Count);
            Assert.IsTrue(pm.Team.Contains(designer));
            Assert.IsTrue(pm.Team.Contains(programmer));
        }

        [TestMethod]
        public void SalaryCalculator_WithProjectManager_Success()
        {
            var pm = new ProjectManager("PM", 5, 70);
            var designer = new Designer("Designer", 5, 50, "UI/UX");
            var employeeHours = new Dictionary<Employee, int>
            {
                { pm, 160 },
                { designer, 160 }
            };
            var salaryCalculator = new SalaryCalculator();

            double totalSalary = salaryCalculator.totalSalaryForEmployees(employeeHours);
            double expectedSalary = (pm.HourRate * 160) + (designer.HourRate * 160);

            Assert.AreEqual(expectedSalary, totalSalary);
        }
    }
}