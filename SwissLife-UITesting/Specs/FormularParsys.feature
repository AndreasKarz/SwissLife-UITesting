@Formulare @UAT
Feature: FormularParsys

Background: Open the Homepage
	Given I open the Homepage
		And I confirm the disclaimer

Scenario: Private bedingte E-Mail validieren
	When I select 'private-bedingte-e-mail'
		And I select input 'Betrifft-1554988802546_0'
		And I select input 'Anrede-1553703294075_1'
		And I select input 'Rueckruf-1553762364726_2'
		And I select input 'Thema-1553762364726_2'
		And I fillout the customer
		And I write 'Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qu' as message to 'Nachricht-1553176699112'
		And I submit the form
	Then The I see the confirmation

