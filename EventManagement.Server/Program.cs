using EventManagementApp.Server.Config;
using EventManagementApp.Server.Core.Initializer;
using EventManagementApp.Server.Core.Interfaces;
using EventManagementApp.Server.Infrastucture;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:7082")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

var connectionString = builder.Configuration.GetConnectionString("MongoDBConnection");
builder.Services.AddSingleton(new MongoClient(connectionString));

builder.Services.AddSingleton<IMongoCollectionFactory, MongoCollectionFactory>();
builder.Services.AddSingleton<DatabaseSeeder>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));



var app = builder.Build();
app.UseDatabaseSeeder();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
