using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Registers controllers as services for handling HTTP requests
builder.Services.AddEndpointsApiExplorer(); // Adds API explorer services for endpoint metadata and documentation
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Books API", Version = "v1" }); // Configures Swagger generation with API information
});

// Configures Entity Framework Core to use an in-memory database named "Books" for the BookContext
builder.Services.AddDbContext<BookContext>(options =>
{
    options.UseInMemoryDatabase("Books");
});

// Seed the database with initial data using the SeedData class
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    SeedData.Initialize(scope.ServiceProvider); // Calls the Initialize method in SeedData to seed the database
}

var app = builder.Build(); // Builds the application instance

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger middleware for API documentation in development
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books API V1"); // Configures Swagger UI to display API documentation
    });
}

app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS
app.UseAuthorization(); // Adds authorization middleware to the request pipeline
app.MapControllers(); // Maps controller routes to endpoints for handling HTTP requests

app.Run(); // Runs the application
