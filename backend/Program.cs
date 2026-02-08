using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        policy => policy
            .WithOrigins("http://localhost:4200", "http://localhost")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

// In-memory data store
var projects = new List<Project>
{
    new(1, "E-Commerce Platform", "Full-stack e-commerce solution with payment integration", 
        "https://images.unsplash.com/photo-1557821552-17105176677c?w=800", 
        "https://github.com/user/ecommerce", "https://demo.example.com",
        new List<string> { "Angular", "ASP.NET Core", "PostgreSQL", "Stripe" }),
    new(2, "Task Management App", "Real-time collaborative task management application",
        "https://images.unsplash.com/photo-1484480974693-6ca0a78fb36b?w=800",
        "https://github.com/user/taskapp", "https://tasks.example.com",
        new List<string> { "React", "Node.js", "MongoDB", "Socket.io" }),
    new(3, "Weather Dashboard", "Beautiful weather dashboard with data visualization",
        "https://images.unsplash.com/photo-1592210454359-9043f067919b?w=800",
        "https://github.com/user/weather", "https://weather.example.com",
        new List<string> { "Vue.js", "Python", "FastAPI", "Chart.js" })
};

var skills = new List<Skill>
{
    new(1, "Angular", "Frontend", 90),
    new(2, "React", "Frontend", 85),
    new(3, "TypeScript", "Frontend", 88),
    new(4, "ASP.NET Core", "Backend", 92),
    new(5, "Node.js", "Backend", 80),
    new(6, "PostgreSQL", "Database", 85),
    new(7, "Docker", "DevOps", 88),
    new(8, "Kubernetes", "DevOps", 82)
};

var experiences = new List<Experience>
{
    new(1, "Tech Corp", "Senior Full Stack Developer", "2021 - Present",
        "Leading development of cloud-native applications using microservices architecture"),
    new(2, "StartUp Inc", "Full Stack Developer", "2019 - 2021",
        "Built and maintained multiple web applications using modern frameworks"),
    new(3, "Digital Agency", "Junior Developer", "2017 - 2019",
        "Developed responsive websites and web applications for various clients")
};

// API Endpoints
app.MapGet("/api/health", () => new { status = "healthy", timestamp = DateTime.UtcNow })
    .WithName("HealthCheck")
    .WithOpenApi();

app.MapGet("/api/projects", () => Results.Ok(projects))
    .WithName("GetProjects")
    .WithOpenApi();

app.MapGet("/api/projects/{id}", (int id) =>
{
    var project = projects.FirstOrDefault(p => p.Id == id);
    return project is not null ? Results.Ok(project) : Results.NotFound();
})
    .WithName("GetProject")
    .WithOpenApi();

app.MapGet("/api/skills", () => Results.Ok(skills))
    .WithName("GetSkills")
    .WithOpenApi();

app.MapGet("/api/experience", () => Results.Ok(experiences))
    .WithName("GetExperience")
    .WithOpenApi();

app.MapPost("/api/contact", async ([FromBody] Contact contact) =>
{
    // In production, you would send email or save to database
    Console.WriteLine($"Contact form submission from {contact.Name} ({contact.Email}): {contact.Message}");
    
    // Simulate async operation
    await Task.Delay(100);
    
    return Results.Ok(new { success = true, message = "Thank you for your message!" });
})
    .WithName("SubmitContact")
    .WithOpenApi();

app.Run();

// Models
record Project(int Id, string Title, string Description, string ImageUrl, string GithubUrl, string DemoUrl, List<string> Technologies);
record Skill(int Id, string Name, string Category, int Proficiency);
record Contact(string Name, string Email, string Message);
record Experience(int Id, string Company, string Position, string Duration, string Description);
