Feature: Institution
	As an administator
	I want to be able to fully manipulate on Institution functionality
	So I can do it when Venue exist and free for event

Background: Sign in
	Given User is on site 
	And User clicks Sign in button
	And Enters "qwerty" to user name input
	And Enters "qwerty" to password field
	And Clicks Sign In button on login form

# TODO: All repeated parameters should be outlined. See https://stackoverflow.com/questions/25388438/specflow-scenario-outline-examples
Scenario: Create Institution
	When User goes to institution page
	And Clicks on the create button
	And Enters Test name to Name input
	And Enters today's date - 1 year to Date input
	And Enters Test Address to Address input
	And Enters Test City to City input
	And Enters 00:30:00 to Expected Delivery Time input
	And Clicks on create button
	Then User clicks on the details of institution with Test name name
	And Sees that Name the same with Test name
	And Sees that Address the same with Test Address
	And Sees that Expected Delivery Time the same with 00:30:00
	And Logout from system