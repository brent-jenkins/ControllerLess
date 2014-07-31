namespace Anterec.ControllerLess.Tests.Mvc
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Anterec.ControllerLess.Mvc;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class ControllerLessHttpHandlerTests
    {
        private RequestContext _requestContext;

        private Mock<IControllerFactory> _controllerFactory;

        private Mock<HttpContextBase> _httpContext;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _httpContext = new Mock<HttpContextBase>();
        }

        [SetUp]
        public void SetUp()
        {
            _requestContext = new RequestContext();
            _controllerFactory = new Mock<IControllerFactory>();
            ControllerBuilder.Current.SetControllerFactory(_controllerFactory.Object);
        }

        [Test]
        public void SampleTest()
        {
            // Arrange
            _requestContext.RouteData = new RouteData();
            _requestContext.RouteData.Values["controller"] = "controller1";
            _requestContext.RouteData.Values["action"] = "action1";

            _controllerFactory.Setup(x => x.CreateController(_requestContext, "controller1")).Returns(new Mock<IController>().Object).Verifiable();

            var handler = new ControllerLessHttpHandler(_requestContext);

            // Act
            handler.ProcessRequest(_httpContext.Object);

            // Assert
            _controllerFactory.Verify(x => x.CreateController(_requestContext, "controller1"), Times.Once());
        }
    }
}
