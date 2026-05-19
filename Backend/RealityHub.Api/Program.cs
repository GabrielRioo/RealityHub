using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RealityHub.Api.Services;
using RealityHub.Application.Interfaces;
using RealityHub.Infrastructure.Persistence;
using RealityHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext (EF Core + Postgres)
builder.Services.AddDbContext<RealityHubDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(RealityHub.Application.UseCases.Votes.CastVote.CastVoteCommand).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(RealityHub.Application.UseCases.Votes.CastVote.CastVoteCommandValidator).Assembly);

builder.Services.AddScoped<IRoundRepository, RoundRepository>();
builder.Services.AddScoped<IParticipantRepository, ParticipantRepository>();
builder.Services.AddScoped<IRoundParticipantRepository, RoundParticipantRepository>();
builder.Services.AddScoped<IVoteRepository, VoteRepository>();
builder.Services.AddScoped<IVoteAttemptLogRepository, VoteAttemptLogRepository>();

builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IRequestMetadataService, RequestMetadataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RealityHubDbContext>();
    await RealityHubDbSeader.SeedAsync(dbContext);
}

app.Run();
