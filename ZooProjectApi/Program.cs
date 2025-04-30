using ZooProjectApi;
using ZooProjectApi.Middleware;
using ZooProjectApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAnimalService, AnimalService>();
builder.Services.AddDbContext<AnimalDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAnimalEndpoints();

app.Run();
