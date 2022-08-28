
using TransactionalTest.Context;
using TransactionalTest.Interfaces;
using TransactionalTest.Repositories;
using TransactionalTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//
// ID
builder.Services.AddTransient<IAppDBContext, AppDBContext>();
// Repositories
builder.Services.AddTransient<IClientRepository, ClientRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IMovementRepository, MovementsRepository>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
//Services
builder.Services.AddTransient<ICompareServices, CompareServices>();
builder.Services.AddTransient<IValidateServices, ValidateServices>();



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

app.Run();
