using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Dependency Injection Container LOL
// builder.Services.AddSingleton<IUserRepository, InMemUserRepository>();

//I am not 100% sure this is the right way to do this, but I need to do something like this to function with SQL.
//since the separation of user and blab tables is specifically an SQL concern, I can't do it anywhere but this dependency injection.
MySqlUserRepository u = new MySqlUserRepository(Dsn.DSN);
builder.Services.AddSingleton<IUserRepository, MySqlUserRepository>(r => u);
builder.Services.AddSingleton<IBlabRepository, MySqlBlabRepository>(r => new MySqlBlabRepository(Dsn.DSN, u));

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
