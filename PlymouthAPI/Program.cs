using Microsoft.EntityFrameworkCore;
using PlymouthAPIData;

var builder = WebApplication.CreateBuilder(args);

//var config = WebApplication.CreateBuilder().Configuration;
//var someConfig = config["someConfig"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionstring = builder.Configuration.GetConnectionString("SqlConnection");

builder.Services.AddDbContext<PlymouthAPIdbcontext>(options => options.UseSqlServer(connectionstring));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
