namespace SocialGameServer;

public class MessagesInformation
{
    public int LastMessageIndex { get; }

    public MessagesInformation(int lastMessageIndex)
    {
        LastMessageIndex = lastMessageIndex;
    }
}