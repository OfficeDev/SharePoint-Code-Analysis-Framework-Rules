How to develop custom rules for SPCAF
=====================================
SPCAF provides a Software Development Kit (SDK) to allow you to create your own rules, metrics or dependency checks. With the SDK you can create a new library with rules which can be dropped into the SPCAF installation and will be used automatically.

Create custom rules, metrics, dependency or inventory checks
------------------------------------------------------------
To create your custom analyzer follow these steps:

1. Create a new project of type "Class Library" in Visual Studio 2010/2012/2013/2014
2. Ensure that TargetFrameworkVersion is "v4.0" and Plattform "AnyCPU"
3. Add a reference to assembly SPCAF.Sdk.dll (can be found in SPCAF installation directory)
4. If you are developing rules checking .net code, add a reference to assembly mono.cecil.dll (can be found in SPCAF installation directory)  
5. Open the "AssemblyInfo.cs file and add following line to describe your rule package 

        assembly:SPCAF.Sdk.Attributes.AnalyzerInformation("My Custom Rules", AnalyzerDescription = "My custom rules description", AnalyzerAuthor = "My Name", AnalyzerUrl = "http://www.spcaf.com") 

6. Create a new class, e.g. "MyFirstRule.cs"
7. In the class override the "Visit" method and decide which type of SharePoint element you want to analyze (e.g. "FetureDefintion", "SolutionDefinition" etc.)
8. Build your project and copy your library to the installation directory of SPCAF (root directory). During next analysis your assembly will be loaded.

Metadata
--------
To integrate your extension into SPCAF the following metadata must be provided as attributes of your rules, metrics etc. The following table describes the metadata in detail:

    ...
    [RuleMetadata(typeof(MyCustomRule.Naming),
        CheckId = "SPC082199",
        DisplayName = "Feature name should start 'MyCompany'",
        Description = "A feature name should be prefixed with the name 'MyCompany'.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "Feature '{0}' should start with 'MyCompany'.",
        Resolution = "Change the folder of the Feature. The name must start with 'MyCompany'.",
        Links = new string[] {
            "Documentation Link 1", "http://...",
            "Documentation Link 2", "http://..." })]
    public class MyCustomRules : Rule
    {
        public override void Visit(FeatureDefinition target, NotificationCollection notifications)
        {
            // code of your custom rule...     
        }
    }
    ...

Name              |  Description
----------------- | ------------
Category          | Defines in which category of the SPCAF reports your results should appear (e.g. 'Naming').
CheckId           | Sefines the unique ID of your rule or metric etc. The Id should start with 3 characters, like 'SPC' (for 'SharePoint Code Check'), followed by 6 digits. Follow the SPCAF Conventions for CheckId for your own CheckId. Sample: 'SPC082199'
DisplayName       | Defines the display name in reports.
Description       | Defines a description (e.g. reasons for the rule).
DefaultSeverity   | Defines the severity ('CriticalError', 'Error', 'CriticalWarning', 'Warning' or 'Info'). For rules see Rules Severity for a description of the severity.
SharePointVersion | Defines for which SharePoint versions the extension applies. "12", "14" and "15" are supported values.
Message           | Defines the message template.
Resolution        | Provides a description of steps to solve the issue.
Links             | List of further hyperlinks to help solving the issue. A single link contains of 2 entries in the list, title and url of the link.

Debugging
---------
For debugging of your library in Visual Studio you can use the command line tools of SPCAF, e.g. 'spcop.exe'.  You have to attach the debugger of Visual Studio to one of these comannd line tools and finally can step through the code of our custom rule. Depending on the type of your custom SPCAF extension choose the appropriate command line tool:

* 'spcop.exe' for rule extensions
* 'spdepend.exe' for dependency extensions
* 'spmetrics.exe' for metric extensions
* 'spinventory.exe' for inventory extensions

To configure debugging follow these steps:

1. Copy the folder with the SPCAF files ('C:\Program Files (x86)\SPCAF') to a temporary folder, e.g. 'D:\temp\SPCAFDebug'. This is recommended to avoid damages of the SPCAF installation.
2. Go to the project settings in Visual Studio and select 'Build Events'. Add the post build command 'copy $(TargetPath) "D:\Temp\SPCAFDebug\"$(TargetFileName)' to copy your assembly to the temporary folder.
3. In project settings select 'Debug', select 'Start external program' and enter 'D:\Temp\SPCAFDebug\spcop.exe'.
4. In 'Command line arguments' enter '-i "[DirectoryWithWSPFiles]" -r "HTML;XML" -o "D:\Temp\SPCAFDebug\SPCop.html"'.
5. In 'Working directory' enter 'D:\Temp\SPCAFDebug'.
6. Add a break point and hit 'F5'

If configured correctly the SPCAF command line tool will start in a command window and your break point will be entered.

Logging
-------

During the analysis SPCAF writes log messages to a log file. Sometimes it may be necessary for your custom rule to write to the log file too. This can be done via SPCAF.Sdk.LoggingService. See sample below:

    using SPCAF.Sdk.Logging;
    ...
    namespace MyCustomRules
    {
      public class MyCustomRule : Rule<SolutionDefinition>
      {
        public override void Visit(SolutionDefinition target, NotificationCollection notifications)
        {
          LoggingService.Log(LogLevel.Debug, "My custom message");       ...
        }
      }
    }

If it is necessary to write message to the output (e.g. to the output window in Visual Studio or to the output of command line tools) you can use the following code:

    LoggingService.Status("My message which should be visible to the user");

Deployment
----------

To allow SPCAF to load your custom rules or metrics you need to copy your custom assembly into the installation directory of SPCAF (typically 'C:\Program Files (x86)\SPCAF\'. SPCAF will load your assembly during the next analysis.

Samples
-------
Use the following samples as a starting point for your own SharePoint code analyzer:

* Rule Sample
* Metric Sample