using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Stix.Models;

namespace Stix.IntegrationTest;

public class ApiIntegrationTest 
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly JsonSerializerOptions _options;

    public ApiIntegrationTest(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _options = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    [Fact]
    public async Task Post_Vulnerability_Should_CreateVulnerability()
    {
        var client = CreateClient();

        var vulnerability = new Vulnerability(
            "vulnerability",
            "2.1",
            "vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80",
            System.DateTime.Now.ToUniversalTime(),
            System.DateTime.Now.ToUniversalTime(),
            "CVE-2010-3333"
        );

        var json = JsonSerializer.Serialize(vulnerability, _options);
        
        var response = await client.PostAsync("/vulnerabilities", new StringContent(json, new MediaTypeHeaderValue("application/json")));

        var body = await response.Content.ReadAsStringAsync();
        
        response.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task Get_Vulnerabilities_Should_ListVulnerabilities()
    { 
        var client = CreateClient();

        var vulnerability = new Vulnerability(
            "vulnerability",
            "2.1",
            "vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80",
            System.DateTime.Now.ToUniversalTime(),
            System.DateTime.Now.ToUniversalTime(),
            "CVE-2010-3333"
        );

        var json = JsonSerializer.Serialize(vulnerability, _options);
        
        var response = await client.PostAsync("/vulnerabilities", new StringContent(json, new MediaTypeHeaderValue("application/json")));
        response.EnsureSuccessStatusCode();
        
        
        response = await client.GetAsync("/vulnerabilities");

        response.EnsureSuccessStatusCode();
        var jsonResponse = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<IList<Vulnerability>>(jsonResponse);
        
        Assert.Equal(1, result.Count);

    }

    private HttpClient CreateClient()
    {
        return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(defaultScheme: "TestScheme").AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });
                    services.Configure<DbSettings>(dbSettings => { dbSettings.DatabaseName = "StixTest-" + Guid.NewGuid(); });
                });
            })
            .CreateClient();
    }
}