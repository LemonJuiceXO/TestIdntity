using GLAB.Api.Accounts;
using GLAB.Api.Users;
using GLAB.Implementation.Accounts;
using GLAB.Implementation.Users;
using TestIdntity.Components;
using Users.Infra.Storages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IAccount, AccountService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserStorage, UserStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();