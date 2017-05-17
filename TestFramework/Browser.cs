using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;


namespace TestFramework
{
    /// <summary>
    /// The Browser class contains methods that perform functions directly with the Selenium WebDriver
    /// These overwrite some of the built in Selenium webdriver calls, adding additional validations around them
    /// to account for webpage wait times
    /// </summary>
    public class Browser
    {
        static IWebDriver _webDriver;
        static WebDriverWait _wait;
        private static int[] _pid;

        /// <summary>
        /// Gets the URL setting from the app.config
        /// </summary>
        /// <returns>The URL in the app.config</returns>
        public static string GetUrl()
        {
            return ConfigurationManager.AppSettings["URL"];
        }
        /// <summary>
        /// Gets the FileNamePart setting from the app.config
        /// This is what will be appended to the beginning of all SpecRun log files
        /// </summary>
        /// <returns>The FileNamePart from the app.config</returns>
        public static string GetFileNamePart()
        {
            return ConfigurationManager.AppSettings["FileNamePart"];
        }
        /// <summary>
        /// Gets the TimeStamp from the app.config
        /// This provides a common start time indicating when the entire run started
        /// Not just the time of the single test
        /// </summary>
        /// <returns>The TimeStamp from the app.config</returns>
        public static DateTime GetTimeStamp()
        {
            var date = ConfigurationManager.AppSettings["TimeStamp"];
            return Convert.ToDateTime(date);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLogFileName()
        {
            return ConfigurationManager.AppSettings["LogFile"];
        }
        public static string GetThreadID()
        {
            return ConfigurationManager.AppSettings["ThreadID"];
        }
        public static bool GetWriteToDb()
        {
            return Convert.ToBoolean(ConfigurationManager.AppSettings["WriteToDb"]);
        }
        public static string GetDatabaseName()
        {
            return ConfigurationManager.AppSettings["DatabaseName"];
        }
        public static void StartWebDriver()
        {
            var myBrowser = ConfigurationManager.AppSettings["browserType"].ToLower();
            DesiredCapabilities _dc;
            IEnumerable<int> pidsBefore;
            IEnumerable<int> pidsAfter;

           switch (myBrowser)
            {
                case "android":
                    Console.WriteLine("Connecting to Appium server");
                    _dc = new DesiredCapabilities();
                    _dc.SetCapability(CapabilityType.Platform, "Android");
                    _dc.SetCapability(CapabilityType.Version, "6.0");
                    _dc.SetCapability("Device", "Android");
                    _dc.SetCapability("deviceName", "Nexus_5:5554");
                    _dc.SetCapability(CapabilityType.BrowserName, "Chrome");
                    _webDriver = new AndroidDriver<IWebElement>(new Uri("http://127.0.0.1:4723/wd/hub"), _dc);
                    _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));
                    //pidsAfter = Process.GetProcessesByName("android").Select(p => p.Id);
                    break;
                case "ios":
                    _dc = new DesiredCapabilities();
                    _dc.SetCapability("platformName", "IOS");
                    _dc.SetCapability("platformVersion", "");
                    _dc.SetCapability("deviceName", "test");
                    _webDriver = new IOSDriver<IOSElement>(_dc);
                    _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));
                    pidsAfter = Process.GetProcessesByName("ios").Select(p => p.Id);
                    break;
                case "ie":
                    pidsBefore = Process.GetProcessesByName("iexplore").Select(p => p.Id);
                    _webDriver = new InternetExplorerDriver(Path.Combine(GetBasePath, @"Drivers\"));
                    _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));
                    pidsAfter = Process.GetProcessesByName("iexplore").Select(p => p.Id);
                    break;
                case "chrome":
                    ChromeOptions options = new ChromeOptions();
                    options.AddArguments("--disable-extensions");
                    pidsBefore = Process.GetProcessesByName("chrome").Select(p => p.Id);
                    _webDriver = new ChromeDriver(Path.Combine(GetBasePath, @"Drivers\"), options);
                    _wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
                    pidsAfter = Process.GetProcessesByName("chrome").Select(p => p.Id);
                    break;
                default:
                    {
                        
                    }
                    break;
            }

        }
        public static bool SearchForString(string text)
        {
            return _webDriver.PageSource.Contains(text);
        }
        public static string Title
        {
            get { return _webDriver.Title; }
        }
        public static ISearchContext Driver
        {
            get { return _webDriver; }
        }
        public static void Goto(string url)
        {
            double initialTimeout = _wait.Timeout.Seconds;
            _wait.Timeout = TimeSpan.FromSeconds(120);
            _webDriver.Url = url;
            _wait.Timeout = TimeSpan.FromSeconds(initialTimeout);
        }
        public static void Quit()
        {
            _webDriver.Close();
            _webDriver.Quit();
            foreach (var pid in _pid)
            {
                try
                {
                    Process.GetProcessById(pid);
                }
                catch (ArgumentException)
                {
                    LogFunctions.WriteInfo("Process " + pid + " completed sucessfully.");
                    continue;
                }
                try
                {
                    var handle = Process.GetProcessById(pid).MainWindowHandle;
                    if (handle.ToInt32() == 0)
                    {
                        LogFunctions.WriteWarning("Waiting for Process " + pid + " to complete on its own." + DateTime.Now);
                        Process.GetProcessById(pid).WaitForExit(300000);
                        LogFunctions.WriteWarning("Wait completed: " + DateTime.Now);
                        try { Process.GetProcessById(pid); }
                        catch (ArgumentException)
                        {
                            LogFunctions.WriteInfo("Process " + pid + " completed sucessfully.");
                            continue;
                        }
                        Process.GetProcessById(pid).Kill();
                        throw new ApplicationException("Manually killed this process");
                    }
                }
                catch (InvalidOperationException)
                {
                    try
                    {
                        Process.GetProcessById(pid).Kill();
                        throw new ApplicationException("Invalid Operation - Manually killed this process");
                    }
                    catch (ArgumentException) { LogFunctions.WriteInfo("Process " + pid + " completed sucessfully on second try."); }
                }
            }
        }
        public static string Url
        {
            get { return _webDriver.Url; }
        }
        public static void WaitForUrlContains(string expected)
        {
            try { _wait.Until(webDriver => Url.Contains(expected)); }
            catch (Exception)
            {
                LogFunctions.WriteError("Not on expected page: " + expected);
                TakeScreenshot("pageerror");
                throw;
            }
        }
        public static void WaitForElementEnabled(IWebElement element)
        {
            try { _wait.Until(webDriver => element.Enabled); }
            catch (StaleElementReferenceException) { if (!WaitForNotFoundElement_Enabled(element)) { LogFunctions.WriteError("Enabled - Stale Element Exception"); TakeScreenshot("elementNotFound"); throw; } }
            catch (NoSuchElementException) { if (!WaitForNotFoundElement_Enabled(element)) { LogFunctions.WriteError("Enabled - No Such Element Exception"); TakeScreenshot("elementNotFound"); throw; } }
            catch (WebDriverTimeoutException) { if (!WaitForNotFoundElement_Enabled(element)) { LogFunctions.WriteError("Enabled - Timeout Exception"); TakeScreenshot("elementNotFound"); throw; } }
        }
        private static bool WaitForNotFoundElement_Enabled(IWebElement element)
        {
            for (var i = 0; i < 5; i++)
            {
                try { _wait.Until(webDriver => element.Enabled); }
                catch (Exception)
                {
                    continue;
                }
                return true;
            }
            return false;
        }
        public static void WaitForElementDisplayed(IWebElement element)
        {
            try { _wait.Until(webDriver => element.Displayed); }
            catch (StaleElementReferenceException) { if (!WaitForNotFoundElement_Display(element)) { LogFunctions.WriteError("Wait for Element to be Displayed - Stale Element Exception"); TakeScreenshot("staleElementError"); throw; } }
            catch (NoSuchElementException) { if (!WaitForNotFoundElement_Display(element)) { LogFunctions.WriteError("Wait for Element to be Displayed - No Such Element Exception"); TakeScreenshot("elementNotFoundError"); throw; } }
            catch (WebDriverTimeoutException)
            {
                if (!WaitForNotFoundElement_Display(element))
                {
                    if (SearchForString("Server Error"))
                    {
                        LogFunctions.WriteError("Server Error Encountered");
                        TakeScreenshot("serverError");
                        Goto(Url);
                        if (SearchForString("Server Error"))
                        {
                            LogFunctions.WriteError("Refreshing the Page did not dismiss the error");
                            throw;
                        }
                    }
                    LogFunctions.WriteError("Wait for Element to be Displayed Timeout Exception");
                    TakeScreenshot("timeoutError");
                    throw;
                }
            }
        }
        public static void WaitForElementNoLongerDisplayed_byID(string elementId)
        {
            if (!IsElementPresent_byId(elementId))
            {
                return;
            }
            try
            {
                _wait.Until(webDriver => webDriver.FindElement(By.Id(elementId)).Displayed == false);
            }
            catch (StaleElementReferenceException)
            {
                if (IsElementPresent_byId(elementId))
                {
                    throw new ApplicationException("Element still present and should not be");
                }
            }
            catch (WebDriverTimeoutException)
            {
                if (IsElementPresent_byId(elementId))
                {
                    throw new ApplicationException("Element still present and should not be");
                }
            }
        }
        public static void WaitForElementNoLongerDisplayed_byCSS(string elementCss)
        {
            try
            {
                _wait.Until(webDriver => webDriver.FindElements(By.CssSelector(elementCss)).Count == 0);
            }
            catch (Exception) { LogFunctions.WriteError("Element is still displayed and should not be"); TakeScreenshot("elementStillShown"); throw; }
        }
        public static void WaitForElementNoLongerDisplayed_byClass(string elementClass)
        {
            try
            {
                _wait.Until(webDriver => webDriver.FindElements(By.ClassName(elementClass)).Count == 0);
            }
            catch (Exception) { LogFunctions.WriteError("Element is still displayed and should not be"); TakeScreenshot("elementStillShown"); throw; }
        }
        public static void WaitForElementNoLongerDisplayed_byID(string element, double timeoutSeconds)
        {
            double initialTimeout = _wait.Timeout.Seconds;
            _wait.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            try
            {
                _wait.Until(webDriver => webDriver.FindElements(By.Id(element)).Count == 0);
            }
            catch (Exception) { LogFunctions.WriteError("Element is still displayed and should not be"); TakeScreenshot("elementStillShown"); throw; }
            _wait.Timeout = TimeSpan.FromSeconds(initialTimeout);
        }

