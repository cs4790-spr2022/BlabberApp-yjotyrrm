using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var userRepo = new MySqlUserRepository(Dsn.DSN);
builder.Services.AddSingleton<IUserRepository, MySqlUserRepository>(r => userRepo);
builder.Services.AddSingleton<IBlabRepository, MySqlBlabRepository>(r => new MySqlBlabRepository(Dsn.DSN, userRepo));


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

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
