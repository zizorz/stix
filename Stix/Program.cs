using Stix.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

await SetUpValidationSchema(builder);


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


async Task SetUpValidationSchema(WebApplicationBuilder scopedBuilder)
{
    Console.WriteLine("Setting up schema validation...");
    const string schemaUrl = "https://raw.githubusercontent.com/oasis-open/cti-stix2-json-schemas/master/schemas/sdos/vulnerability.json";
    var schema = await VulnerabilityValidator.createSchema(schemaUrl);
    scopedBuilder.Services.AddSingleton<IVulnerabilityValidator>(new VulnerabilityValidator(schema));
}