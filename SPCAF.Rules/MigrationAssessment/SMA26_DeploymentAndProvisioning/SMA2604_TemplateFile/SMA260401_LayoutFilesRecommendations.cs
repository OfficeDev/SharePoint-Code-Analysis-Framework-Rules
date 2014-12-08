using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;
using System.Xml;

namespace SPCAF.Rules.MigrationAssessment
{

    [RuleMetadata(typeof(DeploymentAndProvisioningGroup),
        CheckId = "SMA260401",
        DisplayName = "Deploy TemplateFile as Module",
        Description = "TemplateFile is a file referenced in the package manifest manifest.xml. A module will allow for controlled deployment through features, without a module, the WSP will be blocked from deployment.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "The TemplateFile '{0}' should be deployed as Module.",
        Links = new string[]
        {
            "OfficeDev PnP: Samples / Core.BulkDocumentUploader",
            "https://github.com/OfficeDev/PnP",
            "Site Provisioning Techniques",
            "http://blogs.msdn.com/b/vesku/archive/2013/08/23/site-provisioning-techniques-and-remote-provisioning-in-sharepoint-2013.aspx"
        })]
    public class LayoutFilesRecommendations : Rule<TemplateFileReference>
    {
        public override void Visit(TemplateFileReference target, NotificationCollection notifications)
        {
            if(target.Location.ToLower().StartsWith("layouts\\"))
            {
                string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
                this.Notify(target, message, notifications);
            }  
        }
    }
}