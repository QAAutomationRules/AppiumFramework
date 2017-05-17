using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework
{
    /// <summary>
    /// The BrowserFunctions class contains calls to the browser that are common to perform on any page
    /// </summary>
    public static class BrowserFunctions
    {
        /// <summary>
        /// Have the driver go to a specified URL in the browser
        /// </summary>
        /// <param name="url">The URL to go to</param>
        public static void Goto(string url)
        {
            Browser.Goto(url);
        }
        /// <summary>
        /// Have the driver check the title of the webpage that the browser is on
        /// </summary>
        /// <param name="pageTitle">The title that is expected</param>
        /// <returns>true if the title is as expected, false if the title is different than expected</returns>
        public static bool CheckTitle(string pageTitle)
        {
            return Browser.Title == pageTitle;
        }
        /// <summary>
        /// Have the driver check that the title of the webpage that the browser is on contains specified text
        /// </summary>
        /// <param name="text">The text to check for in the title</param>
        /// <returns>true if the text is found, false if the text is not found</returns>
        public static bool CheckUrlContains(string text)
        {
            return Browser.Url.Contains(text);
        }
        /// <summary>
        /// Have the driver check for an error text on the page
        /// This text would need to be updated for the application under test
        /// </summary>
        /// <returns>true if the text is found, false if the text is not found</returns>
        public static bool CheckForError()
        {
            if (!Browser.SearchForString("An error has occurred")) return false;
            LogFunctions.WriteError("Error message found on page"); return true;
        }
        /// <summary>
        /// Select a specified value from a specified drop down
        /// There is redundant logic in this method so that if the specified text is not found in its entirety, 
        /// it will search for an element that contains the specified text.
        /// If now option in the drop down is found, an error will be logged and an exception thrown to stop the test.
        /// </summary>
        /// <param name="element">The drop down to use</param>
        /// <param name="expectedValue">The value to select</param>
        public static void SelectFromDropDown(IWebElement element, string expectedValue)
        {
            if (expectedValue == null){return;}
            var recievedValue = DoSelect(element, expectedValue);
            if (recievedValue != expectedValue) 
            {
                if (recievedValue.Contains(expectedValue)) { return; }
                LogFunctions.WriteWarning("Selected element not as expected. Recieved: " + recievedValue + " should have been: " + expectedValue);
                LogFunctions.WriteWarning("Attempting select one more time.");
                recievedValue = DoSelect(element, expectedValue);
                if (recievedValue != expectedValue)
                {
                    LogFunctions.WriteError("Selected element not as expected. Recieved: " + recievedValue + " should have been: " + expectedValue);
                    throw new Exception("Selected element not as expected"); 
                }
                    LogFunctions.WriteInfo(expectedValue + " has been selected.");
            }
            else
            {
                LogFunctions.WriteInfo(expectedValue + " has been selected.");
            }
        }
        /// <summary>
        /// This method is called by the SelectFromDropDown method and does the actual selection
        /// If no selection can be made, errors are logged and an exception is thrown to stop the test
        /// </summary>
        /// <param name="element">The drop down to use</param>
        /// <param name="expectedValue">The value to select</param>
        /// <returns>The text that was actually selected in the drop down</returns>
        private static string DoSelect(IWebElement element, string expectedValue)
        {
            Browser.WaitForElementDisplayed(element);
            Browser.WaitForElementEnabled(element);
            var selectedElement = new SelectElement(element);
            try
            {
                selectedElement.SelectByText(expectedValue);
            }
            catch (NoSuchElementException)
            {
                var allItems = selectedElement.Options;
                if (allItems.Count == 0) { throw new Exception("There are no options available to select in the drop down"); }
                foreach (var item in allItems.Where(item => item.Text.Contains(expectedValue)))
                {
                    try
                    {
                        selectedElement.SelectByText(item.Text);
                    }
                    catch (NoSuchElementException)
                    {
                        LogFunctions.WriteError("Can not find element with text: " + expectedValue);
                        LogFunctions.WriteError("As alternative, also attempted to select item with text: " + item.Text);
                        LogFunctions.WriteError("Select was unsuccessful");
                        Browser.TakeScreenshot("selectFromDropDown");
                        throw;
                    }
                    LogFunctions.WriteInfo("Item Selected was: " + item.Text);
                    return item.Text;
                }
            }
            return selectedElement.SelectedOption.Text;
        }
        /// <summary>
        /// Gets the option that is currently selected within a specified drop down in the browser
        /// If the method is unable to get the selected text, an error is written and an exception is thrown to stop the test
        /// </summary>
        /// <param name="element">The dropdown element to use</param>
        /// <returns>The selected text if the selection was successful</returns>
        public static string GetSelectedDropDownValue(IWebElement element)
        {
            Browser.WaitForElementDisplayed(element);
            string recievedValue;
            var selectedElement = new SelectElement(element);
            try
            {
                recievedValue = selectedElement.SelectedOption.Text;
            }
            catch
            {
                LogFunctions.WriteError("Could not get selected option");
                Browser.TakeScreenshot("selectedDropDown");
                throw;
            }
            return recievedValue;
        }
        /// <summary>
        /// This method will type a specified value into a specified text box element
        /// Error checking and validation are done to ensure that the expected value has been typed correctly into the text box
        /// If the text is unable to be entered into the text box, errors are written and an exception is thrown to stop the test
        /// </summary>
        /// <param name="element">The text box to type into</param>
        /// <param name="expectedValue">The text that should be entered</param>
        public static void Type(IWebElement element, string expectedValue)
        {
            if (expectedValue == null)
            {
                LogFunctions.WriteError("Did not get an expected value!");
                return;
            }
            if (expectedValue == "blank") { expectedValue = ""; }
            Browser.WaitForElementDisplayed(element);
            Browser.WaitForElementEnabled(element);
            element.Click();
            element.Clear();
            element.SendKeys(expectedValue.Replace("%", "").Trim());
            element.SendKeys(Keys.Tab);
            string recievedValue = element.GetAttribute("value");
            if (recievedValue != expectedValue)
            {
                LogFunctions.WriteWarning("Element value not as expected. Recieved: " + recievedValue + " should have been: " + expectedValue);
                throw new Exception("Typed element not as expected"); 
            }
            LogFunctions.WriteInfo(expectedValue + " has been typed into the element.");
        }
        /// <summary>
        /// Validates that the value of an element is empty
        /// Used with text box elements to validate no text is in the textbox
        /// </summary>
        /// <param name="element">The element to validate</param>
        /// <returns>True if the element is empty, false if there is text in the element or the value is unable to be found</returns>
        public static bool IsElementEmpty(IWebElement element)
        {
            Browser.WaitForElementDisplayed(element);
            string value;
            try { value = element.GetAttribute("value"); }
            catch (StaleElementReferenceException) { return false; }
            return value == "";
        }
        /// <summary>
        /// Gets the text of a specified element
        /// </summary>
        /// <param name="element">The element to get the text of</param>
        /// <returns>The text of the element</returns>
        public static string GetElementText(IWebElement element)
        {
            var text = "";
            while (true)
            {
                try { text = element.Text; }
                catch (StaleElementReferenceException) { }
                return text;
            }
        }
        /// <summary>
        /// The MultiSelectAdd method handles multi select controls, allowing the addition of one or more
        /// elements from the left side of the select to be added to the right side of the select
        /// The XPath within the item list may need to be changed based on the application under test 
        /// </summary>
        /// <param name="availableItemList">The left side of the multi select control, listing the available itmes</param>
        /// <param name="selectedItemList">The right side of the multi select control, listing the selected items</param>
        /// <param name="itemToSelect">The text of the item to be selected</param>
        /// <param name="addButton">The button that performs the addition of the selected element</param>
        public static void MultiSelectAdd(IWebElement availableItemList, IWebElement selectedItemList, string itemToSelect, IWebElement addButton)
        {
            Browser.WaitForElementDisplayed(availableItemList);
            var availableItems = availableItemList.FindElements(By.XPath("child::option"));
            Browser.WaitForElementDisplayed(addButton);

            foreach (var availableItem in availableItems.Where(availableItem => availableItem.Text == itemToSelect))
            {
                Browser.WaitForElementDisplayed(availableItem);
                try { availableItem.Click(); }
                catch (StaleElementReferenceException)
                {
                    Browser.WaitForElementDisplayed(availableItem);
                    try { availableItem.Click(); }
                    catch (Exception) { Browser.TakeScreenshot("AddElementError"); throw; }
                }
                Browser.WaitForElementDisplayed(addButton);
                addButton.Click();
            }
            var foundUserRole = false;
            Browser.WaitForElementDisplayed(selectedItemList);
            var selectedItems = selectedItemList.FindElements(By.XPath("child::option"));
            foreach (var selectedItem in selectedItems)
            {
                Browser.WaitForElementDisplayed(selectedItem);
                if (selectedItem.Text == itemToSelect)
                {
                    foundUserRole = true;
                }
            }
            if (foundUserRole == false)
            {
                LogFunctions.WriteError("Selected item not found: " + itemToSelect);
            }
        }
        /// <summary>
        /// The MultiSelectRemove method handles multi select controls, allowing the removal of one or more
        /// elements from the right side of the select, which will then be put back into the available items on the left side of the list
        /// The XPath within the item list may need to be changed based on the application under test 
        /// </summary>
        /// <param name="availableItemId">The left side of the multi select control, listing the available itmes</param>
        /// <param name="selectedItemId">The right side of the multi select control, listing the selected items</param>
        /// <param name="itemToSelect">The text of the item to be selected</param>
        /// <param name="removeButtonId">The button that performs the removal of the selected element</param>
        public static void MultiSelectRemove(string availableItemId, string selectedItemId, string itemToSelect, string removeButtonId)
        {
            LogFunctions.WriteInfo("Trying selected items list find element");
            var selectedItemList = Browser.FindElement_byId(selectedItemId);
            LogFunctions.WriteInfo("Trying remove button find element");
            var removeButton = Browser.FindElement_byCss(removeButtonId);

            LogFunctions.WriteInfo("Trying selected items list wait for element displayed");
            Browser.WaitForElementDisplayed(selectedItemList);
            LogFunctions.WriteInfo("Trying remove button wait for element displayed");
            Browser.WaitForElementDisplayed(removeButton);

            LogFunctions.WriteInfo("Trying to get selected items list elements");
            var selectedItems = selectedItemList.FindElements(By.XPath("child::option"));
            foreach (var selectedItem in selectedItems.Where(selectedItem => selectedItem.Text == itemToSelect))
            {
                LogFunctions.WriteInfo("Waiting for a selected item");
                Browser.WaitForElementDisplayed(selectedItem);
                try { LogFunctions.WriteInfo("clicking a selected item"); selectedItem.Click(); }
                catch (StaleElementReferenceException)
                {
                    LogFunctions.WriteInfo("stale element after trying to click");
                    return;
                }
                LogFunctions.WriteInfo("Waiting to click on the remove button");
                Browser.WaitForElementDisplayed(removeButton);
                LogFunctions.WriteInfo("Clicking on the remove button");
                removeButton.Click();
                break;
            }
            LogFunctions.WriteInfo("Trying available items list find element second time");
            var availableItemList = Browser.FindElement_byId(availableItemId);
            LogFunctions.WriteInfo("Trying selected items list find element second time");
            selectedItemList = Browser.FindElement_byId(selectedItemId);
            LogFunctions.WriteInfo("Trying remove button find element second time");
            removeButton = Browser.FindElement_byCss(removeButtonId);

            LogFunctions.WriteInfo("Trying available items list wait for element displayed second time");
            Browser.WaitForElementDisplayed(availableItemList);
            LogFunctions.WriteInfo("Trying selected items list wait for element displayed second time"); 
            Browser.WaitForElementDisplayed(selectedItemList);
            LogFunctions.WriteInfo("Trying remove button wait for element displayed second time");
            Browser.WaitForElementDisplayed(removeButton);
            LogFunctions.WriteInfo("Trying to get available items list elements");
            var availableItems = availableItemList.FindElements(By.XPath("child::option"));
            foreach (var availableItem in availableItems)
            {
                LogFunctions.WriteInfo("Waiting for an available item");
                Browser.WaitForElementDisplayed(availableItem);
                if (availableItem.Text != itemToSelect) continue;
                LogFunctions.WriteInfo("Returning available item is the same as the item to select");
                return;
            }
            LogFunctions.WriteError("Selected item not found: " + itemToSelect);
        }
        /// <summary>
        /// The Checkbox method provides the ability to check or uncheck a checkbox and validate that it is set correctly
        /// based on the expected value that is passed in
        /// </summary>
        /// <param name="element">The checkbox element</param>
        /// <param name="turnOn">True if the checkbox should be checked, false if the checkbox should be unchecked</param>
        /// <returns>True if the checkbox is set as expected, false if it is not set as expected</returns>
        public static bool Checkbox(IWebElement element, bool turnOn)
        {
            if (!element.Displayed){return false;}
            if (!element.Enabled){return false;}
            if (element.Selected && turnOn)
            {
                LogFunctions.WriteWarning("Checkbox already selected.  No changes made");
                return element.Selected;
            }
            if (element.Selected && !turnOn)
            {
                LogFunctions.WriteInfo("Checkbox has been un-selected.");
                element.Click();
                return !element.Selected;
            }
            if (!element.Selected && turnOn)
            {
                LogFunctions.WriteInfo("Checkbox has been selected.");
                element.Click();
                return element.Selected;
            }
            if (!element.Selected && !turnOn)
            {
                LogFunctions.WriteWarning("Checkbox already not selected.  No changes made");
                return !element.Selected;
            }
            LogFunctions.WriteError("Checkbox error"); Browser.TakeScreenshot("CheckboxStatusError");
            return false;
        }
        /// <summary>
        /// Gets the current state of a specified checkbox element
        /// </summary>
        /// <param name="element">The checkbox element to validate</param>
        /// <returns>True if the checkbox is checked, false if the checkbox can not be found or is unchecked</returns>
        public static bool GetCheckboxState(IWebElement element)
        {
            if (!element.Displayed) { return false; }
            if (!element.Enabled) { return false; }
            return element.Selected;
        }
    }
}