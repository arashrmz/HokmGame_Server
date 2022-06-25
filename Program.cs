using HokmGame_Server.Data;
using HokmGame_Server.Hubs;
using HokmGame_Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddCors(options => options.AddPolicy("Cors", builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins(
            "http://localhost:8080",
            "http://127.0.0.1:8080",
            "http://0.0.0.0:8080",
            "https://gourav-d.github.io");
}));

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();
//user service used for register & login
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<FriendService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("Cors");
app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
