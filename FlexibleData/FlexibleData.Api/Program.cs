using FlexibleData.Persistence;
using FlexibleData.Application;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FlexibleDataContext>(options=>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlexibleDataConnectionString")));

//register application services
builder.Services.AddApplicationServices();
//register persistence services
builder.Services.AddPersistenceServices();

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

//run the migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<FlexibleDataContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();

//api endpoints
app.MapPost("/flexibledata/create", async ([FromBody] CreateFlexibleDataCommand flexibleData, [FromServices] IMediator mediator) =>
{
    //send the data for processing
    var result = await mediator.Send(flexibleData);

    return Results.Ok(result);
})
.WithName("CreateFlexibleData")
.WithOpenApi();

app.Run();
