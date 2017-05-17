using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.PageObjects
{
    /// <summary>
    /// Sample page containing object mapping and associated methods
    /// This is based on the Amazon.com website
    /// </summary>
    public class AmazonSearchPage
    {
        [FindsBy(How = How.Id, Using = "twotabsearchtextbox")]
        private readonly IWebElement _searchbox = null;
        [FindsBy(How = How.ClassName, Using = "nav-input")]
        private readonly IWebElement _searchButton = null;
        [FindsBy(How = How.Id, Using = "s-results-list-atf")]
        private readonly IWebElement _resultsTable = null;

        public bool VerifyPageReady()
        {
            if (!Browser.IsElementPresent(_searchbox) || !Browser.IsElementPresent(_searchButton))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Type in the search term that has been passed in
        /// </summary>
        /// <param name="searchTerm"></param>
        public void TypeSearchTerm(string searchTerm)
        {
            BrowserFunctions.Type(_searchbox,searchTerm);
            LogFunctions.WriteInfo("Search Term " + searchTerm + " entered into website.");
        }

        /// <summary>
        /// Click on the search button
        /// </summary>
        public void ClickSearchButton()
        {
            Browser.Click(_searchButton);
            LogFunctions.WriteInfo("Search Button has been clicked.");
        }
        /// <summary>
        /// Validate that the result in the given position in the search results has the title that is expected
        /// </summary>
        /// <param name="resultNum">The position in the search results of the element to validate</param>
        /// <param name="resultTitle">The expected title of the element</param>
        /// <returns>True if the result is as expected, false if the result is not as expected</returns>
        public bool ValidateResult(int resultNum, string resultTitle)
        {
            //Get all the rows in the result table
            var rows = _resultsTable.FindElements(By.XPath("child::li"));
            //Get the title of the specified row
            var recievedTitle = rows[resultNum-1].FindElement(By.XPath("child::div/child::div/child::div/child::div/child::div/child::a/child::h2")).Text;
            if (recievedTitle == resultTitle)
            {
                LogFunctions.WriteInfo("Title of item " + resultNum + " in the list of results is as expected: " + recievedTitle);
                return true;
            }
            LogFunctions.WriteError("Title of item " + resultNum + " in the list of results is not as expected.");
            LogFunctions.WriteError("Expected Title: " + resultTitle);
            LogFunctions.WriteError("Title on the page was: " + recievedTitle);
            return false;
        }
    }
}
