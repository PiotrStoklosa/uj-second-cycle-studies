using System;

public class Currency
{
    public static readonly Currency PLN = new Currency(CurrencyCode.PLN, 1.0000);
    public static readonly Currency EUR = new Currency(CurrencyCode.EUR, 4.2360);
    public static readonly Currency GBP = new Currency(CurrencyCode.GBP, 5.1157);
    public static readonly Currency CHF = new Currency(CurrencyCode.CHF, 4.5039);
    public static readonly Currency USD = new Currency(CurrencyCode.USD, 4.0961);
    public static readonly Currency CZK = new Currency(CurrencyCode.CZK, 0.1681);

    public CurrencyCode Code { get; }
    public double ConversionFactor { get; }

    private Currency(CurrencyCode code, double conversionFactor)
    {
        Code = code;
        ConversionFactor = conversionFactor;
    }

    public static double ConvertToPLN(CurrencyCode code, double amount)
    {
        switch (code)
        {
            case CurrencyCode.PLN:
                return amount;
            case CurrencyCode.EUR:
                return amount * EUR.ConversionFactor;
            case CurrencyCode.GBP:
                return amount * GBP.ConversionFactor;
            case CurrencyCode.CHF:
                return amount * CHF.ConversionFactor;
            case CurrencyCode.USD:
                return amount * USD.ConversionFactor;
            case CurrencyCode.CZK:
                return amount * CZK.ConversionFactor;
            default:
                throw new ArgumentException("Unknown currency code.");
        }
    }
}