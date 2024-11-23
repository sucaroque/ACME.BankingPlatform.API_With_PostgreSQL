namespace ACME.BankingPlatform.API.Shared.Domain.Model.ValueObjects;

public class Notification
{
    private readonly List<Error> _errors = [];
    private const string Separator = "||";

    public void AddError(string message)
    {
        _errors.Add(new Error(message));
    }

    public IReadOnlyList<Error> Errors => _errors.AsReadOnly();

    public string ErrorMessage()
    {
        return string.Join($"{Separator} ", _errors.Select(e => e.Message));
    }

    public bool HasErrors => _errors.Count > 0;

    public record Error(string Message);
}
