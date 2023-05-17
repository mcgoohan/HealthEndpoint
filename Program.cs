using mcgoohan.HealthEndpoint;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<MyService>(); 
builder.Services.AddHttpClient();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/health", async context =>
{
     var myService = context.RequestServices.GetRequiredService<MyService>();

     var result = myService.PerformHealthCheck();

        if (result)
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("API is healthy");
        }
        else
        {
            context.Response.StatusCode = 503;
            await context.Response.WriteAsync("API is not healthy");
        }
    });

app.Run();