namespace MoneyState.Model.Message;

public struct Message
{
    public Message()
    {
    }

    public Message(bool isSuccessful, string text)
    {
        IsSuccessful = isSuccessful;
        Text = text;
    }

    public string Text { get; init; } = "";
    public bool IsSuccessful { get; init; } = true;
}