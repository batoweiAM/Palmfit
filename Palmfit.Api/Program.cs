using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Palmfit.Api.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Palmfit.Core.Implementations;
using Palmfit.Core.Services;
using Palmfit.Data.AppDbContext;
//using Palmfit.Data.Seeder;

var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseUrls("http://0.0.0.0:80");

//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
//    .AddEnvironmentVariables()
//    .Build();

var maxUserWatches = builder.Configuration.GetValue<int>("MaxUserWatches");
if (maxUserWatches > 0)
{
    var fileSystemWatcher = new FileSystemWatcher("/")
    {
        EnableRaisingEvents = true
    };
    var maxFiles = maxUserWatches / 2;
    fileSystemWatcher.InternalBufferSize = maxFiles * 4096;
}

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContextAndConfigurations(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Set the Swagger document properties
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Palmfit.Api",
        Version = "v1",
        Description = "Palmfit Backend APIs",
        //Contact = new Microsoft.OpenApi.Models.OpenApiContact
        //{
        //    Name = "Palmfit",
        //    Email = "example@gmail.com" //TODO:Use an actual email
        //}
    });
    // To Enable authorization using Swagger (JWT)
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsI\"",
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});


//var provider = builder.Services.BuildServiceProvider()
//    .GetRequiredService<IConfiguration>();


/**************************************************
 *  Connecting Frontend URL
 ***************************************************/

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


builder.Services.AddCors(options =>
{
    //var frontendURL = provider.GetValue<string>("frontendURL");

    options.AddPolicy("AllowedHosts",
        builder =>
        {
            builder.WithOrigins().AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader()
			   .WithExposedHeaders("Authorization"); // Expose the Authorization header
	});
});

var app = builder.Build();
app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Seeder.SeedData(app).Wait();
app.UseCors();
app.UseCors("AllowedHosts");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();