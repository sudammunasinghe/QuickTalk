using QuickTalk.Application.Interfaces.IRepositories;
using QuickTalk.Application.Interfaces.IServices;
using QuickTalk.Application.Services;
using QuickTalk.Infrastructure.Persistence;
using QuickTalk.Infrastructure.Persistence.Sql.Helpers;
using QuickTalk.Infrastructure.Repositories;
using QuickTalk.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

// ================================== Register Services ===================================================
builder.Services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ISqlQueryLoader, SqlQueryLoader>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
