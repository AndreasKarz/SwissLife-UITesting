@Components @Production
Feature: Alle Komponenten testen
	In order to 
		validate all components after a release
	As a 
		user
	I want to
		be sure, that are no changes on the components

Scenario: Alle Komponenten entsprechen den Original Screenshots
	Given I open the Homepage
	When I confirm the disclaimer
		And I open the component page
		And I remove the nav header
		And I collect all components
	Then Should found 77 components
		And All components should correspond to the basic image 