using Microsoft.AspNetCore.Mvc;
using MinimalEndpoints;
using Microsoft.OpenApi.Models;
using AccelMart.WebAPI.Features.Todo;
using AccelMart.ApplicationServices;
using AccelMart.Infrastructure;

namespace AccelMart.WebAPI;

public static class ProgramExtensions
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastucture();

        builder.Services.AddMinimalEndpoints();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AccelMart.WebAPI API",
                Version = "v1",
                Description = "An API developed using MinimalEndpoint",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Author Name",
                    Url = new Uri("https://github.com/nyronw"),
                },
                License = new OpenApiLicense
                {
                    Name = "AccelMart.WebAPI License",
                    Url = new Uri("https://example.com/license"),
                }
            });

            c.OperationFilter<SecureSwaggerEndpointRequirementFilter>();

            // Set the comments path for the Swagger JSON and UI.
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory)
                .Where(f => Path.GetExtension(f) == ".xml");

            foreach (var xmlFile in xmlFiles)
            {
                c.IncludeXmlComments(xmlFile);
            }

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
        });

        builder.Services.AddSingleton<ITodoRepository, TodoRepository>();

        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "AccelMart.WebAPI API");
        });
        app.UseHttpsRedirection();

        app.UseMinimalEndpoints(options =>
        {
            options.DefaultRoutePrefix = "/api/v1";
            options.DefaultGroupName = "v1";
            options.Filters.Add(new ProducesResponseTypeAttribute(typeof(HttpValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+"));
        });

        return app;
    }
}
