using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlatformService.Data;
using PlatformService.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

if(builder.Environment.IsProduction())
{
    Console.WriteLine("--> Using SQL Server DB");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
}
else
{
    Console.WriteLine("--> Using InMem DB");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem")) ;
}

builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

PrepDb.PrepPopulation(app, builder.Environment.IsProduction());

app.Run();