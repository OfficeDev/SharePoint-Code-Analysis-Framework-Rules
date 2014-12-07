using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;

namespace SPCAF.Rules.MigrationAssessment
{

    [RuleMetadata(typeof(AdvancedCustomizationsGroup),
        CheckId = "SMA290122",
        DisplayName = "Consider to Switch to CSOM for Property bag Manipulation",
        Description = "The PropertyBag is full implemented in CSOM and JSOM, so migration of this component has no blockers.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "Property Bag Access '{0}' in Assembly '{1}' should be changed to use CSOM, in the App Model.")]
    public class PropertyBagRecomendation : Rule<AssemblyFileReferenceWSP>
    {
        public override void Visit(AssemblyFileReferenceWSP target, NotificationCollection notifications)
        {
            foreach (var typeDefinition in target.AssemblyFileReference.TypesThatImplementInterface("Microsoft.SharePoint.Utilities.SPPropertyBag"))
            {
                string message = string.Format(this.MessageTemplate(), typeDefinition.BaseType.FullName, target.ReadableElementName);
                this.Notify(target, message, notifications);
            }          
        }
    }
}