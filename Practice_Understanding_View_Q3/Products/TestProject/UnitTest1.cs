using NUnit.Framework;


namespace Employees.Tests
{
    [TestFixture]
    public class EmployeeTest
    {
        [Test]
        public void Test_Employee_Create_File_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/Employees/Views/Employee/", "Create.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }
    }

}