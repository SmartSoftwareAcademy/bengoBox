using hrapi;
using hrapi.Data;
using hrapi.GraphQL;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet.Common;
using Steeltoe.Discovery.Client;
using GraphQL;

var builder = WebApplication.CreateBuilder(args);
string SERVICE_NAME = "BengoBox.HRAPI";

// Add services to the container.
IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables()
    .Build();
builder.Services.AddControllers();
string conStringDb = "server=127.0.0.1;database=bengoboxauth;uid=root;password='root';port=3306;Allow User Variables=true;";
Globals.conString = conStringDb;
builder.Services.AddDbContext<HRData>(opt => opt.UseMySql(connectionString: conStringDb, new MySqlServerVersion(new Version(8, 0, 29)))); /*new MySqlServerVersion(new Version(8, 0, 29)*/
//blder.Services.AddEntityFrameworkStores<HRData>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddDiscoveryClient(builder.Configuration);
builder.Services.AddGraphQL(b => b
    .AddAutoSchema<Query>()  // schema
    .AddSystemTextJson());   // serializer
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHsts();
app.UseHttpsRedirection();

app.UseCors();
app.UseWebSockets();
app.UseGraphQL("/graphql");            // url to host GraphQL endpoint
app.UseGraphQLPlayground(
    "/",                               // url to host Playground at
    new GraphQL.Server.Ui.Playground.PlaygroundOptions
    {
        GraphQLEndPoint = "/graphql",         // url of GraphQL endpoint
        SubscriptionsEndPoint = "/graphql",   // url of GraphQL endpoint
    });
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
