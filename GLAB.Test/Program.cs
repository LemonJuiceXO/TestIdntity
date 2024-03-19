    using GLAB.APP;
    using GLAB.Api.Accounts;
    using GLAB.Api.Users;
    using GLAB.APP.Members;
    using GLAB.Impl.Members;
    using GLAB.Implementation.Accounts;
    using GLAB.Implementation.Users;
    using GLAB.Infra.Storages.MembersStorages;
    using TestIdntity.Components;
    using Users.Infra.Storages;

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddAuthentication().AddCookie();
    // Add services to the container.
    builder.Services.AddRazorPages();
    builder.Services.AddControllers();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();
    builder.Services.AddControllers();
    builder.Services.AddScoped<IMemberStorage, MemberStorage>();
    builder.Services.AddScoped<IMemberService,MemberService>();
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
    app.UseAuthentication();

    app.UseStaticFiles();

    app.UseAntiforgery();
    app.UseCookiePolicy();
    app.MapControllers();
    app.MapBlazorHub();
    app.UseRouting();
    
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Login}/{action=LoginPage}"
    );


    app.Run();