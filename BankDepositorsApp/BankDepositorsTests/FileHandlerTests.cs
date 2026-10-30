using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDepositorsApp;

namespace BankDepositorsTests
{
    [TestClass]
    public class FileHandlerTests
    {
        private string _testFilePath;
        private const string Iban1 = "UA111111111111111111111111111";
        private const string Iban2 = "UA222222222222222222222222222";

        [TestInitialize]
        public void TestInitialize()
        {
            _testFilePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".json");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [TestMethod]
        public async Task SaveAsync_ShouldWriteDataToFile()
        {
            var list = new List<BankDepositor>
            {
                new BankDepositor("Петренко П.П.", Iban1, 5000m, 2022),
                new BankDepositor("Сидоренко С.С.", Iban2, 15000m, 2024)
            };

            var saver = new JsonFileSaver();
            await saver.SaveAsync(_testFilePath, list);

            Assert.IsTrue(File.Exists(_testFilePath), "Файл не було створено.");
            string text = await File.ReadAllTextAsync(_testFilePath);
            Assert.IsFalse(string.IsNullOrWhiteSpace(text), "Файл порожній.");

            var reader = new JsonFileReader();
            var loaded = await reader.LoadAsync<BankDepositor>(_testFilePath);

            Assert.IsNotNull(loaded);
            Assert.AreEqual(2, loaded.Count);
        }

        [TestMethod]
        public async Task LoadAsync_WhenFileDoesNotExist_ReturnsNull()
        {
            var reader = new JsonFileReader();
            var loaded = await reader.LoadAsync<BankDepositor>(_testFilePath);

            Assert.IsNull(loaded, "Повинно повертати null, якщо файл відсутній.");
        }
    }
}