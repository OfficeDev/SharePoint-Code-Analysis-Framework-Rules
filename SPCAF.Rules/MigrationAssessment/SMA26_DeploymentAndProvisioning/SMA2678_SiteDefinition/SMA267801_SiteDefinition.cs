using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;
using System.Linq;

namespace SPCAF.Rules.MigrationAssessment
{
    [RuleMetadata(typeof(DeploymentAndProvisioningGroup),
        CheckId = "SMA267801",
        DisplayName = "Consider different implementation of SiteDefinitions",
        Description = "There is no direct replacement for SiteDefinitions in the App Model. SiteDefinitions are used during creation of a site and provisions ListTemplates, ContentTypes etc. The alternative implementation focusses on a completely different approach for site provisiong.",
        DefaultSeverity = Severity.CriticalError,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "The SiteDefinition '{0}' needs to be replaced which involves an alternative approach for provisioning of sites.",
        Links = new string[]
        {
             "OfficeDev PnP: See sample Provisioning.SiteCollectionCreation",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Provisioning.SiteCollectionCreation",
             "OfficeDev PnP: See sample Provisioning.SubSiteCreationApp",
            "https://github.com/OfficeDev/PnP/tree/master/Samples/Provisioning.SubSiteCreationApp",
            "Site provisioning techniques, and remote provisioning in SharePoint 2013",
            "http://blogs.msdn.com/b/vesku/archive/2013/08/23/site-provisioning-techniques-and-remote-provisioning-in-sharepoint-2013.aspx",
            "SharePoint 2013 and SharePoint Online solution pack for branding and site provisioning",
            "http://www.microsoft.com/en-us/download/details.aspx?id=42030"           
        })]
    public class SiteDefinitionRecommendations : Rule<SiteDefinitionManifestFileReference>
    {
        public override void Visit(SiteDefinitionManifestFileReference target, NotificationCollection notifications)
        {            
            string message = string.Format(this.MessageTemplate(), target.ReadableElementName);
            this.Notify(target, message, notifications);         
        }
    }
}