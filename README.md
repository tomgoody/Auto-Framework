Basic framework to illustrate how to setup Selenium and Extent Reports using the Page Object Model(POM) in a C# environment.


The Automation-Cookbook folder is not used for running the code and is only included as a reference for how making the code more extensible was solved. 

In a C# environment, import the Config, PageMethods, and TestCases folders as well as the appsettings.json file. 

*appsettings.json file*
The appsettings.json is where all the environmental variables are contained.  It can contain the variables for multiple environments allowing for changing environnments to be completed quickly or a new environment to be added easier. 

*Configuration Folder*
Within the Configuration Folder is 2 files: Configuration.cs and ExtentManager.cs
The Configuration.cs file collects the variables from the appsettings.json file at startup.  This sets the environment the tests will be run, the login credentials, and environment specific variables. 

The ExtentManager.cs file collects the results of the test and compiles them into one report at the end.  Without this file to amend each test results to a single extent report each test run will create a new extent report upon completion and in some cases overwrite previous results. 

*PageMethods Folder*
The page methods folder is where the page elements and methods are stored. At the top of the CS file is the element that is then called by a method further down. This setup makes it easier to reuse elements and if an element changes it only requires being changed in one place for all tests utilizing it.

*TestCases Folder*
The test case files with the Test Cases folder is what pulls together everything described above and executes the test. The test case starts by collecting the environment variables set in the Configuration, then sets the test case variables in the Extent Report. The methods from the Page Methods folder are then called in order using the environment variables from the Configuration.  Once complete the test run is complete, the results are written to a single Extent Report with a datetime stamp, including screenshots at the end of each test for reference to review for false positive results. 
