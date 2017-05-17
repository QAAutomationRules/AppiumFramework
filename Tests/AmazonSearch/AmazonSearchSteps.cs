using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TestFramework;
using TestFramework.PageObjects;

namespace Tests.AmazonSearch
{
    /// <summary>
    /// This is an example of a SpecFlow steps file
    /// This connects the SpecFlow statements to the code behind, ie. Selenium
    /// All feature files have access to all of the steps files, therefore, the regex for each step needs to be unique
    /// </summary>
    [Binding]
    public class AmazonSearchSteps
    {
        /// <summary>
        /// This is how data injection gets parameters from the SpecFlow test case and saves them into data objects 
        /// that can later be used by Selenium or used by other SpecFlow methods
        /// For more information on Context Injection, go to: http://www.specflow.org/documentation/Context-Injection/
        /// </summary>
        private readonly AmazonData _amazonData;

        public AmazonSearchSteps(AmazonData amazonData)
        {
            _amazonData = amazonData;
        }

        /// <summary>
        /// Go to the specified website
        /// </summary>
        /// <param name="siteUrl"></param>
        [Given(@"I go to (.*) website")]
        public void GivenIGoToWebsite(string siteUrl)
        {
            Browser.Goto(siteUrl);
            GivenIAmOnPage(siteUrl);
        }
        
        /// <summary>
        /// Validate the browser is on the page that is expected
        /// </summary>
        /// <param name="page">string expected in the URL</param>
        [Given(@"I am on the (.*) page")]
        public void GivenIAmOnPage(string page)
        {
            if (page.Contains("amazon"))
            {
                try
                {
                    Assert.IsTrue(Pages.AmazonSearchPage.VerifyPageReady());
                }
                catch (AssertFailedException)
                {
                    throw new Exception("Not on Amazon homepage");
                }
            }
        }

        /// <summary>
        /// Enter in the search term provided
        /// </summary>
        /// <param name="searchTerm">Term used in the search</param>
        [Given(@"I enter (.*) into the search box")]
        public void GivenIEnterIntoTheSearchBox(string searchTerm)
        {
            Pages.AmazonSearchPage.TypeSearchTerm(searchTerm);
        }

        /// <summary>
        /// Click on the button to perform the search
        /// </summary>
        [When(@"I press the Search button")]
        public void WhenIPressSearch()
        {
            Pages.AmazonSearchPage.ClickSearchButton();
        }
        
        /// <summary>
        /// Check on the result title in the specified position on the page
        /// </summary>
        /// <param name="resultNum">Number of result to look at</param>
        /// <param name="resultTitle">Title of the result</param>
        [Then(@"the number (.*) result should be: (.*)")]
        public void ThenTheSecondResultShouldBe(int resultNum, string resultTitle)
        {
            try
            {
                Assert.IsTrue(Pages.AmazonSearchPage.ValidateResult(resultNum, resultTitle));
            }
            catch (AssertFailedException)
            {
                throw new Exception("Result is not as expected, Failing test");
            }
        }

        //The below methods are used to provide an example of context injection, passing values provided in the 
        //SpecFlow test between different SpecFlow Steps binding classes
        
        /// <summary>
        /// Store the provided search term
        /// </summary>
        /// <param name="searchTerm">Term used in the search</param>
        [Given(@"I use search term: (.*)")]
        public void GivenIUseSearchTerm(string searchTerm)
        {
            _amazonData.searchTerm = searchTerm;
            LogFunctions.WriteInfo("Stored Search Term: " + _amazonData.searchTerm);
        }

        /// <summary>
        /// Store the result number to be validated
        /// </summary>
        /// <param name="resultNum">The number of the item in the returned list to validate</param>
        [Given(@"I want to validate the number (.*) result Title")]
        public void GivenIwantToValidateTheSpecifiedTitle(int resultNum)
        {
            _amazonData.searchResultNum = resultNum;
            LogFunctions.WriteInfo("Stored Result Number: " + _amazonData.searchResultNum);
        }
        
        /// <summary>
        /// Store the expected title for the result being validated
        /// </summary>
        /// <param name="resultTitle">The expected title of the specified search result</param>
        [Given(@"I expect the returned title to be: (.*)")]
        public void GivenIExpectTheReturnedTitleToBe(string resultTitle)
        {
            _amazonData.searchResultTitle = resultTitle;
            LogFunctions.WriteInfo("Stored Result Title: " + _amazonData.searchResultTitle);
        }
    }
}
