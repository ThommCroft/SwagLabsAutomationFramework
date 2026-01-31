using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using System.Text.RegularExpressions;

namespace AutomationFramework.Drivers.Configuration
{
    public static class ExtentReporting
    {
        private static ExtentReports? _extentReports;
        private static ExtentTest? _scenario;

        // Before Test Run:
        public static void InitialiseExtentReporter()
        {
            _extentReports = new ExtentReports();
        }

        public static void GetOperatingSystemInfo()
        {
            _extentReports?.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }

        public static void InitialiseSparkReports()
        {
            // Test report file path: \SwagLabsAutomationFramework\AutomationFramework\bin\Debug\net8.0
            var spark = new ExtentSparkReporter("TestReport.html");
            _extentReports?.AttachReporter(spark);
        }
        
        // Before Scenario:
        public static void GetBrowserInfo(string browserName)
        {
            _extentReports?.AddSystemInfo("Browser", browserName);
        }
        public static void GetEnvironmentURLInfo(string environmentURL)
        {
            _extentReports?.AddSystemInfo("Environment URL", environmentURL);
        }

        public static void CreateFeatureAndScenarioNodes(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var feature = _extentReports!.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            _scenario = feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);
        }

        // After Step:
        public static void ProcessStepResult(PlaywrightDriverConfiguration playwrightDriverConfiguration, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError == null)
            {
                // Passing
                GetScenarioPassedInformation(scenarioContext);
            }
            else
            {
                // Failing
                Task.Run(async () => await GetScenarioFailedInformation(playwrightDriverConfiguration, featureContext, scenarioContext)).GetAwaiter().GetResult();
            }
        }

        // After Test Run:
        public static void FlushExtentReports()
        {
            _extentReports?.Flush();
        } 

        private static void GetScenarioPassedInformation(ScenarioContext scenarioContext)
        {
            switch (scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case Reqnroll.Bindings.StepDefinitionType.Given:
                    _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case Reqnroll.Bindings.StepDefinitionType.When:
                    _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                case Reqnroll.Bindings.StepDefinitionType.Then:
                    _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static async Task GetScenarioFailedInformation(PlaywrightDriverConfiguration playwrightDriverConfiguration, FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var fileName = $"{featureContext.FeatureInfo.Title.Trim()}_{Regex.Replace(scenarioContext.ScenarioInfo.Title, @"\s", "")}";

            switch (scenarioContext.StepContext.StepInfo.StepDefinitionType)
            {
                case Reqnroll.Bindings.StepDefinitionType.Given:
                    _scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, new ScreenCapture
                    {
                        Title = "Error Screenshot",
                        Path = await playwrightDriverConfiguration.TakeScreenshotAsPathAsync(fileName)
                    });
                    break;
                case Reqnroll.Bindings.StepDefinitionType.When:
                    _scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, new ScreenCapture
                    {
                        Title = "Error Screenshot",
                        Path = await playwrightDriverConfiguration.TakeScreenshotAsPathAsync(fileName)
                    });
                    break;
                case Reqnroll.Bindings.StepDefinitionType.Then:
                    _scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail(scenarioContext.TestError.Message, new ScreenCapture
                    {
                        Title = "Error Screenshot",
                        Path = await playwrightDriverConfiguration.TakeScreenshotAsPathAsync(fileName)
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
