using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Dependency Injection Container LOL
// builder.Services.AddSingleton<IUserRepository, InMemUserRepository>();
builder.Services.AddSingleton<IBlabRepository, MySqlBlabRepository>(r => new MySqlBlabRepository(Dsn.DSN));
builder.Services.AddSingleton<IUserRepository, MySqlUserRepository>(r => new MySqlUserRepository(Dsn.DSN));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.MapRazorPages();

app.Run();
