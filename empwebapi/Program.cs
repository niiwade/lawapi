using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore)
//    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver=new DefaultContractResolver());

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{

    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

//enable single domains



//enable multiple domain


//any domain

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
    RequestPath = new PathString("/s")
});



app.UseAuthorization();

app.MapControllers();

app.Run();


//public class Startup
//{

    //public void ConfigureServices(IServiceCollection services)
    //{

        //enable cors
       // services.AddCors(c =>
        //{
        //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        //});

       // services.AddControllers();

    //}
//}

