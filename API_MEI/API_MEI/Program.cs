using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API_MEI.Data;
using API_MEI.Models;
using API_MEI.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<API_MEIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("API_MEIContext") ?? throw new InvalidOperationException("Connection string 'API_MEIContext' not found.")));



//PARA O REACTTT

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:3000", "http://appname.azurestaticapps.net");
    });
});






//app.MapGet("/TodosTrabalhos", async (TrabalhoesController controller) => await controller.Index());

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers().AddNewtonsoftJson(opt =>
    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");  //No entanto, para que a política de CORS seja aplicada a cada solicitação recebida, você precisa chamar o método UseCors
app.UseAuthorization();

app.MapControllers();

app.Run();
