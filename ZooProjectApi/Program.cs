using ZooProjectApi;
using ZooProjectApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAnimalService, Animals>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAnimalEndpionts();

app.Run();
