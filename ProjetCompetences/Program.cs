using ProjetCompetences.Domain.Repository;
using ProjetCompetences.Domain.Services;
using ProjetCompetences.Infrastructure;
using ProjetCompetences.Infrastructure.Adapters;
using ProjetCompetences.Infrastructure.Mongo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<MongoCollectionProvider>();
builder.Services.AddScoped<PersonneRepoMongo>();
builder.Services.AddScoped<PersonneRepo, PersonneRepoAdaptMongo>();
builder.Services.AddScoped<PersonneService>();

builder.Services.AddScoped<EquipeRepoMongo>();
builder.Services.AddScoped<EquipeRepo, EquipeRepoAdaptMongo>();
builder.Services.AddScoped<EquipeService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
