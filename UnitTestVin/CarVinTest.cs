using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CarVin;

namespace UnitTestVin
{
    [TestClass]
    public class CarVinTest
    {
        [TestMethod]
        public void TestGetCorrectCodeCoutry()
        {
            CarVinClass vin = new CarVinClass();
            string country = vin.CodeCountry("ZFA22300005556777");
            Assert.AreEqual("Италия", country, "Страна определилась неверно");
        }

        [TestMethod]
        public void TestUncorrectVin9thSymbol()
        {
            CarVinClass vin = new CarVinClass();
            bool result = vin.VinCheck("X1GZZZCAS11520863"); // 9-ый символ - не цифра и не X
            Assert.AreEqual(false, result, "Некорректный Vin-код прошел проверку");
        }

        [TestMethod]
        public void TestBoundaryValue()
        {
            CarVinClass vin = new CarVinClass();
            string result = vin.CodeCountry("X3GZZZCA811520863"); // X3-X0 - Россия
            Assert.AreEqual("Россия", result, "Пограничное значение не прошло проверку");
        }

        [TestMethod]
        public void TestWMIDoesntUse()
        {
            CarVinClass vin = new CarVinClass();
            string result = vin.CodeCountry("ELGZZZCA811520863"); // не используется
            Assert.AreEqual("не используется", result, "Не используемое значение не прошло проверку");
        }

        [TestMethod]
        public void TestVinLenMoreNotEqual17()
        {
            CarVinClass vin = new CarVinClass();
            bool result = vin.VinCheck("ELGZZZCA8115208639"); // длина vin-номера 18 символов
            Assert.AreEqual(false, result, "Некорректная по длине строка прошла проверку");
        }

        [TestMethod]
        public void TestVinUncorrectSymbol()
        {
            CarVinClass vin = new CarVinClass();
            bool result = vin.VinCheck("ELGZZZCA8115$0I63"); // символ $ и I
            Assert.AreEqual(false, result, "Неразрешенный символ был пропущен");
        }

        [TestMethod]
        public void TestVinNull()
        {
            CarVinClass vin = new CarVinClass();
            bool result = vin.VinCheck(null);
            Assert.AreEqual(false, result, "Пустая строка была принята");
        }

        [TestMethod]
        public void TestYearHasWhiteSpace()
        {
            CarVinClass vin = new CarVinClass();
            bool result = vin.VinCheck("ELGZZZCA81 520863");
            Assert.AreEqual(false, result, "Пробел был принят");
        }

        [TestMethod]
        public void TestCarYear()
        {
            CarVinClass vin = new CarVinClass();
            int result = vin.Year("X1GZZZCA811520863");
            Assert.AreEqual(2001, result, "Год авто определился неверно");
        }

    }
}
