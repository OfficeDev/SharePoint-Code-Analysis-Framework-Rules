using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{
    [RuleMetadata(typeof(DeploymentAndProvisioningGroup),
        CheckId = "SMA262101",
        DisplayName = "Avoid deployment of Feature via Feature Framework",
        Description = "The Feature Framework creates dependencies on the provisioning XML files. Break this dependency to help future migrations, and updates by using the Remote Provisioning techniques from Microsoft.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "The deployment of Feature '{0}' should not use the Feature Framework.",
        Links = new string[]
        {
            "OfficeDev PnP: Samples",
            "https://github.com/OfficeDev/PnP",
            "Site Provisioning Techniques",
            "http://blogs.msdn.com/b/vesku/archive/2013/08/23/site-provisioning-techniques-and-remote-provisioning-in-sharepoint-2013.aspx"
        })]
    public class FeatureRecommendations : Rule<FeatureDefinition>
    {
        public override void Visit(FeatureDefinition target, NotificationCollection notifications)
        {
            string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
            this.Notify(target, message, notifications);         
        }
    }
}