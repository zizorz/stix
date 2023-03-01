using Microsoft.OpenApi.Models;
using Stix.Validation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});


SetUpOpenApiGeneration(builder);
SetUpAuthorization(builder);
await SetUpValidationSchema(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


void SetUpAuthorization(WebApplicationBuilder scopedBuilder)
{
    scopedBuilder.Services.AddAuthentication("Bearer").AddJwtBearer();
    scopedBuilder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Writer", policy => policy.RequireRole("Admin"));
            options.AddPolicy("Reader", policy => policy.RequireRole("Admin", "User"));
        }
    );
}

void SetUpOpenApiGeneration(WebApplicationBuilder scopedBuilder)
{
    scopedBuilder.Services.AddEndpointsApiExplorer();
    scopedBuilder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Stix API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid JWT token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    });
}

async Task SetUpValidationSchema(WebApplicationBuilder scopedBuilder)
{
    Console.WriteLine("Setting up schema validation...");
    const string schemaUrl = "https://raw.githubusercontent.com/oasis-open/cti-stix2-json-schemas/master/schemas/sdos/vulnerability.json";
    var schema = await VulnerabilityValidator.createSchema(schemaUrl);
    scopedBuilder.Services.AddSingleton<IVulnerabilityValidator>(new VulnerabilityValidator(schema));
}