using API.Intefaces;
using API.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace Tests;

[TestFixture]
public class ForgotPasswordRepositoryTests
{
    private ForgotPasswordRepository repository;
    private Mock<ILogger<ForgotPasswordRepository>> logger;
    private Mock<IContext> context;

    [SetUp]
    public void Setup()
    {
        logger = new();
        context = new();
        repository = new(logger.Object, context.Object);
    }

    [Test]
    public void CheckIfEmailExists_EmailDoesNotExist_ThrowsException()
    {
        context.Setup(method => method.ValidateEmail(It.IsAny<string>())).Throws(It.IsAny<Exception>());
        Assert.Throws<NullReferenceException>(() => repository.CheckIfEmailExists(It.IsAny<string>()));
    }

    [Test]
    public void CheckIfEmailExists_EmailDoesNotExist_ReturnsBoolean()
    {
        context.Setup(method => method.ValidateEmail(It.IsAny<string>())).Returns(false);
        var result = repository.CheckIfEmailExists(It.IsAny<string>());
        Assert.Multiple(() => 
        {
            Assert.That(result, Is.InstanceOf<bool>());
            Assert.That(result, Is.EqualTo(false));
        });
    }
}