using System.Data;
using MySql.Data.MySqlClient;
using SocialNetwork.Users.Application.Mappings;
using SocialNetwork.Users.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

// DB Configs
var connectionString = builder.Configuration.GetConnectionString("SocialNetworkDbConnection");
builder.Services.AddTransient<IDbConnection>(sp => new MySqlConnection(connectionString));

// Swagger Configs
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });

// AutoMapper Configs
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);

// Dependency Injection Configs
IoC.ConfigureContainer(builder.Services, builder.Configuration);

// Default Configs
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
