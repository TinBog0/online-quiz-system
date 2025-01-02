using Microsoft.EntityFrameworkCore;
using OnlineQuizSystemApi.Interfaces.Books;
using OnlineQuizSystemApi.Interfaces.Quizes;
using OnlineQuizSystemApi.Mapping;
using OnlineQuizSystemApi.Models;
using OnlineQuizSystemApi.Repositories.Quizes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OnlineQuizSystemContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineQuizSystemDBConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizService, QuizService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
