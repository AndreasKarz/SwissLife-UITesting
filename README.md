# Automated E2E Testing Framework
DotNet Core solution with Specflow, Selenium, NUnit and Browserstack implementation. 

dotnet test /logger:trx SwissLife-UITesting.Tests.dll

## Test runner
Als Testrunner ist [NUnit](https://nunit.org/) implementiert, das dieses optimal mit den Test Pipelines von Microsoft Azure zusammen arbeitet. Dies betrifft vor allem die [Integration der Attachements](https://stackoverflow.com/questions/60389685/nunit-how-to-attach-screenshot-to-test-attachments-in-azure-pipeline).

## Konfiguration

### Applikation
Der testRunner wird im Hook (Hooks.cs) [BeforeFeature] konfiguriert
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#configuration-providers
https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1
https://www.youtube.com/watch?v=_2_qksdQKCE
Hierarchie => TODO

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