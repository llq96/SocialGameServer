namespace SocialGameServer;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddTransient<IMessagesModel, MockMessagesModel>();
        builder.Services.AddTransient<MessagesRequestHandlers>();

        var app = builder.Build();

        // app.Services.GetService<IMessagesModel>();


        app.Run(RequestHandler);
        app.Run();
    }


    public static class ApplicationPaths
    {
        public const string DiagnosticPath = "/diagnostic";
        public const string MainPath = "/";
        public const string Messages = "/messages";
    }

    public static async Task RequestHandler(HttpContext context)
    {
        switch (context.Request.Path)
        {
            // case ApplicationPaths.DiagnosticPath:
            //     await DiagnosticHandler(context);
            //     break;
            // case ApplicationPaths.MainPath:
            //     await MainPageHandler(context);
            //     break;
            case ApplicationPaths.Messages:
                await context.RequestServices.GetService<MessagesRequestHandlers>()!.MessagesHandler(context);
                break;
            default:
                await ErrorPageHandler(context);
                break;
        }
    }

    public static async Task ErrorPageHandler(HttpContext context)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("Error 404");
    }
}