        public static void ChangeTimeout(double timeoutSeconds)
        {
            _wait.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
        }
        public static double GetTimeout()
        {
            return _wait.Timeout.Seconds;
        }
        private static bool WaitForNotFoundElement_Display(IWebElement element)
        {
            for (int i = 0; i < 5; i++)
            {
                try { _wait.Until(webDriver => element.Displayed); }
                catch (Exception)
                {
                    continue;
                }
                return true;
            }
            return false;
        }
        public static void WaitForElementDisplayed_byId(string elementName)
        {
            try { _wait.Until(webDriver => webDriver.FindElement(By.Id(elementName)).Displayed); }
            catch (StaleElementReferenceException) { WaitForElementDisplayed_byId(elementName); }
            catch (NoSuchElementException) { WaitForElementDisplayed_byId(elementName); }
            catch (WebDriverTimeoutException) { TakeScreenshot("timeoutById"); }
        }
        public static void WaitForElementDisplayed_byName(string elementName)
        {
            try { _wait.Until(webDriver => webDriver.FindElement(By.Name(elementName)).Displayed); }
            catch (StaleElementReferenceException) { WaitForElementDisplayed_byName(elementName); }
            catch (NoSuchElementException) { WaitForElementDisplayed_byName(elementName); }
            catch (WebDriverTimeoutException) { TakeScreenshot("timeoutByName"); }
        }
        public static bool WaitForElementDisplayed_byCss(string elementName)
        {
            var result = true;
            try { _wait.Until(webDriver => webDriver.FindElement(By.CssSelector(elementName)).Displayed); }
            catch (StaleElementReferenceException) { result = WaitForElementDisplayed_byCss(elementName); }
            catch (NoSuchElementException) { result = WaitForElementDisplayed_byCss(elementName); }
            catch (WebDriverTimeoutException) { result = false; }
            return result;
        }
        public static void WaitForElementDisplayed_byClass(string elementName)
        {
            try { _wait.Until(webDriver => webDriver.FindElement(By.ClassName(elementName)).Displayed); }
            catch (StaleElementReferenceException) { WaitForElementDisplayed_byClass(elementName); }
            catch (NoSuchElementException) { WaitForElementDisplayed_byClass(elementName); }
            catch (WebDriverTimeoutException) { TakeScreenshot("timeoutByClass"); }
        }
        public static bool WaitForElementDisplayed_byXPath(string path)
        {
            var result = true;
            try { _wait.Until(webDriver => webDriver.FindElement(By.XPath(path)).Displayed); }
            catch (StaleElementReferenceException) { WaitForElementDisplayed_byXPath(path); }
            catch (NoSuchElementException) { WaitForElementDisplayed_byXPath(path); }
            catch (WebDriverTimeoutException) { result = false; }
            return result;
        }
        public static string GetElementText_byXpath(string path)
        {
            return _webDriver.FindElement(By.XPath(path)).Text;
        }
        public static bool WaitForElementText(string path, string text)
        {
            try { _wait.Until(webDriver => webDriver.FindElement(By.XPath(path)).Text == text); }
            catch (StaleElementReferenceException) { return false; }
            catch (NoSuchElementException) { return false; }
            catch (WebDriverTimeoutException) { TakeScreenshot("timeoutText"); return false; }
            return true;
        }
        public static bool IsElementPresent(IWebElement element)
        {
            bool result;
            try
            {
                result = element.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            catch (StaleElementReferenceException)
            {
                return false;
            }
            catch (WebDriverException)
            {
                return false;
            }
            return result;
        }
        public static bool IsElementPresent_byXpath(string xpath)
        {
            bool result;
            try { result = Driver.FindElement(By.XPath(xpath)).Displayed; }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            return result;
        }
        public static bool IsElementEnabled(IWebElement element)
        {
            bool result;
            try { result = element.Enabled; }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            return result;
        }
        public static bool IsElementPresent_byLinkText(string elementName)
        {
            try { Driver.FindElement(By.LinkText(elementName)); }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            return true;
        }
        public static bool IsElementPresent_byClassName(string elementName)
        {
            try { Driver.FindElement(By.ClassName(elementName)); }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            return true;
        }
        public static bool IsElementPresent_byCssSelector(string elementName)
        {
            try { Driver.FindElement(By.CssSelector(elementName)); }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            return true;
        }
        public static bool IsElementPresent_byId(string elementName)
        {
            try { Driver.FindElement(By.Id(elementName)); }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            return true;
        }
        public static bool IsElementPresent_byName(string elementName)
        {
            try { Driver.FindElement(By.Name(elementName)); }
            catch (NoSuchElementException) { return false; }
            catch (StaleElementReferenceException) { return false; }
            return true;
        }
        public static void DragAndDrop(IWebElement element1, IWebElement element2)
        {
            WaitForElementEnabled(element1);
            WaitForElementEnabled(element2);
            var builder = new Actions(_webDriver);
            var dragAndDrop = builder.ClickAndHold(element1).MoveToElement(element2).Release(element2).Build();
            dragAndDrop.Perform();
        }
        public static ReadOnlyCollection<IWebElement> FindElements_byClass(string elementName)
        {
            return _webDriver.FindElements(By.ClassName(elementName));
        }
        public static ReadOnlyCollection<IWebElement> FindElements_byCss(string elementName)
        {
            return _webDriver.FindElements(By.CssSelector(elementName));
        }
        public static IWebElement FindElement_byCss(string element)
        {
            WaitForElementDisplayed_byCss(element);
            return _webDriver.FindElement(By.CssSelector(element));
        }
        public static IWebElement FindElement_byXPath(string element)
        {
            WaitForElementDisplayed_byXPath(element);
            return _webDriver.FindElement(By.XPath(element));
        }
        public static IWebElement FindElementNoWait_byXPath(string element)
        {
            return _webDriver.FindElement(By.XPath(element));
        }
        public static IWebElement FindElement_byClass(string element)
        {
            WaitForElementDisplayed_byClass(element);
            return _webDriver.FindElement(By.ClassName(element));
        }
        public static IWebElement FindElement_byId(string element)
        {
            WaitForElementDisplayed_byId(element);
            return _webDriver.FindElement(By.Id(element));
        }
        public static IWebElement FindElement_byId_nowait(string element)
        {
            return _webDriver.FindElement(By.Id(element));
        }
        public static IWebElement FindElement_byCss_nowait(string element)
        {
            return _webDriver.FindElement(By.CssSelector(element));
        }
        public static void SwitchToIframe(string frameName)
        {
            try
            {
                _webDriver.SwitchTo().Frame(frameName);
            }
            catch (NoSuchFrameException)
            {
                try
                {
                    _webDriver.SwitchTo().Frame(frameName);
                }
                catch (NoSuchFrameException)
                {
                    LogFunctions.WriteError("Could not switch into IFrame");
                    throw;
                }
            }
        }
        public static string GetPopUpUrl()
        {
            var mainHandle = _webDriver.CurrentWindowHandle;
            var handles = _webDriver.WindowHandles;
            foreach (var handle in handles)
            {
                if (mainHandle == handle)
                {
                    continue;
                }
                _webDriver.SwitchTo().Window(handle);
                break;
            }
            var result = Url;
            _webDriver.SwitchTo().Window(mainHandle);
            return result;
        }
        public static void ClosePopUp()
        {
            var mainHandle = _webDriver.CurrentWindowHandle;
            var handles = _webDriver.WindowHandles;
            foreach (var handle in handles)
            {
                if (mainHandle == handle)
                {
                    continue;
                }
                _webDriver.SwitchTo().Window(handle);
                _webDriver.Close();
                _webDriver.SwitchTo().Window(mainHandle);
                break;
            }
        }
        public static void SwitchToMainContent()
        {
            _webDriver.SwitchTo().DefaultContent();
        }
        public static void Hover(IWebElement element)
        {
            var action = new Actions(_webDriver);
            action.MoveToElement(element).Perform();
        }
        public static void RefreshPage()
        {
            Thread.Sleep(5000);
            var action = new Actions(_webDriver);
            action.SendKeys(Keys.F5).Perform();
        }
        public static void Click(IWebElement element)
        {
            var actions = new Actions(_webDriver);
            actions.MoveToElement(element, 0, -160).Perform();
            actions.Click(element).Perform();
        }
        public static void TakeScreenshot(string type)
        {
            var urlStrings = Url.Split('/');
            var siteName = urlStrings[3];
            try
            {
                var fileNameBase = string.Format("{0}_{1}_{2}",
                    siteName,
                    type,
                    DateTime.Now.ToString("yyyyMMdd_HHmmss"));

                var subDirectory = Path.Combine(Directory.GetCurrentDirectory(), siteName);
                if (!Directory.Exists(subDirectory))
                    Directory.CreateDirectory(subDirectory);

                var screenShotsDirectory = Path.Combine(subDirectory, "ScreenShots");
                if (!Directory.Exists(screenShotsDirectory))
                    Directory.CreateDirectory(screenShotsDirectory);
                var takesScreenshot = _webDriver as ITakesScreenshot;

                if (takesScreenshot == null) return;
                var screenshot = takesScreenshot.GetScreenshot();

                var screenshotFilePath = Path.Combine(screenShotsDirectory, fileNameBase + "_screenshot.png");

                screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);

                Console.WriteLine(@"Screenshot: {0}", new Uri(screenshotFilePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error while taking screenshot: {0}", ex);
            }

        }

        public static string GetBasePath
        {
            get
            {
                var basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                basePath = basePath.Substring(0, basePath.Length - 10);
                return basePath;
            }
        }

        #region

        public static Dictionary<String, Double> coords;

        public static void ResetCoordinates()
        {
            coords = new Dictionary<string, double>();
        }

        public static void AddCoordinate(string key, double value)
        {
            coords.Add(key, value);
        }

        public static void Execute(string command)
        {
            _webDriver.ExecuteJavaScript("mobile: " + command, coords);
        }

        public static void Tap(double X, double Y, int count = 1, double duration = .1)
        {
            coords = new Dictionary<string, double>();
            coords.Add("x", X);
            coords.Add("y", Y);
            coords.Add("tapCount", count);
            coords.Add("duration", duration);
            _webDriver.ExecuteJavaScript("mobile: swipe", coords);
        }

        public static void ScrollTo(string id)
        {
            var elementObject = new Dictionary<string, string>();
            elementObject.Add("element", id);
            _webDriver.ExecuteJavaScript("mobile: scrollTo", elementObject);
        }

        public static void ScrollTo(By xpath)
        {
            var elementObject = new Dictionary<string, By>();
            elementObject.Add("element", xpath);
            _webDriver.ExecuteJavaScript("mobile: scrollTo", elementObject);
        }

        public static void Swipe(double startX, double startY, double endX, double endY)
        {
            coords = new Dictionary<string, double>();
            coords.Add("startX", startX);
            coords.Add("startY", startY);
            coords.Add("endX", endX);
            coords.Add("endY", endY);
            _webDriver.ExecuteJavaScript("mobile: swipe", coords);
        }

        public static void SwipeDown()
        {
            coords = new Dictionary<string, double>();
            coords.Add("startX", 0.5);
            coords.Add("startY", 0.95);
            coords.Add("endX", 0.5);
            coords.Add("endY", 0.05);
            _webDriver.ExecuteJavaScript("mobile: swipe", coords);
        }

        public static void SwipeUp()
        {
            coords = new Dictionary<string, double>();
            coords.Add("startX", 0.5);
            coords.Add("startY", 0.05);
            coords.Add("endX", 0.5);
            coords.Add("endY", 0.95);
            _webDriver.ExecuteJavaScript("mobile: swipe", coords);
        }

        public static void SwipeRight()
        {
            coords = new Dictionary<string, double>();
            coords.Add("startX", 0.05);
            coords.Add("startY", 0.5);
            coords.Add("endX", 0.95);
            coords.Add("endY", 0.5);
            _webDriver.ExecuteJavaScript("mobile: swipe", coords);
        }

        public static void SwipeLeft()
        {
            coords = new Dictionary<string, double>();
            coords.Add("startX", 0.95);
            coords.Add("startY", 0.5);
            coords.Add("endX", 0.05);
            coords.Add("endY", 0.5);
            _webDriver.ExecuteJavaScript("mobile: swipe", coords);
        }


        #endregion


    }
}
