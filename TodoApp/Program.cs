global using System.Linq;
global using TodoApp.Data.Entities;
global using TodoApp.Models;
global using TodoApp.Data;
global using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>(opt =>
	opt.UseInMemoryDatabase("TodoList")
);

builder.Services.AddCors(options => {
    options.AddPolicy("myPolicy",
                      policy => {
                          policy
                            .WithOrigins("https://localhost:7237")
                            //.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo {
        Version = "v1",
        Title = "Todo API",
        Description = "My cool Todo API",
        Contact = new OpenApiContact {
            Name = "Liron Cohen",
            Email = "lironco@sela.co.il",
            Url = new Uri("https://sela.co.il")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// ==================== Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment()) {
	app.UseSwagger(); // Generate the JSON doc based on my code
	app.UseSwaggerUI(); // Expose a url for the json "/swagger"
}

app.UseHttpsRedirection();

app.UseCors("myPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
