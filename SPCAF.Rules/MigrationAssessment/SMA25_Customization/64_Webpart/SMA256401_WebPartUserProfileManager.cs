using System.Collections.Generic;
using System.Linq;
using SPCAF.Sdk.Helpers;
using SPCAF.Sdk.Model;
using SPCAF.Sdk.Model.Extensions;
using SPCAF.Sdk.Rules;
using SPCAF.Sdk;

namespace SPCAF.Rules.MigrationAssessment
{

    [RuleMetadata(typeof(CollaborationCustomizationsGroup),
        CheckId = "SMA256401",
        DisplayName = "WebParts that use the UserProfileManager",
        Description = "SharePoint 2013 gives access to a new set of user profile based methods, and properties which can be used to accomplish almost all tasks existing webparts could use.",
        DefaultSeverity = Severity.CriticalWarning,
        SharePointVersion = new string[] { "12", "14", "15" },
        Message = "For the WebPart {0}, consider the use of a SharePoint app, using CSOM/JSOM with the newer User Profile apis.",
        Resolution = "res:SPCAF.Rules.MigrationAssessment.Resources.SMA256401.html",
        Links = new [] {
            "Using User Profiles in SharePoint 2013",
            "http://msdn.microsoft.com/en-us/library/office/jj163800%28v=office.15%29.aspx"
        })]
    public class WebPartUserProfilesManager : Rule<AssemblyFileReferenceWSP>
    {
        public override void Visit(AssemblyFileReferenceWSP target, NotificationCollection notifications)
        {
            var referencesToMatch = new List<string>
            {
                "Microsoft.Office.Server.UserProfiles.UserProfileManager"
            };
            foreach (var webpart in target.AssemblyFileReference.GetWebParts())
            {
                if (webpart.Methods.ContainsReferences(referencesToMatch))
                {
                    string message = string.Format(this.MessageTemplate(), webpart.BaseType.FullName, target.ReadableElementName);
                    this.Notify(target, message, notifications);
                }
            }
        }
    }
}