# Automated E2E Testing Framework
DotNet Core solution with Specflow, Selenium, NUnit and Browserstack implementation. 

## Test runner
Als Testrunner ist [NUnit](https://nunit.org/) implementiert, das dieses optimal mit den Test Pipelines von Microsoft Azure zusammen arbeitet. Dies betrifft vor allem die [Integration der Attachements](https://stackoverflow.com/questions/60389685/nunit-how-to-attach-screenshot-to-test-attachments-in-azure-pipeline).

## Konfiguration

### Applikation
Die Konfiguration der Tests findet über die appsettings.json statt, welche beliebig erweitert werden kann. Verarbeitet werden die Einstellungen in der Hooks.cs. Die Konfiguration wird als JSON Objekt via Dependency Injection in den Object Container eingebunden. Wichtig: dies kann nur im Before Feature Hook erfolgen, ansonsten funktioniert Multitasking nicht mehr.

### BrowserStack Konfiguration
Die Konfiguration für BrowserStack wird über die appsettings.browserstack.json vorgenommen. Details dazu findest du auf [GitHub](https://github.com/AndreasKarz/FunkyBDD.SxS.Selenium.Browserstack).

### AssemblyInfo
In der AssemblyInfo.cs ist die Einstellung für NUnit, um die Tests parallel zu starten.

## Know-how

### BDD
[Example of a Discovery Workshop](https://www.youtube.com/watch?v=y1oD3cNXybc)
[BDD -- how create a example](https://www.youtube.com/watch?v=7qpSePQtQiM)
[The Big Ideas Behind BDD](https://www.youtube.com/watch?v=47HCnXbeDfc)
[Specflow & Selenium for Developers](https://www.youtube.com/watch?v=r4StJFaGpQs)

### Specflow
[Getting started](https://specflow.org/blog/getting-started-with-specflow-a-video-series/)
[Specflow Context Injection](https://docs.specflow.org/projects/specflow/en/latest/Bindings/Context-Injection.html)
[Working with Hooks](https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html)

### Selenium
[Selenium Browser Automation](https://www.selenium.dev/documentation/en/)
[Overview Of Selenium Locators](https://www.c-sharpcorner.com/article/overview-of-selenium-locators/)

### Atomic Design
https://bradfrost.com/blog/post/atomic-web-design/

### Browserstack
Coming soon...