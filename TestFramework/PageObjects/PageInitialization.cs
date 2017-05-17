using OpenQA.Selenium.Support.PageObjects;

namespace TestFramework.PageObjects
{
    /// <summary>
    /// For each data object that is created, add an entry here to initialize the page the first time that it is called
    /// More informaiton on the Selenium Page Factory: https://code.google.com/p/selenium/wiki/PageFactory
    /// </summary>
    public static class Pages
    {
        /// <summary>
        /// This is just a sample page, to match the AmazonSearchPage.cs page object and act as an initializer
        /// </summary>
        public static AmazonSearchPage AmazonSearchPage
        {
            get
            {
                var searchPage = new AmazonSearchPage();
                PageFactory.InitElements(Browser.Driver, searchPage);
                return searchPage;
            }
        }
    }
}
