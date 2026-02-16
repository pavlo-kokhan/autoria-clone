namespace AutoriaClone.Domain.Providers.Abstract;

public interface ICurrencyProvider
{
    public IReadOnlySet<string> AllCurrencyCodes { get; }
}
