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

app.MapGet("/", () => "Author API Healthcheck Is Ok!");


app.MapGet("/list", () =>
{
    return new List<AuthorModel> {
        new AuthorModel(1,"Milad Jalali" ,1),
        new AuthorModel(2,"Milad Jalali" ,2),
        new AuthorModel(3,"Milad Jalali" ,3),
        new AuthorModel(4,"Milad Jalali" ,4),
        new AuthorModel(5,"Mahdi Jalali" ,5),
        new AuthorModel(6,"Mahdi Jalali" ,6),
        new AuthorModel(7,"Kamran Jalali" ,7),
        new AuthorModel(8,"Kamran Jalali" ,8),
    };
}).WithName("list");

app.Run();

public record AuthorModel(int ID, string Name, int PostID);
