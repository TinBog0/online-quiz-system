using QuizWebbApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
//builder.Services.AddScoped<IQuizService, QuizService>();


builder.Services.AddHttpClient<QuizService>("QuizApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5272");
});

var app = builder.Build();
app.MapGet("/", context =>
{
    context.Response.Redirect("/Login");
    return Task.CompletedTask;
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
