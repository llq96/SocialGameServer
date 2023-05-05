using System;
using System.Collections.Generic;
using System.Linq;
using SocialGameServer;

public interface IMessagesModel
{
    public List<MessageInfo> GetLastMessages();

    public event MessageDelegate OnNewMessageRecieved;

    public delegate void MessageDelegate(MessageInfo messageInfo);

    public void SendNewMessage(string message);
    public Sender GetCurrentSender();
}


public class MockMessagesModel : IMessagesModel
{
    private string _randomText =
        "To be, or not to be, that is the question: Whether 'tis nobler in the mind to suffer The slings and arrows of outrageous fortune, Or to take arms against a sea of troubles And by opposing end them. To die—to sleep, No more; and by a sleep to say we end The heart-ache and the thousand natural shocks That flesh is heir to: 'tis a consummation Devoutly to be wish'd. To die, to sleep; To sleep, perchance to dream—ay, there's the rub: For in that sleep of death what dreams may come, When we have shuffled off this mortal coil, Must give us pause—there's the respect That makes calamity of so long life. For who would bear the whips and scorns of time, Th'oppressor's wrong, the proud man's contumely, The pangs of dispriz'd love, the law's delay, The insolence of office, and the spurns That patient merit of th'unworthy takes, When he himself might his quietus make With a bare bodkin? Who would fardels bear, To grunt and sweat under a weary life, But that the dread of something after death, The undiscovere'd country, from whose bourn No traveller returns, puzzles the will, And makes us rather bear those ills we have Than fly to others that we know not of? Thus conscience doth make cowards of us all, And thus the native hue of resolution Is sicklied o'er with the pale cast of thought, And enterprises of great pith and moment With this regard their currents turn awry And lose the name of action.";

    private List<MessageInfo> _messages;
    public event IMessagesModel.MessageDelegate OnNewMessageRecieved;

    private int LastMessageIndex => _messages.Count > 0 ? _messages.Last().Index : -1;

    private int _countRandomMessages = 101;

    private Sender _currentSender;

    public MockMessagesModel()
    {
        _messages = new();
        for (int i = 0; i < _countRandomMessages; i++)
        {
            AddRandomMessage();
        }
    }

    private void AddRandomMessage()
    {
        var message = GenerateRandomMessage(LastMessageIndex + 1);
        PushNewMessage(message);
    }

    private void PushNewMessage(MessageInfo messageInfo)
    {
        _messages.Add(messageInfo);
        OnNewMessageRecieved?.Invoke(_messages.Last());
    }

    private void PushNewMessage(Sender sender, string message)
    {
        PushNewMessage(new MessageInfo(sender, LastMessageIndex + 1, message));
    }

    private MessageInfo GenerateRandomMessage(int index)
    {
        return new MessageInfo(GetRandomSender(), index, GetRandomText());
    }

    private Sender GetRandomSender()
    {
        return new Sender()
        {
            Name = new string(Guid.NewGuid().ToString().Take(8).ToArray())
        };
    }


    public List<MessageInfo> GetLastMessages()
    {
        return _messages;
    }

    private string GetRandomText()
    {
        var random = new Random();
        return _randomText.Substring(random.Next(0, _randomText.Length - 300), random.Next(0, 300));
    }

    public void SendNewMessage(string message)
    {
        PushNewMessage(GetCurrentSender(), message);
    }

    public Sender GetCurrentSender()
    {
        return _currentSender ??= new Sender()
        {
            Name = "CurrentUser"
        };
    }
}