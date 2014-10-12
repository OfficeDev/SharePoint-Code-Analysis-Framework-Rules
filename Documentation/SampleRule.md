Sample Rule
===========
To create a custom rule for SPCAF start with the steps described in SDK Overview.

The follow code shows a sample rule which checks a naming convention for a feature. The rule checks if each deployed feature starts with the company name 'MyCompany'.

First create a class which holds the Category and Description of your cutom rules.

using System;
using System.ComponentModel;
using SPCAF.Sdk;

    namespace MyCustomRule
    {
      [Category("My Custom Rules")]
      [Description("Description of my custom rules.")]
      [VisitorGroupMetadata(
        Category = "My Custom Rules",
        Description = "Description of my custom rules.")]
      public class MyCustomRuleGroup : IVisitorGroup
      {
      }
    }

Then create a new class for each custom rule.

    using System;
    using System.Text;
    using SPCAF.Sdk;
    using SPCAF.Sdk.Rules;
    using SPCAF.Sdk.Model;

    namespace MyCustomRule
    {
      [RuleMetadata(typeof(MyCustomRuleGroup),
        CheckId = "SPC082199",
        DisplayName = "Feature name should start 'MyCompany'",
        Description = "A feature name should be prefixed with the name 'MyCompany'.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "Feature '{0}' should start with 'MyCompany'.",
        Resolution = "Change the folder of the Feature. The name must start with 'MyCompany'.")]
      public class MyCustomRules : Rule<FeatureDefinition>
      {
        public override void Visit(FeatureDefinition target, NotificationCollection notifications)
        {
          if (!target.FeatureName.StartsWith("MyCompany"))
          {
            string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
            this.Notify(target, message, notifications);
          }
        }
      }
    }
