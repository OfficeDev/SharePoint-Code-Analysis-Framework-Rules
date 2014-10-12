using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{

    [RuleMetadata(typeof(DeploymentAndProvisioningGroup),
        CheckId = "SMA265101",
        DisplayName = "Avoid deployment of Fields via Feature Framework",
        Description = "Deploying fields through the Feature Framework creates dependencies on the provisioning XML files. Break this dependency to help future migrations, and updates by using the Remote Provisioning techniques from Microsoft.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "The deployment of Field '{0}' should not use the Feature Framework. Deploy Fields via CSOM.",
        Resolution = "res:SPCAF.Rules.MigrationAssessment.Resources.SMA265101.html", 
        Links = new string[]
        {
            "OfficeDev PnP: See sample Core.CreateContentTypes",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Core.CreateContentTypes",
            "Site provisioning techniques, and remote provisioning in SharePoint 2013",
            "http://blogs.msdn.com/b/vesku/archive/2013/08/23/site-provisioning-techniques-and-remote-provisioning-in-sharepoint-2013.aspx"
        })]
    public class FieldRecommendations : Rule<FieldDefinition>
    {
        public override void Visit(FieldDefinition target, NotificationCollection notifications)
        {
            string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
            this.Notify(target, message, notifications);         
        }
    }
}