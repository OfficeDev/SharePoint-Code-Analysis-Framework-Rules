using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{

    [RuleMetadata(typeof(AdvancedCustomizationsGroup),
        CheckId = "SMA290120",
        DisplayName = "Remove HttpModules",
        Description = "There is no direct replacment for HttpModules in the App Model. Depending on the purpose of the HttpModules an alternative approach must be implemented.",
        DefaultSeverity = Severity.Error,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "For the HttpModule '{0}' in Assembly '{1}' there is no alternative implementation in the App Model. Question the reason for the current implementation and remove the HttpModule.",
        Resolution = "res:SPCAF.Rules.MigrationAssessment.Resources.SMA290120.html")]
    public class HttpModuleRecommendations : Rule<AssemblyFileReferenceWSP>
    {
        public override void Visit(AssemblyFileReferenceWSP target, NotificationCollection notifications)
        {
            foreach (var typeDefinition in target.AssemblyFileReference.TypesThatImplementInterface("System.Web.IHttpModule"))
            {
                string message = string.Format(this.MessageTemplate(), typeDefinition.BaseType.FullName, target.ReadableElementName);
                this.Notify(target, message, notifications);
            }          
        }
    }
}