Feature: Login
	As a user
	I want to be able to login site
	So I can do it from login form

@Bad
Scenario: Login is failed with wrong credentials
	Given User is on site
	When User clicks Sign in button
	And Enters "example@gmail.com" to user name input
	And Enters "zxcvbn" to password field
	And Clicks Sign In button on login form
	Then Login form has error "Wrong login or password"

@Admin
Scenario: Login is success with role "admin"
	Given User is on site 
	When User clicks Sign in button
	And Enters "qwerty" to user name input
	And Enters "qwerty" to password field
	And Clicks Sign In button on login form
	Then Logout from system
