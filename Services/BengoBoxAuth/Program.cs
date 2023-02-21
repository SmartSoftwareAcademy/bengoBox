using BengoBoxAuth;
using BengoBoxAuth.Configuration;
using BengoBoxAuth.Data;
using BengoBoxAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Steeltoe.Discovery.Client;
using System.Text;
using static Org.BouncyCastle.Math.EC.ECCurve;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
string SERVICE_NAME = "BengoBox.Auth";

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BengoBoxAuth", Version = "v1" });
    c.AddSecurityDefinition(
"token",
new OpenApiSecurityScheme
{
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer",
    In = ParameterLocation.Header,
    Name = HeaderNames.Authorization
}
);
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "token"
                        },
                    },
                    Array.Empty<string>()
                }
        }
    );
});
//builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
           .AddJwtBearer(jwt =>
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

string conStringDb = "server=127.0.0.1;database=bengoboxauth;uid=root;password='root';port=3306;Allow User Variables=true;";
Globals.conString = conStringDb;
builder.Services.AddDbContext<UserData>(opt => opt.UseMySql(connectionString: conStringDb, new MySqlServerVersion(new Version(8, 0, 29)))); /*new MySqlServerVersion(new Version(8, 0, 29)*/
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                 .AddEntityFrameworkStores<UserData>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ServerPolicy",
        builder =>
        {
            /*builder.WithOrigins("http://localhost:8080")
                             .AllowAnyHeader()
                             .AllowAnyMethod();*/
            builder.WithOrigins("")
                             .AllowAnyHeader()
                             .AllowAnyMethod()
                             .SetIsOriginAllowed(origin => true) // allow any origin
                             .AllowCredentials(); // allow credential

        });
});
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
app.UseRouting();
app.UseHsts();
app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
//app.UseDiscoveryClient();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(SERVICE_NAME);
    });
}); 
app.MapControllers();

app.Run();
