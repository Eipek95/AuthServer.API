using Microsoft.AspNetCore.Authorization;
using MiniApp1.API.Requirements;
using SharedLibrary.Configurations;
using SharedLibrary.Extensions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<CustomTokenOption>(builder.Configuration.GetSection("TokenOptions"));//option configure
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOption>();

builder.Services.AddCustomTokenAuth(tokenOptions);

builder.Services.AddSingleton<IAuthorizationHandler, BirthTimeRequirementHandler>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DenemePolicy", policy =>
    {
        policy.RequireClaim("city", "Deneme");
    });
    options.AddPolicy("AgePolicy", policy =>
    {
        policy.Requirements.Add(new BirthDateRequirement(18));//18 yaþ sýnýrý gönderdik
    });
});
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
