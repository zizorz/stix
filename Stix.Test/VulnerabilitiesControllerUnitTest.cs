using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Stix.Controllers;
using Stix.Models;
using Stix.Services;

namespace Stix.Test;

public class VulnerabilitiesControllerUnitTest
{

    [Test]
    public async Task Create_Should_Return_201()
    {
        var serviceMock = new Mock<IVulnerabilityService>();
        var logMock = new Mock<ILogger<VulnerabilitiesController>>();

        var vulnerability = new Vulnerability(
            "vulnerability",
            "2.1",
            "vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80",
            System.DateTime.Now,
            System.DateTime.Now,
            "CVE-2010-3333"
        );

        var controller = new VulnerabilitiesController(serviceMock.Object, logMock.Object);
        var result = await controller.Create(vulnerability);
        
        Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(201));
    }
    
    [Test]
    public async Task Update_Should_Return_204()
    {
        var serviceMock = new Mock<IVulnerabilityService>();
        var logMock = new Mock<ILogger<VulnerabilitiesController>>();

        var vulnerability = new Vulnerability(
            "vulnerability",
            "2.1",
            "vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80",
            System.DateTime.Now,
            System.DateTime.Now,
            "CVE-2010-3333"
        );

        var controller = new VulnerabilitiesController(serviceMock.Object, logMock.Object);
        var result = await controller.Update("vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80", vulnerability);
        
        Assert.That(((StatusCodeResult)result).StatusCode, Is.EqualTo(204));
    }
    
    [Test]
    public async Task Get_Should_Return_200()
    {
        var serviceMock = new Mock<IVulnerabilityService>();
        var logMock = new Mock<ILogger<VulnerabilitiesController>>();

        var vulnerability = new Vulnerability(
            "vulnerability",
            "2.1",
            "vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80",
            System.DateTime.Now,
            System.DateTime.Now,
            "CVE-2010-3333"
        );
        serviceMock
            .Setup(mock => mock.GetByIdAsync("vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80"))
            .Returns(Task.FromResult(vulnerability));
        

        var controller = new VulnerabilitiesController(serviceMock.Object, logMock.Object);
        var result = await controller.Get("vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80");
        
        Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(200));
    }
    
    [Test]
    public async Task List_Should_Return_200()
    {
        var serviceMock = new Mock<IVulnerabilityService>();
        var logMock = new Mock<ILogger<VulnerabilitiesController>>();

        var controller = new VulnerabilitiesController(serviceMock.Object, logMock.Object);
        var result = await controller.List();
        
        Assert.That(((ObjectResult)result).StatusCode, Is.EqualTo(200));
    }
    
    [Test]
    public async Task Delete_Should_Return_204()
    {
        var serviceMock = new Mock<IVulnerabilityService>();
        var logMock = new Mock<ILogger<VulnerabilitiesController>>();

        var controller = new VulnerabilitiesController(serviceMock.Object, logMock.Object);
        var result = await controller.Delete("vulnerability--717cb1c9-eab3-4330-8340-e4858055aa80");
        
        Assert.That(((StatusCodeResult)result).StatusCode, Is.EqualTo(204));
    }
}