namespace MoneyState.Model.Message;

public static class GetMessage
{
    public static Message SuccessGroupInserted() => new(isSuccessful: true, text: "Success");
    public static Message FailedGroupInserted() => new(false, "Не було вставлено");
}