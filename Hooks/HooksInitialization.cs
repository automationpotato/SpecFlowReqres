using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowReqres.Support;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SpecFlowReqres.Hooks
{
    [Binding]
    public sealed class HooksInitialization
    {
        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            TestContext.WriteLine("Runnin before scenario...");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            TestContext.WriteLine("Running after scenario...");
        }
    }
}