# Alias
Agnostic Test Automation Framework using the Page Object pattern

This Framework is intended to be a wrapper around UI test automation drivers and unit test frameworks.

Current implementation only supports Selenium and XUnit but can be expanded as needed.


Revolves around using aliasing to build pages and elements that represent the application under test. Can be used to compose ui automation frameworks for small or large scale web sites/applications.


Install core framework with:

>PM> Install-Package Aliases.Common


Install selenium driver wrappers:

>PM> Install-Package Aliases.Drivers.Selenium


Install XUnit executor wrappers:

>PM> Install-Package Aliases.TestExecutors.XUnit


Check out [Getting Started](https://github.com/Jarga/Alias/wiki/Getting-Started) for a basic overview.
