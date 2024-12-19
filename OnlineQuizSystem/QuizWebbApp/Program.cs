using QuizWebbApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register QuizService and its HttpClient in one go
builder.Services.AddHttpClient<QuizService>("QuizApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5272");
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
