using Application.Features.TaskEntity.Commands;
using Application.Features.TaskEntity.Validators;
using Core.Interfaces;
using Infraestructure.Repositories;
using FluentValidation;
using Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Behaviors;
using TaskManagmentApp.Middlewares;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string not found in environment variables.");
    }

    options.UseSqlServer(connectionString, sqlOptions =>
        sqlOptions.MigrationsAssembly("Infraestructure")  // migrations from
    );
});

// Enable Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()                    
              .AllowAnyMethod();                  
    });
});

// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTaskCommand>());
// Register FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskCommandValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
// Register Repository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Registrar middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS with the specified policy
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();
