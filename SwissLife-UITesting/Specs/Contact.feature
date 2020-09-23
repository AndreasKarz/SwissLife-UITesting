@Base @Production
Feature: Homepage Contacts
	In order to 
		validate the contact possabilities
	As a 
		user
	I want to
		be sure, that i can contact the SwissLife

Background: Open the Homepage
	Given I open the Homepage
		And I confirm the disclaimer

Scenario: Check Mail form
		And I navigate to '/de/ueber-uns/kontakt.html'
	When I open the contact form for private customers
		And I fillout the form with valid infos
		And I submit the form
	Then I see the confirmation