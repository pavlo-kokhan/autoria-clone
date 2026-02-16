using System.Globalization;
using AutoriaClone.Domain.Providers.Abstract;

namespace AutoriaClone.Domain.Providers;

public class CurrencyProvider : ICurrencyProvider
{
    public CurrencyProvider()
    {
        AllCurrencyCodes = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .Select(c =>
            {
                try
                {
                    var currencySymbol = new RegionInfo(c.Name).ISOCurrencySymbol;

                    return currencySymbol;
                }
                catch (Exception)
                {
                    return null;
                }
            })
            .Where(cs => cs is not null)
            .ToHashSet()!;
    }

    public IReadOnlySet<string> AllCurrencyCodes { get; }
}
