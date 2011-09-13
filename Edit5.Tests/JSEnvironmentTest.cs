using Edit5.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using IronJS.Hosting;

namespace Edit5.Tests
{
    /// <summary>
    /// This is a test class for JSEnvironmentTest and is intended
    /// to contain all JSEnvironmentTest Unit Tests
    /// </summary>
    [TestClass()]
    public class JSEnvironmentTest
    {
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext { get; set; }

        /// <summary>
        /// A test for Main
        /// </summary>
        [TestMethod()]
        [DeploymentItem("Edit5.Core.dll")]
        public void MainTest()
        {
            JSEnvironment.Initialize();
            Assert.IsNotNull(JSEnvironment.Main);
        }
    }
}
