using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// This file is the entry point for the PickYourTrip.Web application.
// It configures the application's services and the HTTP request pipeline.

// Create a new WebApplicationBuilder instance.
var builder = WebApplication.CreateBuilder(args);

// Add services to the application's service container.
// We are only using static files, so we don't need any MVC controllers or views.
builder.Services.AddEndpointsApiExplorer();

// Add the missing authorization services.
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// This middleware enables serving static files from the wwwroot folder.
// Your index.html will be served from here.
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// This sets the default route to your index.html file and handles all unmatched routes
// (which is correct for a Single Page Application like this).
app.MapFallbackToFile("index.html");

app.Run();
