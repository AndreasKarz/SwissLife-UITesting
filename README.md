# Automated E2E Testing Framework
DotNet Core solution with Specflow, Selenium, NUnit and Browserstack implementation. 

## Test runner
Als Testrunner ist [NUnit](https://nunit.org/) implementiert, das dieses optimal mit den Test Pipelines von Microsoft Azure zusammen arbeitet. Dies betrifft vor allem die [Integration der Attachements](https://stackoverflow.com/questions/60389685/nunit-how-to-attach-screenshot-to-test-attachments-in-azure-pipeline).

## Konfiguration

### Applikation
Die Konfiguration der Tests findet �ber die appsettings.json statt, welche beliebig erweitert werden kann. Verarbeitet werden die Einstellungen in der Hooks.cs. Die Konfiguration wird als JSON Objekt via Dependency Injection in den Object Container eingebunden. Wichtig: dies kann nur im Before Feature Hook erfolgen, ansonsten funktioniert Multitasking nicht mehr.

### BrowserStack Konfiguration
Die Konfiguration f�r BrowserStack wird �ber die appsettings.browserstack.json vorgenommen. Details dazu findest du auf [GitHub](https://github.com/AndreasKarz/FunkyBDD.SxS.Selenium.Browserstack).

### AssemblyInfo
In der AssemblyInfo.cs ist die Einstellung f�r NUnit, um die Tests parallel zu starten.




