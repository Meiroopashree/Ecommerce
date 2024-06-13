using NUnit.Framework;


namespace Products.Tests
{
    [TestFixture]
    public class ProductTest
    {
        [Test]
        public void Test_Product_Create_File_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/Products/Views/Product/", "Create.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }
    }

}