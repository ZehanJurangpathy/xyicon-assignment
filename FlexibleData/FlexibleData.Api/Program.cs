using FlexibleData.Api.Middleware;
using FlexibleData.Application;
using FlexibleData.Application.Features.FlexibleData.Commands.CreateFlexibleData;
using FlexibleData.Application.Features.FlexibleData.Queries.GetFlexibleData;
using FlexibleData.Application.Features.FlexibleData.Queries.GetKeyCount;
using FlexibleData.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FlexibleDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FlexibleDataConnectionString")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//register application services
builder.Services.AddApplicationServices(builder.Configuration.GetConnectionString("FlexibleDataConnectionString"));
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

var retryPolicy = Policy
    .Handle<SqlException>()
    .WaitAndRetry(new[]
    {
        TimeSpan.FromSeconds(10),
        TimeSpan.FromSeconds(15),
        TimeSpan.FromSeconds(20)
    });

retryPolicy.Execute(() =>
{
    try
    {
        //run the migrations
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<FlexibleDataContext>();

            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
    }
    catch (SqlException ex)
    {
        // Log or handle the exception
        
    }
});



app.UseHttpsRedirection();
//register custom exception handler
app.UseCustomExceptionHandler();

//api endpoints
app.MapPost("/flexibledata/create", async ([FromBody] CreateFlexibleDataCommand flexibleData, [FromServices] IMediator mediator) =>
{
    //send the data for processing
    var result = await mediator.Send(flexibleData);

    return Results.Ok(result);
})
.WithName("CreateFlexibleData")
.WithOpenApi();

app.MapGet("/flexibledata/get/{id?}", async (Guid? id, [FromServices] IMediator mediator) =>
{
    //send the data for processing
    var result = await mediator.Send(new GetFlexibleDataQuery { Id = id });

    if (id.HasValue)
    {
        if (result is null || !result.Any())
        {
            //no flexible data found for the id provided
            return Results.NotFound();
        }

        //flexible data found for the id
        return Results.Ok(result.First());
    }
    else
    {
        //id was not provided. hence return all the data returned from  he database
        return Results.Ok(result);
    }
})
.WithName("GetFlexibleData")
.WithOpenApi();

app.MapGet("/flexibledata/count/{key?}", async (string? key, [FromServices] IMediator mediator) =>
{
    //send the data for processing
    var result = await mediator.Send(new GetKeyCountQuery { Key = key });

    if (key is not null)
    {
        if (result is null || !result.Any())
        {
            //no statistics found for the key provided
            return Results.NotFound();
        }

        //statistics data found for the key
        return Results.Ok(result.First());
    }
    else
    {
        //key was not provided. hence return all the data returned from  he database
        return Results.Ok(result);
    }
})
.WithName("GetKeyCount")
.WithOpenApi();

app.Run();
