using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace SocialGameServer;

public class MessagesRequestHandlers
{
    private IMessagesModel _messagesModel;

    public MessagesRequestHandlers(IMessagesModel messagesModel)
    {
        _messagesModel = messagesModel;
    }

    public async Task MessagesHandler(HttpContext context)
    {
        if (context.Request.Method == HttpMethod.Get.ToString())
        {
            // await context.Response.WriteAsync("This is GET request\n");

            context.Response.ContentType = "application/json; charset=utf-8";


            List<MessageInfo> lastMessages = _messagesModel.GetLastMessages();
            // await context.Response.WriteAsync($"{lastMessages == null}\n");
            // await context.Response.WriteAsync($"{lastMessages.Count}\n");

            var json = JsonSerializer.Serialize(lastMessages);
            await context.Response.WriteAsync(json);
        }
    }
}


// public async Task DiagnosticHandler(HttpContext context)
// {
//     var debugLog = GetDiagnosticLog(context);
//
//     // context.Response.ContentType = "text/html; charset=utf-8";
//     await context.Response.WriteAsync(debugLog);
//
//     Debug.WriteLine(debugLog);
//
//
//     string GetDiagnosticLog(HttpContext httpContext)
//     {
//         var headers = httpContext.Request.Headers;
//
//         List<string> debugLines = new();
//         debugLines.Add($"Count headers = {headers.Count}");
//         debugLines = headers.Select(x => $"Key:   [{x.Key}]   Value:   [{x.Value}]").ToList();
//         var debugLog = string.Join("\n", debugLines);
//         return debugLog;
//     }
// }
//
// public async Task MainPageHandler(HttpContext context)
// {
//     var query = context.Request.Query;
//     var list = query.Select(x => $"Key:   [{x.Key}]   Value:   [{string.Join(" ", x.Value)}]").ToList();
//
//     await context.Response.WriteAsync("MainPage\n");
//     await context.Response.WriteAsync(string.Join("\n", list));
//     // context.Response.Headers.ContentDisposition = "attachment; filename=priroda.jpg";
//     // context.Response.Headers.Add("Test1","TestValue");
//     // await context.Response.SendFileAsync(@"C:\Users\bovin\Desktop\priroda.jpg");
// }