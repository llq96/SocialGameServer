namespace SocialGameServer;

public class MessageInfo
{
    public Sender Sender { get; }
    public int Index { get; }
    public string Message { get; }

    public MessageInfo(Sender sender, int index, string message)
    {
        Sender = sender;
        Index = index;
        Message = message;
    }
}