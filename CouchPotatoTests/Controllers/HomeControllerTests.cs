using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using CouchPotato.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;

namespace CouchPotato.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void HomeControllerTest()
        {
            HomeController controller = new HomeController(null);
            Assert.IsNotNull(controller.Index());
            Assert.IsNotNull(controller.Privacy());
        }
    }
}