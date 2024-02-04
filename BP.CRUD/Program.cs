using BP.CRUD.Data;
using BP.CRUD.Domain.Commands.Client.Create;
using BP.CRUD.Domain.Commands.Client.Delete;
using BP.CRUD.Domain.Commands.Client.DeleteLogic;
using BP.CRUD.Domain.Commands.Client.Update;
using BP.CRUD.Repository;
using BP.CRUD.Repository.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<DbContextClass>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebApiDatabase"))
);


builder.Services.AddScoped<IClientRepository, ClientRepository>().Reverse();


builder.Services.AddTransient<IValidator<CreateClientCommand>, CreateClientValidator>();
builder.Services.AddTransient<IValidator<DeleteClientCommand>, DeleteClientValidator>();
builder.Services.AddTransient<IValidator<UpdateClientCommand>, UpdateClientValidator>();
builder.Services.AddTransient<IValidator<DeleteClientLogicCommand>, DeleteClientLogicValidator>();


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
