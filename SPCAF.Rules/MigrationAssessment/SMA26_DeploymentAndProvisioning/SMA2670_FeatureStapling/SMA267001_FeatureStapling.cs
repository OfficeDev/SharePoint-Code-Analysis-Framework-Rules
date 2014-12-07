using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{
    [RuleMetadata(typeof(DeploymentAndProvisioningGroup),
        CheckId = "SMA267001",
        DisplayName = "Avoid deployment of Feature Staplings via Feature Framework",
        Description = "Using the Feature Stapling Feature of the Feature Framework creates dependencies on the provisioning XML files. Break this dependency to help future migrations, and updates by using the Remote Provisioning techniques from Microsoft.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "The deployment of Feature Stapling '{0}' should not use the Feature Framework.",
        Links = new string[]
        {
            "SharePoint 2013 App Deployment through App Stapling",
            "http://blogs.msdn.com/b/richard_dizeregas_blog/archive/2013/03/04/sharepoint-2013-app-deployment-through-quot-app-stapling-quot.aspx"
        })]
    public class FeatureStaplingRecommendations : Rule<FeatureSiteTemplateAssociationDefinition>
    {
        public override void Visit(FeatureSiteTemplateAssociationDefinition target, NotificationCollection notifications)
        {
            string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
            this.Notify(target, message, notifications);         
        }
    }
}