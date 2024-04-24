using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApplicationVMS.Data;
using WebApplicationVMS.Repository.Abstract;
using WebApplicationVMS.Repository.Implementation;
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.AddTransient<IFileServices, FileServices>();
builder.Services.AddTransient<IVisitorRepository, VisitorRepository>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("https://localhost:7124").AllowAnyMethod().AllowAnyHeader();
                          
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
app.UseStaticFiles(new StaticFileOptions
{
        FileProvider=new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath,"Uploads")),
        RequestPath="/Uploads"
});
app.UseRouting();
app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
