
using HopLearn.Services.UserService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Samro.Core.Services.ChatHub;
using WinWin.Core.Interfaces;
using WinWin.Core.Interfaces.BlogandBlogGroup;
using WinWin.Core.Interfaces.ChatAnd_Message;
using WinWin.Core.Interfaces.ChatHub;
using WinWin.Core.Interfaces.RoleInterfaces;
using WinWin.Core.Interfaces.Sports;
using WinWin.Core.Interfaces.TournamentAndMatch;
using WinWin.Core.Services;
using WinWin.Core.Services.BlogandBlogGroupServices;
using WinWin.Core.Services.ChatHub;
using WinWin.Core.Services.RoleServices;
using WinWin.Core.Services.SportServices;
using WinWin.Core.Services.TournamentAndMatch;
using WinWin.Core.Tools.Account;
using WinWin.DataLayer.Contextes;
using WinWin.DataLayer.Entities.Roles;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
#region JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = Convert.FromBase64String(jwtSettings["SecretKey"]);
var validationKey = new SymmetricSecurityKey(key)
{
    KeyId = "e2f3b8b4-25e6-4d57-8c56-0f2b0d9e63f7"
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = validationKey,
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("JwtToken"))
            {
                var encryptedToken = context.Request.Cookies["JwtToken"];
                var encryptionService = context.HttpContext.RequestServices.GetRequiredService<EncryptionService>();
                var decryptedToken = encryptionService.Decrypt(encryptedToken);
                context.Token = decryptedToken;
                Console.WriteLine($"✅ Token Verified: {context.Token}");
            }
            else
            {
                Console.WriteLine("❌ Token Verify failed");
            }
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("❌ Authorize Failed " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            context.HandleResponse();
            return Task.CompletedTask;
        }
    };
});
#endregion

#region Database Context
builder.Services.AddDbContext<SamroContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("WinWinTestConnectionString2022")));

//builder.Services.AddDbContext<SamroContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("WinWinTestConnectionString2019")));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddSignalR();

//builder.Services.AddDbContext<SamroContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("SamroConnection")));
#endregion

#region Dependency Injection (IOC)
builder.Services.AddTransient<IUser, UserServices>();
builder.Services.AddTransient<IMatch, MatchServices>();
builder.Services.AddTransient<IRole, RoleServices>();
builder.Services.AddTransient<IRolePermission, RolePermissionServices>();
builder.Services.AddTransient<IBlogGroup, BlogGroupServices>();
builder.Services.AddTransient<IBlog, BlogServices>();
builder.Services.AddTransient<IMessage, MessageServices>();
builder.Services.AddTransient<IRoom, RoomServices>();
builder.Services.AddTransient<ITournament, TournamentServices>();
builder.Services.AddTransient<ISport, SportServices>();
builder.Services.AddSingleton<EncryptionService>();
builder.Services.AddSingleton<JwtService>();
builder.Services.AddTransient<AccountTools>();
builder.Services.AddScoped<PasswordHasher<User>>();
#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseCors("AllowAllOrigins");

#region Endpoint Configuration
app.Use(async (context, next) =>
{
    var startTime = DateTime.UtcNow;
    Console.WriteLine($"📥 Start Request: {context.Request.Method} {context.Request.Path} in {startTime}");

    await next(context);

    var endTime = DateTime.UtcNow;
    var duration = endTime - startTime;
    Console.WriteLine($"📤 End Request: {context.Request.Method} {context.Request.Path} in {endTime} - Response Time: {duration.TotalMilliseconds} ms");
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
#endregion
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapHub<ChatHub.Hubs.ChatHub>("/chatHub");  
//});
//app.Urls.Add("https://localhost:5000");
app.Run();
//Saamro.ir