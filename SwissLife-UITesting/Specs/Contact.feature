@Base @Production
Feature: Homepage Contacts
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: Open the Homepage
	Given I open the Homepage
		And I confirm the disclaimer

Scenario: Check Mail form
		And I navigate to '/de/ueber-uns/kontakt.html'
	When I open the contact form for private customers
		And I fillout the form with valid infos
		And I submit the form
	Then I see the confirmation