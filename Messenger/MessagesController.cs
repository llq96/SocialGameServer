using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SocialGameServer;

[Route("messages")]
public class MessagesController : Controller
{
    private IMessagesModel _messagesModel;

    public MessagesController(IMessagesModel messagesModel)
    {
        _messagesModel = messagesModel;
    }

    [HttpGet]
    [Route("")]
    public async Task GetMessages()
    {
        await SendJson(_messagesModel.GetAllMessages());
    }

    [HttpGet]
    [Route("before/{id:int}")]
    public async Task GetMessagesBefore(int id)
    {
        await SendJson(_messagesModel.GetMessagesBefore(id));
    }

    [HttpGet]
    [Route("before/{id:int}/{count:int}")]
    public async Task GetMessagesBefore(int id, int count)
    {
        await SendJson(_messagesModel.GetMessagesBefore(id).TakeLast(count).ToList());
    }

    [HttpGet]
    [Route("after/{id:int}")]
    public async Task GetMessagesAfter(int id)
    {
        await SendJson(_messagesModel.GetMessagesAfter(id));
    }

    [HttpGet]
    [Route("after/{id:int}/{count:int}")]
    public async Task GetMessagesAfter(int id, int count)
    {
        await SendJson(_messagesModel.GetMessagesAfter(id).Take(count).ToList());
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task GetMessageByID(int id)
    {
        // await SendJson(_messagesModel.GetMessage(id));
        var messageInfo = _messagesModel.GetMessage(id);
        Debug.WriteLine(messageInfo.Sender.Name);
        await SendJson(messageInfo);
    }

    [HttpGet]
    [Route("information")]
    public async Task GetInformation()
    {
        var lastMessageIndex = _messagesModel.LastMessageIndex;
        var resultObject = new MessagesInformation(lastMessageIndex);
        await SendJson(resultObject);
    }

    [HttpPost]
    [Route("")]
    public async Task AddNewMessage()
    {
        var form = HttpContext.Request.Form;

        _messagesModel.SendNewMessage(form["Sender"], form["Message"]);

        // var lastMessageIndex = _messagesModel.LastMessageIndex;
        // var resultObject = new MessagesInformation(lastMessageIndex);
        // await SendJson(resultObject);
    }

    private async Task SendJson<T>(T obj)
    {
        HttpContext.Response.ContentType = "application/json; charset=utf-8";
        var json = JsonConvert.SerializeObject(obj);
        await HttpContext.Response.WriteAsync(json);
    }
}