using Application;
using Application.Middlewares;
using Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

ApplicationServiceRegistration.AddApplicationServices(builder.Services);

IdentityServiceRegistration.AddIdentityServices(builder.Services, builder.Configuration);

PersistenceServiceRegistration.AddPersistenceServices(builder.Services, builder.Configuration);

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IssentialZ API V1");
        c.RoutePrefix = "issentialz-api/swagger";
    });
}

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);



app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
