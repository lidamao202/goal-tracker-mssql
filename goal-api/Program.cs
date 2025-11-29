using goal_api.Data;
using goal_api.Repositories;
using goal_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Register Dapper context
builder.Services.AddSingleton<DapperContext>();

// Register repositories
builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();

// Register services
builder.Services.AddScoped<IGoalTrackerService, GoalTrackerService>();

// Configure CORS for Vue dev server
builder.Services.AddCors(options =>
{
    options.AddPolicy("VueDev", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://127.0.0.1:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("VueDev");
app.MapControllers();

app.Run();
