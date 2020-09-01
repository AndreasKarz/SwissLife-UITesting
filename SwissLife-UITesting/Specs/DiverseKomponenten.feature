@screenshots
Feature: DiverseKomponenten

Background: Open the Homepage
	Given I open the Homepage

Scenario: Alle Komponenten mit Screenshots abdecken
	When I confirm the disclaimer
	Then Die Screenshots aller Komponenten stimmen mit der Baseline überein
	