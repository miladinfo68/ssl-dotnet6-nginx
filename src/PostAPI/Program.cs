using System.Net;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.Listen(IPAddress.Any, 80);
//    options.Listen(IPAddress.Any, 443, listenOptions =>
//    {
//        listenOptions.UseHttps();
//    });
//});

var app = builder.Build();

app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.MapGet("/", () => "Post API Healthcheck Is Ok!");


app.MapGet("/list", () =>
{
    return new List<PostModel> {
        new PostModel(1,"Post 1" ,DateTime.Now.AddDays(-10)),
        new PostModel(2,"Post 2" ,DateTime.Now.AddDays(-9)),
        new PostModel(3,"Post 3" ,DateTime.Now.AddDays(-8)),
        new PostModel(4,"Post 4" ,DateTime.Now.AddDays(-7)),
        new PostModel(5,"Post 5" ,DateTime.Now.AddDays(-2)),
        new PostModel(6,"Post 6" ,DateTime.Now.AddDays(-1)),
        new PostModel(7,"Post 7" ,DateTime.Now.AddDays(-1)),
        new PostModel(8,"Post 8" ,DateTime.Now.AddDays(-1)),
        new PostModel(9,"Post 9" ,DateTime.Now.AddDays(-1)),
        new PostModel(10,"Post 10" ,DateTime.Now.AddDays(-1)),
    };
}).WithName("list");

app.Run();

public record PostModel(int ID, string Name, DateTime CreatedAt);
