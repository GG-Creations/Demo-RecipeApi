using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using RecipesApi;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RecipesContext>(opt =>
    opt.UseInMemoryDatabase("RecipesList"));




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//add swashbuckle documendation nugget
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Recipes_by_GG-creations",
        Description = "An ASP.NET Core Web API for managing RECIPES (it uses EF Core In-Memory for db)",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Me",
            Url = new Uri("https://www.linkedin.com/in/ioannis-gkirnis-83b8b4217/")
        },
        License = new OpenApiLicense
        {
            Name = "Whatever Licence",
            Url = new Uri("https://example.com/license")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//injection
builder.Services.AddTransient<DataGenerator>();




var app = builder.Build();



//Seed Data

SeedData(app);

//Seed Data
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataGenerator>();
        service.Initialize();
    }
}



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




