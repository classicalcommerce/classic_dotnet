using API.Intefaces;
using API.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

[TestFixture]
public class ForgotPasswordServiceTests
{
    private ForgotPasswordService service;
    private Mock<ILogger<ForgotPasswordService>> logger;
    private Mock<IForgotPasswordRepository> repository;

    [SetUp]
    public void Setup()
    {
        logger = new();
        repository = new();
        service = new(logger.Object, repository.Object);
    }

    [Test]
    public void CheckIfEmailExists_EmailDoesNotExist_ThrowsException()
    {
        repository.Setup(method => method.CheckIfEmailExists(It.IsAny<string>())).Throws(It.IsAny<Exception>());
        Assert.Throws<NullReferenceException>(() => service.CheckIfEmailExists(It.IsAny<string>()));
    }

    [Test]
    public void CheckIfEmailExists_EmailDoesNotExist_ReturnsBoolean()
    {
        repository.Setup(method => method.CheckIfEmailExists(It.IsAny<string>())).Returns(false);
        var result = service.CheckIfEmailExists(It.IsAny<string>());
        Assert.Multiple(() => 
        {
            Assert.That(result, Is.InstanceOf<bool>());
            Assert.That(result, Is.EqualTo(false));
        });
    }
}