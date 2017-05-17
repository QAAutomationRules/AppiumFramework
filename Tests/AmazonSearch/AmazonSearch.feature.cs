﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.1.0.0
//      SpecFlow Generator Version:2.0.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Tests.AmazonSearch
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [TechTalk.SpecRun.FeatureAttribute("Amazon Search", Description="\tThis is an example of using Specflow and Selenium to run tests against Amazon.co" +
        "m", SourceFile="AmazonSearch\\AmazonSearch.feature", SourceLine=0)]
    public partial class AmazonSearchFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AmazonSearch.feature"
#line hidden
        
        [TechTalk.SpecRun.FeatureInitialize()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Amazon Search", "\tThis is an example of using Specflow and Selenium to run tests against Amazon.co" +
                    "m", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [TechTalk.SpecRun.FeatureCleanup()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        [TechTalk.SpecRun.ScenarioCleanup()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Search Amazon - hardcoded URL", new string[] {
                "amazon",
                "HardCodedURL"}, Description=@"	This test will enter a search term on the Amazon.com website
	This hardcodes the url into the test case and will ignore any url chosen in the Interface
	It will then check to see that the title of the second result is as expected

	Associated Test Cases: 12345", SourceLine=4)]
        public virtual void SearchAmazon_HardcodedURL()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Amazon - hardcoded URL", new string[] {
                        "amazon",
                        "HardCodedURL"});
#line 5
this.ScenarioSetup(scenarioInfo);
#line 12
 testRunner.Given("I go to http://www.amazon.com website", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 13
 testRunner.And("I enter SpecFlow into the search box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.When("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
 testRunner.Then("the number 1 result should be: The Cucumber Book: Behaviour-Driven Development fo" +
                    "r Testers and Developers (Pragmatic Programmers)", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Search Amazon - URL chosen in Interface", new string[] {
                "amazon",
                "URLinInterface"}, Description=@"	This test will enter a search term on the Amazon.com website
	This will use the URL as chosen in the interface and is not hardcoded into the test case
	It will then check to see that the title of the second result is as expected

	Associated Test Cases: 97865", SourceLine=17)]
        public virtual void SearchAmazon_URLChosenInInterface()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Amazon - URL chosen in Interface", new string[] {
                        "amazon",
                        "URLinInterface"});
#line 18
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("I am on the Amazon home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
 testRunner.And("I enter SpecFlow into the search box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.When("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then("the number 1 result should be: The Cucumber Book: Behaviour-Driven Development fo" +
                    "r Testers and Developers (Pragmatic Programmers)", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Search Amazon - Example of Data Injection", new string[] {
                "amazon",
                "DataInjection"}, Description=@"	This test will use data injection to save the variables passed in via SpecFlow so that they can be read by Selenium
	This test will enter a search term on the Amazon.com website
	This will use the URL as chosen in the interface and is not hardcoded into the test case
	It will then check to see that the title of the second result is as expected

	Associated Test Cases: 45678", SourceLine=30)]
        public virtual void SearchAmazon_ExampleOfDataInjection()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Amazon - Example of Data Injection", new string[] {
                        "amazon",
                        "DataInjection"});
