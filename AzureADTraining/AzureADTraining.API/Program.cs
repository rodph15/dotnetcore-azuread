using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
    {                  
        o.Authority = "https://login.microsoftonline.com/24ae6e9a-9550-478e-bc14-adb9262edad6/v2.0";
        o.Audience = "api://b9105081-8d65-4fbc-b318-c1c2f3101f88";
        o.TokenValidationParameters.ValidIssuer = "https://sts.windows.net/24ae6e9a-9550-478e-bc14-adb9262edad6/";
    });
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
