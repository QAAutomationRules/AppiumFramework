using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TestFramework;
using TestFramework.PageObjects;

namespace Tests.AmazonSearch
{
    /// <summary>
    /// This SpecFlow steps file shows how Data Injection can be used to pass variables between Steps files
    /// Allows the test writer to only specify a parameter once and allow the entire system to access it
    /// </summary>
    [Binding]
    public class AmazonSearchStepsContextInjection
    {
        /// <summary>
        /// This is how data injection gets parameters from the SpecFlow test case and saves them into data objects 
        /// that can later be used by Selenium or used by other SpecFlow methods
        /// For more information on Context Injection, go to: http://www.specflow.org/documentation/Context-Injection/
        /// </summary>
        private readonly AmazonData _amazonData;
        public AmazonSearchStepsContextInjection(AmazonData amazonData)
        {
            _amazonData = amazonData;
        }

        /// <summary>
        /// Enter in the search term that was stored into the data object
        /// </summary>
        [When(@"I enter the search term using Context Injection")]
        public void GivenIEnterSearchTerm()
        {
            Pages.AmazonSearchPage.TypeSearchTerm(_amazonData.searchTerm);
        }

        /// <summary>
        /// Check on the result title in the specified position on the page using the stored data
        /// </summary>
        [Then(@"Validate the search result using Context Injection")]
        public void ThenTheSecondResultShouldBe()
        {
            try
            {
                Assert.IsTrue(Pages.AmazonSearchPage.ValidateResult(_amazonData.searchResultNum, _amazonData.searchResultTitle));
            }
            catch (AssertFailedException)
            {
                throw new Exception("Result is not as expected, Failing test");
            }
        }
    }
}
