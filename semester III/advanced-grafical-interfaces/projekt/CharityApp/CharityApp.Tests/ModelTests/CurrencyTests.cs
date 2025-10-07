using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CharityApp.Tests
{
    [TestClass]
    public class CurrencyTests
    {
        [TestMethod]
        public void ConvertToPLN_ShouldReturnCorrectConversion_ForEUR()
        {

            double amountInEUR = 20.0;
            double result = Currency.ConvertToPLN(CurrencyCode.EUR, amountInEUR);
            Assert.AreEqual(20.0 * 4.2360, result, "The conversion from EUR to PLN is incorrect.");
        }

        [TestMethod]
        public void ConvertToPLN_ShouldReturnCorrectConversion_ForUSD()
        {
            double amountInUSD = 10.0;
            double result = Currency.ConvertToPLN(CurrencyCode.USD, amountInUSD);
            Assert.AreEqual(10.0 * 4.0961, result, "The conversion from USD to PLN is incorrect.");
        }

        [TestMethod]
        public void ConvertToPLN_ShouldReturnCorrectConversion_ForGBP()
        {
            double amountInGBP = 5.0;
            double result = Currency.ConvertToPLN(CurrencyCode.GBP, amountInGBP);
            Assert.AreEqual(5.0 * 5.1157, result, "The conversion from GBP to PLN is incorrect.");
        }

        [TestMethod]
        public void ConvertToPLN_ShouldReturnCorrectConversion_ForCZK()
        {
            double amountInCZK = 30.0;
            double result = Currency.ConvertToPLN(CurrencyCode.CZK, amountInCZK);
            Assert.AreEqual(30.0 * 0.1681, result, "The conversion from CZK to PLN is incorrect.");
        }

        [TestMethod]
        public void ConvertToPLN_ShouldReturnCorrectConversion_ForCHF()
        {
            double amountInCHF = 50.0;
            double result = Currency.ConvertToPLN(CurrencyCode.CHF, amountInCHF);
            Assert.AreEqual(50.0 * 4.5039, result, "The conversion from Chf to PLN is incorrect.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertToPLN_ShouldThrowArgumentException_ForUnknownCurrencyCode()
        {
            Currency.ConvertToPLN((CurrencyCode)999, 10.0);
        }

        [TestMethod]
        public void ConversionFactor_ShouldBeCorrect_ForPLN()
        {
            var plnCurrency = Currency.PLN;
            Assert.AreEqual(1.0000, plnCurrency.ConversionFactor, "The conversion factor for PLN is incorrect.");
        }
    }
}