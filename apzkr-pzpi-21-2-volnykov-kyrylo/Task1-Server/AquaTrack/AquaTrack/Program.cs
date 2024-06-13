using AquaTrack.Repositories.Interfaces;
using AquaTrack.Repositories;
using AquaTrack.Database;
using Microsoft.EntityFrameworkCore;
using AquaTrack.Services.Interfaces;
using AquaTrack.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddScoped<IAnalysisReportRepository, AnalysisReportRepository>();
builder.Services.AddScoped<IResearchReportRepository, ResearchReportRepository>();
builder.Services.AddScoped<ISensorDataRepository, SensorDataRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAquariumRepository, AquariumRepository>();
builder.Services.AddScoped<IInhabitantRepository, InhabitantRepository>();
builder.Services.AddScoped<IFeedingScheduleRepository, FeedingScheduleRepository>();

builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();
builder.Services.AddScoped<IAquariumService, AquariumService>();
builder.Services.AddScoped<IInhabitantService, InhabitantService>();
builder.Services.AddScoped<IFeedingScheduleService, FeedingScheduleService>();
builder.Services.AddScoped<IAnalysisReportService, AnalysisReportService>();
builder.Services.AddScoped<ISensorDataService, SensorDataService>();
builder.Services.AddScoped<IResearchReportService, ResearchReportService>();


builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var ReactCors = "ReactCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ReactCors,
        builder =>
        {
            builder.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        });
});
    
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors(ReactCors);
app.UseAuthorization();

app.MapControllers();

app.Run();
