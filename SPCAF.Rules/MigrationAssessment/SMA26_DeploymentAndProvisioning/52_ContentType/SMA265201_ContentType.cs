using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{
    [RuleMetadata(typeof(DeploymentAndProvisioningGroup),
        CheckId = "SMA265201",
        DisplayName = "Avoid deployment of ContentTypes via Feature Framework",
        Description = "Deploying ContentTypes through the Feature Framework creates dependencies on the provisioning XML files. Break this dependency to help future migrations, and updates by using the Remote Provisioning techniques from Microsoft.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "The deployment of ContentType '{0}' should not use the Feature Framework. Deploy ContentTypes via CSOM.",
        Resolution = "res:SPCAF.Rules.MigrationAssessment.Resources.SMA265201.html", 
        Links = new string[]
        {
            "OfficeDev PnP: See sample Core.CreateContentTypes",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Core.CreateContentTypes",   
            "OfficeDev PnP: See sample Core.CreateDocumentContentType",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Core.CreateDocumentContentType",
            "Site provisioning techniques, and remote provisioning in SharePoint 2013",
            "http://blogs.msdn.com/b/vesku/archive/2013/08/23/site-provisioning-techniques-and-remote-provisioning-in-sharepoint-2013.aspx",
            "FTC to CAM - Stop creating content types and site columns declaratively",
            "http://blogs.msdn.com/b/vesku/archive/2013/11/06/ftc-to-cam-stop-creating-content-types-and-site-columns-declaratively.aspx"
        })]
    public class ContentTypeRecommendations : Rule<ContentTypeDefinition>
    {
        public override void Visit(ContentTypeDefinition target, NotificationCollection notifications)
        {
            string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
            this.Notify(target, message, notifications);         
        }
    }
}