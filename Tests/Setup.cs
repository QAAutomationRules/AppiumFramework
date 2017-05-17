using System;
using TechTalk.SpecFlow;
using TestFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace Tests
{
    /// <summary>
    /// Spec Run start up to create the web driver, initialize log values, and get information from the Interface.
    /// </summary>
    [Binding]
    public class SpecRunSetup
    {
        public static bool TestPass = true;
        public static DateTime StartTime;
        public static string MyLog = "";
        public static DateTime MyTimeStamp;
        private static string _mainUrl = "";
        public static bool WriteToDb = true;

        /// <summary>
        /// All setup parameters run before each feature
        /// This includes creating a new web driver instance and getting values out of the app config.
        /// </summary>
        [BeforeFeature]
        public static void Startup()
        {
            Browser.StartWebDriver();
            _mainUrl = Browser.GetUrl();
            MyLog = Browser.GetLogFileName();
            MyTimeStamp = Browser.GetTimeStamp();
            BrowserFunctions.Goto(_mainUrl);
            WriteToDb = Browser.GetWriteToDb();
        }

        /// <summary>
        /// All setup parameters to run before each scenario
        /// Resets the TestPass variable to true and sets the start time stamp
        /// </summary>
        [BeforeScenario]
        public static void ScenarioSetup()
        {
            TestPass = true;
            StartTime = DateTime.Now;
        }

        /// <summary>
        /// Scenario Cleanup will be run after each scenario is executed.
        /// It will log the result to the database, clear the scenario context and create an assert for a failed test if needed.
        /// </summary>
        [AfterScenario]
        public static void ScenarioCleanup()
        {
            //Uncomment this out once the database information has been added to the LogResult method to enable logging each test result to a database
            //LogResult();
            ScenarioContext.Current.Clear();
            if (TestPass == false)
            {
                Assert.Fail("Test Case Failed");
            }
        }

        /// <summary>
        /// This method is used to write the test results of each test run to a database.
        /// Update the Server, Database(in the dbName variable), User Id, and Password field in the connection string below
        /// The SqlCommand will also need to be updated with the tablename and the columns adjusted to match the database
        /// </summary>
        public static void LogResult()
        {
            var si = ScenarioContext.Current.ScenarioInfo;
            var fi = FeatureContext.Current.FeatureInfo;
            var error = ScenarioContext.Current.TestError;
            const string dbName = "<databasename>";
            var errorText = "";
            string testResult;
            if (error != null) { errorText = error.Message; }
            if (TestPass && error == null) { testResult = "Pass"; }
            else { testResult = "Fail"; }
            if (WriteToDb)
            {
                using (var conn = new SqlConnection())
                {
                    conn.ConnectionString =
                        "Server=<server>;Database="+dbName+";User Id=<username>;Password=<password>";
                    conn.Open();
                    try
                    {
                        var command =
                            new SqlCommand(
                                "insert into dbo.<tableName> Values ('" + fi.Title + "','" + si.Title.Replace("'", "") + "',0,'" + MyTimeStamp +
                                "','" + StartTime + "','" + DateTime.Now + "','" +
                                errorText.Replace("\"", "") + "','" + _mainUrl + "','" + testResult + "','" + MyLog +
                                "','" + Browser.GetThreadID() + "','" + "0" + "')", conn);
                        Assert.IsTrue(command.ExecuteNonQuery() == 1);
                    }
                    catch (SqlException)
                    {
                        LogFunctions.WriteError("Did not get expected result from SQL.");
                        throw;
                    }
                    LogFunctions.WriteInfo("Result Logged to "+dbName+" Database");
                }
                
            }
        }

        /// <summary>
        /// Put here all clean up parameters that should run after the entire feature set is run
        /// </summary>
        [AfterFeature]
        public static void CleanUp()
        {
            Browser.Quit();
        }


    }
}
