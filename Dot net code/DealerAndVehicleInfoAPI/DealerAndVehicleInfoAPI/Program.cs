using DealerAndVehicleInfoAPI.DataContext;

using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DealerAndVechicleDbContext>();
//builder.Services.AddScoped<IRepository, Repository<DealerAndVechicleDbContext>();

// Get connection string from the appsetting 
builder.Services.AddDbContext<DealerAndVechicleDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DealerAndVehicleDBCOnnection")));

var app = builder.Build();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   // app.UseSwagger();
    //app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id:int?}",
                defaults: new { controller = "DealerAndVechicleInformation", action = "GetDelalerInfo" }
            );
app.Run();
