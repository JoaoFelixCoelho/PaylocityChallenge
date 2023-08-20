using Api;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Mappings;
using Api.Models;
using Api.Services.Dependents;
using Api.Services.Employees;
using Api.Services.Interfaces;
using Api.Services.Paychecks;
using Api.Services.SeedHelper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SqliteDbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

builder.Services.AddAutoMapper(x => x.AddProfile<MappingProfile>());
builder.Services.AddScoped<ISeedHelper, SeedHelper>();
builder.Services.AddScoped<IDBBasicOperations<InsertEmployeeDto, UpdateEmployeeDto, RemoveEmployeeDto, ReadEmployeeDto>, EmployeeService>();
builder.Services.AddScoped<IGetOperations<GetEmployeeDto>, EmployeeService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDBBasicOperations<InsertDependentDto, UpdateDependentDto, RemoveDependentDto, ReadDependentDto>, DependentService>();
builder.Services.AddScoped<IGetOperations<GetDependentDto>, DependentService>();
builder.Services.AddScoped<IDependentService, DependentService>();
builder.Services.AddScoped<IPaycheckService, PaycheckService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
