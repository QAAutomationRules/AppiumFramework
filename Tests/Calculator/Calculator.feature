Feature: Calculator
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
	Given I have entered 50 into the calculator

@calculator @add
Scenario: Add two numbers
	Description of the scenario goes here

	Associated Test Cases: Not Yet Written

	And I have entered 70 into the calculator
	When I press add
	Then the result should be 120 on the screen

@calculator @subtract
Scenario: Subtract two numbers
	This scenario subtracts two numbers

	Associated Test Cases: N/A

	And I have entered 10 into the calculator
	When I press subtract
	Then the result should be 40 on the screen
