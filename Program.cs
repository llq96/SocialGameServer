namespace SocialGameServer;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<IMessagesModel, MockMessagesModel>();
        // builder.Services.AddTransient<MessagesController>();

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllerRoute("messages", "{controller=Messages}"); });
        app.Run();
    }
}