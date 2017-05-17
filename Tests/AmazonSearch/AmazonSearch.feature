Feature: Amazon Search
	This is an example of using Specflow and Selenium to run tests against Amazon.com

@amazon @HardCodedURL
Scenario: Search Amazon - hardcoded URL
	This test will enter a search term on the Amazon.com website
	This hardcodes the url into the test case and will ignore any url chosen in the Interface
	It will then check to see that the title of the second result is as expected

	Associated Test Cases: 12345
	
	Given I go to http://www.amazon.com website
	And I enter SpecFlow into the search box
	When I press the Search button
	Then the number 1 result should be: The Cucumber Book: Behaviour-Driven Development for Testers and Developers (Pragmatic Programmers)

@amazon @URLinInterface
Scenario: Search Amazon - URL chosen in Interface
	This test will enter a search term on the Amazon.com website
	This will use the URL as chosen in the interface and is not hardcoded into the test case
	It will then check to see that the title of the second result is as expected

	Associated Test Cases: 97865
	
	Given I am on the Amazon home page
	And I enter SpecFlow into the search box
	When I press the Search button
	Then the number 1 result should be: The Cucumber Book: Behaviour-Driven Development for Testers and Developers (Pragmatic Programmers)

@amazon @DataInjection
Scenario: Search Amazon - Example of Data Injection
	This test will use data injection to save the variables passed in via SpecFlow so that they can be read by Selenium
	This test will enter a search term on the Amazon.com website
	This will use the URL as chosen in the interface and is not hardcoded into the test case
	It will then check to see that the title of the second result is as expected

	Associated Test Cases: 45678
	
	Given I am on the Amazon home page
		And I use search term: book about rain
		And I want to validate the number 1 result Title
		And I expect the returned title to be: Splish! Splash!: A Book About Rain (Amazing Science: Weather)
	When I enter the search term using Context Injection
		And I press the Search button
	Then Validate the search result using Context Injection

@amazon @MultipleSearches
Scenario Outline: Search Amazon - Multiple Searches
	This test will enter a search term from the list provided into the Amazon.com website
	This will use the URL as chosen in the interface and is not hardcoded into the test case
	It will then check to see that the title of the specified result is as expected

	Associated Test Cases: 1974
	
	Given I am on the Amazon home page
	And I enter <SearchTerm> into the search box
	When I press the Search button
	Then the number <ResultNum> result should be: <Title>

	Examples:
	| TestName                   | SearchTerm      | ResultNum | Title                                                                                              |
	| Search for Test            | Test            | 5         | Vision Test                                                                                        |
	| Search for SpecFlow        | SpecFlow        | 1         | The Cucumber Book: Behaviour-Driven Development for Testers and Developers (Pragmatic Programmers) |
	| Search for Book about Rain | book about rain | 3         | Rain Forests (Magic Tree House Research Guide)                                                     |