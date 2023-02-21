using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Eureka;
using Ocelot.Provider.Polly;
using Ocelot.Cache.CacheManager;
using Steeltoe.Discovery.Client;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.Administration;

var builder = WebApplication.CreateBuilder(args);
string SERVICE_NAME = "TDBSoft.APIGateway";


IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{Directory.GetCurrentDirectory()}.json", true, true)
    .AddJsonFile("ocelot.json", true, true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true)
    //.AddOcelot(Directory.GetCurrentDirectory()+"/OcelotFiles", builder.Environment)
    .AddEnvironmentVariables()
    .Build();
// Add services to the container.
builder.Services.AddOcelot(configuration)
 .AddEureka()
 .AddPolly()
 //.AddCacheManager(x =>
 //{
 //    x.WithDictionaryHandle();
 //})
.AddAdministration("/administration", "admin");
builder.Services.AddDiscoveryClient(builder.Configuration);
// .AddConfigStoredInConsul();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAuthentication()
           .AddJwtBearer("JwtBearer",jwt =>
           {
               var key = Encoding.ASCII.GetBytes("53BDH@GE7*llpO&z*56777HGNVov~rq86ETBFUG#*@E@xXA&TE71n9Bhdh57g"/*builder.Configuration.GetSection("JwtConfig:secret").Value*/);

               jwt.SaveToken = true;
               jwt.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   RequireExpirationTime = false
               };
           });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
app.UseRouting();
app.UseStaticFiles();
//app.UseDiscoveryClient();
await app.UseOcelot();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseEndpoints(config =>
{
    config.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(SERVICE_NAME);
    });
    config.MapGet("/info", async context =>
    {
        await context.Response.WriteAsync($"{SERVICE_NAME}, running on {context.Request.Host}");
    });
});

app.MapControllers();

app.Run();
