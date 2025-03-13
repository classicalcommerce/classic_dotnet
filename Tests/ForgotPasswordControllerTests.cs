using API.Controllers;
using API.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

[TestFixture]
public class ForgotPasswordControllerTests
{
    private ForgotPasswordController controller;
    private Mock<IForgotPasswordService> _service;
    
    [SetUp]
    public void Setup()
    {
        _service = new();
        controller = new(_service.Object);
    }

    [Test]
    public void CheckIfEmailExists_ThrowsException_ReturnsInternalServerError()
    {
        _service.Setup(action => action.CheckIfEmailExists(It.IsAny<string>())).Throws(It.IsAny<Exception>());
        var actionResult = controller.CheckIfEmailExists(It.IsAny<string>());
        Assert.Multiple(() => {
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.InstanceOf<ObjectResult>());
            var result = actionResult as ObjectResult;
            Assert.That(result!.StatusCode, Is.EqualTo(500));
            Assert.That(result.Value, Is.InstanceOf<Exception>());
        });
    }

    [Test]
    public void CheckIfEmailExists_EmailDoesNotExist_ReturnsNotFound()
    {
        _service.Setup(action => action.CheckIfEmailExists(It.IsAny<string>())).Returns(false);
        var actionResult = controller.CheckIfEmailExists(It.IsAny<string>());
        Assert.Multiple(() => {
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.InstanceOf<NotFoundObjectResult>());
            var result = actionResult as NotFoundObjectResult;
            Assert.That(result!.Value, Is.EqualTo(false));
            Assert.That(result.StatusCode, Is.EqualTo(404));
        });
    }

    [Test]
    public void CheckIfEmailExists_EmailExist_ReturnsFound()
    {
        _service.Setup(action => action.CheckIfEmailExists(It.IsAny<string>())).Returns(true);
        var actionResult = controller.CheckIfEmailExists(It.IsAny<string>());
        Assert.Multiple(() => {
            Assert.That(actionResult, Is.Not.Null);
            Assert.That(actionResult, Is.InstanceOf<OkObjectResult>());
            var result = actionResult as OkObjectResult;
            Assert.That(result!.Value, Is.EqualTo(true));
            Assert.That(result.StatusCode, Is.EqualTo(200));
        });
    }
}