using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, o =>
    {
        o.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        o.Authority = "https://login.microsoftonline.com/24ae6e9a-9550-478e-bc14-adb9262edad6/v2.0";
        o.ClientId = "c79834b0-8437-481f-9350-c969dab0b234";
        o.Scope.Add("api://b9105081-8d65-4fbc-b318-c1c2f3101f88/AdminAccess");
        o.ResponseType = "code";
        o.SaveTokens = true;
        o.ClientSecret = "xNr8Q~PP4sUCaNqKdVFVsuXOEnM1ISvRlt92Mas6";
        o.TokenValidationParameters = new TokenValidationParameters()
        {
            NameClaimType = "name"
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// auth endpoint https://login.microsoftonline.com/24ae6e9a-9550-478e-bc14-adb9262edad6/oauth2/v2.0/authorize

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();