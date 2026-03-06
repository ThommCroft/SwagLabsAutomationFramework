using AutomationFramework.Drivers;
using AutomationFramework.Drivers.Configuration;
using AutomationFramework.Support.Enums;
using Microsoft.Playwright;
using Reqnroll.BoDi;
using System.Text.RegularExpressions;

namespace AutomationFramework.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly PlaywrightDriverConfiguration _playwrightDriverConfiguration;

        private readonly IObjectContainer _objectContainer;
        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        private IPlaywright? _playwrightDriver;
        private IBrowser? _browser;
        private IBrowserContext? _browserContext;
        private IPage? _page;

        public Hooks(IObjectContainer objectContainer, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _playwrightDriverConfiguration = new PlaywrightDriverConfiguration(objectContainer);
            _objectContainer = objectContainer;
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void InitialiseExtentReports()
        {
            ExtentReporting.InitialiseExtentReporter();
            ExtentReporting.GetOperatingSystemInfo();
            ExtentReporting.InitialiseSparkReports();
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            // Set Headless to true before running in CI/CD pipeline.
            await _playwrightDriverConfiguration.DriverSetUp(browserType: BrowserTypeEnum.Chromium, testEnvironment: TestEnvironmentType.PREPROD, isHeadless: true, timeoutMilliseconds: 5000, slowMoMilliseconds: 500);

            _playwrightDriver = _playwrightDriverConfiguration.PlaywrightDriver;
            _browser = _playwrightDriverConfiguration.Browser;
            _browserContext = _playwrightDriverConfiguration.BrowserContext;
            _page = _playwrightDriverConfiguration.Page;

            _scenarioContext.Set<string>(data: _playwrightDriverConfiguration.CurrentURL, key: "CurrentURL");

            await _browserContext.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            ExtentReporting.GetBrowserInfo(_browser.BrowserType.Name);
            ExtentReporting.GetEnvironmentURLInfo(_playwrightDriverConfiguration.CurrentURL);
            ExtentReporting.CreateFeatureAndScenarioNodes(_featureContext, _scenarioContext);
        }

        [AfterStep]
        public void AfterStep()
        {
            ExtentReporting.ProcessStepResult(_playwrightDriverConfiguration, _featureContext, _scenarioContext);
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            var scenarioFailed = _scenarioContext.TestError != null;

            if (scenarioFailed)
            {
                var tracesDir = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "TestResults",
                    "Traces"
                );

                Directory.CreateDirectory(tracesDir);

                var fileName = $"{_scenarioContext.ScenarioInfo.Title}_{DateTime.Now:yyyyMMdd_HHmmss}.zip";
                var tracePath = Path.Combine(tracesDir, fileName);

                await _browserContext.Tracing.StopAsync(new()
                {
                    Path = tracePath
                });

                Console.WriteLine($"Trace saved to: {tracePath}");
            }
            else
            {
                // Stop without saving
                await _browserContext.Tracing.StopAsync();
            }

            await _playwrightDriverConfiguration.BrowserContext.CloseAsync();
            await _playwrightDriverConfiguration.Browser.CloseAsync();
            _playwrightDriverConfiguration.PlaywrightDriver?.Dispose();
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            ExtentReporting.FlushExtentReports();
        }
    }
}
