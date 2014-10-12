Sample Metric
=============
To create a custom metric for SPCAF start with the steps described in SDK Overview.

The follow code shows a sample metric which counts the number of farm features which are hidden (which should be avoided).
```
using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Metrics;
using SPCAF.Sdk;
using System.Linq;

namespace MyCustomMetric
{
  [MetricMetadata(typeof(MyCustomCategory),
    CheckId = "SPM010204",
    DisplayName = "Hidden Farm Features",
    ShortName = "Hidden Farm Features",
    Description = "Counts the number of hidden features with scope 'Farm'.",
    Message = "Solution '{0}' contains {1} hidden Features with Scope='Farm'",
    SharePointVersion = new string[] { "12", "14", "15" },
    Unit = MetricUnit.Number,
    Aggregation = MetricAggregation.Sum)]
  public class NumberOfFarmFeatures : Metric<SolutionDefinition>
  {
    public override void Visit(SolutionDefinition solution, NotificationCollection notifications)
    {
      int numberOfFarmFeatures = solution
        .FeatureManifests
        .Enum()
        .Where(t => t.FeatureDefinition.Scope == FeatureScope.Farm)
        .Where(t => t.FeatureDefinition.Hidden.IsTrue())
        .Count();

      Notify(solution, numberOfFarmFeatures, notifications, t => t.Name);
    }
  }
}
```
