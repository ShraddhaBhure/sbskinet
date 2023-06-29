using  Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
// dotnet add package Microsoft.EntityFrameworkCore.SqlServer
// using Microsoft.EntityFrameworkCore;
//builder.Services.AddSqlServer<StoreContext>(builder.Configuration.GetConnectionString("DefaultConnection"));





var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// dotnet add package Microsoft.EntityFrameworkCore.InMemory
// using Microsoft.EntityFrameworkCore;
//builder.Services.AddDbContext<StoreContext>(options =>
   // options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
  
builder.Services.AddDbContext<StoreContext>(opt=>
{
opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
