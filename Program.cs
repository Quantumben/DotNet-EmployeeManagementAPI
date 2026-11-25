
using EmployeeManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection
builder.Services.AddScoped<EmployeeService>();

// Health checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Custom middleware for logging requests
app.Use(async (context, next) =>
{
    app.Logger.LogInformation(
        "Request: {Method} {Path}",
        context.Request.Method,
        context.Request.Path);

    await next();
});

// Static files and default documents
app.UseDefaultFiles();
app.UseStaticFiles();

// Home API
app.MapGet("/api", () =>
{
    return Results.Ok(new
    {
        Message = "Welcome to Employee API",
        Status = "Running"
    });
});

// Get all employees
app.MapGet("/api/employees",
    (EmployeeService service) =>
{
    return Results.Ok(service.GetAll());
});

// Get an employee by ID
app.MapGet("/api/employees/{id:int}",
    (int id, EmployeeService service) =>
{
    var employee = service.GetById(id);

    if (employee is null)
    {
        return Results.NotFound(
            new { Message = "Employee not found" });
    }

    return Results.Ok(employee);
});

// Read configuration
app.MapGet("/api/company",
    (IConfiguration config) =>
{
    return Results.Ok(new
    {
        CompanyName = config["Company:Name"]
    });
});

// HTTP Context
app.MapGet("/api/request-info",
    (HttpContext context) =>
{
    return Results.Ok(new
    {
        Method = context.Request.Method,
        Path = context.Request.Path.ToString()
    });
});

// Health check endpoint
app.MapHealthChecks("/health");

app.Run();