#line 31
this.ScenarioSetup(scenarioInfo);
#line 39
 testRunner.Given("I am on the Amazon home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
  testRunner.And("I use search term: book about rain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
  testRunner.And("I want to validate the number 1 result Title", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
  testRunner.And("I expect the returned title to be: Splish! Splash!: A Book About Rain (Amazing Sc" +
                    "ience: Weather)", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.When("I enter the search term using Context Injection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 44
  testRunner.And("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.Then("Validate the search result using Context Injection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        public virtual void SearchAmazon_MultipleSearches(string testName, string searchTerm, string resultNum, string title, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "amazon",
                    "MultipleSearches"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Amazon - Multiple Searches", @__tags);
#line 48
this.ScenarioSetup(scenarioInfo);
#line 55
 testRunner.Given("I am on the Amazon home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 56
 testRunner.And(string.Format("I enter {0} into the search box", searchTerm), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.When("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 58
 testRunner.Then(string.Format("the number {0} result should be: {1}", resultNum, title), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Search Amazon - Multiple Searches, Search for Test", new string[] {
                "amazon",
                "MultipleSearches"}, SourceLine=61)]
        public virtual void SearchAmazon_MultipleSearches_SearchForTest()
        {
            this.SearchAmazon_MultipleSearches("Search for Test", "Test", "5", "Vision Test", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Search Amazon - Multiple Searches, Search for SpecFlow", new string[] {
                "amazon",
                "MultipleSearches"}, SourceLine=61)]
        public virtual void SearchAmazon_MultipleSearches_SearchForSpecFlow()
        {
            this.SearchAmazon_MultipleSearches("Search for SpecFlow", "SpecFlow", "1", "The Cucumber Book: Behaviour-Driven Development for Testers and Developers (Pragm" +
                    "atic Programmers)", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.ScenarioAttribute("Search Amazon - Multiple Searches, Search for Book about Rain", new string[] {
                "amazon",
                "MultipleSearches"}, SourceLine=61)]
        public virtual void SearchAmazon_MultipleSearches_SearchForBookAboutRain()
        {
            this.SearchAmazon_MultipleSearches("Search for Book about Rain", "book about rain", "3", "Rain Forests (Magic Tree House Research Guide)", ((string[])(null)));
#line hidden
        }
        
        [TechTalk.SpecRun.TestRunCleanup()]
        public virtual void TestRunCleanup()
        {
            TechTalk.SpecFlow.TestRunnerManager.GetTestRunner().OnTestRunEnd();
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.1.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class AmazonSearchFeature_MsTest
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AmazonSearch.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Amazon Search", "\tThis is an example of using Specflow and Selenium to run tests against Amazon.co" +
                    "m", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "Amazon Search")))
            {
                Tests.AmazonSearch.AmazonSearchFeature_MsTest.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Search Amazon - hardcoded URL")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Amazon Search")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("amazon")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("HardCodedURL")]
        public virtual void SearchAmazon_HardcodedURL()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Amazon - hardcoded URL", new string[] {
                        "amazon",
                        "HardCodedURL"});
#line 5
this.ScenarioSetup(scenarioInfo);
#line 12
 testRunner.Given("I go to http://www.amazon.com website", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 13
 testRunner.And("I enter SpecFlow into the search box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
 testRunner.When("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 15
 testRunner.Then("the number 1 result should be: The Cucumber Book: Behaviour-Driven Development fo" +
                    "r Testers and Developers (Pragmatic Programmers)", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Search Amazon - URL chosen in Interface")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Amazon Search")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("amazon")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("URLinInterface")]
        public virtual void SearchAmazon_URLChosenInInterface()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Amazon - URL chosen in Interface", new string[] {
                        "amazon",
                        "URLinInterface"});
#line 18
this.ScenarioSetup(scenarioInfo);
#line 25
 testRunner.Given("I am on the Amazon home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 26
 testRunner.And("I enter SpecFlow into the search box", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 27
 testRunner.When("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 28
 testRunner.Then("the number 1 result should be: The Cucumber Book: Behaviour-Driven Development fo" +
                    "r Testers and Developers (Pragmatic Programmers)", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Search Amazon - Example of Data Injection")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Amazon Search")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("amazon")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("DataInjection")]
        public virtual void SearchAmazon_ExampleOfDataInjection()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Amazon - Example of Data Injection", new string[] {
                        "amazon",
                        "DataInjection"});
#line 31
this.ScenarioSetup(scenarioInfo);
#line 39
 testRunner.Given("I am on the Amazon home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
  testRunner.And("I use search term: book about rain", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
  testRunner.And("I want to validate the number 1 result Title", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 42
  testRunner.And("I expect the returned title to be: Splish! Splash!: A Book About Rain (Amazing Sc" +
                    "ience: Weather)", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 43
 testRunner.When("I enter the search term using Context Injection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 44
  testRunner.And("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
 testRunner.Then("Validate the search result using Context Injection", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        public virtual void SearchAmazon_MultipleSearches(string testName, string searchTerm, string resultNum, string title, string[] exampleTags)
        {
            string[] @__tags = new string[] {
                    "amazon",
                    "MultipleSearches"};
            if ((exampleTags != null))
            {
                @__tags = System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Concat(@__tags, exampleTags));
            }
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Search Amazon - Multiple Searches", @__tags);
#line 48
this.ScenarioSetup(scenarioInfo);
#line 55
 testRunner.Given("I am on the Amazon home page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 56
 testRunner.And(string.Format("I enter {0} into the search box", searchTerm), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 57
 testRunner.When("I press the Search button", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 58
 testRunner.Then(string.Format("the number {0} result should be: {1}", resultNum, title), ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Search Amazon - Multiple Searches: Search for Test")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Amazon Search")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("amazon")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("MultipleSearches")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Search for Test")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:TestName", "Search for Test")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:SearchTerm", "Test")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ResultNum", "5")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Title", "Vision Test")]
        public virtual void SearchAmazon_MultipleSearches_SearchForTest()
        {
            this.SearchAmazon_MultipleSearches("Search for Test", "Test", "5", "Vision Test", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Search Amazon - Multiple Searches: Search for SpecFlow")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Amazon Search")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("amazon")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("MultipleSearches")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Search for SpecFlow")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:TestName", "Search for SpecFlow")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:SearchTerm", "SpecFlow")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ResultNum", "1")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Title", "The Cucumber Book: Behaviour-Driven Development for Testers and Developers (Pragm" +
            "atic Programmers)")]
        public virtual void SearchAmazon_MultipleSearches_SearchForSpecFlow()
        {
            this.SearchAmazon_MultipleSearches("Search for SpecFlow", "SpecFlow", "1", "The Cucumber Book: Behaviour-Driven Development for Testers and Developers (Pragm" +
                    "atic Programmers)", ((string[])(null)));
#line hidden
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Search Amazon - Multiple Searches: Search for Book about Rain")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "Amazon Search")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("amazon")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("MultipleSearches")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("VariantName", "Search for Book about Rain")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:TestName", "Search for Book about Rain")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:SearchTerm", "book about rain")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:ResultNum", "3")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("Parameter:Title", "Rain Forests (Magic Tree House Research Guide)")]
        public virtual void SearchAmazon_MultipleSearches_SearchForBookAboutRain()
        {
            this.SearchAmazon_MultipleSearches("Search for Book about Rain", "book about rain", "3", "Rain Forests (Magic Tree House Research Guide)", ((string[])(null)));
#line hidden
        }
    }
}
#pragma warning restore
#endregion
