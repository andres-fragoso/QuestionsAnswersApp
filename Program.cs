using QuestionsAnswersApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using QuestionsAnswersApp.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x =>
   x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Talexa .NET Interview Question",
        Description = "Backend for a question-answering application.",
        TermsOfService = new Uri("https://www.theappchefs.com/"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://www.theappchefs.com/contact/")
        },
        License = new OpenApiLicense
        {
            Name = "Resources",
            Url = new Uri("https://www.theappchefs.com/services/")
        }
    });
});

builder.Services.AddDbContext<InterviewDBContext>(options=>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("constring"));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
    }
    );
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
